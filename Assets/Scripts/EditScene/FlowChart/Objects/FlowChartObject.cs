using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlowChartObject
{
    public string Name { get; set; }
    public List<FlowChartObject> Parent { get; set; }
    public GameObject Prefab {
         get => Prefab == null ? OriginalPrefab : Prefab; 
         set => Prefab = value; 
         }
         
    protected GameObject OriginalPrefab;
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
