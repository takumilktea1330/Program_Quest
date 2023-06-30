using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlankObject : FlowChartObject
{
    public BlankObject() : base()
    {
        Name = "Blank";
        OriginalPrefab = Resources.Load<GameObject>("Prefabs/FlowChart/BlankPrefab");
        Init();
    }
}
