using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class QualityBalanceUI : MonoBehaviour
{
    //UI Elements
    public TextMeshProUGUI energizedCalmingText;
    public TextMeshProUGUI freshNostalgicText;
    public TextMeshProUGUI upliftingDepressingText;
    public TextMeshProUGUI sweetBitterText;
    public TextMeshProUGUI spicyBlandText;
    public TextMeshProUGUI blessedCursedText;

    // reference to DrinkManager
    private DrinkManager drinkManager;
    void Start()
    {
        drinkManager = DrinkManager.Instance;
    }

    public void UpdateUI()
    {
        energizedCalmingText.text = drinkManager.energizedCalmingBalance.ToString();
        freshNostalgicText.text = drinkManager.freshNostalgicBalance.ToString();
        upliftingDepressingText.text = drinkManager.upliftingDepressingBalance.ToString();
        sweetBitterText.text = drinkManager.sweetBitterBalance.ToString();
        spicyBlandText.text = drinkManager.spicyBlandBalance.ToString();
        blessedCursedText.text = drinkManager.blessedCursedBalance.ToString();
    }
}
