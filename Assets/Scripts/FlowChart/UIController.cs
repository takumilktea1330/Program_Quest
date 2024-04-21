using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    GameObject CreateFlowPanel;
    Dropdown ModeDropdown;
    PropertyWindow propertyWindow;
    private void Awake() {
        CreateFlowPanel = transform.Find("CreateFlowPanel").gameObject;
        ModeDropdown = transform.Find("ModeDropdown").gameObject.GetComponent<Dropdown>();
        propertyWindow = transform.Find("PropertyWindow").gameObject.GetComponent<PropertyWindow>();
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
    public void ToProcessingMode(string message = null)
    {
        ChartMode.CurrentState = ChartMode.State.Processing;
        Debug.Log("Processing...: " + message);
        ModeDropdown.gameObject.SetActive(false);
        CreateFlowPanel.SetActive(false);
        ClosePropertyWindow();
    }
    public void OpenPropertyWindow(Flow targetFlow)
    {
        propertyWindow.Open(targetFlow);
    }
    public void ClosePropertyWindow()
    {
        propertyWindow.Close();
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
