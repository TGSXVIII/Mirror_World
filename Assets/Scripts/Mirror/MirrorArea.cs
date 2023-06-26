using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorArea : MonoBehaviour
{
    public GameObject targetArea;
    public GameObject Player;
    public Vector3 areaAdjustment;
    public float mirrorCooldown = 2f; // Adjust the cooldown duration as needed
    private float lastInteractionTime = 0f;

    public void Mirror()
    {
        // Check if enough time has passed since the last interaction
        if (Time.time - lastInteractionTime >= mirrorCooldown)
        {
            // Implement your interaction logic here
            Debug.Log("Interacting with object: " + gameObject.name);

            // Move the player to another location
            Player.transform.position = targetArea.transform.position - areaAdjustment;

            // Update the last interaction time
            lastInteractionTime = Time.time;
        }

        else
        {
            // The player cannot go through the door yet due to the cooldown
            Debug.Log("Mirror interaction on cooldown.");
        }

    }
}