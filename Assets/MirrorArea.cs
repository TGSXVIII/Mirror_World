using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorArea : MonoBehaviour
{
    public GameObject targetArea;
    public GameObject Player;
    public Vector3 areaAdjustment;

    public void Mirror()
    {
        // Implement your interaction logic here
        Debug.Log("Interacting with object: " + gameObject.name);

        // Move the player to another location
        Player.transform.position = targetArea.transform.position - areaAdjustment;
    }
}