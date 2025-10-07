using System.Collections.Generic;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    // Singleton instance
    public static OrderManager Instance;

    //private List<Order> activeOrders = new List<Order>();
    // variables to track the current order and its steps
    private Order currentOrder;
    public List<OrderStep> currentOrderSteps = new List<OrderStep>();
    private int currentStepIndex = 0;
    public bool orderCompleted;

    public ICustomer currentCustomer { get; private set; }
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void SetCurrentOrder(ICustomer customer, DrinkRecipe recipe)
    {
        currentCustomer = customer;

        Order newOrder = new Order(customer.CustomerName, recipe);
        currentOrder = newOrder;
        currentOrderSteps.Clear();
        currentStepIndex = 0;
        orderCompleted = false;
        foreach (string step in recipe.steps)
        {
            currentOrderSteps.Add(new OrderStep { stepName = step });
        }

    }

    public bool AttemptStep(string attemptedStep)
    {
        if (currentOrder == null)
            return false;

        bool success = currentOrder.TryStep(attemptedStep);

        if (success)
        {
            if (currentOrder.isCompleted)
            {
                Debug.Log($"{currentOrder.customerID}'s order complete!");
                orderCompleted = true;
                return false;
            }
            else if (currentStepIndex >= currentOrderSteps.Count)
            {
                return false; // No more steps to complete
            }
            else
            if (currentOrderSteps[currentStepIndex].stepName == attemptedStep)
            {
                currentOrderSteps[currentStepIndex].isCompleted = true;
                currentStepIndex++;
                Debug.Log($"Step '{attemptedStep}' completed for order from {currentOrder.customerID}.");
                return true; // Step completed successfully
            }
        }
        Debug.Log($"Step '{attemptedStep}' failed for order from {currentOrder.customerID}. Resetting order.");
        currentOrder.Reset();
        return false;
    }

    // clear all the order states
    public void ClearCurrentOrder()
    {
        currentOrder = null;
        currentOrderSteps.Clear();
        currentStepIndex = 0;
        orderCompleted = false;
    }
}
