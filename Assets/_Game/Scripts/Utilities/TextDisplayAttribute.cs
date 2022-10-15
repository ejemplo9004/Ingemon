using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextDisplayAttribute : PropertyAttribute
{
    public string Text = string.Empty;
    
    public TextDisplayAttribute(string text) => Text = text;
}
