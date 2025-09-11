using UnityEngine;
using TMPro;

public class WaterMachineUI : MonoBehaviour
{
    private WaterMachine waterMachine;
    public TextMeshProUGUI machineNameTextUI;
    void Start()
    {
        machineNameTextUI = GetComponentInChildren<TextMeshProUGUI>();

        waterMachine = GetComponentInParent<WaterMachine>();
        if (waterMachine != null)
        {
            machineNameTextUI.text = "Water Machine"; //set name directly
        }
        else
        {
            Debug.LogError("WaterMachineUI: Water machine component not found in parent.");
        }
    }
}
