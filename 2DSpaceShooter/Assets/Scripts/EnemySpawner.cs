using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is attached to the EnemySpawner object.
/// It handles enemy wave-spawning logic.
/// </summary>
public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<WaveConfig> waves;
    [SerializeField] private WaveConfig bossWave;
    [SerializeField] private int startingWave = 0;
    [SerializeField] private float waveInterval = 1.5f;
    [SerializeField] private int numberOfLoops = 2;

    private GameSession gameSession;
    private bool continueNextWave = true;

    IEnumerator Start()
    {
        gameSession = FindObjectOfType<GameSession>();
        do
        {
            // Start the wave spawning coroutine.
            yield return StartCoroutine(SpawnAllWaves());
            numberOfLoops -= 1;
        }
        while (numberOfLoops > 0);
    }

    private void Update()
    {
        if (gameSession.GetNumberOfEnemiesAlive() == 0)
        {
            continueNextWave = true;
        }
    }

    private IEnumerator SpawnAllWaves()
    {
        for (int i = startingWave; i < waves.Count; i++)
        {
            // Iterate through the waves list and spawn waves
            var currentWave = waves[i];
            continueNextWave = false;
            yield return StartCoroutine(SpawnAllEnemiesInWave(currentWave));
            while (!continueNextWave)
            {
                // Until every enemy in the current wave is destroyed, don't spawn the next wave.
                yield return new WaitForEndOfFrame();
            }
            yield return new WaitForSeconds(waveInterval);
        }
        SpawnBoss();
    }

    private IEnumerator SpawnAllEnemiesInWave(WaveConfig waveConfig)
    {
        for (int i = 0; i < waveConfig.GetNumberOfEnemies(); i++)
        {
            // Spawn the number of enemies configured inside the waveConfig scriptable object.
            gameSession.AddEnemy();
            var enemyPrefab = waveConfig.GetEnemyPrefab();
            var enemyInstance = Instantiate(enemyPrefab, waveConfig.GetWaypoints()[0].transform.position,
                Quaternion.identity);
            enemyInstance.GetComponent<EnemyMovement>().SetWaveConfig(waveConfig);
            // Wait for a short duration before spawning the next enemy.
            yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawns());
        }
    }

    private void SpawnBoss()
    {
        Debug.Log("Spawn Boss!");
        StartCoroutine(SpawnBossWithDelay());
    }

    private IEnumerator SpawnBossWithDelay()
    {
        yield return new WaitForSeconds(waveInterval);
        yield return SpawnAllEnemiesInWave(bossWave);
    }
}
