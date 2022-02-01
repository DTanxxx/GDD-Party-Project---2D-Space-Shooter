using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is the parent class containing common behavior that all ships should have.
/// In our case, this means the Enemy and Player classes will inherit from this class.
/// </summary>
public class BaseShip : MonoBehaviour
{
    // These serialize fields are available to all classes inheriting from this class, eg Enemy.
    [Header("Stats")]
    [SerializeField] protected int health = 100;

    [Header("Combat")]
    [SerializeField] protected GameObject projectilePrefab = null;
    [SerializeField] protected float projectileSpeed = 2.0f;
    [SerializeField] protected GameObject[] projectileSpawnPoints = null;

    [Header("Effects")]
    [SerializeField] protected AudioClip firingSFX = null;
    [SerializeField] [Range(0, 1)] protected float firingVolume = 0.5f;
    [SerializeField] protected AudioClip destructionSFX = null;
    [SerializeField] [Range(0, 1)] protected float destructionVolume = 0.2f;
    [SerializeField] protected ParticleSystem destructionParticles = null;
    [SerializeField] protected float destructionExplosionDuration = 0.5f;
    [SerializeField] protected float fadeWhenHitDuration = 0.5f;
    [SerializeField] protected float fadeAlpha = 80.0f;

    private Coroutine fadeCoroutine;
    private Color originalSpriteColor;
    
    // The 3 methods below are available to child classes.
    virtual protected void Fire(int direction, float damageBonus)
    {
        // Instantiate the projectile prefab.
        foreach (GameObject spawnPoint in projectileSpawnPoints)
        {
            var projectile = Instantiate(projectilePrefab, spawnPoint.transform.position, Quaternion.identity);
            // Give it a velocity going downwards.
            projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(0, direction * projectileSpeed);
            projectile.GetComponent<Projectile>().ApplyDamageBonus(damageBonus);
        }
        // Play a firing sound effect.
        AudioSource.PlayClipAtPoint(firingSFX, Camera.main.transform.position, firingVolume);
    }

    protected void TakeProjectileDamage(Collider2D collision)
    {
        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
            GetComponent<SpriteRenderer>().material.color = originalSpriteColor;
        }
        fadeCoroutine = StartCoroutine(FadeWhenHit());
        Projectile projectile = collision.gameObject.GetComponent<Projectile>();
        // Calculate the new health after damage is applied; restrict it be >= 0
        health = Mathf.Max(health - projectile.GetDamage(), 0);
        // Destory the projectile.
        Destroy(collision.gameObject);
    }

    protected void TakeCollisionDamage()
    {
        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
            GetComponent<SpriteRenderer>().material.color = originalSpriteColor;
        }
        fadeCoroutine = StartCoroutine(FadeWhenHit());
    }

    protected IEnumerator FadeWhenHit()
    {
        var renderer = GetComponent<SpriteRenderer>();
        originalSpriteColor = renderer.material.color;
        renderer.material.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, fadeAlpha);
        yield return new WaitForSeconds(fadeWhenHitDuration);
        renderer.material.color = originalSpriteColor;
    }

    virtual protected void Die()
    {
        // Play a destruction sound effect.
        AudioSource.PlayClipAtPoint(destructionSFX, Camera.main.transform.position, destructionVolume);
        // Instantiate a destruction particle system.
        var explosion = Instantiate(destructionParticles, transform.position, Quaternion.identity);
        // Destroy the particle system after a short duration, which by then the particles would have disappeared.
        Destroy(explosion.gameObject, destructionExplosionDuration);
        // Destroy the ship.
        Destroy(gameObject);
    }
}
