using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor.Rendering;

public class Customer : MonoBehaviour
{
    //reference to the game manager
    private GameManager gameManager;

    //variables for the name
    public string customerName;
    public CustomerNameDatabase nameDatabase; // Reference to the name database


    //variables for the order
    public DrinkRecipeDatabase drinkRecipeDatabase; // Reference to the drink recipe database
    public DrinkRecipe currentRecipe;
    public int currentOrderNum = 0;
    public int maxOrderNum = 1;
    public List<OrderStep> currentOrder = new List<OrderStep>();
    private int currentStepIndex = 0;

    //Variables for the time limit
    public float orderTimeLimit = 30f; // Time limit for the order in seconds
    private float orderTimer;
    private bool orderInProgress = false;

    //variables for the UI
    private Slider patienceSlider;

    void Start()
    {
        customerName = GetRandomName(); // Get a random name from the database
        patienceSlider = GetComponentInChildren<Slider>();
        patienceSlider.gameObject.SetActive(false);
        gameManager = FindAnyObjectByType<GameManager>();
    }

    void Update()
    {
        if (orderInProgress)
        {
            orderTimer -= Time.deltaTime;

            if (patienceSlider != null)
            {
                patienceSlider.value = orderTimer / orderTimeLimit;
            }

            if (orderTimer <= 0f)
            {
                OnOrderTimeout();
            }
        }
    }

    public string GetRandomName()
    {
        if (nameDatabase != null && nameDatabase.names.Count > 0)
        {
            return nameDatabase.names[Random.Range(0, nameDatabase.names.Count)];
        }
        return "Customer"; // Generic name if the database is empty or not assigned
    }

    public bool GenerateOrder()
    {
        if (drinkRecipeDatabase == null || drinkRecipeDatabase.allRecipes.Count == 0)
            return false;

        currentRecipe = drinkRecipeDatabase.allRecipes[Random.Range(0, drinkRecipeDatabase.allRecipes.Count)];
        OrderManager.Instance.AddOrder(customerName, currentRecipe);

        foreach (string step in currentRecipe.steps)
        {
            currentOrder.Add(new OrderStep { stepName = step });
        }
        orderTimer = orderTimeLimit; // Reset the timer
        orderInProgress = true;

        if (patienceSlider != null)
        {
            patienceSlider.gameObject.SetActive(true);
            patienceSlider.value = 1f;
        }

        if (gameManager != null)
            gameManager.OnCustomerOrderStarted();

        Debug.Log($"{customerName} has generated an order for {currentRecipe.drinkName}. with steps: {string.Join(", ", currentRecipe.steps)}");
        return true;

    }

    void OnOrderTimeout()
    {
        Debug.Log($"{customerName} ran out of patience and left!");
        orderInProgress = false;
        gameManager.currentCustomers--;
        Destroy(gameObject, 2f);
    }

    public bool TryCompleteStep(string attemptedStep)
    {
        if (currentStepIndex >= currentOrder.Count)
        {
            return false; // No more steps to complete
        }
        if (currentOrder[currentStepIndex].stepName == attemptedStep)
        {
            currentOrder[currentStepIndex].isCompleted = true;
            currentStepIndex++;
            return true; // Step completed successfully
        }
        else
        {
            ResetOrderProgress();
            Debug.Log($"Step '{attemptedStep}' failed for customer {customerName}. Resetting order.");
            return false;
        }
    }

    void ResetOrderProgress()
    {
        foreach (OrderStep step in currentOrder)
        {
            step.isCompleted = false;
        }
        currentStepIndex = 0;
        Debug.Log($"{customerName}'s order has been reset.");
    }

    public void CompleteOrder()
    {
        if (currentRecipe != null)
        {
            Debug.Log("Adding money: " + currentRecipe.cost);
            FindAnyObjectByType<PlayerInteraction>().AddMoney(currentRecipe.cost);
        }
    }
}
