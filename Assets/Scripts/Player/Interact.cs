using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Interact : MonoBehaviour
{
    #region Variables

    public float interactDistance = 1f;
    private bool isHidden = false; // Flag to check if the player is currently hidden
    private Vector3 originalPosition; // The original position of the player
    public Vector3 targetPosition; // The destination for the player
    public Rigidbody2D rb;
    public float interractCooldown = 0.5f; // Adjust the cooldown duration as needed
    private float lastInteractionTime = 0f;

    #endregion

    private void Update()
    {
        if (Input.GetKeyDown("e"))
        {
            if (Time.time - lastInteractionTime >= interractCooldown)
            {
                // Update the last interaction time
                lastInteractionTime = Time.time;

                interact();
            }

            else
            {
                // The player cannot go through the door yet due to the cooldown
                Debug.Log("Interact on cooldown.");
            }
        }
    }

    private void interact()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, interactDistance);
        //Debug.Log("Number of colliders detected: " + hitColliders.Length);

        foreach (Collider2D collider in hitColliders)
        {
            // Pick up items
            if (collider.CompareTag("item"))
            {
                PickUp pickUp = collider.gameObject.GetComponent<PickUp>();
                pickUp.Item();
            }

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
                    transform.position = targetPosition;

                    // Disable the camera controller script
                    Camera.main.GetComponent<CameraController>().enabled = false;

                    // Set the flag to indicate that the player is hidden
                    isHidden = true;

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

            // Move the player to another door
            if (collider.CompareTag("door"))
            {
                LockedItem lockedItem = collider.gameObject.GetComponent<LockedItem>();
                
                if (!lockedItem.Unlock())
                {
                    InteractableObject interactableObject = collider.gameObject.GetComponent<InteractableObject>();
                    interactableObject.Door();
                }
            }

            // Open a chest and take the items inside
            if (collider.CompareTag("chest"))
            {
                LockedItem lockedItem = collider.gameObject.GetComponent<LockedItem>();

                if (!lockedItem.Unlock())
                {
                    SearchItem searchItem = collider.gameObject.GetComponent<SearchItem>();
                    searchItem.OpenChest();
                }
            }

            // Save the game
            if (collider.CompareTag("diary"))
            {
                SaveGame saveGame = collider.gameObject.GetComponent<SaveGame>();
                saveGame.Save();
            }
        }
    }
}
