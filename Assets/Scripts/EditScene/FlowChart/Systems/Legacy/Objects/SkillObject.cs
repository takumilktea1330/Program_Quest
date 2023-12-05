using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SkillObject : FlowChartObject
{
    public SkillBase SkillBase { get; set; }
    public SkillObject(SkillBase skillBase) : base()
    {
        SkillBase = skillBase;
        Init();
    }
    public override void Init()
    {
        base.Init();
        Name = SkillBase.Name;
        OriginalPrefab = Resources.Load<GameObject>("Prefabs/FlowChart/SkillObjectPrefab");
    }
}
