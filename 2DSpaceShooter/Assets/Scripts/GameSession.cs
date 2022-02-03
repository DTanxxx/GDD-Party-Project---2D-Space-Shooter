using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is attached to the Game Session object.
/// The Game Session object is persistent throughout the entire game session.
/// It keeps track of the enemies alive and player score.
/// </summary>
public class GameSession : MonoBehaviour
{
    [SerializeField] private int numberOfEnemiesAlive = 0;
    [SerializeField] private int playerHealth = -1;

    public bool paused;

    private void Awake()
    {
        SetUpSingleton();
    }

    private void Start()
    {
        paused = false;
    }

    private void Update()
    {
        // Pause the game when 'P' key is pressed.
        if (Input.GetKeyDown(KeyCode.P) && !paused)
        {
            Time.timeScale = 0;
            paused = true;
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            Time.timeScale = 1;
            paused = false;
        }
    }

    private void SetUpSingleton()
    {
        // Make this object a singleton so it persists through scenes.
        int numberGameSessions = FindObjectsOfType<GameSession>().Length;
        if (numberGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void AddEnemy()
    {
        numberOfEnemiesAlive += 1;
    }

    public void RemoveEnemy()
    {
        numberOfEnemiesAlive -= 1;
    }

    public int GetNumberOfEnemiesAlive()
    {
        return numberOfEnemiesAlive;
    }

    public void ResetGame()
    {
        Destroy(gameObject);
    }

    public int GetPlayerHealth()
    {
        return playerHealth;
    }

    public void SetPlayerHealth(int health)
    {
        playerHealth = health;
    }
}
