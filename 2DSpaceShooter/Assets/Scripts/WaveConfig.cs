using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is attached to the WaveConfig scriptable object.
/// It contains data for an enemy wave.
/// </summary>
[CreateAssetMenu(menuName = "Enemy Wave Config")]
public class WaveConfig : ScriptableObject
{
    [SerializeField] private GameObject pathPrefab = null;
    [SerializeField] private GameObject enemyPrefab = null;
    [SerializeField] private float moveSpeed = 5.0f;
    [SerializeField] private float timeBetweenSpawns = 0.5f;
    [SerializeField] private int numberOfEnemies = 5;

    public GameObject GetEnemyPrefab()
    {
        return enemyPrefab;
    }

    public List<Transform> GetWaypoints()
    {
        // Get the Transform of pathPrefab's children game objects.
        var waveWaypoints = new List<Transform>();
        foreach (Transform child in pathPrefab.transform)
        {
            waveWaypoints.Add(child);
        }
        return waveWaypoints;
    }

    public float GetTimeBetweenSpawns()
    {
        return timeBetweenSpawns;
    }

    public int GetNumberOfEnemies()
    {
        return numberOfEnemies;
    }

    public float GetMoveSpeed()
    {
        return moveSpeed;
    }
}
