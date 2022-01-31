using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreBossPowerUpSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] powerUps;
    [SerializeField] private float yOffset = 5.0f;
    [SerializeField] private float delayBeforeSpawn = 1.0f;

    private GameObject[] powerUpInstances;

    private void Start()
    {
        powerUpInstances = new GameObject[powerUps.Length];
        PreBossPowerUp.OnMouseClickPowerUpDelegate += DestroyPowerUps;
    }

    private void OnDisable()
    {
        PreBossPowerUp.OnMouseClickPowerUpDelegate -= DestroyPowerUps;
    }

    public IEnumerator ShowPowerUps()
    {
        FindObjectOfType<PowerUpSpawner>().SetToSpawn(false);
        yield return new WaitForSeconds(delayBeforeSpawn);
        var temp = new GameObject[powerUps.Length];

        for (int i = 0; i < powerUps.Length; ++i)
        {
            // Spawn that shit.
            var leftBoundary = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
            var rightBoundary = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;
            var firstXPos = (rightBoundary - leftBoundary) / (powerUps.Length+1);
            
            var powerUp = Instantiate(powerUps[i], 
                new Vector2(leftBoundary + (i+1) * firstXPos, transform.position.y + yOffset),
                Quaternion.identity);
            powerUpInstances[i] = powerUp;
        }
    }

    private void DestroyPowerUps(PowerUpConfig config)
    {
        foreach (var powerUp in powerUpInstances)
        {
            Destroy(powerUp);
        }
    }
}
