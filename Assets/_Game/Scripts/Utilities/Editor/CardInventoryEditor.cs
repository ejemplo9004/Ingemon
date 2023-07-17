using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CardInventory))]
public class CardInventoryEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var script = (CardInventory)target;

        if(GUILayout.Button("Generate Card Dictionary", GUILayout.Height(40)))
        {
            script.GenerateCardDictionary();
        }
        
        if(GUILayout.Button("Clear Card Dictionary", GUILayout.Height(40)))
        {
            script.ClearCardDictionary();
        }
    }
}
