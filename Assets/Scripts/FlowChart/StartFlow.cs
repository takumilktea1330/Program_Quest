using System.Collections;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;

public class StartFlow : Flow
{
    public override void Init(FlowData data = null)
    {
        base.Init(data);
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
}
