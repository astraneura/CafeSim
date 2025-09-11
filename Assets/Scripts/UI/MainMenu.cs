using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// This script handles the main menu functionality, allowing players to start the game or quit the application.

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {

        SceneManager.LoadSceneAsync(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
