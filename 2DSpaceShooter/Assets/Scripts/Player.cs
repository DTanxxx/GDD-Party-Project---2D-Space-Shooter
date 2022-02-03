using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is attached to the Player game object and contains player behavior.
/// It handles timed firing.
/// It inherits from BaseShip class.
/// </summary>
public class Player : BaseShip
{
    [Header("Player Movement")]
    [SerializeField] private float moveSpeed = 5.0f;
    [SerializeField] private float movementBoundaryPadding = 5.0f;

    [Header("Player Combat")]
    [SerializeField] private float fireInterval = 0.5f;

    private float leftBoundary;
    private float rightBoundary;
    private float topBoundary;
    private float bottomBoundary;

    private PowerUpConfig powerUp;
    private GameSession gameSession;
    private float powerUpTime = 0.0f;
    private float firingTimer = 0.0f;
    private float baseDamageMultiplier = 1.0f;

    private void Start()
    {
        // Calculate the player's movement boundaries so player does not go off the screen.
        SetUpMoveBoundaries();
        gameSession = FindObjectOfType<GameSession>();
        if (gameSession.GetPlayerHealth() > 0)
        {
            // Makes player health consistent through levels.
            // Player health is stored in GameSession, which is a singleton.
            health = gameSession.GetPlayerHealth();
        }
    }

    private void OnDisable()
    {
        // Save player health to GameSession.
        gameSession.SetPlayerHealth(health);
    }

    private void Update()
    {
        if (gameSession.paused) { return; }
        // Handle player input each frame.
        HandleMovementInput();
        HandleFireInput();

        if (powerUpTime > 0.0f)
        {
            if (Time.realtimeSinceStartup - powerUpTime > powerUp.GetPowerUpDuration())
            {
                powerUpTime = 0.0f;
            }
        }
    }

    private void HandleMovementInput()
    {
        // Obtain relevant keybind data and use them to calculate a new player position after player gives some movement input.
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

        // Clamp the player's new position using the movement boundaries we calculated earlier.
        var newXPos = Mathf.Clamp(transform.position.x + deltaX, leftBoundary, rightBoundary);
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, bottomBoundary, topBoundary);
        transform.position = new Vector2(newXPos, newYPos);
    }

    private void HandleFireInput()
    {
        firingTimer -= Time.deltaTime;
        // Fire according to a timer.
        if (Input.GetKey(KeyCode.Space) && firingTimer <= 0.0f)
        {
            firingTimer = fireInterval;
            if (powerUpTime > 0.0f && powerUp.GetFireRateMultiplier() > 0.0f)
            {
                firingTimer = firingTimer / powerUp.GetFireRateMultiplier();
            }
            if (powerUpTime > 0.0f)
            {
                Fire(1, powerUp.GetDamageMultiplier());
            }
            else
            {
                Fire(1, (int)baseDamageMultiplier);
            }
        }
    }

    private void SetUpMoveBoundaries()
    {
        // Calculate the movement space for the player using Camera's perspective.
        Camera mainCamera = Camera.main;
        leftBoundary = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + movementBoundaryPadding;
        rightBoundary = mainCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - movementBoundaryPadding;
        topBoundary = mainCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - movementBoundaryPadding;
        bottomBoundary = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + movementBoundaryPadding;
    }

    private void ApplyPowerUp()
    {
        // Apply power up effects when player captures a power up.
        health += powerUp.GetHealthBuff();
        // Set a timer for the power up.
        powerUpTime = Time.realtimeSinceStartup;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "EnemyProjectile")
        {
            // Take damage only if the player collides with an enemy projectile.
            if (powerUpTime > 0.0f && powerUp.GetInvincibilityBuff())
            {
                Destroy(collision.gameObject);
                return;
            }
            TakeProjectileDamage(collision);
        }
        else if (collision.gameObject.tag == "PowerUp")
        {
            // If player collided with a power up, use it.
            powerUp = collision.gameObject.GetComponent<PowerUp>().GetPowerUpConfig();
            ApplyPowerUp();
            Destroy(collision.gameObject);
        }
    }

    public int GetHealth()
    {
        return health;
    }
}
