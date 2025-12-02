using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.Rendering;

public class PlayerInteraction : MonoBehaviour
{
    public InputActionReference interactAction;
    public InputActionReference continueAction;

    public bool canGenerateOrder = true;

    private float moneyMade = 0f;

    [SerializeField] private TextMeshProUGUI moneyText;
    private ICustomer currentCustomer;
    private void Start()
    {
        DrinkManager.Instance = FindAnyObjectByType<DrinkManager>();
    }
    private void OnEnable()
    {
        interactAction.action.performed += OnInteract;
        interactAction.action.Enable();
        continueAction.action.performed += OnContinue;
        continueAction.action.Enable();
    }

    private void OnDisable()
    {
        interactAction.action.performed -= OnInteract;
        interactAction.action.Disable();
        continueAction.action.performed -= OnContinue;
        continueAction.action.Disable();
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
                    if (OrderManager.Instance.currentCustomer == customer)
                    {
                        customer.CompleteOrder();
                        Debug.Log("Customer order completed.");
                        OrderManager.Instance.orderCompleted = false;
                        GameManager.Instance.OnCustomerOrderCompleted();
                        canGenerateOrder = true; // Allow generating a new order
                        currentCustomer = null;
                    }
                    else
                    {
                        Debug.Log("This is not the current customer to complete an order for.");
                    }
                }
                else
                if (customer != null)
                {
                    currentCustomer = customer;
                    if (customer.GenerateOrder())
                    {
                        currentCustomer.Speak();
                        canGenerateOrder = false; // limit to one active order at a time
                    }
                    else
                    {
                        Debug.Log("Failed to generate order for customer.");
                    }
                }
            }
            else if (hit.collider.CompareTag("ToppingsBox"))
            {
                hit.collider.GetComponent<ToppingsBox>().OpenToppingMenu();
            } else 
            if (hit.collider.CompareTag("Trash"))
            {
                DrinkManager.Instance.ResetDrinkValues();
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
                    // Ingredient machineIngredient = machine.GetIngredient();
                    // if (machineIngredient != null)
                    // {
                    //     DrinkManager.Instance.CalculateEmotionalValue(machineIngredient);
                    //     DrinkManager.Instance.CalculatePhysicalValue(machineIngredient);
                    // }
                }
            }
        }
    }

    private void OnContinue(InputAction.CallbackContext context)
    {
        // This can be used for dialogue continuation if needed
        //use for closing the dialogue box after speaking for now
        if (currentCustomer == null)
            return;
        if (ToppingsBox.ToppingsMenuOpen)
            return;
        currentCustomer.CloseDialogue();

    }

    public void AddMoney(float amount)
    {
        moneyMade += amount;
        OrderManager.Instance.totalMoneyMade = moneyMade;
        OrderManager.Instance.dataController.GetComponent<UserProfileData>().moneyMade = moneyMade;
        if (moneyText != null)
        {
            moneyText.text = "Money: $" + moneyMade.ToString("F2");
        }
        Debug.Log("Total Money Made: " + moneyMade);
    }
}
