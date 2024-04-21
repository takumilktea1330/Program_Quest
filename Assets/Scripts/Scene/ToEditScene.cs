using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

public class ToEditScene : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
    bool isClicked;
    Text text;
    Color normalColor;
    Color pressedColor = Color.gray;
    [SerializeField] LoadingScreen loadingScreen;

    public bool IsClicked { get => isClicked; set => isClicked = value; }
    void Awake()
    {
        text = GetComponent<Text>();
        normalColor = GetColor("#007AFF");
        Init();
    }
    private void Init()
    {
        text.color = normalColor;
        IsClicked = false;
    }
    private Color GetColor(string colorCode)
    {
        Color color = default(Color);
        if (ColorUtility.TryParseHtmlString(colorCode, out color))
        {
            //Colorを生成できたらそれを返す
            return color;
        }
        else
        {
            //Colorの生成に失敗したら黒を返す
            return Color.black;
        }
    }
    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        if (isClicked)
        {
            return;
        }
        isClicked = true;
        loadingScreen.LoadNextScene("EditScene");
    }
    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        text.color = pressedColor;
    }
    void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    {
        text.color = normalColor;
    }
}