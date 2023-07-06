using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class ImageButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
    bool isClicked;
    Image image;
    Color normalColor = Color.blue;
    Color pressedColor = Color.gray;

    public bool IsClicked { get => isClicked; set => isClicked = value; }

    public event UnityAction OnClick;
    public event UnityAction OnTouch;
    public event UnityAction OnRelease;

    public void Init()
    {
        image = GetComponent<Image>();
        image.color = normalColor;
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
        image.color = pressedColor;
        OnTouch?.Invoke();
    }
    void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    {
        image.color = normalColor;
        OnRelease?.Invoke();
    }
}
