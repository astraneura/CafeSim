using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class ToppingsBox : MonoBehaviour
{
    private DrinkManager drinkManager;
    public GameObject toppingMenuUI; // Reference to the Topping Menu UI
    public MouseLook mouseLook;
    public GameObject firstButton;
    public static bool ToppingsMenuOpen = false;
    void Start()
    {
        toppingMenuUI.SetActive(false);
        drinkManager = DrinkManager.Instance;
    }

    public void OpenToppingMenu()
    {
        toppingMenuUI.SetActive(true);
        ToppingsMenuOpen = true;
        Time.timeScale = 0f; // Pause the game
        mouseLook.enabled = false;
        Cursor.lockState = CursorLockMode.None;

        // Set the first button as the selected UI element
        EventSystem.current.SetSelectedGameObject(null);
        StartCoroutine(SelectNextFrame());
    }

    private IEnumerator SelectNextFrame()
    {
        yield return null; // Wait for the next frame
        EventSystem.current.SetSelectedGameObject(firstButton);
    }

    public void CloseToppingMenu()
    {
        toppingMenuUI.SetActive(false);
        ToppingsMenuOpen = false;
        Time.timeScale = 1f; // Resume the game
        mouseLook.enabled = true;
        Cursor.lockState = CursorLockMode.Locked;

        EventSystem.current.SetSelectedGameObject(null);
    }

    public void AddEnergizedToDrink()
    {
        drinkManager.AddEnergizedToDrink();
    }
    public void AddCalmingToDrink()
    {
        drinkManager.AddCalmingToDrink();
    }
    public void AddLightToDrink()
    {
        drinkManager.AddLightToDrink();
    }
    public void AddHeavyToDrink()
    {
        drinkManager.AddHeavyToDrink();
    }
    public void AddFreshToDrink()
    {
        drinkManager.AddFreshToDrink();
    }
    public void AddNostalgicToDrink()
    {
        drinkManager.AddNostalgicToDrink();
    }
    public void AddUpliftingToDrink()
    {
        drinkManager.AddUpliftingToDrink();
    }
    public void AddDepressingToDrink()
    {
        drinkManager.AddDepressingToDrink();
    }
    public void AddWarmToDrink()
    {
        drinkManager.AddWarmToDrink();
    }
    public void AddColdToDrink()
    {
        drinkManager.AddColdToDrink();
    }
    public void AddCreamyToDrink()
    {
        drinkManager.AddCreamyToDrink();
    }
    public void AddThinToDrink()
    {
        drinkManager.AddThinToDrink();
    }
    public void AddSweetToDrink()
    {
        drinkManager.AddSweetToDrink();
    }
    public void AddBitterToDrink()
    {
        drinkManager.AddBitterToDrink();
    }
    public void AddSpicyToDrink()
    {
        drinkManager.AddSpicyToDrink();
    }
    public void AddBlandToDrink()
    {
        drinkManager.AddBlandToDrink();
    }
    public void AddBlessedToDrink()
    {
        drinkManager.AddBlessedToDrink();
    }
    public void AddCursedToDrink()
    {
        drinkManager.AddCursedToDrink();
    }
}
