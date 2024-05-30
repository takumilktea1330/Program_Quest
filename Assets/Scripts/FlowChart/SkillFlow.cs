using System.Collections;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;

public class SkillFlow : Flow
{
    public override void Init(FlowData data = null)
    {
        base.Init(data);
        Data.Type = "Skill";
        if (data == null)
        {
            SelectSkill();
        }
        else Display();
    }
    public override void Display()
    {
        if(Data.Name == null) Debug.LogError("SkillFlow: Data.Name is null");
        var skill = SkillManager.GetSkill(Data.Name);
        gameObject.GetComponent<SpriteRenderer>().sprite = skill.DisplaySprite;
    }
    public void SelectSkill()
    {
        uiController.OpenSelectSkillUI(this);
    }
    public override IEnumerator Connect(Flow target)
    {
        Next = target;
        Data.Next = target.Data.ID;
        uiController.ShowMessage("Success", $"Connected: {Data.Name} -> {target.Data.Name}");
        SaveChartDataasJson.Save();
        yield break;
    }
}
