using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void StartGame() {
        DataPersistenceManager.instance.NewGame();
    }

    public void LoadGame() {
        DataPersistenceManager.instance.LoadGame();
    }

    public void ExitGame() {
        Application.Quit();
    }
}
