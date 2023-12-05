using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuButton : MonoBehaviour
{
    private void Start()
    {
        // Add a click event listener to the button
        Button mainMenuButton = GetComponent<Button>();
        if (mainMenuButton != null)
        {
            mainMenuButton.onClick.AddListener(OnMainMenuButtonClick);
        }
    }

    private void OnMainMenuButtonClick()
    {
        // Call the LoadMainMenu method from GameEventsManager
        GameEventsManager.instance.LoadMainMenu();
    }
}