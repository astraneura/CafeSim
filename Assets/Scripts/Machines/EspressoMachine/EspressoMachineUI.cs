using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EspressoMachineUI : MonoBehaviour
{
   [SerializeField] private EspressoMachine espressoMachine;
   [SerializeField] private SpecialEspresso specialEspresso;
    public TextMeshProUGUI machineNameTextUI;
    void Start()
    {
        machineNameTextUI = GetComponentInChildren<TextMeshProUGUI>();

        if (espressoMachine != null || specialEspresso != null)
        {
            machineNameTextUI.text = "Espresso Machine"; //set name directly
        }
        else
        {
            Debug.LogError("EspressoMachineUI: Espresso machine component not found in parent.");
        }
    }
}
