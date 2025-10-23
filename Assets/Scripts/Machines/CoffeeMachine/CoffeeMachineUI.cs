using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CoffeeMachineUI : MonoBehaviour
{
    [SerializeField]private CoffeeMachine coffeeMachine;
    [SerializeField] private SpecialCoffee specialCoffee;
    public TextMeshProUGUI machineNameTextUI;
    void Start()
    {
        machineNameTextUI = GetComponentInChildren<TextMeshProUGUI>();

        if (coffeeMachine || specialCoffee != null)
        {
            machineNameTextUI.text = "Coffee Machine"; //set name directly
        }
        else
            Debug.LogError("CoffeeMachineUI: Coffee machine component not found in parent.");
    }
}
