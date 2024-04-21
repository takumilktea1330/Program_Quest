using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class Flow : MonoBehaviour
{
    public FlowData Data;
    protected GameObject canvas; 
    PropertyWindow propertyWindow;

    public virtual void Init(string id)
    {
        Data = new()
        {
            StructId = id,
        };
        canvas = GameObject.Find("MainCanvas");
        propertyWindow = canvas.transform.Find("PropertyWindow").GetComponent<PropertyWindow>();
    }

    public virtual void Display()
    {
    }

    public virtual void ShowData()
    {
        Debug.Log($"This is Flow(ID: {Data.StructId}) Data\n" +
        $"Position: {transform.position}");
    }
}