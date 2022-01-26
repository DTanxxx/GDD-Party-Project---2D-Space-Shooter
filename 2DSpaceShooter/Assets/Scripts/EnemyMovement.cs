using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is attached to the Enemy game object and contains enemy movement functionality.
/// It contains path following functionality.
/// </summary>
public class EnemyMovement : MonoBehaviour
{
    private WaveConfig waveConfig;
    private int waypointIndex = 0;
    private List<Transform> waypoints;

    private void Update()
    {
        FollowPath();
    }

    private void FollowPath()
    {
        if (waveConfig == null) { return; }

        // If waypointIndex is less than waypoints.Count, then enemy still has points to move to.
        if (waypointIndex < waypoints.Count)
        {
            var targetPosition = waypoints[waypointIndex].transform.position;
            // Calculate the speed.
            var movementThisFrame = Time.deltaTime * waveConfig.GetMoveSpeed();
            // Update enemy's position.
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementThisFrame);

            if (transform.position == targetPosition)
            {
                // Once enemy moves to the target waypoint's position, increment waypointIndex so it refers
                // to the next waypoint.
                waypointIndex++;
            }
        }
        else
        {
            if (GetComponent<Boss>() != null)
            {
                // It is a Boss! Resume its path.
                waypointIndex = 1;
            }
            else
            {
                // Once enemy finishes the path, destroy it.
                FindObjectOfType<GameSession>().RemoveEnemy();
                Destroy(gameObject);
            }
        }
    }

    public void SetWaveConfig(WaveConfig waveConfig)
    {
        // Initialise this script's properties.
        this.waveConfig = waveConfig;
        waypoints = this.waveConfig.GetWaypoints();
        transform.position = waypoints[waypointIndex].transform.position;
    }
}
