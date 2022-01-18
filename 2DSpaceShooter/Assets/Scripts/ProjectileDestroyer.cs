using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class is attached to ProjectileDestroyer game object to destroy projectiles.
public class ProjectileDestroyer : MonoBehaviour
{
    // Unity callback, when this game object's collider contacts any other colliders.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(collision.gameObject);
    }
}
