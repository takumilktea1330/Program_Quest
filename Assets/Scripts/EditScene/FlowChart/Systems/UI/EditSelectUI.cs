using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class EditSelectUI : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public UnityAction AddButtonOnClick;
    public UnityAction EditButtonOnClick;
    public UnityAction MoveButtonOnClick;
    public UnityAction DeleteButtonOnClick;
    public UnityAction OnTouch;
    public UnityAction OnRelease;
    List<CombineButton> buttons = new List<CombineButton>();

    private void Init()
    {
        buttons.AddRange(GetComponentsInChildren<CombineButton>());
        buttons.ForEach(button => button.OnClick += ButtonClicked);
        buttons.ForEach(button => button.OnTouch += () => OnTouch?.Invoke());
        buttons.ForEach(button => button.OnRelease += () => OnRelease?.Invoke());
        buttons[0].OnClick += AddButtonClicked;   // Skill
        buttons[1].OnClick += EditButtonClicked;      // For
        buttons[2].OnClick += MoveButtonClicked;   // While
        buttons[3].OnClick += DeleteButtonClicked;   // While
        Open();
    }
    private void Start()
    {
        Init();
    }

    private void ButtonClicked()
    {
        buttons.ForEach(button => button.IsClicked = true);
    }
    private void AddButtonClicked()
    {
        Debug.Log("AddButtonClicked");
        AddButtonOnClick?.Invoke();
    }
    private void EditButtonClicked()
    {
        Debug.Log("EditButtonClicked");
        EditButtonOnClick?.Invoke();
    }
    private void MoveButtonClicked()
    {
        Debug.Log("MoveButtonClicked");
        MoveButtonOnClick?.Invoke();
    }
    private void DeleteButtonClicked()
    {
        Debug.Log("DeleteButtonClicked");
        DeleteButtonOnClick?.Invoke();
    }

    // スクリーンの移動操作を不可にする
    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        OnTouch?.Invoke();
    }
    void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    {
        OnRelease?.Invoke();
    }

    public void Open()
    {
        gameObject.SetActive(true);
        buttons.ForEach(button => button.Init());
    }
    public void Close()
    {
        gameObject.SetActive(false);
    }
}
