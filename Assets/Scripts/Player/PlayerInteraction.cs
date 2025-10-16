using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private InputActionReference interactAction;

    public bool canGenerateOrder = true;

    private float moneyMade = 0f;

    [SerializeField] private TextMeshProUGUI moneyText;

    private void Start()
    {
        DrinkManager.Instance = FindAnyObjectByType<DrinkManager>();
    }
    private void OnEnable()
    {
        interactAction.action.performed += OnInteract;
        interactAction.action.Enable();
    }

    private void OnDisable()
    {
        interactAction.action.performed -= OnInteract;
        interactAction.action.Disable();
    }

    private void OnInteract(InputAction.CallbackContext context)
    {

        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            // Check if the hit object has the "Customer" tag
            if (hit.collider.CompareTag("Customer"))
            {
                if (!canGenerateOrder && !OrderManager.Instance.orderCompleted)
                {
                    Debug.Log("Cannot generate a new order until the current one is completed.");
                    return;
                }
                // Loop through all MonoBehaviours on the hit object to find one that implements ICustomer
                ICustomer customer = null;
                foreach (var mb in hit.collider.GetComponents<MonoBehaviour>())
                {
                    if (mb is ICustomer)
                    {
                        customer = (ICustomer)mb;
                        break;
                    }
                }

                if (customer == null)
                {
                    Debug.LogError("Hit object tagged 'Customer' but no ICustomer found.");
                    return;
                }

                if (OrderManager.Instance.orderCompleted && customer != null)
                {
                    customer.CompleteOrder();
                    Debug.Log("Customer order completed.");
                    OrderManager.Instance.orderCompleted = false;
                    canGenerateOrder = true; // Allow generating a new order
                }
                else
                if (customer != null)
                {
                    customer.GenerateOrder();
                    canGenerateOrder = false; // limit to one active order at a time

                }
            }
            else
            {
                // Handle other interactions
                IOrderStepSourceInterface machine = hit.collider.GetComponent<IOrderStepSourceInterface>();
                if (machine != null)
                {
                    ICustomer activeCustomer = OrderManager.Instance.currentCustomer;
                    if (activeCustomer == null)
                    {
                        Debug.Log("No active customer to serve.");
                        return;
                    }
                    machine.Interact(activeCustomer);
                    Ingredient machineIngredient = machine.GetIngredient();
                    if (machineIngredient != null)
                    {
                        DrinkManager.Instance.CalculateEmotionalValue(machineIngredient);
                        DrinkManager.Instance.CalculatePhysicalValue(machineIngredient);
                    }
                }
            }
        }
    }

    public void AddMoney(float amount)
    {
        moneyMade += amount;
        OrderManager.Instance.totalMoneyMade = moneyMade;
        if (moneyText != null)
        {
            moneyText.text = "Money: $" + moneyMade.ToString("F2");
        }
        Debug.Log("Total Money Made: " + moneyMade);
    }
}
