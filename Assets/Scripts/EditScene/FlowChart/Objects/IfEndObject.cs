using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IfEndObject : FlowChartObject
{
    public IfEndObject() : base()
    {
        Init();
    }
    public override void Init()
    {
        base.Init();
        Name = "IfEnd";
        Type = "IfEnd";
        ExecutionTime = 0;
        Explain = "IfEnd";
        OriginalPrefab = Resources.Load<GameObject>("Prefabs/FlowChart/IfEndPrefab");
    }
}
