using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlowChartObject
{
    public string Name { get; set; }
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
    public virtual void Init() { }
    public FlowChartObject()
    {
    }
}
