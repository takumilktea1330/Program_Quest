using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhileObject : FlowChartObject
{
    public int VSize { get => GetVSize(); }
    public int HSize { get => GetHSize(); }

    public List<FlowChartObject> Children { get; set; }
    int GetVSize()
    {
        int vSize = 1;
        foreach (var item in Children)
        {
            if (item is IfObject)
            {
                vSize += (item as IfObject).VSize;
            }
            else if (item is WhileObject)
            {
                vSize += (item as WhileObject).VSize;
            }
            else vSize++;
        }
        return vSize;
    }
    int GetHSize()
    {
        int hSize = 1;
        foreach (var item in Children)
        {
            if (item is IfObject)
            {
                if ((item as IfObject).HSize > hSize)
                {
                    hSize = (item as IfObject).HSize;
                }
                else if ((item as WhileObject).HSize > hSize)
                {
                    hSize = (item as WhileObject).HSize;
                }
            }
        }
        return hSize;
    }
    public WhileObject() : base()
    {
        Name = "WhileObject";
        Type = "While";
        ExecutionTime = 0;
        Explain = "The following flows are executed as long as the conditions are true.";
        OriginalPrefab = Resources.Load<GameObject>("Prefabs/FlowChart/WhilePrefab");
        Children = new List<FlowChartObject>();
        Init();
    }
}