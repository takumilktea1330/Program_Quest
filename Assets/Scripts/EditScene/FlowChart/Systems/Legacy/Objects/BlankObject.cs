using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BlankObject : FlowChartObject
{
    public BlankObject() : base()
    {
        Name = "Blank";
        Type = "Blank";
        ExecutionTime = 0;
        Explain = "Blank";
        OriginalPrefab = Resources.Load<GameObject>("Prefabs/FlowChart/BlankPrefab");
        Init();
    }
}
