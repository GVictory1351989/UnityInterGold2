using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardInteract : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private Canvas canvas;
    private RectTransform rect;
    private Vector3 startPos;
    private bool isDragging = false;
    public bool isOverDiscard = false;
    public GameObject DiscardZone;
    public CardUnit CardUnit;
    bool IsSelected = false;

    void Start()
    {
        rect = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
    }
    internal void PassData(CardUnit cardData, Sprite cardDisplay)
    {
        CardUnit = cardData;
        GetComponent<Image>().sprite = cardDisplay;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        startPos = rect.anchoredPosition;
       
    }

  

    public void OnPointerUp(PointerEventData eventData)
    {
        IsSelected = !IsSelected;
        if (IsSelected)
        {
            GetComponent<Image>().color = Color.grey;
            PlayerIOManager.SendMessage("selectedcard", CardUnit.Id);
        }
        else
            GetComponent<Image>().color = Color.white;

    }

   
}
