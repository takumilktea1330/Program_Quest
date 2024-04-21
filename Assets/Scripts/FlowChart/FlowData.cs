/// <summary>
/// Flow Data Save with JsonUtility
/// </summary>
[System.Serializable]
public class FlowData
{
    public string Type;
    public string ID;
    public string Next;// skill->next, ifflow(true)
    public string Branch;// is used only ifflow(false)

    public int PosX;
    public int PosY;

    public string SkillName;
    public string CondFormula;
}
