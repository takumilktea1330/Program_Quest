using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageUI : MonoBehaviour
{
    [SerializeField] Text titleText;
    [SerializeField] Text messageText;
    void Start()
    {
        Close();
    }

    public void Show(string title, string message)
    {
        titleText.text = title;
        messageText.text = message;
        gameObject.SetActive(true);
        StartCoroutine(Hide());
    }

    IEnumerator Hide()
    {
        yield return new WaitForSeconds(2);
        gameObject.SetActive(false);
    }

    void Close()
    {
        gameObject.SetActive(false);
    }

}
