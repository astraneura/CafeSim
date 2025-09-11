using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private InputActionReference interactAction;
    [SerializeField] private InputActionReference switchAction;

    


    private void OnEnable()
    {
        interactAction.action.performed += OnInteract;
        switchAction.action.performed += OnSwitch;
        interactAction.action.Enable();
        switchAction.action.Enable();
    }

    private void OnDisable()
    {
        interactAction.action.performed -= OnInteract;
        switchAction.action.performed -= OnSwitch;
        interactAction.action.Disable();
        switchAction.action.Disable();
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
                // Try to get the Customer component
                Customer customer = hit.collider.GetComponent<Customer>();
                if (OrderManager.Instance.orderCompleted && customer != null)
                {
                    Destroy(customer.gameObject);
                    Debug.Log("Customer order completed and customer destroyed.");
                    OrderManager.Instance.orderCompleted = false;
                }
                else
                if (customer != null && customer.currentOrderNum < customer.maxOrderNum)
                {
                    customer.currentOrderNum++;
                    customer.GenerateOrder();
                    Debug.Log("Generated order for customer.");
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
    private void OnSwitch(InputAction.CallbackContext context)
    {
        OrderManager.Instance.SwitchFocus();
    }
}
