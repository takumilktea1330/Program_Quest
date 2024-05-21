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
