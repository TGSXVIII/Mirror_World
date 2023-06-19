using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MirrorInteract : MonoBehaviour
{
    public float interactDistance = 1f;
    public Image mirrorImage;
    public InventoryManager inventoryManager;
    private string requiredItemName = "Mirror";
    public Vector3 targetPosition; //The destination for the player
    public Sprite activeMirrorSprite; //The new sprite you want to display
    public Sprite inactiveMirrorSprite; //The old sprite you want to display

    private void Update()
    {
        MirrorAreaCheck();

        if (Input.GetKeyDown("q") && inventoryManager.HasItem(requiredItemName))
        {
            Interact();
        }
    }

    private void MirrorAreaCheck()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, interactDistance);
        //Debug.Log("Number of colliders detected: " + hitColliders.Length);

        int Counter = 0;

        foreach (Collider2D collider in hitColliders)
        {
            if (collider.CompareTag("mirror area"))
            {
                Counter++;
            }
        }

        if (Counter != 0)
        {
            // Change the sprite to the new sprite
            mirrorImage.sprite = activeMirrorSprite;
        }
        else
        {
            // Change/Keep the sprite to the old sprite
            mirrorImage.sprite = inactiveMirrorSprite;
        }
    }

    private void Interact()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, interactDistance);
        Debug.Log("Number of colliders detected: " + hitColliders.Length);

        foreach (Collider2D collider in hitColliders)
        {
            if (collider.CompareTag("mirror area"))
            {
                MirrorArea mirrorArea = collider.gameObject.GetComponent<MirrorArea>();
                mirrorArea.Mirror();
            }
        }
    }
}
