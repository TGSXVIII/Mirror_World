using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MirrorInteract : MonoBehaviour
{
    #region Variables

    [Header("Required item")]
    private string requiredItemName = "Mirror";

    [Header("Target position")]
    public Vector3 targetPosition; //The destination for the player

    [Header("Sprites")]
    public Sprite activeMirrorSprite; //The new sprite you want to display
    public Sprite inactiveMirrorSprite; //The old sprite you want to display

    [Header("Interact")]
    public float interactDistance = 1f;

    [Header("Misc")]
    public InventoryManager inventoryManager;
    
    private bool checkMirrorStatus = false;
    
    #endregion

    private void Update()
    {
        if (inventoryManager.HasItem(requiredItemName))
        {
            MirrorAreaCheck();
        }

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

        if (Counter != 0 && checkMirrorStatus == false)
        {
            inventoryManager.updateMirrorUI(activeMirrorSprite);
            checkMirrorStatus = true;
        }

        else if (Counter == 0 && checkMirrorStatus == true)
        {
            inventoryManager.updateMirrorUI(inactiveMirrorSprite);
            checkMirrorStatus = false;
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
