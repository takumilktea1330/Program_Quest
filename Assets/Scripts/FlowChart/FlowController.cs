using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class FlowController : MonoBehaviour
{
    public GameObject Canvas;
    const int MAXARR = 256;
    AsyncOperationHandle<GameObject> _skillFlowPrefabHandler;
    AsyncOperationHandle<GameObject> _branchFlowPrefabHandler;
    List<Flow> flows; // this list control flows existing in this chart

    private void Start()
    {
        StartCoroutine(Load());
        flows = new();
    }

    // load flow prefabs from Assets/Prefabs
    private IEnumerator Load()
    {
        _skillFlowPrefabHandler = Addressables.LoadAssetAsync<GameObject>("Prefabs/SkillFlowPrefab");
        yield return _skillFlowPrefabHandler;
        _branchFlowPrefabHandler = Addressables.LoadAssetAsync<GameObject>("Prefabs/IfFlowPrefab");
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

    public void CreateBranchFlow() //add flow to the chart
    {
        string newStructId = Guid.NewGuid().ToString("N"); // get struct id
        BranchFlow newFlow;
        GameObject newFlowObject;

        Debug.Log("Begin to add if flow");
        newFlowObject = Instantiate(_branchFlowPrefabHandler.Result, new Vector3(0, 0, 0), Quaternion.identity);
        newFlow = newFlowObject.GetComponent<BranchFlow>();
        newFlow.Init(newStructId);
        flows.Add(newFlow);
    }

    public void DeleteFlow(Flow targetFlow)
    {
        flows.Remove(targetFlow);
        Destroy(targetFlow.gameObject);
    }
}
