using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class PlayerPause : MonoBehaviour
{
    [SerializeField] private InputActionReference pauseAction;
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private GameObject instructionsUI;
    private MouseLook mouseLook;
    public GameObject firstButton;


    private void Awake()
    {
        if (pauseMenuUI != null)
        {
            pauseMenuUI.SetActive(false); // Ensure the pause menu is hidden at the start
            instructionsUI.SetActive(false); // Ensure the instructions UI is hidden at the start
        }
        else
        {
            Debug.LogWarning("PauseMenu or InstructionsUI GameObject not found in the scene.");
        }
        mouseLook = GetComponent<MouseLook>();
        EventSystem.current.SetSelectedGameObject(null);
        StartCoroutine(SelectNextFrame());
    }
    private void OnEnable()
    {
        pauseAction.action.performed += OnPause;
        pauseAction.action.Enable();
    }

    private void OnDisable()
    {
        pauseAction.action.performed -= OnPause;
        pauseAction.action.Disable();
    }

    void OnPause(InputAction.CallbackContext context)
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0; // Pause the game
            Cursor.lockState = CursorLockMode.None;
            pauseMenuUI.SetActive(true);
            mouseLook.enabled = false;
        }
        else
        {
            Time.timeScale = 1; // Resume the game
            Cursor.lockState = CursorLockMode.Locked;
            pauseMenuUI.SetActive(false);
            mouseLook.enabled = true;
        }
    }

    public void OnResume()
    {
        Time.timeScale = 1; // Resume the game
        Cursor.lockState = CursorLockMode.Locked;
        pauseMenuUI.SetActive(false);
        mouseLook.enabled = true;
    }

    public void OnInstructions()
    {
        pauseMenuUI.SetActive(false);
        instructionsUI.SetActive(true);
    }

    public void OnBack()
    {
        instructionsUI.SetActive(false);
        pauseMenuUI.SetActive(true);
    }

    public void OnQuit()
    {
        Application.Quit(); // Quit the application
    }

    private IEnumerator SelectNextFrame()
    {
        yield return null; // Wait for the next frame
        EventSystem.current.SetSelectedGameObject(firstButton);
    }
}
