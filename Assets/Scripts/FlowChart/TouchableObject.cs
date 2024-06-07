using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchableObject : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Action OnDragAction;
    public Action OnPointerDownAction;
    public Action OnPointerUpAction;
    public Action OnPointerClickAction;
    public PointerEventData pointerEventData;
    public void OnBeginDrag(PointerEventData eventData)
    {
        // Debug.Log("OnBeginDrag");
    }

    public void OnDrag(PointerEventData eventData)
    {
        pointerEventData = eventData;
        OnDragAction?.Invoke();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Debug.Log("OnEndDrag");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // Debug.Log("OnPointerClick");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // Debug.Log("OnPointerDown");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // Debug.Log("OnPointerUp");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
