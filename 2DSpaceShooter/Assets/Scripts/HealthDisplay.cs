using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This class is attached to the health UI.
/// It handles the update of health text.
/// </summary>
public class HealthDisplay : MonoBehaviour
{
    [SerializeField] private Text healthText = null;

    private Player player;

    private void Start()
    {
        player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        // Updates text using player health information.
        healthText.text = player.GetHealth().ToString();
    }
}
