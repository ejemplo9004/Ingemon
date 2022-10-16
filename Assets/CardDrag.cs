using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
 
public class CardDrag : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler, IPointerEnterHandler
{
    private Vector3 offset;
    public float safeDistance;
    public CardSpriteController cardController;
    public CanvasGroup canvasGroup;
    private Vector3 originalPosition;
    public RectTransform rt;
    public void Awake(){
        rt = GetComponent<RectTransform>();
    }
    public void Update(){
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        originalPosition = transform.position;
    }
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition + offset;
    }
 
    public void OnPointerDown(PointerEventData eventData)
    {
        offset = transform.position - Input.mousePosition;
    }
 
    public void OnPointerUp(PointerEventData eventData)
    {
        int currentEnergy = CombatSingletonManager.Instance.turnManager.info.energizer.currentEnergy;
        int cost = cardController.card.info.cost;
        bool checkCost = currentEnergy < cost;
        if((Vector3.Distance(transform.position, originalPosition) <= safeDistance) || checkCost) ResetPosition();
        else {
            cardController.PlayCard();
        }
       
    }
    public void ResetPosition()
    {   
        transform.position = originalPosition;
    }
}