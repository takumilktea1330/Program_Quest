using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class FlowChartObject
{
    public string Name { get; set; }
    public string Type { get; set; }
    public int ExecutionTime { get; set; }
    public string Explain { get; set; }
    public List<FlowChartObject> Parent { get; set; }
    private GameObject prefab;

    public GameObject Prefab
    {
        get
        {
            if (prefab == null)
            {
                prefab = OriginalPrefab;
            }
            return prefab;
        }
        set
        {
            prefab = value;
        }
    }


    public GameObject OriginalPrefab;
    public Text DispText{ get => GetText();}
    private Text GetText()
    {
        Transform canv = Prefab.transform.Find("Canvas");
        if (canv != null)
        {
            Transform textobj = canv.Find("Text");
            if (textobj != null)
            {
                return textobj.GetComponent<Text>();
            }
        }
        return null;
    }
    public Vector3 Place { get; set; }
    public virtual void Init(){}
}
