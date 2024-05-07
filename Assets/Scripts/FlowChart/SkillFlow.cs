using UnityEngine;
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
    public override void Connect(Flow target)
    {
        Next = target;
        Data.Next = target.Data.ID;
    }
    public override void DrawConnectLine(AsyncOperationHandle<GameObject> _connectLinePrefabHandler)
    {
        if (line != null) Destroy(line.gameObject);
        if (Next == null) return;
        Debug.Log("SkillFlow: DrawConnectLine");
        line = Instantiate(_connectLinePrefabHandler.Result, Vector3.zero, Quaternion.identity).GetComponent<LineRenderer>();
        line.SetPosition(0, transform.position);
        line.SetPosition(1, Next.transform.position);
    }
}
