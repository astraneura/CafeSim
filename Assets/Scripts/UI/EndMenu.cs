using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// This script handles the end menu functionality, allowing players to replay the game or quit

public class EndMenu : MonoBehaviour
{
    private void Start()
    {
        Cursor.lockState = CursorLockMode.None; // Unlock the cursor when the end menu is displayed
        Cursor.visible = true; // Make the cursor visible
    }
    public void ReplayGame()
    {

        SceneManager.LoadSceneAsync(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
