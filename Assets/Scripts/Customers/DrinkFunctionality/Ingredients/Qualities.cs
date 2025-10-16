using System.Collections.Generic;
using UnityEngine;

public class Qualities
{
    //This script holds string values for all qualities
    //This will support the ordering for special customers to generate a custom order
    //as opposed to a predefined recipe
    //Emotional Qualities

    public List<string> emotionalQualities = new List<string>
    {
        "Energized", "Calming",
        "Light", "Heavy",
        "Fresh", "Nostalgic",
        "Uplifting", "Depressing",
        "Warm", "Cold"
    };
    //Physical Qualities
    public List<string> physicalQualities = new List<string>
    {
        "Creamy", "Thin",
        "Sweet", "Bitter",
        "Spicy", "Bland",
        "Blessed", "Cursed"
    };
}
