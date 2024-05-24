using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceAlert : MonoBehaviour
{
    [SerializeField] TMPro.TMP_Text messageText;
    [SerializeField] Button choice0Button;
    [SerializeField] Button choice1Button;
    [SerializeField] Button CancelButton;
    int result = -1;

    /// <summary>
    /// Show an alert with two choices
    /// return Ienumerator<int> result
    /// 0: choice0 1: choice1 2: cancel
    /// </summary>
    /// <param name="message"></param>
    /// <param name="choice0"></param>
    /// <param name="choice1"></param>
    /// <returns></returns>
    public IEnumerator Alert(string message, string choice0, string choice1)
    {
        Open();
        result = -1;
        messageText.text = message;
        choice0Button.GetComponentInChildren<TMPro.TMP_Text>().text = choice0;
        choice1Button.GetComponentInChildren<TMPro.TMP_Text>().text = choice1;
        yield return new WaitUntil(() => result != -1);
        Close();
        yield return result; 
    }
    public IEnumerator Init()
    {
        choice0Button.onClick.AddListener(() => { result = 0; });
        choice1Button.onClick.AddListener(() => { result = 1; });
        CancelButton.onClick.AddListener(() => { result = 2; });
        Close();
        yield break;
    }
    public void Open()
    {
        gameObject.SetActive(true);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
}
