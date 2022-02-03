using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] powerUps;
    [SerializeField] private float minSpawnInterval = 15.0f;
    [SerializeField] private float maxSpawnInterval = 30.0f;
    [SerializeField] private float spawnPositionOffset = 10.0f;

    private int currentPowerUpIndex = 0;

    private void Start()
    {
        StartCoroutine(SpawnPowerUps());
    }

    private IEnumerator SpawnPowerUps()
    {
        // DO NOT SPAWN DURING PRE BOSS BUFF SELECTION
        while (true)
        {
            var timeToWait = Random.Range(minSpawnInterval, maxSpawnInterval);
            yield return new WaitForSeconds(timeToWait);
            var minXPosition = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + spawnPositionOffset;
            var maxXPosition = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - spawnPositionOffset;
            var spawnXPosition = Random.Range(minXPosition, maxXPosition);
            Instantiate(powerUps[currentPowerUpIndex],
                new Vector3(spawnXPosition, transform.position.y, transform.position.z),
                Quaternion.identity);
            currentPowerUpIndex += 1;
            if (currentPowerUpIndex == powerUps.Length)
            {
                // Last power up, reset index/
                currentPowerUpIndex = 0;
            }
        }
    }
}
