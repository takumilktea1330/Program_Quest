using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;

public class BranchFlow : Flow
{
    SetConditionUI setConditionUI;
    public Flow Branch { get; set; }
    public override void Init(string id, bool isnew = true)
    {
        base.Init(id, isnew);
        Data.Type = "Branch";
        setConditionUI = canvas.transform.Find("SetConditionUI").GetComponent<SetConditionUI>();
        if (isnew) SetCondition();
        else Display();
    }
    public override void Display()
    {
        if (Data.Name == null) Debug.LogError("BranchFlow: Data.Name is null");
        // var skill = SkillManager.GetSkill(Data.Name);
        // gameObject.GetComponent<SpriteRenderer>().sprite = skill.DisplaySprite;
    }
    public void SetCondition()
    {
        //OnSetCondition.Invoke(this);
    }
    public override void Connect(Flow flow)
    {
        // NextかBranchのいずれかに接続します
        // 実装予定
        Debug.Log("BranchFlow Connect");
    }
}
