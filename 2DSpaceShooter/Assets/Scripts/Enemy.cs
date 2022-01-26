using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is attached to the Enemy game object and contains enemy behavior.
/// It handles timed firing from enemy.
/// It inherits from BaseShip class.
/// </summary>
public class Enemy : BaseShip
{
    // These are the Enemy class specific serialize fields.
    [Header("Enemy Stats")]
    [SerializeField] protected int scoreWhenDestroyed = 20;

    [Header("Enemy Combat")]
    [SerializeField] protected float minFireInterval = 0.3f;
    [SerializeField] protected float maxFireInterval = 0.7f;

    protected GameSession gameSession;
    protected float firingCountDown;

    virtual protected void Start()
    {
        // Initialise the firingCountDown variable, which is used as the interval between each firing.
        firingCountDown = Random.Range(minFireInterval, maxFireInterval);
        gameSession = FindObjectOfType<GameSession>();
    }

    virtual protected void Update()
    {
        // Deduct firingCountDown.
        firingCountDown -= Time.deltaTime;
        // If count down goes below 0, call Fire() and reset count down.
        if (firingCountDown <= 0.0f)
        {
            // Call Fire() from the parent class.
            Fire(-1, 1);
            firingCountDown = Random.Range(minFireInterval, maxFireInterval);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Enemy takes damage only if the projectile has tag "PlayerProjectile" ie shot from the player.
        // This is to prevent enemy ships taking damage from other enemy ships.
        if (collision.gameObject.tag == "PlayerProjectile")
        {
            // Call TakeProjectileDamage() from the parent class.
            TakeProjectileDamage(collision);
            if (health <= 0)
            {
                // If health goes below 0, reduce the enemy count in scene and call Die().              
                Die();
            }
        }
    }

    // Overrides the parent class' Die() method to add extra functionality that is specific to this class.
    override protected void Die()
    {
        // Add to player's score before calling parent class' Die().        
        base.Die();
        gameSession.RemoveEnemy();
        gameSession.AddToPlayerScore(scoreWhenDestroyed);
        // If this is a Boss, then transition to the next level.
        if (GetComponent<Boss>())
        {
            FindObjectOfType<LevelManager>().LoadNextScene();
        }
    }
}
