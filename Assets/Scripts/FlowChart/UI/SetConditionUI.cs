using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class SetConditionUI : MonoBehaviour
{
    UIController uiController;
    Flow targetFlow;


    public IEnumerator Init()
    {
        yield return uiController = transform.parent.GetComponent<UIController>();
        Debug.Log("SetConditionUI: Initialized!");
        //Close();
    }


    public void Open(Flow targetFlow)
    {
        gameObject.SetActive(true);
        uiController.ToProcessingMode();
        this.targetFlow = targetFlow;
    }

    void SetSkill(Skill skill)
    {
        targetFlow.Data.Name = skill.Name;
        targetFlow.Display();
        uiController.ToViewMode();
        Close();
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
}
