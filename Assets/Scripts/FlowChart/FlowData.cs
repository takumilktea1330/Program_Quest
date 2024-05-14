using System;
[Serializable]
public class FlowData
{
    public string Type;
    public string ID;
    public string Next;// skill->next, branchflow(true)
    public string Branch;// is used only branchflow(false)

    public float PosX;
    public float PosY;

    public string Name;
}
