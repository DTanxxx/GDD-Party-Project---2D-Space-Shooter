using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is attached to the Projectile game object.
/// </summary>
public class Projectile : MonoBehaviour
{
    [SerializeField] protected int damage = 10;

    public int GetDamage()
    {
        return damage;
    }

    public void SetDamage(int damage)
    {
        this.damage = damage;
    }

    public void ApplyDamageBonus(float damageBonus)
    {
        damage = (int)(damage * damageBonus);
    }
}
