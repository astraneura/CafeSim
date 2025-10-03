using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseCustomer
{
    //reference to the game manager
    public GameManager gameManager;

    //name variables
    public string customerName;
    public CustomerNameDatabase nameDatabase;

    //order variables
    public DrinkRecipeDatabase drinkRecipeDatabase;
    public DrinkRecipe currentRecipe;

    //time limit variables
    public float orderTimeLimit = 30f;
    protected float orderTimer;
    protected bool orderInProgress = false;

    public Slider patienceSlider;



    //Gets a random name from the database
    public string GetCustomerName()
    {
        if (nameDatabase != null && nameDatabase.names.Count > 0)
        {
            int randomIndex = Random.Range(0, nameDatabase.names.Count);
            return nameDatabase.names[randomIndex];
        }
        return "Customer"; // Generic name if the database is empty or not assigned  
    }

    public bool GenerateOrder()
    {
        if (drinkRecipeDatabase == null || drinkRecipeDatabase.allRecipes.Count == 0)
            return false;

        if (OrderManager.Instance.orderCompleted == false)
            return false;

        currentRecipe = drinkRecipeDatabase.allRecipes[Random.Range(0, drinkRecipeDatabase.allRecipes.Count)];

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

    public void UpdateOrderTimer()
    {
         if (!orderInProgress) return;

        orderTimer -= Time.deltaTime;

        if (patienceSlider != null)
        {
            patienceSlider.value = orderTimer / orderTimeLimit;
        }

        if (orderTimer <= 0f)
        {
            orderInProgress = false;
            Debug.Log($"{customerName} ran out of patience!");
        }
    }
}
