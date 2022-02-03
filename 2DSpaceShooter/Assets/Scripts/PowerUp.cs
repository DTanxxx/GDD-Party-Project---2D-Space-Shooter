using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is attached to a Power Up prefab.
/// </summary>
public class PowerUp : MonoBehaviour
{
    [SerializeField] private PowerUpConfig powerUpConfig = null;
    [SerializeField] private float moveSpeed = 10.0f;

    private void Update()
    {
        // The prefab should move downwards the screen after being spawned.
        transform.Translate(new Vector3(0, -moveSpeed * Time.deltaTime, 0), Space.World);
    }

    public PowerUpConfig GetPowerUpConfig()
    {
        return powerUpConfig;
    }
}
