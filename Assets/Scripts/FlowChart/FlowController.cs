using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

public class FlowController : MonoBehaviour
{
    AsyncOperationHandle<GameObject> _skillFlowPrefabHandler;
    AsyncOperationHandle<GameObject> _branchFlowPrefabHandler;
    AsyncOperationHandle<GameObject> _startFlowPrefabHandler;
    [SerializeField] UIController uiController;
    List<Flow> flows; // this list control flows existing in this chart
    Flow selectedFlow; // this is the flow selected by user
    string startFlowID;

    private IEnumerator Start()
    {
        yield return Load();
        yield return SkillManager.Init();
        uiController.ToViewMode();
        flows = new();
        startFlowID = CreateStartFlow();
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
        yield break;
    }

    private string CreateStartFlow()
    {
        string newStructId = Guid.NewGuid().ToString("N"); // get struct id
        GameObject newFlowObject = Instantiate(_startFlowPrefabHandler.Result, new Vector3(-6, 2.5f, 0), Quaternion.identity);
        Flow newFlow = newFlowObject.GetComponent<Flow>();
        newFlow.Init(newStructId);
        flows.Add(newFlow);
        return newStructId;
    }

    public void CreateSkillFlow()
    {
        string newStructId = Guid.NewGuid().ToString("N"); // get struct id
        GameObject newFlowObject = Instantiate(_skillFlowPrefabHandler.Result, new Vector3(0, 0, 0), Quaternion.identity);
        SkillFlow newFlow = newFlowObject.GetComponent<SkillFlow>();
        newFlow.Init(newStructId);
        flows.Add(newFlow);
    }

    public void CreateBranchFlow()
    {
        string newStructId = Guid.NewGuid().ToString("N"); // get struct id
        GameObject newFlowObject = Instantiate(_branchFlowPrefabHandler.Result, new Vector3(0, 0, 0), Quaternion.identity);
        BranchFlow newFlow = newFlowObject.GetComponent<BranchFlow>();
        newFlow.Init(newStructId);
        flows.Add(newFlow);
    }

    public void DeleteFlow(Flow targetFlow)
    {
        flows.Remove(targetFlow);
        Destroy(targetFlow.gameObject);
    }

    void ViewModeHandler()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                selectedFlow = hit.collider.GetComponent<Flow>();
                uiController.OpenPropertyWindow(selectedFlow);
            }
            else
            {
                selectedFlow = null;
                uiController.ClosePropertyWindow();
            }
        }
    }

    void MoveModeHandler()
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
                }
                else if (Input.GetMouseButtonUp(0))
                {
                    selectedFlow = null;
                }
                else
                {
                    if (selectedFlow != null)
                    {
                        selectedFlow.transform.position = hit.point;
                    }
                }
            }
        }
    }
    void ConnectionModeHandler()
    {

    }

    private void Update()
    {
        switch (ChartMode.CurrentState)
        {
            case ChartMode.State.View:
                ViewModeHandler();
                MoveModeHandler(); 
                break;
            case ChartMode.State.Connection:
                ConnectionModeHandler();
                break;
        }
    }
}
