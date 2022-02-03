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
    private BossStateMachine stateMachine;

    private void Start()
    {
        stateMachine = GetComponent<BossStateMachine>();
    }

    private void Update()
    {
        FollowPath();
    }

    private void FollowPath()
    {
        if (waveConfig == null) { return; }

        if (GetComponent<BossStateMachine>())
        {
            // It's a Boss!
            // Check its state.
            BossStateMachine stateMachine = GetComponent<BossStateMachine>();
            if (stateMachine.GetCurrentState() == BossState.Fire)
            {
                // Only move if boss is in Fire state.
                Move();
            }
            return;
        }
        Move();
    }

    private void Move()
    {
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
            // Retrace the path.
            waypointIndex = 1;
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
