using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class is the behavior attached to Enemy game object
public class Enemy : BaseShip
{
    [Header("Stats")]
    //[SerializeField] private float health = 100.0f;
    [SerializeField] private int scoreWhenDestroyed = 20;

    [Header("Combat")]
    [SerializeField] private float minFireInterval = 0.3f;
    [SerializeField] private float maxFireInterval = 0.7f;
    //[SerializeField] private GameObject projectilePrefab = null;
    //[SerializeField] private float projectileSpeed = 2.0f;
    //[SerializeField] private GameObject projectileSpawnPoint = null;

    /*[Header("Effects")]
    //[SerializeField] private AudioClip firingSFX = null;
    [SerializeField] [Range(0, 1)] private float firingVolume = 0.5f;
    [SerializeField] private AudioClip destructionSFX = null;
    [SerializeField] [Range(0, 1)] private float destructionVolume = 0.2f;
    [SerializeField] private ParticleSystem destructionParticles = null;
    [SerializeField] private float destructionExplosionDuration = 0.5f;*/

    private GameSession gameSession;
    private float firingCountDown;

    private void Start()
    {
        // Initialise the firingCountDown variable, which is used as an interval between each firing.
        firingCountDown = Random.Range(minFireInterval, maxFireInterval);
        gameSession = FindObjectOfType<GameSession>();
    }

    private void Update()
    {
        firingCountDown -= Time.deltaTime;
        if (firingCountDown <= 0.0f)
        {
            // Fire if firingCountDown reaches 0 or less, then reset the count down for the next fire.
            Fire();
            firingCountDown = Random.Range(minFireInterval, maxFireInterval);
        }
    }

    /*private void Fire()
    {
        // Instantiate the projectile prefab, then give it a velocity going downwards and play a sound effect.
        var projectile = Instantiate(projectilePrefab, projectileSpawnPoint.transform.position, Quaternion.identity);
        projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);
        AudioSource.PlayClipAtPoint(firingSFX, Camera.main.transform.position, firingVolume);
    }*/

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerProjectile")
        {
            // Act only if the projectile game object comes from the player
            /*Projectile projectile = collision.gameObject.GetComponent<Projectile>();
            health = Mathf.Max(health - projectile.GetDamage(), 0);
            if (health <= 0)
            {
                // Take damage and if health reaches 0 or less, this enemy dies.
                gameSession.RemoveEnemy();
                Die();
            }

            // Destroy the projectile game object from the scene.
            Destroy(collision.gameObject);*/
            TakeProjectileDamage(true, collision, gameSession);
            if (health <= 0)
            {
                gameSession.RemoveEnemy();               
                Die();
            }
        }
    }

    override protected void Die()
    {
        gameSession.AddToPlayerScore(scoreWhenDestroyed);
        base.Die();
    }
}
