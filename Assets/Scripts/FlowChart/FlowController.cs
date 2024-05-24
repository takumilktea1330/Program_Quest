using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class FlowController : MonoBehaviour
{
    AsyncOperationHandle<GameObject> _skillFlowPrefabHandler;
    AsyncOperationHandle<GameObject> _branchFlowPrefabHandler;
    AsyncOperationHandle<GameObject> _startFlowPrefabHandler;
    AsyncOperationHandle<GameObject> _connectLinePrefabHandler;
    [SerializeField] UIController uiController;
    Flow selectedFlow; // a flow selected by user
    // IEnumerator blinkCoroutine;

    private IEnumerator Start()
    {
        uiController.OpenLoadElementScreen();
        yield return SkillManager.Init();
        Debug.Log("SkillManager initialized");
        yield return Init();
        Debug.Log("UI initialized");
        yield return LoadChart();
        Debug.Log("Chart loaded");
        uiController.CloseLoadElementScreen();
    }

    private IEnumerator Init()
    {
        yield return uiController.InitUIs();
        yield return LoadPrefabs();
        ChartMode.CurrentState = ChartMode.State.View;
    }
    
    private IEnumerator LoadPrefabs()
    {
        yield return _startFlowPrefabHandler = Addressables.LoadAssetAsync<GameObject>("Prefabs/StartFlowPrefab");
        yield return _skillFlowPrefabHandler = Addressables.LoadAssetAsync<GameObject>("Prefabs/SkillFlowPrefab");
        yield return _branchFlowPrefabHandler = Addressables.LoadAssetAsync<GameObject>("Prefabs/BranchFlowPrefab");
        yield return _connectLinePrefabHandler = Addressables.LoadAssetAsync<GameObject>("Prefabs/ConnectLinePrefab");
    }

    private void Update()
    {
        switch (ChartMode.CurrentState)
        {
            case ChartMode.State.View:
                ViewModeHandler();
                break;
            case ChartMode.State.Connection:
                StartCoroutine(ConnectModeHandler());
                break;
        }
    }

    private string CreateStartFlow()
    {
        return CreateFlow<StartFlow>(_startFlowPrefabHandler.Result, new Vector3(-6, 2.5f, 0));
    }

    public void CreateSkillFlow()
    {
        CreateFlow<SkillFlow>(_skillFlowPrefabHandler.Result, new Vector3(0, 0, 0));
    }

    public void CreateBranchFlow()
    {
        CreateFlow<BranchFlow>(_branchFlowPrefabHandler.Result, new Vector3(0, 0, 0));
    }

    private string CreateFlow<T>(GameObject prefab, Vector3 position) where T : Flow
    {
        string newStructId = Guid.NewGuid().ToString("N"); // get struct id
        GameObject newFlowObject = Instantiate(prefab, position, Quaternion.identity);
        T newFlow = newFlowObject.GetComponent<T>();
        newFlow.Init(newStructId);
        ChartData.Flows.Add(newFlow);
        return newStructId;
    }

    public void RemoveFlow()
    {
        StartCoroutine(DeleteFlow());
    }
    IEnumerator DeleteFlow()
    {
        Flow targetFlow = selectedFlow;
        if(targetFlow == null)
        {
            uiController.ShowMessage("Error", "No flow selected");
            yield break;
        }
        if(targetFlow is StartFlow)
        {
            uiController.ShowMessage("Error", "Cannot delete StartFlow");
            yield break;
        }
        IEnumerator enumerator = uiController.GetChoiceAlertResult("Are you sure you want to delete this flow?", "Yes", "No");
        yield return enumerator;
        switch((int)enumerator.Current)
        {
            case 0:
            Debug.Log("Delete Flow");
                break;
            case 1:
            Debug.Log("Operation Canceled");
                yield break;
            case 2:
                uiController.ShowMessage("Operation Canceled", "Operation Canceled");
                yield break;
            default:
                Debug.LogError("Invalid result");
                yield break;
        }
        ChartData.Flows.Remove(targetFlow);
        Destroy(targetFlow.gameObject);
        SaveChartDataasJson.Save();
    }

    public void SelectFlowOnBoard(Flow targetFlow)
    {
        selectedFlow = targetFlow;
        uiController.OpenPropertyWindow(selectedFlow);
    }

    public void DrawConnectLines()
    {
        foreach (Flow flow in ChartData.Flows)
        {
            Debug.Log($"DrawConnectLines: {flow.Data.Name}");
            flow.DrawConnectLine(_connectLinePrefabHandler);
        }
    }

    private IEnumerator LoadChart()
    {
        var flowDataList = SaveChartDataasJson.Load();
        Debug.Log("FlowDataList loaded");
        if(flowDataList == null)
        {
            Debug.Log("New flowchart created");
            uiController.ShowMessage("Info", "No data found. Creating a new flowchart...");
            yield return ChartData.StartFlowID = CreateStartFlow();
            Debug.Log("New flowchart created");
            yield break;
        }
        else if(flowDataList.Count == 0)
        {
            uiController.ShowMessage("Info", "No data found. Creating a new flowchart...");
            yield return ChartData.StartFlowID = CreateStartFlow();
            yield break;
        }
        ChartData.Flows.Clear();
        foreach (FlowData flowData in flowDataList)
        {
            GameObject prefab = null;
            switch (flowData.Type)
            {
                case "Start":
                    prefab = _startFlowPrefabHandler.Result;
                    break;
                case "Skill":
                    prefab = _skillFlowPrefabHandler.Result;
                    break;
                case "Branch":
                    prefab = _branchFlowPrefabHandler.Result;
                    break;
            }
            GameObject newFlowObject = Instantiate(prefab, new Vector3(flowData.PosX, flowData.PosY, 0), Quaternion.identity);
            Flow newFlow = newFlowObject.GetComponent<Flow>();
            newFlow.Data = flowData;
            newFlow.ShowData();
            newFlow.Init(flowData.ID, false);
            ChartData.Flows.Add(newFlow);
        }
        foreach(Flow flow in ChartData.Flows)
        {
            if(flow.Data.Next != null)
            {
                flow.Next = ChartData.Flows.Find(f => f.Data.ID == flow.Data.Next);
            }
            if (flow is BranchFlow bflow)
            {
                if (bflow.Data.Branch != null)
                {
                    bflow.Branch = ChartData.Flows.Find(f => f.Data.ID == flow.Data.Branch);
                }
            }
        }
        DrawConnectLines();
        yield return null;
    }

    void ViewModeHandler()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    selectedFlow = hit.collider.GetComponent<Flow>();
                    selectedFlow.ShowData();
                    uiController.OpenPropertyWindow(selectedFlow);
                }
                else
                {
                    if (selectedFlow != null)
                    {
                        // 細かすぎる動きには反応させない
                        if (Vector3.SqrMagnitude(selectedFlow.transform.position - hit.point) > 0.01f)
                        {
                            selectedFlow.MoveFlowTo(hit.point);
                            DrawConnectLines();
                        }
                    }
                }
            }
            else if (Input.GetMouseButtonDown(0) && selectedFlow != null)
            {
                selectedFlow = null;
                uiController.ClosePropertyWindow();
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            selectedFlow = null;
            DrawConnectLines();
        }
    }

    IEnumerator ConnectModeHandler()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                // 接続開始: selectedFlow -> targetFlow
                if (selectedFlow != null)
                {
                    Flow sourceFlow = selectedFlow;
                    Flow targetFlow = hit.collider.GetComponent<Flow>();

                    // 接続先がStartFlowの場合は接続できない
                    if (targetFlow is StartFlow)
                    {
                        uiController.ShowMessage("Error", "Cannot connect to StartFlow");
                    }
                    // 接続先が自分自身の場合は接続できない
                    else if (selectedFlow == targetFlow)
                    {
                        uiController.ShowMessage("Error", "Cannot connect to itself");
                    }
                    else
                    {
                        yield return sourceFlow.Connect(targetFlow);
                    }
                    DrawConnectLines();
                    sourceFlow.StopBlink();
                    selectedFlow = null;
                    yield break;
                }
                // 接続元を選択
                else
                {
                    Debug.Log("Source selected");
                    selectedFlow = hit.collider.GetComponent<Flow>();
                    selectedFlow.StartBlink();
                }
            }
            else
            {
                if (selectedFlow != null)
                {
                    Debug.Log("Cancel connection");
                    // 接続元以外をクリックした場合は選択を解除
                    selectedFlow.StopBlink();
                    selectedFlow.gameObject.GetComponent<SpriteRenderer>().color = Color.white; //元の色に戻す
                    selectedFlow = null;
                }
            }
        }
        yield break;
    }

    void SelectModeHandler()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                selectedFlow = hit.collider.GetComponent<Flow>();
                uiController.OpenSelectSkillUI(selectedFlow);
            }
        }
    }
}
