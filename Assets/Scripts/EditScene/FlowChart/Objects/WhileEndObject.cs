using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhileEndObject : FlowChartObject
{
    public WhileEndObject() : base()
    {
        Init();
    }
    public override void Init()
    {
        base.Init();
        Name = "WhileEnd";
        Type = "WhileEnd";
        ExecutionTime = 0;
        Explain = "WhileEnd";
        OriginalPrefab = Resources.Load<GameObject>("Prefabs/FlowChart/WhileEndPrefab");
    }
}
