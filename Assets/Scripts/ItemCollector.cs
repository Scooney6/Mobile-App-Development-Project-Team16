using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollector : MonoBehaviour
{
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
            StartCoroutine(ActivateSlendermanWalk());
        }
    }

    IEnumerator ActivateSlenderman()
    {
        if (slendermanAppearSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(slendermanAppearSound);
        }

        Vector3 spawnPosition = playerTransform.position + playerTransform.forward * 4f;
        Quaternion targetRotation = Quaternion.LookRotation(playerTransform.position - spawnPosition);

        slendermanInstance = Instantiate(slendermanPrefab, spawnPosition, targetRotation);
        yield return new WaitForSeconds(10f);

        Destroy(slendermanInstance);
        slendermanActivated = false;
    }

    IEnumerator ActivateSlendermanWalk()
    {
        Vector3 spawnPosition = playerTransform.position + playerTransform.forward * 4f;
        Quaternion targetRotation = Quaternion.LookRotation(playerTransform.position - spawnPosition);

        slendermanInstance = Instantiate(slendermanPrefab, spawnPosition, targetRotation);

        Animator slendermanAnimator = slendermanInstance.GetComponent<Animator>();
        if (slendermanAnimator != null)
        {
            slendermanAnimator.Play("Walk");
        }

        Vector3 direction = (playerTransform.position - spawnPosition).normalized;

        float elapsedTime = 0f;
        while (elapsedTime < 8f)
        {
            slendermanInstance.transform.Translate(direction * Time.deltaTime);
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
}
