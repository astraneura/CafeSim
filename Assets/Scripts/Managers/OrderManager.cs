using System.Collections.Generic;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    public static OrderManager Instance;

    private List<Order> activeOrders = new List<Order>();
    private int focusedOrderIndex = 0;
    public bool orderCompleted;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void AddOrder(string customerID, DrinkRecipe recipe)
    {
        Order newOrder = new Order(customerID, recipe);
        activeOrders.Add(newOrder);

        if (activeOrders.Count == 1)
        {
            focusedOrderIndex = 0;
        }
    }

    public void SwitchFocus()
    {
        if (activeOrders.Count == 0)
            return;

        focusedOrderIndex = (focusedOrderIndex + 1) % activeOrders.Count;
        Debug.Log($"Focused order is now: {activeOrders[focusedOrderIndex].customerID}");
    }

    public void AttemptStep(string attemptedStep)
    {
        if (activeOrders.Count == 0)
            return;

        Order current = activeOrders[focusedOrderIndex];
        bool success = current.TryStep(attemptedStep);

        if (success)
        {
            Debug.Log($"Step '{attemptedStep}' completed for order from {current.customerID}.");
            if (current.isCompleted)
            {
                Debug.Log($"{current.customerID}'s order complete!");
                orderCompleted = true;
                activeOrders.RemoveAt(focusedOrderIndex);

                if (focusedOrderIndex >= activeOrders.Count)
                    focusedOrderIndex = Mathf.Max(0, activeOrders.Count - 1);
            }
        }
        else
        {
            Debug.Log($"Step '{attemptedStep}' failed for order from {current.customerID}. Resetting order.");
            current.Reset();
        }
    }
    
        public Order GetFocusedOrder()
    {
        if (activeOrders.Count == 0) return null;
        return activeOrders[focusedOrderIndex];
    }
}
