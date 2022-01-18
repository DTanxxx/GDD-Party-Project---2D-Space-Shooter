using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class is attached to the EnemySpawner game object.
public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<WaveConfig> waves;
    [SerializeField] private int startingWave = 0;
    [SerializeField] private bool looping = false;
    [SerializeField] private float waveInterval = 1.5f;

    private GameSession gameSession;
    private bool continueNextWave = true;

    IEnumerator Start()
    {
        gameSession = FindObjectOfType<GameSession>();
        do
        {
            yield return StartCoroutine(SpawnAllWaves());
        }
        while (looping);
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
            var currentWave = waves[i];
            continueNextWave = false;
            yield return StartCoroutine(SpawnAllEnemiesInWave(currentWave));
            while (!continueNextWave)
            {
                yield return new WaitForEndOfFrame();
            }
            yield return new WaitForSeconds(waveInterval);
        }
    }

    private IEnumerator SpawnAllEnemiesInWave(WaveConfig waveConfig)
    {
        for (int i = 0; i < waveConfig.GetNumberOfEnemies(); i++)
        {
            gameSession.AddEnemy();
            var enemyPrefab = waveConfig.GetEnemyPrefab();
            var enemyInstance = Instantiate(enemyPrefab, waveConfig.GetWaypoints()[0].transform.position,
                Quaternion.identity);
            enemyInstance.GetComponent<EnemyMovement>().SetWaveConfig(waveConfig);
            yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawns());
        }
    }
}
