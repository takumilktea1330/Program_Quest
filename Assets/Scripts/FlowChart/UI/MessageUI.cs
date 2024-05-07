using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageUI : MonoBehaviour
{
    Text titleText;
    Text messageText;
    void Start()
    {
        gameObject.SetActive(false);
        titleText = transform.Find("Title").GetComponent<Text>();
        messageText = transform.Find("Message").GetComponent<Text>();
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

}
