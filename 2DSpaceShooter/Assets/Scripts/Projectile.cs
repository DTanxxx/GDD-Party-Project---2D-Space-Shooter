using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class is attached to the Projectile game object, storing relevant information of a projectile.
public class Projectile : MonoBehaviour
{
    [SerializeField] private int damage = 10;

    // Getter method for the damage stat.
    public int GetDamage()
    {
        return damage;
    }
}
