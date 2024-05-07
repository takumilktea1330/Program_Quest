using System;
using System.Collections;
using System.Collections.Generic;
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
    IEnumerator blinkCoroutine;

    private IEnumerator Start()
    {
        yield return Load();
        yield return SkillManager.Init();
        uiController.ToViewMode();
        ChartData.Flows = new List<Flow>();
        ChartData.StartFlowID = CreateStartFlow();

        yield break;
    }



    // load flow prefabs from Assets/Prefabs
    private IEnumerator Load()
    {
        _startFlowPrefabHandler = Addressables.LoadAssetAsync<GameObject>("Prefabs/StartFlowPrefab");
        yield return _startFlowPrefabHandler;
        _skillFlowPrefabHandler = Addressables.LoadAssetAsync<GameObject>("Prefabs/SkillFlowPrefab");
        yield return _skillFlowPrefabHandler;
        _branchFlowPrefabHandler = Addressables.LoadAssetAsync<GameObject>("Prefabs/BranchFlowPrefab");
        yield return _branchFlowPrefabHandler;
        _connectLinePrefabHandler = Addressables.LoadAssetAsync<GameObject>("Prefabs/ConnectLinePrefab");
        yield return _connectLinePrefabHandler;
        yield break;
    }


    private string CreateStartFlow()
    {
        string newStructId = Guid.NewGuid().ToString("N"); // get struct id
        GameObject newFlowObject = Instantiate(_startFlowPrefabHandler.Result, new Vector3(-6, 2.5f, 0), Quaternion.identity);
        Flow newFlow = newFlowObject.GetComponent<Flow>();
        newFlow.Init(newStructId);
        ChartData.Flows.Add(newFlow);
        return newStructId;
    }

    public void CreateSkillFlow()
    {
        string newStructId = Guid.NewGuid().ToString("N"); // get struct id
        GameObject newFlowObject = Instantiate(_skillFlowPrefabHandler.Result, new Vector3(0, 0, 0), Quaternion.identity);
        SkillFlow newFlow = newFlowObject.GetComponent<SkillFlow>();
        newFlow.Init(newStructId);
        ChartData.Flows.Add(newFlow);
    }

    public void CreateBranchFlow()
    {
        string newStructId = Guid.NewGuid().ToString("N"); // get struct id
        GameObject newFlowObject = Instantiate(_branchFlowPrefabHandler.Result, new Vector3(0, 0, 0), Quaternion.identity);
        BranchFlow newFlow = newFlowObject.GetComponent<BranchFlow>();
        newFlow.Init(newStructId);
        ChartData.Flows.Add(newFlow);
    }

    public void DeleteFlow(Flow targetFlow)
    {
        ChartData.Flows.Remove(targetFlow);
        Destroy(targetFlow.gameObject);
    }

    public void DrawConnectLines()
    {
        foreach (Flow flow in ChartData.Flows)
        {
            flow.DrawConnectLine(_connectLinePrefabHandler);
        }
    }

    void ViewModeHandler()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
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
                        if(Vector3.SqrMagnitude(selectedFlow.transform.position - hit.point) > 0.01f)
                        {
                            selectedFlow.transform.position = hit.point;
                            //DestroyConnectLines(); //最適化できそう
                            DrawConnectLines(); //最適化できそう
                        }
                    }
                }
            }
            else if(Input.GetMouseButtonDown(0) && selectedFlow != null)
            {
                selectedFlow = null;
                uiController.ClosePropertyWindow();
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            selectedFlow = null;
            //DestroyConnectLines();
            DrawConnectLines();
        }
    }
    void ConnectModeHandler()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                // 接続開始: selectedFlow -> targetFlow
                if(selectedFlow != null)
                {
                    // 接続先がStartFlowの場合は接続できない
                    Flow targetFlow = hit.collider.GetComponent<Flow>();
                    Debug.Log("Start connection");
                    if (targetFlow is StartFlow)
                    {
                        Debug.Log("Cannot connect to StartFlow");

                    }
                    // 接続先が自分自身の場合は接続できない
                    else if(selectedFlow == targetFlow)
                    {
                        Debug.Log("Cannot connect to itself");
                    }
                    else
                    {
                        selectedFlow.Connect(targetFlow);
                        Debug.Log($"Connected {selectedFlow.Data.ID} -> {targetFlow.Data.ID}");
                    }
                    Debug.Log("End connection");
                    if(blinkCoroutine != null)StopCoroutine(blinkCoroutine);
                    DrawConnectLines();
                    selectedFlow.gameObject.GetComponent<SpriteRenderer>().color = Color.white; //元の色に戻す
                    selectedFlow = null;
                    return;
                }
                // 接続元を選択
                else
                {
                    Debug.Log("Source selected");
                    selectedFlow = hit.collider.GetComponent<Flow>();
                    blinkCoroutine = selectedFlow.Blink();
                    StartCoroutine(blinkCoroutine);
                }
            }
            else
            {
                if(selectedFlow != null)
                {
                    Debug.Log("Cancel connection");
                    // 接続元以外をクリックした場合は選択を解除
                    if (blinkCoroutine != null) StopCoroutine(blinkCoroutine);
                    selectedFlow.gameObject.GetComponent<SpriteRenderer>().color = Color.white; //元の色に戻す
                    selectedFlow = null;
                }
            }
        }
    }

    private void Update()
    {
        switch (ChartMode.CurrentState)
        {
            case ChartMode.State.View:
                ViewModeHandler(); 
                break;
            case ChartMode.State.Connection:
                ConnectModeHandler();
                break;
        }
    }
}
