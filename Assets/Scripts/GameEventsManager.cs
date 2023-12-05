using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEventsManager : MonoBehaviour
{
    public GameObject gameWinUI;
    public static GameEventsManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one Game Events Manager in the scene.");
        }
        instance = this;
    }

    public event Action onPlayerDeath;
    public void PlayerDeath() 
    {
        if (onPlayerDeath != null) 
        {
            onPlayerDeath();
        }
    }

    public event Action onObjectiveCollected;
    public void ObjectiveCollected() 
    {
        if (onObjectiveCollected != null) 
        {
            onObjectiveCollected();
        }
    }

    public void gameWin() {
        gameWinUI.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadMainMenu()
    {
        // Load the main menu scene
        SceneManager.LoadScene("MainMenu"); // Replace "MainMenuScene" with the actual scene name
    }

}