using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CoffeeMachineUI : MonoBehaviour
{
    private CoffeeMachine coffeeMachine;
    public TextMeshProUGUI machineNameTextUI;
    void Start()
    {
        machineNameTextUI = GetComponentInChildren<TextMeshProUGUI>();

        coffeeMachine = GetComponentInParent<CoffeeMachine>();
        if (coffeeMachine != null)
        {
            machineNameTextUI.text = "Coffee Machine"; //set name directly
        }
        else
        {
            Debug.LogError("CoffeeMachineUI: Coffee machine component not found in parent.");
        }
    }
}
