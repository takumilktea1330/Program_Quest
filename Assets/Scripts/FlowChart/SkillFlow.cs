using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;

public class SkillFlow : Flow
{
    SelectSkillUI selectSkillUI;
    public override void Init(string id, bool isnew = true)
    {
        base.Init(id, isnew);
        Data.Type = "Skill";
        selectSkillUI = canvas.transform.Find("SelectSkillView").GetComponent<SelectSkillUI>();
        if(isnew) SelectSkill();
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
        selectSkillUI.Open(this);
    }
    public override void Connect(Flow target)
    {
        Next = target;
        Data.Next = target.Data.ID;
    }
    public override void DrawConnectLine(AsyncOperationHandle<GameObject> _connectLinePrefabHandler)
    {
        if (line != null) Destroy(line.gameObject);
        if (Next == null) return;
        line = Instantiate(_connectLinePrefabHandler.Result, Vector3.zero, Quaternion.identity).GetComponent<LineRenderer>();
        line.SetPosition(0, transform.position);
        line.SetPosition(1, Next.transform.position);
    }
}
