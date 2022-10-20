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
    public void Awake()
    {
    }
    public void Update()
    {
    }
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition + offset;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        originalPosition = transform.position;
        offset = transform.position - Input.mousePosition;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        int currentEnergy = CombatSingletonManager.Instance.turnManager.info.energizer.currentEnergy;
        int cost = cardController.card.info.cost;
        bool checkCost = currentEnergy < cost;
        if ((Vector3.Distance(transform.position, originalPosition) <= 5))
            EnableCardInfo(true);
        if ((Vector3.Distance(transform.position, originalPosition) <= safeDistance) || checkCost)
            ResetPosition();
        else
        {
            cardController.PlayCard();
        }

    }
    public void ResetPosition()
    {
        transform.position = originalPosition;
    }
    public void EnableCardInfo(bool activate)
    {
        if (!activate) return;
        CombatSingletonManager.Instance.uiManager.UpdateCardInfo(cardController.card);
        CombatSingletonManager.Instance.uiManager.ShowCardInfo(true);
    }
}