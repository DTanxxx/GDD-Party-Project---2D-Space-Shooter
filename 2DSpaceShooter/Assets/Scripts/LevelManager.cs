using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This class is attached to the Scene Manager game object.
/// It handles scene transitioning.
/// </summary>
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
        SceneManager.LoadScene("Level 1");
    }

    public void LoadNextScene()
    {
        StartCoroutine(WaitAndLoad(sceneID:SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void LoadGameOverScene()
    {
        StartCoroutine(WaitAndLoad(sceneName:"Game Over"));
    }

    private IEnumerator WaitAndLoad(string sceneName="", int sceneID=-1)
    {
        // Loads the game over scene after a short delay.
        yield return new WaitForSeconds(transitionDelay);
        if (sceneName.Length > 0)
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            SceneManager.LoadScene(sceneID);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
