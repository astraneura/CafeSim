using System.Collections.Generic;
using UnityEngine;

// This script defines a database for drink recipes

[CreateAssetMenu(fileName = "DrinkRecipeDatabase", menuName = "CafeGame/DrinkRecipeDatabase")]
public class DrinkRecipeDatabase : ScriptableObject
{
    public List<DrinkRecipe> allRecipes;
}
