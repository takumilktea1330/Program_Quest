using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class Chart : MonoBehaviour
{
    [SerializeField] SelectSkillUI selectSkillUI;
    [SerializeField] AddFlowUI addFlowUI;
    const int MAXARR = 256;
    AsyncOperationHandle<GameObject> skillFlowPrefabHandler;
    AsyncOperationHandle<GameObject> ifFlowPrefabHandler;
    Flow[] flows = new Flow[MAXARR]; // array to control flow exists in this chart
    List<int> unusedIds = Enumerable.Range(0, MAXARR).ToList(); // a list to get unique struct id

    private void Start()
    {
        StartCoroutine(Load());
        selectSkillUI.OnSettingSkillEnded += ToAddFlow;
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


    public void AddFlow(string type) //add flow to the chart
    {
        int newStructId = GenStructId(); // get struct id

        if(newStructId == -1) // non-unique struct id exception
        {
            Debug.Log($"Ovewflow Exception: Adding Flow({type}) is canceled");
            return;
        }

        Flow newFlow;
        GameObject newFlowObject;
        switch (type)
        {
            case "skill":
                Debug.Log("Begin to add skill flow");
                newFlowObject = Instantiate(skillFlowPrefabHandler.Result, new Vector3(0, 0, 0), Quaternion.identity);
                newFlow = newFlowObject.GetComponent<Flow>();
                newFlow.Init(newStructId, type);
                newFlow.OnSelectSkill += ToSelectSkill;
                newFlow.SelectSkill();
                break;
            case "if":
                Debug.Log("Begin to add if flow");
                newFlowObject = Instantiate(ifFlowPrefabHandler.Result, new Vector3(0, 0, 0), Quaternion.identity);
                newFlow = newFlowObject.GetComponent<Flow>();
                newFlow.Init(newStructId, type);
                //newFlow.SetCondition();
                break;
            default:
                Debug.Log($"Argument Exception occured at AddFlow() in Chart.cs\n" + "Adding Flow({type}) is canceled");
                ReturnStructId(newStructId);
                return;
        }
        flows[newStructId] = newFlow;
    }

    public void DeleteFlow(Flow targetFlow)
    {
        ReturnStructId(targetFlow.Data.StructId);
        Destroy(targetFlow.gameObject);
    }

    void ToSelectSkill(Flow flow)
    {

        selectSkillUI.Open(flow);
        addFlowUI.Close();
    }

    void ToAddFlow()
    {
        selectSkillUI.Close();
        addFlowUI.Open();

    }

    private void ReturnStructId(int id)
    {
        flows[id] = null;
        unusedIds.Add(id);
    }
    
    private int GenStructId()
    {
        if(unusedIds.Count == 0)
        {
            Debug.Log("cannot allocate unique id due to overflow");
            return -1;
        }
        int newId = unusedIds[0];
        unusedIds.RemoveAt(0);
        return newId;
    }

}
