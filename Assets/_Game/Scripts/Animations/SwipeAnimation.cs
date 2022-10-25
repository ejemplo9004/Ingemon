using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwipeAnimation : MonoBehaviour
{
    public float rotateTime = .7f, moveTime = 1.2f;
    void Start()
    {
        LeanTween.delayedCall(2f, ()=>{
            LTSeq anim = LeanTween.sequence(); 
            anim.append(LeanTween.rotateZ(gameObject, 58f, rotateTime).setEaseInOutExpo());
            anim.append(LeanTween.moveLocalY(gameObject, 90f, moveTime).setEaseInOutExpo().setLoopPingPong());
        });

        Destroy(gameObject, 8f);
    }

}
