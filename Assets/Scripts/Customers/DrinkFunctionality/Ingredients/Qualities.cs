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

    public List<string> negativeEmotionalQualities = new List<string>
    {
        "Energized",
        "Heavy",
        "Nostalgic",
        "Depressing",
        "Cold"
    };

    public List<string> negativePhysicalQualities = new List<string>
    {
        "Thin",
        "Bitter",
        "Bland",
        "Cursed"
    };

    public List<string> positiveEmotionalQualities = new List<string>
    {
        "Calming",
        "Light",
        "Fresh",
        "Uplifting",
        "Warm"
    };

    public List<string> positivePhysicalQualities = new List<string>
    {
        "Creamy",
        "Sweet",
        "Spicy",
        "Blessed"
    };
}
