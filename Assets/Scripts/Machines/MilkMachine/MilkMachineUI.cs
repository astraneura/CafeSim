using UnityEngine;
using TMPro;

public class MilkMachineUI : MonoBehaviour
{
    private MilkMachine milkMachine;
    public TextMeshProUGUI machineNameTextUI;
    void Start()
    {
        machineNameTextUI = GetComponentInChildren<TextMeshProUGUI>();

        milkMachine = GetComponentInParent<MilkMachine>();
        if (milkMachine != null)
        {
            machineNameTextUI.text = "Milk Machine"; //set name directly
        }
        else
        {
            Debug.LogError("MilkMachineUI: Milk machine component not found in parent.");
        }
    }
}
