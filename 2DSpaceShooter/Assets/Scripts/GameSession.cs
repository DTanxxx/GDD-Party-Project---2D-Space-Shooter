using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    private int numberOfEnemiesAlive = 0;
    private int playerScore = 0;

    private void Awake()
    {
        SetUpSingleton();
    }

    private void SetUpSingleton()
    {
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
        numberOfEnemiesAlive++;
    }

    public void RemoveEnemy()
    {
        numberOfEnemiesAlive--;
    }

    public int GetNumberOfEnemiesAlive()
    {
        return numberOfEnemiesAlive;
    }

    public void ResetGame()
    {
        Destroy(gameObject);
    }

    public int GetPlayerScore()
    {
        return playerScore;
    }

    public void AddToPlayerScore(int amount)
    {
        playerScore += amount;
    }
}
