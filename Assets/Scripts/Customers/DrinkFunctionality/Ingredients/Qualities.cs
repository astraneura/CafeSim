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
        "Fresh", "Nostalgic",
        "Uplifting", "Depressing",
    };
    //Physical Qualities
    public List<string> physicalQualities = new List<string>
    {
        "Sweet", "Bitter",
        "Spicy", "Bland",
        "Blessed", "Cursed"
    };

    public List<string> negativeEmotionalQualities = new List<string>
    {
        "Energized",
        "Nostalgic",
        "Depressing",
    };

    public List<string> negativePhysicalQualities = new List<string>
    {
        "Bitter",
        "Bland",
        "Cursed"
    };

    public List<string> positiveEmotionalQualities = new List<string>
    {
        "Calming",
        "Fresh",
        "Uplifting",
    };

    public List<string> positivePhysicalQualities = new List<string>
    {
        "Sweet",
        "Spicy",
        "Blessed"
    };
}
