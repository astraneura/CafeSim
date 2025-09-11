using System.Collections.Generic;
using UnityEngine;


// This scriptable object holds a list of customer names that can be used in the game.
// It can be created and edited in the Unity editor, allowing for adding or modifying names without changing the code.
[CreateAssetMenu(fileName = "CustomerNameDatabase", menuName = "CafeGame/CustomerNameDatabase")]
public class CustomerNameDatabase : ScriptableObject
{
    public List<string> names = new List<string>();
}
