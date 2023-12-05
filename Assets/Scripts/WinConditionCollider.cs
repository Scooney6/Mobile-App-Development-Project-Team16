using UnityEngine;

public class WinConditionCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Notify GameEventsManager about the win condition
            GameEventsManager.instance.gameWin();
        }
    }
}