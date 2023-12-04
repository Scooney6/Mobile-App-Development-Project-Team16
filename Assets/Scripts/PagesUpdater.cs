using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PagesUpdater : MonoBehaviour, IDataPersistence
{
    [SerializeField] private int totalObjectives = 0;

    private int objectivesCollected = 0;

    private TextMeshProUGUI text;

    private void Awake() 
    {
        text = this.GetComponent<TextMeshProUGUI>();
    }

    private void Start() 
    {
        // subscribe to events
        GameEventsManager.instance.onObjectiveCollected += onObjectiveCollected;
    }

    public void LoadData(GameData data) 
    {
        foreach(KeyValuePair<string, bool> pair in data.objectivesCollected) 
        {
            if (pair.Value) 
            {
                objectivesCollected++;
            }
        }
    }

    public void SaveData(GameData data)
    {
        // no data needs to be saved for this script
    }

    private void OnDestroy() 
    {
        // unsubscribe from events
        GameEventsManager.instance.onObjectiveCollected -= onObjectiveCollected;
    }

    private void onObjectiveCollected() 
    {
        objectivesCollected++;
    }

    private void Update() 
    {
        text.text = objectivesCollected + " / " + totalObjectives;
    }
}
