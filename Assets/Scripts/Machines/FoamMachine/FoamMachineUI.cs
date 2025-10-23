using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FoamMachineUI : MonoBehaviour
{
    [SerializeField] private FoamMachine foamMachine;
    [SerializeField] private SpecialFoam specialFoam;
    public TextMeshProUGUI machineNameTextUI;
    void Start()
    {
        machineNameTextUI = GetComponentInChildren<TextMeshProUGUI>();

        if (foamMachine != null || specialFoam != null)
        {
            machineNameTextUI.text = "Foam Machine"; //set name directly
        }
        else
        {
            Debug.LogError("FoamMachineUI: Foam machine component not found in parent.");
        }
    }
}
