using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] GameObject CreateFlowPanel;
    [SerializeField] Dropdown ModeDropdown;
    [SerializeField] PropertyWindow propertyWindow;
    [SerializeField] MessageUI messageUI;
    [SerializeField] SelectSkillUI selectSkillUI;
    [SerializeField] LoadElementScreen loadElementScreen;

    public IEnumerator InitUIs()
    {
        yield return selectSkillUI.Init();
    }
    public void ToViewMode()
    {
        ChartMode.CurrentState = ChartMode.State.View;
        ModeDropdown.gameObject.SetActive(true);
        CreateFlowPanel.SetActive(true);
    }
    public void ToConnectMode()
    {
        ChartMode.CurrentState = ChartMode.State.Connection;
        ModeDropdown.gameObject.SetActive(true);
        CreateFlowPanel.SetActive(false);
        ClosePropertyWindow();
    }
    public void ToProcessingMode()
    {
        ChartMode.CurrentState = ChartMode.State.Processing;
        ModeDropdown.gameObject.SetActive(false);
        CreateFlowPanel.SetActive(false);
        ClosePropertyWindow();
    }
    public void ShowMessage(string title, string message)
    {
        messageUI.Show(title, message);
    }
    public void OpenPropertyWindow(Flow targetFlow)
    {
        propertyWindow.Open(targetFlow);
    }
    public void ClosePropertyWindow()
    {
        propertyWindow.Close();
    }
    public void OpenSelectSkillUI(Flow targetFlow)
    {
        selectSkillUI.Open(targetFlow);
    }
    public void CloseSelectSkillUI()
    {
        selectSkillUI.Close();
    }
    public void OpenLoadElementScreen()
    {
        loadElementScreen.Open();
    }
    public void CloseLoadElementScreen()
    {
        loadElementScreen.Close();
    }
    public void ModeChange()
    {
        switch (ModeDropdown.value)
        {
            case 0:
                ToViewMode();
                break;
            case 1:
                ToConnectMode();
                break;
        }
    }
}
