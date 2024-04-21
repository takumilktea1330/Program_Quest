using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class SkillFlow : Flow
{
    SelectSkillUI selectSkillUI;
    public override void Init(string id)
    {
        base.Init(id);
        Data.Type = "Skill";
        selectSkillUI = canvas.transform.Find("SelectSkillView").GetComponent<SelectSkillUI>();
        SelectSkill();
    }
    public override void Display()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = SkillManager.GetSkill(Data.SkillName).DisplaySprite;
    }
    public void SelectSkill()
    {
        selectSkillUI.Open(this);
    }
    public override void Connect(Flow target, AsyncOperationHandle<GameObject> connectLinePrefabHandler)
    {
        GameObject connectLine = Instantiate(connectLinePrefabHandler.Result, canvas.transform);
        line = connectLine.GetComponent<LineRenderer>();
        line.SetPosition(0, transform.position);
        line.SetPosition(1, target.transform.position);
    }
}
