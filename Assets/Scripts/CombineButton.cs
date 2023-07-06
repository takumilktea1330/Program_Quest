using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class CombineButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
    bool isClicked;
    Image image;
    Text text;
    Color normalColor = Color.blue;
    Color pressedColor = Color.gray;

    public bool IsClicked { get => isClicked; set => isClicked = value; }

    public event UnityAction OnClick;
    public event UnityAction OnTouch;
    public event UnityAction OnRelease;

    public void Init()
    {
        image = GetComponentInChildren<Image>();
        text = GetComponentInChildren<Text>();
        if (image != null) image.color = normalColor;
        if(text != null)text.color = normalColor;
        IsClicked = false;
    }
    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        if (IsClicked)
        {
            return;
        }
        IsClicked = true;
        OnClick?.Invoke();
    }
    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        if (image != null) image.color = pressedColor;
        if (text != null) text.color = pressedColor;
        OnTouch?.Invoke();
    }
    void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    {
        if (image != null) image.color = normalColor;
        if (text != null) text.color = normalColor;
        OnRelease?.Invoke();
    }
}
