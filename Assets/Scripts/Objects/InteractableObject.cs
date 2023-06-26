using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public GameObject targetDoor; 
    public GameObject Player;
    public Vector3 doorAdjustment;

    public void Interact()
    {
        // Implement your interaction logic here
        Debug.Log("Interacting with object: " + gameObject.name);
    }

    public void Hide()
    {
        // Implement your interaction logic here
        Debug.Log("Interacting with object: " + gameObject.name);
    }

    public void Door()
    {
        // Implement your interaction logic here
        Debug.Log("Interacting with object: " + gameObject.name);

        // Move the player to another location
        Player.transform.position = targetDoor.transform.position - doorAdjustment;
    }

    public void Chest()
    {
        // Implement your interaction logic here
        Debug.Log("Interacting with object: " + gameObject.name);
    }
}
