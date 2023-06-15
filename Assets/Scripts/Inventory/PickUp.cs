using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    //public AudioClip interactSound;
    public InventoryManager inventoryManager;
    public GameObject item;

    public void Item()
    {
        // Implement your interaction logic here
        Debug.Log("Interacting with object: " + gameObject.name);

        // Play the interaction sound
        //if (interactSound != null)
        //{
        //    AudioSource.PlayClipAtPoint(interactSound, transform.position);
        //}

        // Add the item to the player's inventory
        SpriteRenderer sr = item.GetComponent<SpriteRenderer>();
        inventoryManager.AddItem(item.name, 1, sr.sprite);

        // Disable the object upon collision
        gameObject.SetActive(false);
    }
}
