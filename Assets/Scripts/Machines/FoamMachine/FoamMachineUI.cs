using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FoamMachineUI : MonoBehaviour
{
    private FoamMachine foamMachine;
    public TextMeshProUGUI machineNameTextUI;
    void Start()
    {
        machineNameTextUI = GetComponentInChildren<TextMeshProUGUI>();

        foamMachine = GetComponentInParent<FoamMachine>();
        if (foamMachine != null)
        {
            machineNameTextUI.text = "Foam Machine"; //set name directly
        }
        else
        {
            Debug.LogError("FoamMachineUI: Foam machine component not found in parent.");
        }
    }
}
