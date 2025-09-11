using System.Collections.Generic;
using UnityEngine;

// this script creates a reusable drink recipe object to create objects to add to the drink recipe database

[CreateAssetMenu(fileName = "Ingredient", menuName = "CafeGame/Ingredients")]
public class Ingredient : ScriptableObject
{
    public string ingredientName;
    public string emotionalQuality;
    public int emotionalQualityValue;

    public string physicalQuality;
    public int physicalQualityValue;
}