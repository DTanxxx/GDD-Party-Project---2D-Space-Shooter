using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreBossPowerUp : MonoBehaviour
{
    [SerializeField] private PowerUpConfig powerUpConfig = null;

    public static Action<PowerUpConfig> OnMouseClickPowerUpDelegate;

    // Detects mouse click
    // Trigger event subscribed by EnemySpawner to spawn boss
    // ... and PowerUpSpawner to spawn power ups
    // ... and PlayerController to use the power up
    private void OnMouseDown()
    {
        OnMouseClickPowerUpDelegate.Invoke(powerUpConfig);
    }
}
