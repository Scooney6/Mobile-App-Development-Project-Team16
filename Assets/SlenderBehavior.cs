using System.Collections;
using UnityEngine;

public class SlendermanBehavior : MonoBehaviour
{
    public GameObject player; // Reference to the player character
    public GameObject slenderman; // Reference to the Slenderman object
    public int pagesRequiredForJumpscare = 2; // Number of pages required for the jumpscare
    public float jumpscareDistance = 3f; // Distance at which Slenderman appears
    public float jumpscareDuration = 5f; // Duration of the jumpscare in seconds

    private bool isJumpscareActive = false;
    private int pagesCollected = 0;

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
        pagesCollected++;
        Debug.Log("Pages Collected: " + pagesCollected);

        // Check if the required number of pages for the jumpscare is reached
        if (pagesCollected == pagesRequiredForJumpscare)
        {
            StartCoroutine(TriggerJumpscare());
        }
    }

    private IEnumerator TriggerJumpscare()
    {
        isJumpscareActive = true;

        // Calculate Slenderman's position relative to the player
        Vector3 jumpscarePosition = player.transform.position +
                                    (player.transform.forward * jumpscareDistance);

        // Set Slenderman's position and activate
        slenderman.transform.position = jumpscarePosition;
        slenderman.SetActive(true);

        // Wait for the jumpscare duration
        yield return new WaitForSeconds(jumpscareDuration);

        // Deactivate Slenderman after the jumpscare duration
        slenderman.SetActive(false);

        isJumpscareActive = false;
    }
}
