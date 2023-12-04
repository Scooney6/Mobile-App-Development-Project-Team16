using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    [SerializeField]
    private float slendermanRunSpeed = 3f; 

    private int pages = 0;
    public GameObject slendermanPrefab;
    private GameObject slendermanInstance;
    private bool slendermanActivated = false;
    private Transform playerTransform;

    public AudioClip slendermanAppearSound;
    public AudioClip thirdPageCollectSound;
    private AudioSource audioSource;

    private void Start()
    {
        playerTransform = GetComponent<Transform>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("LostPages"))
        {
            CollectPages(other.gameObject);
        }
    }

    private void CollectPages(GameObject page)
    {
        Destroy(page);
        pages++;

        if (pages == 2 && !slendermanActivated)
        {
            slendermanActivated = true;
            StartCoroutine(ActivateSlenderman());
        }

        if (pages == 3)
        {
            PlayThirdPageCollectSound();
        }

        if (pages == 4)
        {
            StartCoroutine(ActivateSlendermanRun());
        }

        if (pages == 6)
        {
            StartCoroutine(ChasePlayerWithSlenderman());
        }
    }

    IEnumerator ActivateSlenderman()
    {
        PlaySlendermanAppearSound();

        Vector3 spawnPosition = playerTransform.position + playerTransform.forward * 4f;
        Quaternion targetRotation = Quaternion.LookRotation(playerTransform.position - spawnPosition);

        slendermanInstance = Instantiate(slendermanPrefab, spawnPosition, targetRotation);
        yield return new WaitForSeconds(10f);

        Destroy(slendermanInstance);
        slendermanActivated = false;
    }

    IEnumerator ActivateSlendermanRun()
    {
        PlaySlendermanAppearSound();

        Vector3 spawnPosition = playerTransform.position + playerTransform.forward * 4f;
        Quaternion targetRotation = Quaternion.LookRotation(playerTransform.position - spawnPosition);

        slendermanInstance = Instantiate(slendermanPrefab, spawnPosition, targetRotation);

        Animator slendermanAnimator = slendermanInstance.GetComponent<Animator>();
        if (slendermanAnimator != null)
        {
            slendermanAnimator.Play("Scream");
        }

        yield return new WaitForSeconds(8f);

        Destroy(slendermanInstance);
    }

    IEnumerator ChasePlayerWithSlenderman()
    {
        PlaySlendermanAppearSound();

        Vector3 spawnPosition = playerTransform.position + playerTransform.forward * 4f;
        Quaternion targetRotation = Quaternion.LookRotation(playerTransform.position - spawnPosition);

        slendermanInstance = Instantiate(slendermanPrefab, spawnPosition, targetRotation);

        Animator slendermanAnimator = slendermanInstance.GetComponent<Animator>();
        if (slendermanAnimator != null)
        {
            slendermanAnimator.Play("Run");
        }

        float chaseDuration = 20f; // Slenderman chases player for 20 seconds
        float elapsedTime = 0f;
        while (elapsedTime < chaseDuration)
        {
            Vector3 targetDirection = (playerTransform.position - slendermanInstance.transform.position).normalized;
            Vector3 newDirection = Vector3.RotateTowards(slendermanInstance.transform.forward, targetDirection, 5f * Time.deltaTime, 0.0f);
            slendermanInstance.transform.rotation = Quaternion.LookRotation(newDirection);

            slendermanInstance.transform.Translate(slendermanInstance.transform.forward * Time.deltaTime * slendermanRunSpeed); 
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Destroy(slendermanInstance);
    }

    private void PlayThirdPageCollectSound()
    {
        if (thirdPageCollectSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(thirdPageCollectSound);
        }
    }

    private void PlaySlendermanAppearSound()
    {
        if (slendermanAppearSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(slendermanAppearSound);
        }
    }
}
