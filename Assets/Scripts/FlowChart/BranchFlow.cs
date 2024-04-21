using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;

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
    public override void Connect(Flow flow, AsyncOperationHandle<GameObject> connectLinePrefabHandler)
    {
        base.Connect(flow, connectLinePrefabHandler);
    }
}
