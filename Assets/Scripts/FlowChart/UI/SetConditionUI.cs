using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using TMPro;

public class SetConditionUI : MonoBehaviour
{
    [SerializeField] UIController uiController;
    [SerializeField] TMP_Dropdown sourceUnitDropdown;
    [SerializeField] TMP_Dropdown sourceStatusDropdown;
    [SerializeField] TMP_Dropdown targetUnitDropdown;
    [SerializeField] TMP_Dropdown targetStatusDropdown;
    [SerializeField] TMP_InputField percentageInputField;
    [SerializeField] TMP_Dropdown operatorDropdown;
    [SerializeField] UnityEngine.UI.Button setButton;
    private BranchFlow targetFlow;


    public IEnumerator Init()
    {
        sourceUnitDropdown.value = 0; // 0: Player, 1: Enemy
        sourceStatusDropdown.value = 1; // 0: HP, 1: ATK, 2: DEF
        targetUnitDropdown.value = 1; // 0: Player, 1: Enemy
        targetStatusDropdown.value = 0; // 0: HP, 1: ATK, 2: DEF
        percentageInputField.text = "100"; // 100%
        operatorDropdown.value = 0; // 0: >=, 1: =<
        Close();
        yield break;
    }


    public void Open(BranchFlow targetFlow)
    {
        gameObject.SetActive(true);
        uiController.ToProcessingMode();
        this.targetFlow = targetFlow;
    }

    public void IFOnEndEdit()
    {
        GetIFValue();
    }
    private int GetIFValue()
    {
        if(int.TryParse(percentageInputField.text, out int value))
        {
            if (value <= 0)
            {
                uiController.ShowMessage("Error", "Please enter a integer value more than 0");
                percentageInputField.text = "100";
                return 100;
            }
            else return value;
        }
        else
        {
            uiController.ShowMessage("Error", "Please enter a integer value more than 0");
            percentageInputField.text = "100";
            return 100;
        }
    }

    public void SetCondition()
    {
        FlowData data = new()
        {
            ID = targetFlow.Data.ID,
            Name = targetFlow.Data.Name,
            Type = targetFlow.Data.Type,
            PosX = targetFlow.Data.PosX,
            PosY = targetFlow.Data.PosY,
            SourceUnit = sourceUnitDropdown.options[sourceUnitDropdown.value].text,
            SourceStatus = sourceStatusDropdown.options[sourceStatusDropdown.value].text,
            TargetUnit = targetUnitDropdown.options[targetUnitDropdown.value].text,
            TargetStatus = targetStatusDropdown.options[targetStatusDropdown.value].text,
            Operator = operatorDropdown.options[operatorDropdown.value].text,
            Percentage = GetIFValue()
            
        };
        targetFlow.Data = data;
        uiController.ToViewMode();
        SaveChartDataasJson.Save();
        Close();
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
}
