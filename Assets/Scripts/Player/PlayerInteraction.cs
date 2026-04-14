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

    //interaction UI
    [SerializeField] private GameObject talkIcon;
    [SerializeField] private GameObject interactIcon;
    [SerializeField] private float interactDistance = 3f;

    [SerializeField] private TextMeshProUGUI moneyText;
    private ICustomer currentCustomer;
    public bool isMenuOpen = false;

    private void Start()
    {
        DrinkManager.Instance = FindAnyObjectByType<DrinkManager>();
    }

    private void Update()
    {
        updateInteractionUI();
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
            ICustomer customer = null;
            // Check if the hit object has the "Customer" tag
            if (hit.collider.CompareTag("Customer"))
            {
                // Loop through all MonoBehaviours on the hit object to find one that implements ICustomer
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
                    Debug.LogError("Hit object tagged 'Customer' but no ICustomer found."); // This should never happen if the game is set up correctly, but it's good to check.
                    return;
                }

                if (!(customer is ConfusedCustomer) && !canGenerateOrder && !OrderManager.Instance.orderCompleted)
                {
                    Debug.Log("Cannot generate a new order until the current one is completed.");
                    return;
                }

                if (customer is ConfusedCustomer confusedCustomer)
                {
                    if (OrderManager.Instance.currentCustomer == customer &&
                        confusedCustomer.IsOrderCompleted())
                    {
                        customer.CompleteOrder();
                        Debug.Log("ConfusedCustomer order completed.");
                        GameManager.Instance.OnCustomerOrderCompleted();
                        canGenerateOrder = true; // Allow generating a new order
                        currentCustomer = null;
                    }
                    else if (OrderManager.Instance.currentCustomer != customer)
                    {
                        currentCustomer = customer;
                        if (customer.GenerateOrder())
                        {
                            currentCustomer.Speak();
                            canGenerateOrder = false; // limit to one active order at a time
                        }
                    }
                    else
                    {
                        Debug.Log("Cannot interact with this customer right now.");
                    }
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
            }
            else
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
        DialogueManager.GetInstance().ContinueStory();
    }

    public void AddMoney(float amount)
    {
        moneyMade += amount;
        OrderManager.Instance.totalMoneyMade = moneyMade;
        if (OrderManager.Instance.dataController != null)
        {
            var data = OrderManager.Instance.dataController.GetComponent<UserProfileData>();
            if (data != null)
            {
                data.moneyMade = moneyMade;
            }
        }
        if (moneyText != null)
        {
            moneyText.text = "Money: $" + moneyMade.ToString("F2");
        }
        Debug.Log("Total Money Made: " + moneyMade);
    }

    private void updateInteractionUI()
    {
        talkIcon.SetActive(false);
        interactIcon.SetActive(false);
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;

        if (!Physics.Raycast(ray, out hit, interactDistance))
            return;

        if (hit.collider.CompareTag("Customer"))
        {
            if (isMenuOpen)
                return;

            ICustomer customer = hit.collider.GetComponent<ICustomer>();
            if (customer == null)
                return;

            if (!canGenerateOrder && OrderManager.Instance.currentCustomer != customer)
                return;

            if (OrderManager.Instance.orderCompleted
                && OrderManager.Instance.currentCustomer != customer)
                return;

            talkIcon.SetActive(true);
            return;
        }

        if (hit.collider.GetComponent<IOrderStepSourceInterface>() != null ||
           hit.collider.CompareTag("ToppingsBox") ||
           hit.collider.CompareTag("Trash") && !isMenuOpen)
        {
            if (OrderManager.Instance.currentCustomer != null && !isMenuOpen)
                interactIcon.SetActive(true);
        }
    }

}
