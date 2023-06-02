using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class AddFlowSelectUI : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public UnityAction SkillButtonOnClick;
    public UnityAction IfButtonOnClick;
    public UnityAction WhileButtonOnClick;
    public UnityAction OnTouch;
    public UnityAction OnRelease;
    List<TextButton> buttons = new List<TextButton>();
    private void Start()
    {
        buttons.AddRange(GetComponentsInChildren<TextButton>());
        buttons.ForEach(button => button.OnClick += ButtonClicked);
        buttons.ForEach(button => button.OnTouch += () => OnTouch?.Invoke());
        buttons.ForEach(button => button.OnRelease += () => OnRelease?.Invoke());
        buttons[0].OnClick += SkillButtonClicked;   // Skill
        buttons[1].OnClick += IfButtonClicked;      // For
        buttons[2].OnClick += WhileButtonClicked;   // While
        Close();
    }

    private void ButtonClicked()
    {
        buttons.ForEach(button => button.IsClicked = true);
    }
    private void SkillButtonClicked()
    {
        Debug.Log("SkillButtonClicked");
        SkillButtonOnClick?.Invoke();
    }
    private void IfButtonClicked()
    {

    }
    private void WhileButtonClicked()
    {

    }
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
