using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

// This script handles the display of the customer's name in a UI text box

public class NameTextBox : MonoBehaviour
{
    private ICustomer customer;
    public TextMeshProUGUI nameTextBox;
    private Transform cameraTransform;
    private Vector3 offset = new Vector3(0, 180, 0);
    void Start()
    {
        nameTextBox = GetComponentInChildren<TextMeshProUGUI>();
        cameraTransform = Camera.main.transform;

        foreach (var mb in GetComponents<MonoBehaviour>())
        {
            if (mb is ICustomer)
            {
                customer = (ICustomer)mb;
                break;
            }
        }

        if (customer != null)
        {
            nameTextBox.text = customer.CustomerName;
            Debug.Log("Customer name: " + customer.CustomerName);
        }
        else
        {
            nameTextBox.text = "No Name";
            Debug.LogWarning("No ICustomer implementation found on the GameObject.");
        }
    }

    void LateUpdate()
    {
        transform.LookAt(cameraTransform);
        transform.Rotate(offset);
    }
}