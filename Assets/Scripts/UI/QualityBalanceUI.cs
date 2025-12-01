using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class QualityBalanceUI : MonoBehaviour
{
    //UI Elements
    public TextMeshProUGUI energizedCalmingText;
    public TextMeshProUGUI lightHeavyText;
    public TextMeshProUGUI freshNostalgicText;
    public TextMeshProUGUI upliftingDepressingText;
    public TextMeshProUGUI warmColdText;
    public TextMeshProUGUI creamyThinText;
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
        lightHeavyText.text = drinkManager.lightHeavyBalance.ToString();
        freshNostalgicText.text = drinkManager.freshNostalgicBalance.ToString();
        upliftingDepressingText.text = drinkManager.upliftingDepressingBalance.ToString();
        warmColdText.text = drinkManager.warmColdBalance.ToString();
        creamyThinText.text = drinkManager.creamyThinBalance.ToString();
        sweetBitterText.text = drinkManager.sweetBitterBalance.ToString();
        spicyBlandText.text = drinkManager.spicyBlandBalance.ToString();
        blessedCursedText.text = drinkManager.blessedCursedBalance.ToString();
    }
}
