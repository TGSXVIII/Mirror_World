using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DarkArea : MonoBehaviour
{
    #region Variables

    public float interactDistance = 1f;
    public bool checkDarkStatus = false;
    public GameObject playerLight;
    public GameObject globalLight;
    public InventoryManager inventoryManager;

    #endregion

    // Update is called once per frame
    void Update()
    {
        DarkAreaCheck();
    }
    private void DarkAreaCheck()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, interactDistance);
        //Debug.Log("Number of colliders detected: " + hitColliders.Length);

        int Counter = 0;

        foreach (Collider2D collider in hitColliders)
        {
            if (collider.CompareTag("dark area"))
            {
                Counter++;
            }
        }

        if (Counter != 0 && checkDarkStatus == false)
        {
            globalLight.GetComponent<Light2D>().enabled = true;
            checkDarkStatus = true;
            if (inventoryManager.HasItem("Lit candle"))
            {
                playerLight.GetComponent<Light2D>().enabled = true;
            }
        }

        else if (Counter == 0 && checkDarkStatus == true)
        {
            globalLight.GetComponent<Light2D>().enabled = false;
            checkDarkStatus = false;
            if (inventoryManager.HasItem("Lit candle"))
            {
                playerLight.GetComponent<Light2D>().enabled = false;
            }
        }
    }
}