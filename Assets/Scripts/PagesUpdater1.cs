using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PagesUpdater : MonoBehaviour, IDataPersistence
{
    public GameEventsManager gameManager;
    [SerializeField] private int totalObjectives = 7;

    private int objectivesCollected = 0;

    private TextMeshProUGUI text;

    // Add a reference to the collider
    private Collider winConditionCollider;

    private void Awake()
    {
        text = this.GetComponent<TextMeshProUGUI>();
        // Create an empty GameObject to hold the collider
        GameObject colliderObject = new GameObject("WinConditionColliderObject");
        winConditionCollider = colliderObject.AddComponent<BoxCollider>();
        winConditionCollider.isTrigger = true;
        winConditionCollider.gameObject.SetActive(false);
    }

    private void Start()
    {
        if (GameEventsManager.instance != null && GameEventsManager.instance.gameWinUI != null)
        {
            GameEventsManager.instance.gameWinUI.SetActive(false);
        }

        // subscribe to events
        GameEventsManager.instance.onObjectiveCollected += onObjectiveCollected;
    }

    public void LoadData(GameData data)
    {
        foreach (KeyValuePair<string, bool> pair in data.objectivesCollected)
        {
            if (pair.Value)
            {
                objectivesCollected++;
            }
        }

        Debug.Log("Loaded data. Total objectives collected: " + objectivesCollected);
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

        // Check if all pages are collected
        if (objectivesCollected == totalObjectives)
        {
            DeleteRandomExitGate(); // Instead of deleting shears, delete an exit gate
        }
    }

    private void DeleteRandomExitGate()
    {
        // Get all exit gates in the scene
        GameObject[] exitGates = GameObject.FindGameObjectsWithTag("Exit");

        // Check if there are enough exit gates to delete
        if (exitGates.Length > 0)
        {
            // Randomly select and delete an exit gate
            int randomIndex = Random.Range(0, exitGates.Length);
            GameObject selectedExitGate = exitGates[randomIndex];

            // Move the collider to the position of the deleted gate
            winConditionCollider.transform.position = selectedExitGate.transform.position;
            winConditionCollider.gameObject.SetActive(true);

            Destroy(selectedExitGate);

            Debug.Log("Exit gate deleted!");
        }
        else
        {
            Debug.LogWarning("No exit gates found to delete.");
        }
    }

    private void Update()
    {
        text.text = objectivesCollected + " / " + totalObjectives;
    }
}
