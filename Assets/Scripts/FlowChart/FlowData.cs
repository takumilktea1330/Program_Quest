/// <summary>
/// Flow Data Save with JsonUtility
/// </summary>
[System.Serializable]
public class FlowData
{
    public string Type;
    public string StructId;
    public int NextId1;// skill->next, ifflow(true)
    public int NextId2;// is used only ifflow(false)

    public int PosX;
    public int PosY;

    public string SkillName;
    public string CondFormula;
}
