using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EspressoMachineUI : MonoBehaviour
{
    private EspressoMachine espressoMachine;
    public TextMeshProUGUI machineNameTextUI;
    void Start()
    {
        machineNameTextUI = GetComponentInChildren<TextMeshProUGUI>();

        espressoMachine = GetComponentInParent<EspressoMachine>();
        if (espressoMachine != null)
        {
            machineNameTextUI.text = "Espresso Machine"; //set name directly
        }
        else
        {
            Debug.LogError("EspressoMachineUI: Espresso machine component not found in parent.");
        }
    }
}
