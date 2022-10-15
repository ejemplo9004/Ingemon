using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuffUIController : MonoBehaviour
{
    public int timer { get; set; }
    public int value { get; set; }
    private float maxHeight;
    private float timerUnit;
    [SerializeField] private Image background;
    [SerializeField] private TextMeshProUGUI text;
    
    public void SetValues(int value, int timer)
    {
        this.timer = timer;
        this.value = value;
        text.SetText(value != 0? value.ToString() : "");
        maxHeight = background.rectTransform.sizeDelta.y;
        timerUnit = (timer != -1)? maxHeight / timer : maxHeight;
    }

    public void UpdateTimer(int timer)
    {
        this.timer = timer;
        background.rectTransform.sizeDelta = new Vector2(background.rectTransform.sizeDelta.x, timer * timerUnit);
        if (timer == 0)
        {
            Destroy(gameObject);
        }
    }

    public void UpdateValue(int value)
    {
        this.value = value;
        text.SetText(value.ToString());
        if (value == 0)
        {
            Destroy(gameObject);
        }
    }
}
