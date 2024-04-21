using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;


public class Flow : MonoBehaviour
{
    public FlowData Data;
    protected GameObject canvas; 
    //PropertyWindow propertyWindow;
    protected LineRenderer line = null;
    protected Flow Next;

    Color blinkColor;

    public virtual void Init(string id)
    {
        Data = new()
        {
            ID = id,
        };
        canvas = GameObject.Find("MainCanvas");
        //propertyWindow = canvas.transform.Find("PropertyWindow").GetComponent<PropertyWindow>();
        blinkColor = Color.white;
        blinkColor.a = 0.5f;
    }

    public virtual void Display()
    {
    }

    public virtual void ShowData()
    {
        Debug.Log($"This is Flow(ID: {Data.ID}) Data\n" +
        $"Position: {transform.position}");
    }

    public virtual void Connect(Flow flow)
    {
        
    }

    public IEnumerator Blink()
    {
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        while (true)
        {
            spriteRenderer.color = blinkColor;
            yield return new WaitForSeconds(0.5f);
            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(0.5f);
        }
    }

    public virtual void DrowConnectLine(AsyncOperationHandle<GameObject> _connectLinePrefabHandler)
    {
        Debug.Log("Flow DrowConnectLine");
    }

    public void DestroyConnectLine()
    {
        Destroy(line);
    }
}