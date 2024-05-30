using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UI;

public class PropertyWindow : MonoBehaviour
{
    [SerializeField] Text flowName;
    [SerializeField] Text description;

    void Start()
    {
        Close();
    }
    void Init()
    {
        flowName.text = "";
        description.text = "";
    }
    public void Open(Flow targetFlow)
    {
        gameObject.SetActive(true);
        ShowFlowData(targetFlow);
    }
    public void Close()
    {
        gameObject.SetActive(false);
        Init();
    }
    void ShowFlowData(Flow targetFlow)
    {
        if(targetFlow is SkillFlow)
        {
            flowName.text = targetFlow.Data.Name;
            description.text = SkillManager.GetSkill(targetFlow.Data.Name).Description;
        }
        else if(targetFlow is BranchFlow)
        {
            flowName.text = targetFlow.Data.Name;
            description.text = $"{targetFlow.Data.SourceUnit}の{targetFlow.Data.SourceStatus}が"+
            $"{targetFlow.Data.TargetUnit}の{targetFlow.Data.TargetStatus}の{targetFlow.Data.Percentage}%{targetFlow.Data.Operator}のとき"+
            "True側を実行し、そうでないならFalse側を実行します";
        }
        else
        {
            Debug.Log("Unknown Flow Type!");
        }
    }
}
