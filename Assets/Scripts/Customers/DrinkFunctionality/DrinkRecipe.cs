using System.Collections.Generic;
using UnityEngine;

// this script creates a reusable drink recipe object to create objects to add to the drink recipe database

[CreateAssetMenu(fileName = "Drink Recipe", menuName = "CafeGame/DrinkRecipe")]
public class DrinkRecipe : ScriptableObject
{
    public string drinkName;
    public List<string> steps = new List<string>();
    public float cost;
}