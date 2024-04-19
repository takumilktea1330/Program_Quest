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
        SelectSkill();
    }
    public void SelectSkill()
    {
        selectSkillUI = canvas.transform.Find("SelectSkillView").GetComponent<SelectSkillUI>();
        selectSkillUI.Open(this);
    }
}
