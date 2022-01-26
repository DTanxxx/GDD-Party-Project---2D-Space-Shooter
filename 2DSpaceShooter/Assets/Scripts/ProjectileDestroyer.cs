using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is attached to the ProjectileDestroyer game object.
/// It handles the destruction of projectiles that fly off the scene.
/// </summary>
public class ProjectileDestroyer : MonoBehaviour
{
    // Unity callback, when this game object's collider contacts any other colliders.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Projectile>() || collision.GetComponent<PowerUp>())
        {
            Destroy(collision.gameObject);
        }       
    }
}
