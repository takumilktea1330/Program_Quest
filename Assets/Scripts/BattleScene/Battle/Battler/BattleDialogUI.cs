using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UI;

public class BattleDialogUI : MonoBehaviour
{
    private Text dialogText;
    void Start()
    {
        dialogText = GetComponentInChildren<Text>();
    }
    public IEnumerator DialogUpdate(string message)
    {
        dialogText.text = "";
        foreach(char letter in message)
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(0.1f);
        }
        yield return null;
    }
    public IEnumerator AddToDialog(string message)
    {
        dialogText.text += "\n";
        foreach (char letter in message)
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(0.1f);
        }
        yield return null;
    }
}
