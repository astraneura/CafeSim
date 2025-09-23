using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private InputActionReference interactAction;
    [SerializeField] private InputActionReference switchAction;
    
    private bool canGenerateOrder = true;


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
                // Try to get the Customer component
                Customer customer = hit.collider.GetComponent<Customer>();
                if (OrderManager.Instance.orderCompleted && customer != null)
                {
                    Destroy(customer.gameObject);
                    Debug.Log("Customer order completed and customer destroyed.");
                    OrderManager.Instance.orderCompleted = false;
                    canGenerateOrder = true; // Allow generating a new order
                }
                else
                if (customer != null && customer.currentOrderNum < customer.maxOrderNum && canGenerateOrder)
                {
                    customer.currentOrderNum++;
                    customer.GenerateOrder();
                    Debug.Log("Generated order for customer.");
                    canGenerateOrder = false; // limit to one active order at a time

                }
            }
            else
            {
                // Handle other interactions
                IOrderStepSourceInterface stepSource = hit.collider.GetComponent<IOrderStepSourceInterface>();
                if (stepSource != null)
                {
                    string stepName = stepSource.GetOrderStepName();
                    Debug.Log($"Attempting step: {stepName}");
                    OrderManager.Instance.AttemptStep(stepName);
                }
            }
        }
    }
}
