using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UI;

public class PropertyWindow : MonoBehaviour
{
    Text flowName;
    Text description;

    void Start()
    {
        flowName = transform.Find("FlowName").GetComponent<Text>();
        description = transform.Find("Description").GetComponent<Text>();
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
        targetFlow.ShowData();
        if(targetFlow is SkillFlow)
        {
            flowName.text = targetFlow.Data.SkillName + " (Skill Flow) ";
            description.text = SkillManager.GetSkill(targetFlow.Data.SkillName).Description;
        }
        else if(targetFlow is BranchFlow)
        {
            // name: 
        }
        else
        {
            Debug.Log("Unknown Flow Type!");
        }
    }
}
