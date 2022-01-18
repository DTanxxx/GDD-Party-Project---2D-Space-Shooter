using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private float transitionDelay = 2.0f;

    public void LoadStartMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadGameScene()
    {
        if (FindObjectOfType<GameSession>() != null)
        {
            // Remove the existing singleton
            FindObjectOfType<GameSession>().ResetGame();
        }
        Debug.Log("Load Game!");
        SceneManager.LoadScene("Game Scene");
    }

    public void LoadGameOverScene()
    {
        StartCoroutine(WaitAndLoad());
    }

    private IEnumerator WaitAndLoad()
    {
        yield return new WaitForSeconds(transitionDelay);
        SceneManager.LoadScene("Game Over");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
