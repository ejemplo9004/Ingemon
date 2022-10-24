using System;
using UnityEngine;
using UnityEngine.UI;

public class UIHighlighter : MonoBehaviour
{
    private float cosTime, passedTime, cos, r, a;
    [SerializeField] private float showTime;
    [SerializeField] private Button btn;
    private ColorBlock original, cols;
    private bool working;

    private void OnEnable()
    {
        cosTime = 0;
        passedTime = 0;
        original = btn.colors;
        cols = btn.colors;
        btn.colors = cols;
        r = original.normalColor.r;
        a = original.normalColor.a;
        working = true;
    }

    private void Update()
    {
        if (working)
        {
            passedTime += Time.deltaTime;
            if (passedTime > showTime)
            {
                cos = (Mathf.Cos(cosTime) + 1) / 2;
                cols.normalColor = new Color(r, cos, cos, a);
                btn.colors = cols;
                cosTime += Time.deltaTime;
            }
        }
    }

    public void StopIt()
    {
        btn.colors = original;
        working = false;
    }
}