using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StartText : MonoBehaviour
{
    public TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        if (DataPersistenceManager.instance.HasGameData()) {
            text.text = "New Game";
        }
    }

}
