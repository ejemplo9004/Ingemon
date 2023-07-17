using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TutorialController : MonoBehaviour
{
    public Button back;
    public Button forward;
    public GameObject[] slides;
    public int actualSlide = 0;

    public void OnEnable()
    {
        back.onClick.AddListener(() => moveSlide(-1));
        forward.onClick.AddListener(() => moveSlide(1));
        foreach (GameObject sl in slides)
        {
            sl.SetActive(false);
        }
        slides[actualSlide].SetActive(true);
    }
    public void moveSlide(int value)
    {
        if(actualSlide+value >= slides.Length) gameObject.SetActive(false);
        else if(actualSlide == Mathf.Clamp(value + actualSlide, 0, slides.Length-1)) return;
        GameObject oldSlide = slides[actualSlide];
        actualSlide = Mathf.Clamp(value + actualSlide, 0, slides.Length-1);
        float pos;
        if (value < 0) pos = 800f;
        else pos = -800f;
        LeanTween.moveLocalX(oldSlide,pos,.15f).setEaseInBack()
            .setOnComplete(() => {
                oldSlide.SetActive(false);
                slides[actualSlide].SetActive(true);
                slides[actualSlide].transform.localScale = Vector3.zero;
                LeanTween.scale(slides[actualSlide], Vector3.one, .15f).setEaseOutBounce().setDelay(.05f);
                LeanTween.moveLocalX(oldSlide,0,0f);
            });
    }

    public void OnDisable()
    {
        back.onClick.RemoveAllListeners();
        forward.onClick.RemoveAllListeners();
    }
}

