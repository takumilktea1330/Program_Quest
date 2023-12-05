using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class IfObject : FlowChartObject
{
    public int HSize { get => TrueHSize + FalseHSize; }
    public int VSize { get => TrueVSize > FalseVSize ? TrueVSize : FalseVSize; }
    public int TrueHSize { get => GetTrueHSize(); }
    public int FalseHSize { get => GetFalseHSize(); }
    public int TrueVSize { get => GetTrueVSize(); }
    public int FalseVSize { get => GetFalseVSize(); }

    //条件付けに関するもの
    //条件の比較対象一覧
    enum ConditionMode
    {
        HP,
        MP,
        Turn,
        Attack,
        Defense,
    }

    public List<FlowChartObject> TrueList { get; set; }
    public List<FlowChartObject> FalseList { get; set; }
    int GetTrueHSize()
    {
        int trueHSize = 1;
        foreach (var item in TrueList)
        {
            if(item is IfObject)
            {
                if((item as IfObject).HSize > trueHSize)
                {
                    trueHSize = (item as IfObject).HSize;
                }
            }
            else if (item is WhileObject)
            {
                if ((item as WhileObject).HSize > trueHSize)
                {
                    trueHSize = (item as WhileObject).HSize;
                }
            }
        }
        return trueHSize;
    }
    int GetFalseHSize()
    {
        int falseHSize = 1;
        foreach (var item in FalseList)
        {
            if (item is IfObject)
            {
                if ((item as IfObject).HSize > falseHSize)
                {
                    falseHSize = (item as IfObject).HSize;
                }
            }
            else if (item is IfObject)
            {
                if ((item as WhileObject).HSize > falseHSize)
                {
                    falseHSize = (item as WhileObject).HSize;
                }
            }
        }
        return falseHSize;
    }
    int GetTrueVSize()
    {
        int trueVSize = 1;
        foreach (var item in TrueList)
        {
            if (item is IfObject)
            {
                trueVSize += (item as IfObject).VSize;
            }
            else if (item is WhileObject)
            {
                trueVSize += (item as WhileObject).VSize;
            }
            else trueVSize++;
        }
        return trueVSize;
    }
    int GetFalseVSize()
    {
        int falseVSize = 1;
        foreach (var item in FalseList)
        {
            if (item is IfObject)
            {
                falseVSize += (item as IfObject).VSize;
            }
            else if (item is WhileObject)
            {
                falseVSize += (item as WhileObject).VSize;
            }
            else falseVSize++;
        }
        return falseVSize;
    }
    public IfObject(): base()
    {
        Name = "IfObject";
        Type = "If";
        ExecutionTime = 1;
        Explain = "If condition is true, execute flow below. If not, execute another one.";
        OriginalPrefab = Resources.Load<GameObject>("Prefabs/FlowChart/IfPrefab");
        TrueList = new List<FlowChartObject>();
        FalseList = new List<FlowChartObject>();

        Init();
    }
}