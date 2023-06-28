using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedItem : MonoBehaviour
{
    [Header("Misc")]
    public InventoryManager inventoryManager;
    public string requiredItemName;
    public bool isLocked;

    public bool Unlock()
    {
        if (inventoryManager.HasItem(requiredItemName) || (inventoryManager.HasItem("skeletonKey")))
        {
            if (!inventoryManager.HasItem("skeletonKey") && isLocked)
            {
                inventoryManager.RemoveItem(requiredItemName, 1);
            }
                
            Debug.Log("unlocked");
            isLocked = false;
        }
        return isLocked;
    }
}
