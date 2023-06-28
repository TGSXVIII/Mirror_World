using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Hide : MonoBehaviour
{
    private bool isHidden = false; // Flag to check if the player is currently hidden
    private Vector3 originalPosition; // The original position of the player
    public GameObject targetPosition; // The destination for the player
    public GameObject targetPositionDark;
    public Rigidbody2D rb;
    public InventoryManager inventoryManager;
    public Sprite unlitCandle;

    public void hidePlayer(Collider2D collider)
    {
        // Hide the player
        if (collider.CompareTag("hide"))
        {
            if (!isHidden)
            {
                rb.isKinematic = true;
                rb.bodyType = RigidbodyType2D.Static;

                // Store the original position of the player
                originalPosition = transform.position;

                // Move the player to the hideout location
                transform.position = targetPosition.transform.position;

                // Disable the camera controller script
                Camera.main.GetComponent<CameraController>().enabled = false;

                // Set the flag to indicate that the player is hidden
                isHidden = true;
                if(inventoryManager.HasItem("Lit candle"))
                {
                    inventoryManager.RemoveItem("Lit candle", 1);
                    inventoryManager.AddItem("Unlit candle", 1, unlitCandle);
                }
            }

            else
            {
                rb.isKinematic = false;
                rb.bodyType = RigidbodyType2D.Dynamic;

                // Move the player back to the original position
                transform.position = originalPosition;

                // Enable the camera controller script   
                Camera.main.GetComponent<CameraController>().enabled = true;

                // Reset the flag to indicate that the player is no longer hidden
                isHidden = false;
            }
        }
    }
}
