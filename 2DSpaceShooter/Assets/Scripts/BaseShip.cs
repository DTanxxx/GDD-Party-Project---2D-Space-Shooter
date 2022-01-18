using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseShip : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] protected float health = 100.0f;

    [Header("Combat")]
    [SerializeField] protected GameObject projectilePrefab = null;
    [SerializeField] protected float projectileSpeed = 2.0f;
    [SerializeField] protected GameObject projectileSpawnPoint = null;

    [Header("Effects")]
    [SerializeField] protected AudioClip firingSFX = null;
    [SerializeField] [Range(0, 1)] protected float firingVolume = 0.5f;
    [SerializeField] protected AudioClip destructionSFX = null;
    [SerializeField] [Range(0, 1)] protected float destructionVolume = 0.2f;
    [SerializeField] protected ParticleSystem destructionParticles = null;
    [SerializeField] protected float destructionExplosionDuration = 0.5f;

    protected void Fire()
    {
        // Instantiate the projectile prefab, then give it a velocity going downwards and play a sound effect.
        var projectile = Instantiate(projectilePrefab, projectileSpawnPoint.transform.position, Quaternion.identity);
        projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);
        AudioSource.PlayClipAtPoint(firingSFX, Camera.main.transform.position, firingVolume);
    }

    protected void TakeProjectileDamage(bool isEnemy, Collider2D collision, GameSession gameSession = null)
    {
        Projectile projectile = collision.gameObject.GetComponent<Projectile>();
        health = Mathf.Max(health - projectile.GetDamage(), 0);
        Destroy(collision.gameObject);
    }

    virtual protected void Die()
    {
        AudioSource.PlayClipAtPoint(destructionSFX, Camera.main.transform.position, destructionVolume);
        var explosion = Instantiate(destructionParticles, transform.position, Quaternion.identity);
        Destroy(explosion.gameObject, destructionExplosionDuration);
        Destroy(gameObject);
    }
}
