using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

// This script handles the end menu functionality, allowing players to replay the game or quit

public class EndMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI totalOrdersCompletedText;
    [SerializeField] private TextMeshProUGUI totalOrdersFailedText;
    [SerializeField] private TextMeshProUGUI totalMoneyMadeText;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None; // Unlock the cursor when the end menu is displayed
        Cursor.visible = true; // Make the cursor visible

        totalOrdersCompletedText.text = "Total Orders Completed: " + OrderManager.Instance.totalOrdersCompleted;
        totalOrdersFailedText.text = "Total Orders Failed: " + OrderManager.Instance.totalOrdersFailed;
        totalMoneyMadeText.text = "Total Money Made: $" + OrderManager.Instance.totalMoneyMade.ToString("F2");
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
