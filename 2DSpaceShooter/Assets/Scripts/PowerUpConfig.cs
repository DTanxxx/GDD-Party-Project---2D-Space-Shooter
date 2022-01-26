using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Power Up Config")]
public class PowerUpConfig : ScriptableObject
{
    [SerializeField] private float fireRateMultiplier = 2.0f;
    [SerializeField] private int healthBuff = 150;
    [SerializeField] private bool invincibilityBuff = true;
    [SerializeField] private float damageMultiplier = 1.5f;
    [SerializeField] private float powerUpDuration = 5.0f;

    public float GetFireRateMultiplier()
    {
        return fireRateMultiplier;
    }

    public int GetHealthBuff()
    {
        return healthBuff;
    }

    public bool GetInvincibilityBuff()
    {
        return invincibilityBuff;
    }

    public float GetDamageMultiplier()
    {
        return damageMultiplier;
    }

    public float GetPowerUpDuration()
    {
        return powerUpDuration;
    }
}
