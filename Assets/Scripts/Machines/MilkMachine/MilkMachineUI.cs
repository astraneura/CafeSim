using UnityEngine;
using TMPro;

public class MilkMachineUI : MonoBehaviour
{
    [SerializeField] private MilkMachine milkMachine;
    [SerializeField] private SpecialMilk specialMilk;
    public TextMeshProUGUI machineNameTextUI;
    void Start()
    {
        machineNameTextUI = GetComponentInChildren<TextMeshProUGUI>();

        if (milkMachine != null || specialMilk != null)
        {
            machineNameTextUI.text = "Milk Machine"; //set name directly
        }
        else
        {
            Debug.LogError("MilkMachineUI: Milk machine component not found in parent.");
        }
    }
}
