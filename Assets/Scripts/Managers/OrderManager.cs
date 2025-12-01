using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OrderManager : MonoBehaviour
{
    // Singleton instance
    public static OrderManager Instance;

    private PlayerInteraction pInteract;
    // variables to track the current order and its steps
    public Order currentOrder;
    public List<OrderStep> currentOrderSteps = new List<OrderStep>();
    private int currentStepIndex = 0;
    public bool orderCompleted;
    public int totalOrdersCompleted = 0;
    public int totalOrdersFailed = 0;
    public float totalMoneyMade = 0;

    public GameObject dataController;

    // variables for the UI
    [SerializeField] private TextMeshProUGUI orderStepText;
    [SerializeField] private Transform orderStepsContainer;

    public ICustomer currentCustomer { get; private set; }
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        pInteract = FindAnyObjectByType<PlayerInteraction>();
        totalOrdersCompleted = 0;
        totalOrdersFailed = 0;
        DontDestroyOnLoad(this.gameObject);
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
            var stepTextObj = Instantiate(orderStepText, orderStepsContainer);
            stepTextObj.text = step;
            stepTextObj.fontSize = 24;
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
                Transform completedStepTransform = orderStepsContainer.GetChild(currentStepIndex);
                completedStepTransform.GetComponent<TextMeshProUGUI>().color = Color.green;
                orderCompleted = true;
                totalOrdersCompleted++;
                dataController.GetComponent<UserProfileData>().ordersCompleted = totalOrdersCompleted;
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
                // Change color of the completed step's UI element
                if (currentStepIndex < orderStepsContainer.childCount)
                {
                    Transform completedStepTransform = orderStepsContainer.GetChild(currentStepIndex);
                    completedStepTransform.GetComponent<TextMeshProUGUI>().color = Color.green;
                }
                currentStepIndex++;
                Debug.Log($"Step '{attemptedStep}' completed for order from {currentOrder.customerID}.");
                return true; // Step completed successfully
            }
        }
        Debug.Log($"Step '{attemptedStep}' failed for order from {currentOrder.customerID}. Resetting order.");
        currentOrder.Reset();
        currentStepIndex = 0;
        for (int i = 0; i < currentOrderSteps.Count; i++)
        {
            currentOrderSteps[i].isCompleted = false;
        }
        foreach (Transform child in orderStepsContainer)
        {
            child.GetComponent<TextMeshProUGUI>().color = Color.white;
        }
        return false;
    }

    // clear all the order states
    public void ClearCurrentOrder()
    {
        currentOrder = null;
        currentOrderSteps.Clear();
        currentStepIndex = 0;
        orderCompleted = false;
        currentCustomer = null;
        DrinkManager.Instance.ResetDrinkValues();
        pInteract.canGenerateOrder = true; // Allow generating a new order
        foreach (Transform child in orderStepsContainer)
        {
            Destroy(child.gameObject);
        }
    }

    public void SetConfusedCustomer(ICustomer customer)
    {
        currentCustomer = customer;
        currentOrder = null;
        currentOrderSteps.Clear();
        orderCompleted = false;
    }
}
