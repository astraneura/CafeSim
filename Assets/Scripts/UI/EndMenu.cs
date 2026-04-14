using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

// This script handles the end menu functionality, allowing players to replay the game or quit

public class EndMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI totalOrdersCompletedText;
    [SerializeField] private TextMeshProUGUI totalOrdersFailedText;
    [SerializeField] private TextMeshProUGUI totalMoneyMadeText;
    [SerializeField] private TextMeshProUGUI clearText;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None; // Unlock the cursor when the end menu is displayed
        Cursor.visible = true; // Make the cursor visible


    }
    public void ReplayGame()
    {
        StartCoroutine(Replay());
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private IEnumerator Replay()
    {
        if (OrderManager.Instance != null)
        {
            OrderManager.Instance.ResetManager();
        }

        yield return null;

        SceneManager.LoadScene(0);
    }
}
