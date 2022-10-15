using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(TextDisplayAttribute))]
public class DrawTextDisplay : PropertyDrawer
{
    private const float Padding = 20f;
    private float height = 0f;
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        TextDisplayAttribute textDisplay = attribute as TextDisplayAttribute;
        GUIStyle style = EditorStyles.helpBox;
        style.alignment = TextAnchor.MiddleLeft;
        style.wordWrap = true;
        style.padding = new RectOffset(10, 10, 10, 10);
        style.fontSize = 12;

        height = style.CalcHeight(new GUIContent(textDisplay.Text), Screen.width);
        
        return height + Padding;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        TextDisplayAttribute textDisplay = attribute as TextDisplayAttribute;

        position.height = height;
        position.y += Padding * 0.5f;
        
        EditorGUI.HelpBox(position, textDisplay.Text, MessageType.Info);
    }
}
