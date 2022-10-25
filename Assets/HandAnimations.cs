using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandAnimations : MonoBehaviour
{
    public List<GameObject> cardObjects;
    public void Start(){
        StartCoroutine(Anim());
    }
    public IEnumerator Anim(){
        foreach (GameObject card in cardObjects){
            yield return null;
            LeanTween.moveLocalY(card,2.2f,.8f).setEase(LeanTweenType.punch);
            
        }
    }
}
