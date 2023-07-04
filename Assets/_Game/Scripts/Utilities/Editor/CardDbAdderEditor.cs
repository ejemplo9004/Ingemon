using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CardDbAdder))]
public class CardDbAdderEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        CardDbAdder cardAdder = (CardDbAdder)target;
        if (GUILayout.Button("Add Card to DataBase"))
        {
            cardAdder.AddCardsToDB();
        }
    }
}
