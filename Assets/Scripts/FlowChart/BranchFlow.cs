using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;

public class BranchFlow : Flow
{
    public Flow Branch { get; set; }
    private LineRenderer falseSideLine = null;
    public override void Init(string id, bool isnew = true)
    {
        base.Init(id, isnew);
        Data.Name = "Branch";
        Data.Type = "Branch";
        if (isnew) SetCondition();
        else Display();
    }
    public void SetCondition()
    {
        Debug.Log("SetCondition");
        uiController.OpenSetConditionUI(this);
    }
    public override IEnumerator Connect(Flow target)
    {
        Debug.Log("BranchFlow Connect");
        IEnumerator i = uiController.GetChoiceAlertResult(messageText, "False", "True");
        yield return i;
        int result = (int) i.Current;
        if (result == 0)
        {
            Data.Branch = target.Data.ID;
            Branch = target;
            uiController.ShowMessage("Success", $"Connected: {Data.Name}(False) -> {target.Data.Name}");
        }
        else if (result == 1)
        {
            Data.Next = target.Data.ID;
            Next = target;
            uiController.ShowMessage("Success", $"Connected: {Data.Name}(True) -> {target.Data.Name}");
        }
        else if (result == 2)
        {
            uiController.ShowMessage("Operation Canceled", "接続をキャンセルしました");
            yield break;
        }
        else
        {
            Debug.LogError("BranchFlow: Invalid result");
            yield break;
        }
        SaveChartDataasJson.Save();
        yield break;
    }
    public override void DrawConnectLine(AsyncOperationHandle<GameObject> _connectLinePrefabHandler)
    {
        base.DrawConnectLine(_connectLinePrefabHandler);
        if (falseSideLine != null) Destroy(falseSideLine.gameObject);
        if (Branch != null)
        {
            falseSideLine = Instantiate(_connectLinePrefabHandler.Result, Vector3.zero, Quaternion.identity).GetComponent<LineRenderer>();
            falseSideLine.GetComponentInChildren<ParticleOnLine>().Set(transform.position, Branch.transform.position, "#FA0A0A");
            falseSideLine.SetPosition(0, transform.position);
            falseSideLine.SetPosition(1, Branch.transform.position);
        }
    }

    readonly string messageText = "この条件分岐をTrue側とFalse側のどちらに接続しますか？";
}
