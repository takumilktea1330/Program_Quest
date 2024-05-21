using System;
[Serializable]
public class FlowData
{
    public string Name = "";
    public string Type = "";
    public string ID = "";
    public string Next = "";

    public float PosX = 0;
    public float PosY = 0;

    public string Branch = "";
    public string SourceUnit = "";
    public string SourceStatus = "";
    public string TargetUnit = "";
    public string TargetStatus = "";
    public string Operator = "";
    public int Percentage = 0;
}
