using UnityEngine;
using TMPro;

public class WaterMachineUI : MonoBehaviour
{
    [SerializeField] private WaterMachine waterMachine;
    [SerializeField] private SpecialWater specialWater;
    public TextMeshProUGUI machineNameTextUI;
    void Start()
    {
        machineNameTextUI = GetComponentInChildren<TextMeshProUGUI>();

        if (waterMachine != null || specialWater != null)
        {
            machineNameTextUI.text = "Water Machine"; //set name directly
        }
        else
        {
            Debug.LogError("WaterMachineUI: Water machine component not found in parent.");
        }
    }
}
