using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Dummy : MonoBehaviour, IDragHandler
{
    private Vector3 offset;
    public float safeDistance;
    private Vector3 originalPosition;
    public float amplitude;
    public float speed;
    public void Awake()
    {

    }
    public void Update()
    {

    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition + offset;
        StartCoroutine(ShakeAnimation());
    }
    public IEnumerator ShakeAnimation(){
        while(true){
            transform.rotation = Quaternion.Euler(new Vector3(0,0,amplitude*Mathf.Sin(Time.time * speed)));
            yield return null;
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        originalPosition = transform.position;
        offset = transform.position - Input.mousePosition;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        StopAllCoroutines();
        if ((Vector3.Distance(transform.position, originalPosition) <= safeDistance) )
            ResetPosition();
    }
    public void ResetPosition()
    {
        transform.position = originalPosition;
    }
}