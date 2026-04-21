using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;

public class PlayerPause : MonoBehaviour
{
    [SerializeField] private InputActionReference pauseAction;
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private GameObject instructionsUI;
    [SerializeField] private PlayerInteraction playerInteraction;
    public AudioMixer mixer;
    private MouseLook mouseLook;


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
            mixer.SetFloat("MasterLowPass", 350f);
            pauseMenuUI.SetActive(true);
            mouseLook.enabled = false;
            playerInteraction.isMenuOpen = true;
        }
        else
        {
            Time.timeScale = 1; // Resume the game
            Cursor.lockState = CursorLockMode.Locked;
            mixer.SetFloat("MasterLowPass", 22000f);
            pauseMenuUI.SetActive(false);
            mouseLook.enabled = true;
            playerInteraction.isMenuOpen = false;

        }
    }

    public void OnResume()
    {
        Time.timeScale = 1; // Resume the game
        Cursor.lockState = CursorLockMode.Locked;
        pauseMenuUI.SetActive(false);
        mouseLook.enabled = true;
        playerInteraction.isMenuOpen = false;
        mixer.SetFloat("MasterLowPass", 22000f);
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
}
