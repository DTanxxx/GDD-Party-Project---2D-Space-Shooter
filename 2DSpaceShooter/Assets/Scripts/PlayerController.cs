using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class is a behavior attached to the Player game object.
public class PlayerController : MonoBehaviour
{
    [SerializeField] private int health = 500;
    [SerializeField] private float moveSpeed = 5.0f;
    [SerializeField] private float movementBoundaryPadding = 5.0f;
    [SerializeField] private float fireInterval = 0.5f;
    [SerializeField] private GameObject projectilePrefab = null;
    [SerializeField] private float projectileSpeed = 2.0f;
    [SerializeField] private GameObject projectileSpawnPoint = null;
    [SerializeField] private AudioClip firingSFX = null;
    [SerializeField] [Range(0, 1)] private float firingVolume = 1.0f;
    [SerializeField] private AudioClip destructionSFX = null;
    [SerializeField] [Range(0, 1)] private float destructionVolume = 0.2f;
    [SerializeField] private ParticleSystem destructionParticles = null;
    [SerializeField] private float destructionExplosionDuration = 0.5f;

    private float leftBoundary;
    private float rightBoundary;
    private float topBoundary;
    private float bottomBoundary;

    private Coroutine firingCoroutine;

    private void Start()
    {
        // Calculate the player's movement boundaries so player does not go off the screen.
        SetUpMoveBoundaries();
    }

    private void Update()
    {
        // Handle player input each frame.
        HandleMovementInput();
        HandleFireInput();
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
        // Control the firing keybind.
        if (Input.GetKeyDown(KeyCode.Space))
        {
            firingCoroutine = StartCoroutine(FireContinuously());
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            StopCoroutine(firingCoroutine);
        }
    }

    private IEnumerator FireContinuously()
    {
        // When we press Space, this coroutine is executed, which involved an infinite loop.
        while (true)
        {
            // For each iteration of the loop, we instantiate a player's projectile and gives it a velocity upwards.
            var projectile = Instantiate(projectilePrefab, projectileSpawnPoint.transform.position, Quaternion.identity);
            projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
            // Play a sound effect for player firing.
            AudioSource.PlayClipAtPoint(firingSFX, Camera.main.transform.position, firingVolume);
            // Wait asynchronously a fireInterval amount of time before executing the next iteration of the loop.
            yield return new WaitForSeconds(fireInterval);
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "EnemyProjectile")
        {
            // Take damage only if the player collides with an enemy projectile.
            Projectile projectile = collision.gameObject.GetComponent<Projectile>();
            health = Mathf.Max(health - projectile.GetDamage(), 0);
            if (health <= 0)
            {
                Die();
            }

            Destroy(collision.gameObject);
        }
    }

    private void Die()
    {
        // Same as Enemy's Die()
        AudioSource.PlayClipAtPoint(destructionSFX, Camera.main.transform.position, destructionVolume);
        var explosion = Instantiate(destructionParticles, transform.position, Quaternion.identity);
        Destroy(explosion, destructionExplosionDuration);
        Destroy(gameObject);
        FindObjectOfType<LevelManager>().LoadGameOverScene();
    }

    public int GetHealth()
    {
        return health;
    }
}
