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
    private bool toSpawn = true;
    private Coroutine spawnPowerUpsCoroutine;

    private void Start()
    {
        PreBossPowerUp.OnMouseClickPowerUpDelegate += EnableToSpawn;
        spawnPowerUpsCoroutine = StartCoroutine(SpawnPowerUps());
    }

    private void OnDisable()
    {
        PreBossPowerUp.OnMouseClickPowerUpDelegate -= EnableToSpawn;
    }

    private IEnumerator SpawnPowerUps()
    {
        // DO NOT SPAWN DURING PRE BOSS BUFF SELECTION
        while (toSpawn)
        {
            var timeToWait = Random.Range(minSpawnInterval, maxSpawnInterval);
            yield return new WaitForSeconds(timeToWait);
            if (!toSpawn) { break; }
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

    public void SetToSpawn(bool toSpawn)
    {
        this.toSpawn = toSpawn;
        StopCoroutine(spawnPowerUpsCoroutine);
    }

    private void EnableToSpawn(PowerUpConfig config)
    {
        Debug.Log("Enabled!");
        toSpawn = true;
        StartCoroutine(SpawnPowerUps());
    }
}
