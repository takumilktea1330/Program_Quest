using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] UnityEngine.GameObject CreateFlowPanel;
    [SerializeField] UnityEngine.GameObject ModeChangeButton;
    [SerializeField] PropertyWindow propertyWindow;
    [SerializeField] MessageUI messageUI;
    [SerializeField] SelectSkillUI selectSkillUI;
    [SerializeField] SetConditionUI setConditionUI;
    [SerializeField] LoadElementScreen loadElementScreen;

    public IEnumerator InitUIs()
    {
        yield return selectSkillUI.Init();
        yield return setConditionUI.Init();
    }
    public void ToViewMode()
    {
        ChartMode.CurrentState = ChartMode.State.View;
        ModeChangeButton.SetActive(true);
        CreateFlowPanel.SetActive(true);
    }
    public void ToConnectMode()
    {
        ChartMode.CurrentState = ChartMode.State.Connection;
        ModeChangeButton.SetActive(true);
        CreateFlowPanel.SetActive(false);
        ClosePropertyWindow();
    }
    public void ToProcessingMode()
    {
        ChartMode.CurrentState = ChartMode.State.Processing;
        ModeChangeButton.SetActive(false);
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
        if(ChartMode.CurrentState == ChartMode.State.View)
        {
            ToConnectMode();
        }
        else
        {
            ToViewMode();
        }
    }
}
