using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using static UnityEditor.Experimental.GraphView.GraphView;

public class MonsterMovement : MonoBehaviour
{
    public GameObject player;
    private Transform playerT;
    public Transform waypointB;
    public Transform waypointA;
    public float stopDistance = 2f;
    public float movementSpeed = 5f;
    private Transform targetWaypoint;
    public float followDistance = 10f;

    // Start is called before the first frame update
    void Start()
    {
        playerT = GameObject.FindGameObjectWithTag("Player").transform;
        targetWaypoint = waypointA; // Start by moving towards waypoint A
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate the distance between the enemy and the player
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        // If the player is within follow distance, move towards them
        if (distanceToPlayer <= followDistance)
        {

            // Move towards the player
            transform.position = Vector3.MoveTowards(transform.position, playerT.position, movementSpeed * Time.deltaTime);

            // If the player is within attack distance, attack them
            if (distanceToPlayer <= stopDistance)
            {

            }
        }

        else
        {
            // Move towards the current target waypoint
            transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.position, movementSpeed * Time.deltaTime);

            // Check if the enemy has reached the current target waypoint
            if (Vector3.Distance(transform.position, targetWaypoint.position) < 0.1f)
            {
                // Switch to the next target waypoint
                if (targetWaypoint == waypointA)
                    targetWaypoint = waypointB;
                else
                    targetWaypoint = waypointA;
            }
        }
    }
}
