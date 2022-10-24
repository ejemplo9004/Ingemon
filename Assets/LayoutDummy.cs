using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayoutDummy : MonoBehaviour
{
    public GameObject emptyCard, card, spawn;
    public float time = 2f;
    public AnimationCurve lt;
    void Start()
    {
        AddCard();
    }

    void Update()
    {
        
    }

    public void AddCard()
    {
        var empty = Instantiate(emptyCard,transform);
        var c = Instantiate(card, spawn.transform);
        c.transform.position = new Vector3(c.transform.position.x, empty.transform.position.y,0f);
        c.transform.SetParent(empty.transform);

        LeanTween.moveLocalX(c,0f, time).setEase(lt).setOnComplete(() => {
            c.transform.SetParent(transform);
            Destroy(empty);});
    }
}
