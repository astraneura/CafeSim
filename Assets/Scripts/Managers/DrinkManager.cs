using System.Collections.Generic;
using UnityEngine;

public class DrinkManager : MonoBehaviour
{
    public static DrinkManager Instance;
    public static OrderManager orderManager;

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



    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        orderManager = FindAnyObjectByType<OrderManager>();
    }

    public int CalculateEmotionalValue(Ingredient ingredient)
    {
        if (ingredient == null)
            return 0;

        if (ingredient.emotionalQuality == "Energized" || ingredient.emotionalQuality == "Calming")
        {
            energizedCalmingBalance += ingredient.emotionalQualityValue;
            Debug.Log("Calculated Energized/Calming Emotional Value: " + energizedCalmingBalance);
            return energizedCalmingBalance;
        }
        else if (ingredient.emotionalQuality == "Light" || ingredient.emotionalQuality == "Heavy")
        {
            lightHeavyBalance += ingredient.emotionalQualityValue;
            Debug.Log("Calculated Light/Heavy Emotional Value: " + lightHeavyBalance);
            return lightHeavyBalance;
        }
        else if (ingredient.emotionalQuality == "Fresh" || ingredient.emotionalQuality == "Nostalgic")
        {
            freshNostalgicBalance += ingredient.emotionalQualityValue;
            Debug.Log("Calculated Fresh/Nostalgic Emotional Value: " + freshNostalgicBalance);
            return freshNostalgicBalance;
        }
        else if (ingredient.emotionalQuality == "Uplifting" || ingredient.emotionalQuality == "Depressing")
        {
            upliftingDepressingBalance += ingredient.emotionalQualityValue;
            Debug.Log("Calculated Uplifting/Depressing Emotional Value: " + upliftingDepressingBalance);
            return upliftingDepressingBalance;
        }
        else if (ingredient.emotionalQuality == "Warm" || ingredient.emotionalQuality == "Cold")
        {
            warmColdBalance += ingredient.emotionalQualityValue;
            Debug.Log("Calculated Warm/Cold Emotional Value: " + warmColdBalance);
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
            return creamyThinBalance;
        }
        else
        if (ingredient.physicalQuality == "Sweet" || ingredient.physicalQuality == "Bitter")
        {
            sweetBitterBalance += ingredient.physicalQualityValue;
            Debug.Log("Calculated Sweet/Bitter Physical Value: " + sweetBitterBalance);
            return sweetBitterBalance;
        }
        else
        if (ingredient.physicalQuality == "Spicy" || ingredient.physicalQuality == "Bland")
        {
            spicyBlandBalance += ingredient.physicalQualityValue;
            Debug.Log("Calculated Spicy/Bland Physical Value: " + spicyBlandBalance);
            return spicyBlandBalance;
        }
        else
        if (ingredient.physicalQuality == "Blessed" || ingredient.physicalQuality == "Cursed")
        {
            blessedCursedBalance += ingredient.physicalQualityValue;
            Debug.Log("Calculated Blessed/Cursed Physical Value: " + blessedCursedBalance);
            return blessedCursedBalance;
        }
        return 0;
    }
}