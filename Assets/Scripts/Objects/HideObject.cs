using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideObject : MonoBehaviour
{
    private bool isHiding = false;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isHiding)
            {
                ExitHide();
            }

            else
            {
                EnterHide();
            }
        }
    }

    void EnterHide()
    {
        // Perform actions to enter hiding mode
        spriteRenderer.enabled = false;
        // Add any additional actions here

        isHiding = true;
    }

    void ExitHide()
    {
        // Perform actions to exit hiding mode
        spriteRenderer.enabled = true;
        // Add any additional actions here

        isHiding = false;
    }
}

