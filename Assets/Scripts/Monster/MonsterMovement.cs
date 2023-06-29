using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMovement : MonoBehaviour
{
    #region Variables

    [Header("Player")]
    public Transform player;
    private bool isFollowingPlayer;

    [Header("Movement")]
    public float moveSpeed = 5f;
    public float waitTime = 2f;
    public float detectionRange = 3f;
    public float detectionAngle = 60f; // Angle within which the player is detected
    private bool isFacingRight = true; // Track the direction the enemy is facing

    [Header("Waypoints")]
    private Transform targetWaypoint;
    public Transform waypointA;
    public Transform waypointB;

    [Header("Wait")]
    private bool isWaiting;

    #endregion

    private void Start()
    {
        // Set the initial target waypoint
        targetWaypoint = waypointA;
    }

    private void Update()
    {
        if (!isFollowingPlayer)
        {
            // Move towards the current target waypoint
            transform.position = Vector2.MoveTowards(transform.position, targetWaypoint.position, moveSpeed * Time.deltaTime);

            // Check if the enemy has reached the target waypoint
            if (Vector2.Distance(transform.position, targetWaypoint.position) < 0.1f)
            {
                // If not waiting, randomly select the next target waypoint
                if (!isWaiting)
                {
                    StartCoroutine(WaitAndSwitchWaypoint());
                }
            }

            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            // Check if the player is within the detection range and angle
            if (distanceToPlayer < detectionRange && IsPlayerInDetectionAngle())
            {
                // Move towards the player if they are too close
                isFollowingPlayer = true;
            }

            // Flip the sprite if changing direction
            if (targetWaypoint.position.x > transform.position.x && !isFacingRight)
            {
                Flip();
            }
            else if (targetWaypoint.position.x < transform.position.x && isFacingRight)
            {
                Flip();
            }
        }

        else
        {
            // Follow the player
            transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);

            // Stop following the player if they move too far away
            if (Vector2.Distance(transform.position, player.position) > detectionRange)
            {
                isFollowingPlayer = false;
                targetWaypoint = waypointA; // Set the next target waypoint after stopping following the player
            }
        }
    }

    IEnumerator WaitAndSwitchWaypoint()
    {
        isWaiting = true;
        yield return new WaitForSeconds(waitTime);
        isWaiting = false;

        // Randomly select the next target waypoint
        targetWaypoint = (targetWaypoint == waypointA) ? waypointB : waypointA;
    }

    void Flip()
    {
        // Switch the direction the enemy is facing
        isFacingRight = !isFacingRight;

        // Flip the sprite by flipping the scale on the x-axis
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    bool IsPlayerInDetectionAngle()
    {
        // Get the direction to the player
        Vector2 playerDirection = (player.position - transform.position).normalized;

        // Get the facing direction of the monster
        Vector2 facingDirection = isFacingRight ? Vector2.right : Vector2.left;

        // Calculate the angle between the player direction and facing direction
        float angle = Vector2.Angle(playerDirection, facingDirection);

        // Return true if the angle is within the detection angle
        return angle <= detectionAngle * 0.5f;
    }
}