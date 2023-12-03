using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    private int pages = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("LostPages"))
        {
            CollectPages(other.gameObject);
        }
    }

    private void CollectPages(GameObject page)
    {
        Destroy(page); 
        pages++;
        Debug.Log("Pages Collected: " + pages);
    }
}

