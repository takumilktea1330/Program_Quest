using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class Chart : MonoBehaviour
{
    public GameObject Canvas;
    const int MAXARR = 256;
    AsyncOperationHandle<GameObject> skillFlowPrefabHandler;
    AsyncOperationHandle<GameObject> ifFlowPrefabHandler;
    Flow[] flows = new Flow[MAXARR]; // array to control flow exists in this chart
    List<int> unusedIds = Enumerable.Range(0, MAXARR).ToList(); // a list to get unique struct id

    private void Start()
    {
        StartCoroutine(Load());
    }

    // load flow prefabs from Assets/Prefabs
    private IEnumerator Load()
    {
        skillFlowPrefabHandler = Addressables.LoadAssetAsync<GameObject>("Prefabs/SkillFlowPrefab");
        yield return skillFlowPrefabHandler;
        ifFlowPrefabHandler = Addressables.LoadAssetAsync<GameObject>("Prefabs/IfFlowPrefab");
        yield return ifFlowPrefabHandler;
        yield break;
    }

    public void AddSkillFlow()
    {
        int newStructId; // get struct id
        try
        {
            newStructId = GetStructId();
        }
        catch (Exception)
        {
            Debug.Log("Adding SkillFlow is canceled");
            return;
        }
        SkillFlow newFlow;
        GameObject newFlowObject;

        Debug.Log("Begin to add skill flow");
        newFlowObject = Instantiate(skillFlowPrefabHandler.Result, new Vector3(0, 0, 0), Quaternion.identity);
        newFlow = newFlowObject.GetComponent<SkillFlow>();
        newFlow.Init(newStructId, "skill");

        newFlow.SelectSkill();

        flows[newStructId] = newFlow;
    }

    public void AddIfFlow(string type) //add flow to the chart
    {
        int newStructId; // get struct id
        try
        {
            newStructId = GetStructId();
        }
        catch (Exception)
        {
            Debug.Log("Adding IfFlow is canceled");
            return;
        }

        IfFlow newFlow;
        GameObject newFlowObject;

        Debug.Log("Begin to add if flow");
        newFlowObject = Instantiate(ifFlowPrefabHandler.Result, new Vector3(0, 0, 0), Quaternion.identity);
        newFlow = newFlowObject.GetComponent<IfFlow>();
        newFlow.Init(newStructId, type);

        flows[newStructId] = newFlow;
    }

    public void DeleteFlow(Flow targetFlow)
    {
        ReturnStructId(targetFlow.Data.StructId);
        Destroy(targetFlow.gameObject);
    }

    private void ReturnStructId(int id)
    {
        flows[id] = null;
        unusedIds.Add(id);
    }
    
    private int GetStructId()
    {
        if(unusedIds.Count == 0)
        {
            throw new Exception();
        }
        int newId = unusedIds[0];
        unusedIds.RemoveAt(0);
        return newId;
    }

}
