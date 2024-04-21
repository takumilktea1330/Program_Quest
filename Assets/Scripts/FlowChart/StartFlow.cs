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
    public override void Connect(Flow target)
    {
        Next = target;
        Data.Next = target.Data.ID;
    }
    public override void DrowConnectLine(AsyncOperationHandle<GameObject> _connectLinePrefabHandler)
    {
        Destroy(line);
        if(Next == null) return;
        line = Instantiate(_connectLinePrefabHandler.Result, Vector3.zero, Quaternion.identity).GetComponent<LineRenderer>();
        line.SetPosition(0, transform.position);
        line.SetPosition(1, Next.transform.position);
    }
}
