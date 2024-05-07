using UnityEngine;
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
    public override void DrawConnectLine(AsyncOperationHandle<GameObject> _connectLinePrefabHandler)
    {
        if(line != null) Destroy(line.gameObject);
        if (Next == null) return;
        Debug.Log("StartFlow: DrawConnectLine");
        line = Instantiate(_connectLinePrefabHandler.Result, Vector3.zero, Quaternion.identity).GetComponent<LineRenderer>();
        line.SetPosition(0, transform.position);
        line.SetPosition(1, Next.transform.position);
    }
}
