using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    private RaycastHit2D hit;


    void Update()
    {
        // Check for input to interact
        if (Input.GetKeyDown("e"))
        {
            // Cast a raycast forward from the player's position
            hit = Physics2D.Raycast(transform.position, transform.right, 1f);

            // Check if the raycast hits an object
            if (hit.collider != null)
            {
                // Get the reference to the interactable object's script
                InteractableObject interactableObject = hit.collider.GetComponent<InteractableObject>();

                // Check if the object has an InteractableObject script
                if (interactableObject != null)
                {
                    // Call the Interact method on the object
                    interactableObject.Interact();
                }
            }
        }
    }
}
