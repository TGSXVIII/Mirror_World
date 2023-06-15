using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.U2D;

public class SearchItem : MonoBehaviour
{
    public GameObject[] itemsToAppear;
    public InventoryManager inventoryManager;


    public void OpenChest()
    {
        foreach (GameObject item in itemsToAppear)
        {
            SpriteRenderer sr = item.GetComponent<SpriteRenderer>();
            inventoryManager.AddItem(item.name, 1, sr.sprite);
        }
    }
}
