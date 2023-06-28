using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Interact : MonoBehaviour
{
    #region Variables
    public GameObject player;
    public float interactDistance = 1f;
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
                Hide hide = player.GetComponent<Hide>();
                hide.hidePlayer(collider);
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
