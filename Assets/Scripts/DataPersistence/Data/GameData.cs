using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public long lastUpdated;
    public Vector3 playerPosition;
    public Vector3 slenderPosition;
    public SerializableDictionary<string, bool> objectivesCollected;
    public AttributesData playerAttributesData;

    // the values defined in this constructor will be the default values
    // the game starts with when there's no data to load
    public GameData() 
    {
        slenderPosition = Vector3.zero;
        playerPosition = new Vector3(52.5f, 6f, 66f);
        objectivesCollected = new SerializableDictionary<string, bool>();
        playerAttributesData = new AttributesData();
    }
}