using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This class is attached to the score UI.
/// It handles the update of score text.
/// </summary>
public class ScoreDisplay : MonoBehaviour
{
    [SerializeField] private Text scoreText = null;

    private GameSession gameSession;

    private void Start()
    {
        gameSession = FindObjectOfType<GameSession>();
    }

    private void Update()
    {
        scoreText.text = gameSession.GetPlayerScore().ToString();
    }
}
