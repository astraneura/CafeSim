using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GenericCustomer : MonoBehaviour, ICustomer
{
    //manager references
    private GameManager gameManager;
    private DrinkManager drinkManager;

    //database references
    public CustomerNameDatabase nameDatabase; // Reference to the name database
    public DrinkRecipeDatabase drinkRecipeDatabase; // Reference to the drink recipe database

    public string CustomerName => customerName;
    public string customerName;

    public float orderTimeLimit = 30f;
    protected float orderTimer;
    protected bool orderInProgress = false;

    public DrinkRecipe currentRecipe;
    public List<OrderStep> currentOrder = new List<OrderStep>();

    void Awake()
    {
        customerName = GetCustomerName();

    }
    public string GetCustomerName()
    {
        if (nameDatabase != null && nameDatabase.names.Count > 0)
        {
            int randomIndex = Random.Range(0, nameDatabase.names.Count);
            return nameDatabase.names[randomIndex];
        }
        return "Customer"; // Fallback name
    }

    public bool GenerateOrder()
    {
        if (drinkRecipeDatabase == null || drinkRecipeDatabase.allRecipes.Count == 0)
            return false;

        currentRecipe = drinkRecipeDatabase.allRecipes[Random.Range(0, drinkRecipeDatabase.allRecipes.Count)];
        currentOrder.Clear();
        foreach (string step in currentRecipe.steps)
        {
            currentOrder.Add(new OrderStep { stepName = step });
        }
        orderTimer = orderTimeLimit; // Reset the timer
        orderInProgress = true;

        OrderManager.Instance.SetCurrentOrder(this, currentRecipe);

        if (gameManager != null)
            gameManager.OnCustomerOrderStarted();

        Debug.Log("Current order: " + string.Join(", ", currentRecipe.steps));
        return true;
    }

    public void UpdateOrderTimer()
    {
        if (!orderInProgress)
            return;

        orderTimer -= Time.deltaTime;
        if (orderTimer <= 0f)
        {
            orderInProgress = false;
            Debug.Log(customerName + "'s order has timed out!");
            OnOrderTimeout();
        }
    }

    public void OnOrderTimeout()
    {
        orderInProgress = false;
        Debug.Log($"{customerName} ran out of patience and left!");
        Destroy(gameObject, 2f);
    }

    public void ResetOrderProgress()
    {
        foreach (OrderStep step in currentOrder)
        {
            step.isCompleted = false;
        }
        Debug.Log($"{customerName}'s order progress has been reset.");
    }

    public void CompleteOrder()
    {
        if (currentRecipe != null)
        {
            OrderManager.Instance.ClearCurrentOrder();
            Debug.Log("Adding money: " + currentRecipe.cost);
            FindAnyObjectByType<PlayerInteraction>().AddMoney(currentRecipe.cost);
            Destroy(gameObject);
        }
    }
}
