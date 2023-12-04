using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Page : MonoBehaviour, IDataPersistence
{
    [SerializeField] private string id;

    [ContextMenu("Generate guid for id")]
    private void GenerateGuid() 
    {
        id = System.Guid.NewGuid().ToString();
    }

    private MeshRenderer visual;
    private ParticleSystem collectParticle;
    private bool collected = false;

    private void Awake() 
    {
        visual = this.GetComponent<MeshRenderer>();
    }

    public void LoadData(GameData data) 
    {
        data.objectivesCollected.TryGetValue(id, out collected);
        if (collected) 
        {
            visual.gameObject.SetActive(false);
        }
    }

    public void SaveData(GameData data) 
    {
        if (data.objectivesCollected.ContainsKey(id))
        {
            data.objectivesCollected.Remove(id);
        }
        data.objectivesCollected.Add(id, collected);
    }

    private void OnTriggerEnter() 
    {
        if (!collected) 
        {
            collected = true;
            visual.gameObject.SetActive(false);
            GameEventsManager.instance.ObjectiveCollected();
        }
    }

}
