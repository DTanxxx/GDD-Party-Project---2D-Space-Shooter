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

    private PlayerController playerController;

    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
    }

    private void Update()
    {
        healthText.text = playerController.GetHealth().ToString();
    }
}
