using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Chart : MonoBehaviour
{
    const int MAXARR = 256;
    GameObject skillPrefab;
    GameObject ifPrefab;
    Flow[] flows = new Flow[MAXARR]; // array to control flow exists in this chart
    List<int> unusedIds = Enumerable.Range(0, MAXARR).ToList(); // a list to get unique struct id

    private void Start()
    {
        // load prefabs from Assets/Resources
        skillPrefab = Resources.Load<GameObject>("Prefabs/FlowChart/SkillPrefab");
        ifPrefab = Resources.Load<GameObject>("Prefabs/FlowChart/IfPrefab");

        //for test it will be deleted
        Test();
    }

    //for test it will be deleted
    private void Test()
    {
        Debug.Log("This is a test program. If it is not test, delete or comment out Test() from Start()");
        AddFlow("skill");
    }


    public void AddFlow(string type) //add flow to the chart
    {
        int newStructId = GenStructId(); // get struct id

        if(newStructId == -1) // non-unique struct id exception
        {
            Debug.Log($"Adding Flow({type}) is canceled");
            return;
        }

        Flow newFlow;
        GameObject newFlowObject;
        switch (type)
        {
            case "skill":
                Debug.Log("Begin to add skill flow");
                newFlowObject = Instantiate(skillPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                newFlow = newFlowObject.GetComponent<Flow>();
                newFlow.Init(newStructId, type);
                newFlow.SelectSkill();
                break;
            case "if":
                Debug.Log("Begin to add if flow");
                newFlowObject = Instantiate(ifPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                newFlow = newFlowObject.GetComponent<Flow>();
                newFlow.Init(newStructId, type);
                newFlow.SetCondition();
                break;
            default:
                Debug.Log("Argument Exception occured at AddFlow in Chart.cs");
                ReturnStructId(newStructId);
                Debug.Log($"Adding Flow({type}) is canceled");
                return;
        }
        flows[newStructId] = newFlow;
        Debug.Log("Adding Flow is accomplished!");
    }

    private void ReturnStructId(int id)
    {
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
