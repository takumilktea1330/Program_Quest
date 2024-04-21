using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class StartFlow : Flow
{
    public override void Init(string id)
    {
        base.Init(id);
        Data.Type = "Start";
    }
    public override void Display()
    {
    }
    public override void Connect(Flow target, AsyncOperationHandle<GameObject> connectLinePrefabHandler)
    {
        GameObject connectLine = Instantiate(connectLinePrefabHandler.Result, canvas.transform);
        line = connectLine.GetComponent<LineRenderer>();
        line.SetPosition(0, transform.position);
        line.SetPosition(1, target.transform.position);
    }
}
