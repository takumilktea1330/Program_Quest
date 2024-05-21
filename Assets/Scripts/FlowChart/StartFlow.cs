using System.Collections;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;

public class StartFlow : Flow
{
    public override void Init(string id, bool isnew = true)
    {
        base.Init(id, isnew);
        Data.Type = "Start";
        Data.Name = "Start";
    }
    public override void Display(){}
    public override IEnumerator Connect(Flow target)
    {
        Next = target;
        Data.Next = target.Data.ID;
        uiController.ShowMessage("Success", $"Connected: {Data.Name} -> {target.Data.Name}");
        SaveChartDataasJson.Save();
        yield break;
    }
    public override void DrawConnectLine(AsyncOperationHandle<GameObject> _connectLinePrefabHandler)
    {
        if(line != null) Destroy(line.gameObject);
        if (Next == null) return;
        line = Instantiate(_connectLinePrefabHandler.Result, Vector3.zero, Quaternion.identity).GetComponent<LineRenderer>();
        line.GetComponentInChildren<ParticleOnLine>().Set(transform.position, Next.transform.position);
        line.SetPosition(0, transform.position);
        line.SetPosition(1, Next.transform.position);
    }
}
