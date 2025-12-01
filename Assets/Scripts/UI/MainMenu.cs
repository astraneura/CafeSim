using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// This script handles the main menu functionality, allowing players to start the game or quit the application.

public class MainMenu : MonoBehaviour
{
    public GameObject intructionsUI;
    public GameObject mainMenuUI;

    void Awake()
    {
        intructionsUI.SetActive(false);
        mainMenuUI.SetActive(true);
    }
    public void PlayGame()
    {

        SceneManager.LoadSceneAsync(1);
    }

    public void OnInstructions()
    {
        mainMenuUI.SetActive(false);
        intructionsUI.SetActive(true);
    }

    public void OnBack()
    {
        intructionsUI.SetActive(false);
        mainMenuUI.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
