using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContinueDisable : MonoBehaviour
{
    public Button button;
    // Start is called before the first frame update
    void Start()
    {
        if (!DataPersistenceManager.instance.HasGameData()) {
            button.interactable = false;
        } else {
            button.interactable = true;
        }
    }
}
