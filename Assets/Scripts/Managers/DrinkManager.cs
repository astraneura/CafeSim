using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DrinkManager : MonoBehaviour
{
    public static DrinkManager Instance;

    // Emotional Value Tracking
    public int energizedCalmingBalance;
    public int lightHeavyBalance;
    public int freshNostalgicBalance;
    public int upliftingDepressingBalance;
    public int warmColdBalance;

    // Physical Value Tracking
    public int creamyThinBalance;
    public int sweetBitterBalance;
    public int spicyBlandBalance;
    public int blessedCursedBalance;

    public QualityBalanceUI balanceUI;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public int CalculateEmotionalValue(Ingredient ingredient)
    {
        if (ingredient == null)
            return 0;

        if (ingredient.emotionalQuality == "Energized" || ingredient.emotionalQuality == "Calming")
        {
            energizedCalmingBalance += ingredient.emotionalQualityValue;
            Debug.Log("Calculated Energized/Calming Emotional Value: " + energizedCalmingBalance);
            balanceUI.UpdateUI();
            return energizedCalmingBalance;
        }
        else if (ingredient.emotionalQuality == "Light" || ingredient.emotionalQuality == "Heavy")
        {
            lightHeavyBalance += ingredient.emotionalQualityValue;
            Debug.Log("Calculated Light/Heavy Emotional Value: " + lightHeavyBalance);
            balanceUI.UpdateUI();
            return lightHeavyBalance;
        }
        else if (ingredient.emotionalQuality == "Fresh" || ingredient.emotionalQuality == "Nostalgic")
        {
            freshNostalgicBalance += ingredient.emotionalQualityValue;
            Debug.Log("Calculated Fresh/Nostalgic Emotional Value: " + freshNostalgicBalance);
            balanceUI.UpdateUI();
            return freshNostalgicBalance;
        }
        else if (ingredient.emotionalQuality == "Uplifting" || ingredient.emotionalQuality == "Depressing")
        {
            upliftingDepressingBalance += ingredient.emotionalQualityValue;
            Debug.Log("Calculated Uplifting/Depressing Emotional Value: " + upliftingDepressingBalance);
            balanceUI.UpdateUI();
            return upliftingDepressingBalance;
        }
        else if (ingredient.emotionalQuality == "Warm" || ingredient.emotionalQuality == "Cold")
        {
            warmColdBalance += ingredient.emotionalQualityValue;
            Debug.Log("Calculated Warm/Cold Emotional Value: " + warmColdBalance);
            balanceUI.UpdateUI();
            return warmColdBalance;
        }
        return 0;
    }

    public int CalculatePhysicalValue(Ingredient ingredient)
    {
        if (ingredient == null)
            return 0;

        if (ingredient.physicalQuality == "Creamy" || ingredient.physicalQuality == "Thin")
        {
            creamyThinBalance += ingredient.physicalQualityValue;
            Debug.Log("Calculated Creamy/Thin Physical Value: " + creamyThinBalance);
            balanceUI.UpdateUI();
            return creamyThinBalance;
        }
        else
        if (ingredient.physicalQuality == "Sweet" || ingredient.physicalQuality == "Bitter")
        {
            sweetBitterBalance += ingredient.physicalQualityValue;
            Debug.Log("Calculated Sweet/Bitter Physical Value: " + sweetBitterBalance);
            balanceUI.UpdateUI();
            return sweetBitterBalance;
        }
        else
        if (ingredient.physicalQuality == "Spicy" || ingredient.physicalQuality == "Bland")
        {
            spicyBlandBalance += ingredient.physicalQualityValue;
            Debug.Log("Calculated Spicy/Bland Physical Value: " + spicyBlandBalance);
            balanceUI.UpdateUI();
            return spicyBlandBalance;
        }
        else
        if (ingredient.physicalQuality == "Blessed" || ingredient.physicalQuality == "Cursed")
        {
            blessedCursedBalance += ingredient.physicalQualityValue;
            Debug.Log("Calculated Blessed/Cursed Physical Value: " + blessedCursedBalance);
            balanceUI.UpdateUI();
            return blessedCursedBalance;
        }
        return 0;
    }
    
    public void ResetDrinkValues()
    {
        energizedCalmingBalance = 0;
        lightHeavyBalance = 0;
        freshNostalgicBalance = 0;
        upliftingDepressingBalance = 0;
        warmColdBalance = 0;

        creamyThinBalance = 0;
        sweetBitterBalance = 0;
        spicyBlandBalance = 0;
        blessedCursedBalance = 0;
        balanceUI.UpdateUI();
    }


    //TEMP - REFACTOR LATER
    public int AddEnergizedToDrink()
    {
        energizedCalmingBalance -= 1;
        balanceUI.UpdateUI();
        return energizedCalmingBalance;
    }
    public int AddCalmingToDrink()
    {
        energizedCalmingBalance += 1;
        balanceUI.UpdateUI();
        return energizedCalmingBalance;
    }
    public int AddLightToDrink()
    {
        lightHeavyBalance += 1;
        balanceUI.UpdateUI();
        return lightHeavyBalance;
    }
    public int AddHeavyToDrink()
    {
        lightHeavyBalance -= 1;
        balanceUI.UpdateUI();
        return lightHeavyBalance;
    }
    public int AddFreshToDrink()
    {
        freshNostalgicBalance += 1;
        balanceUI.UpdateUI();
        return freshNostalgicBalance;
    }
    public int AddNostalgicToDrink()
    {
        freshNostalgicBalance -= 1;
        balanceUI.UpdateUI();
        return freshNostalgicBalance;
    }
    public int AddUpliftingToDrink()
    {
        upliftingDepressingBalance += 1;
        balanceUI.UpdateUI();
        return upliftingDepressingBalance;
    }
    public int AddDepressingToDrink()
    {
        upliftingDepressingBalance -= 1;
        balanceUI.UpdateUI();
        return upliftingDepressingBalance;
    }
    public int AddWarmToDrink()
    {
        warmColdBalance += 1;
        balanceUI.UpdateUI();
        return warmColdBalance;
    }
    public int AddColdToDrink()
    {
        warmColdBalance -= 1;
        balanceUI.UpdateUI();
        return warmColdBalance;
    }
    public int AddCreamyToDrink()
    {
        creamyThinBalance += 1;
        balanceUI.UpdateUI();
        return creamyThinBalance;
    }
    public int AddThinToDrink()
    {
        creamyThinBalance -= 1;
        balanceUI.UpdateUI();
        return creamyThinBalance;
    }
    public int AddSweetToDrink()
    {
        sweetBitterBalance += 1;
        balanceUI.UpdateUI();
        return sweetBitterBalance;
    }
    public int AddBitterToDrink()
    {
        sweetBitterBalance -= 1;
        balanceUI.UpdateUI();
        return sweetBitterBalance;
    }
    public int AddSpicyToDrink()
    {
        spicyBlandBalance += 1;
        balanceUI.UpdateUI();
        return spicyBlandBalance;
    }
    public int AddBlandToDrink()
    {
        spicyBlandBalance -= 1;
        balanceUI.UpdateUI();
        return spicyBlandBalance;
    }
    public int AddBlessedToDrink()
    {
        blessedCursedBalance += 1;
        balanceUI.UpdateUI();
        return blessedCursedBalance;
    }
    public int AddCursedToDrink()
    {
        blessedCursedBalance -= 1;
        balanceUI.UpdateUI();
        return blessedCursedBalance;
    }

    //temp - expose balance values for ConfusedCustomer to access
    public int GetEmotionalBalanceForQuality(string emotionalQuality)
{
    switch (emotionalQuality)
    {
        case "Energizing":
        case "Calming":
            return energizedCalmingBalance;
        case "Light":
        case "Heavy":
            return lightHeavyBalance;
        case "Fresh":
        case "Nostalgic":
            return freshNostalgicBalance;
        case "Uplifting":
        case "Depressing":
            return upliftingDepressingBalance;
        case "Warm":
        case "Cold":
            return warmColdBalance;
        default:
            return 0;
    }
}

public int GetPhysicalBalanceForQuality(string physicalQuality)
{
    switch (physicalQuality)
    {
        case "Creamy":
        case "Thin":
            return creamyThinBalance;
        case "Sweet":
        case "Bitter":
            return sweetBitterBalance;
        case "Spicy":
        case "Bland":
            return spicyBlandBalance;
        case "Blessed":
        case "Cursed":
            return blessedCursedBalance;
        default:
            return 0;
    }
}
}