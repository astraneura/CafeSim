using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

// This script handles the display of the customer's name in a UI text box

public class NameTextBox : MonoBehaviour
{
    private Customer customer;
    private string customerNameText;
    public TextMeshProUGUI nameTextBox;
    private Transform trans;
    private Vector3 offset = new Vector3(0, 180, 0);
    void Start()
    {
        nameTextBox = GetComponentInChildren<TextMeshProUGUI>();
        trans = GameObject.Find("Camera").GetComponent<Transform>();

        customer = GetComponentInParent<Customer>();
        if (customer != null)
        {
            customerNameText = customer.customerName;
            Debug.Log($"Customer Name: {customerNameText}");
            nameTextBox.text = customerNameText;
        }
        else
        {
            Debug.LogError("Customer component not found in parent.");
        }
    }

    void Update()
    {
        transform.LookAt(trans);
        transform.Rotate(offset);
    }
}