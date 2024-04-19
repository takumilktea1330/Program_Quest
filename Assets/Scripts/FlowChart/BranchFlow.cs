using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BranchFlow : Flow
{
    public override void Init(string id)
    {
        base.Init(id);
        Data.Type = "Branch";
    }
    public void SetCondition()
    {
        //OnSetCondition.Invoke(this);
    }
}
