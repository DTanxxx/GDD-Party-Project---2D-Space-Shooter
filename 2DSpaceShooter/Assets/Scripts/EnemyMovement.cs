using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        if (waypointIndex < waypoints.Count)
        {
            var targetPosition = waypoints[waypointIndex].transform.position;
            var movementThisFrame = Time.deltaTime * waveConfig.GetMoveSpeed();
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementThisFrame);

            if (transform.position == targetPosition)
            {
                waypointIndex++;
            }
        }
        else
        {
            FindObjectOfType<GameSession>().RemoveEnemy();
            Destroy(gameObject);
        }
    }

    public void SetWaveConfig(WaveConfig waveConfig)
    {
        this.waveConfig = waveConfig;
        waypoints = this.waveConfig.GetWaypoints();
        transform.position = waypoints[waypointIndex].transform.position;
    }
}
