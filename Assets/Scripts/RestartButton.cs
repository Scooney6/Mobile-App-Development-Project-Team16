using UnityEngine;
using UnityEngine.UI;

public class RestartButton : MonoBehaviour
{
    private void Start()
    {
        // Add a click event listener to the button
        Button restartButton = GetComponent<Button>();
        if (restartButton != null)
        {
            restartButton.onClick.AddListener(OnRestartButtonClick);
        }
    }

    private void OnRestartButtonClick()
    {
        // Call the RestartGame method from GameEventsManager
        GameEventsManager.instance.RestartGame();
    }
}