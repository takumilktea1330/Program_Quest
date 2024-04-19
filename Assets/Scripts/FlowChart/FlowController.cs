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
    Dropdown modeDropdown;
    List<Flow> flows; // this list control flows existing in this chart
    Flow selectedFlow; // this is the flow selected by user

    public enum State
    {
        View,
        Move,
        Connection,
    }

    private void Start()
    {
        StartCoroutine(Load());
        StartCoroutine(SkillManager.Init());
        modeDropdown = GameObject.Find("ModeDropdown").GetComponent<Dropdown>();
        state = State.View;
        flows = new();
    }

    public State state;
    public void ModeChange()
    {
        switch (modeDropdown.value)
        {
            case 0:
                ToViewMode();
                break;
            case 1:
                ToMoveMode();
                break;
            case 2:
                ToConnectMode();
                break;
        }
    }
    public void ToViewMode()
    {
        state = State.View;
    }
    public void ToMoveMode()
    {
        state = State.Move;
    }
    public void ToConnectMode()
    {
        state = State.Connection;
    }

    // load flow prefabs from Assets/Prefabs
    private IEnumerator Load()
    {
        _skillFlowPrefabHandler = Addressables.LoadAssetAsync<GameObject>("Prefabs/SkillFlowPrefab");
        yield return _skillFlowPrefabHandler;
        _branchFlowPrefabHandler = Addressables.LoadAssetAsync<GameObject>("Prefabs/BranchFlowPrefab");
        yield return _branchFlowPrefabHandler;
        yield break;
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

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (state == State.Move)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    selectedFlow = hit.collider.GetComponent<Flow>();
                    selectedFlow.ShowData();
                }
                if (Input.GetMouseButton(0))
                {
                    if (selectedFlow != null)
                    {
                        selectedFlow.transform.position = hit.point;
                    }
                }
                if (Input.GetMouseButtonUp(0))
                {
                    selectedFlow = null;
                }
            }
        }
    }
}
