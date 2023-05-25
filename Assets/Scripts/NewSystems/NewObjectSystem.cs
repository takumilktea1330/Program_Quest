using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewObjectSystem : MonoBehaviour
{
    [SerializeField] PlayerController player;
    List<FlowChartObject> objects = new List<FlowChartObject>();
    List<IfObject> ifObjects = new List<IfObject>();

    GameObject[] objectPrefabs;
    List<int> existIds = new List<int>();
    int[] alreadyCalculatedTrue;
    int[] alreadyCalculatedFalse; //もう計算したものはもう良くない？の気持ち

    public void AddNewObject(FlowChartObject obj)//どこに？は必要そう
    {
        objects.Add(obj);
        if (obj is IfObject)
        {
            ifObjects.Add((IfObject)obj);
        }
    }

    void RemoveObject(int code)
    {
        objects.RemoveAt(code);
    }

    void Display()
    {
        //Listから全てをInstantiateして表示する
        //実験的
        int count = 0;
        for (int i = 0; i < objects.Count; i++)
        {
            FlowChartObject obj = objects[i];
            if (obj.Prefab != null)
            {
                objectPrefabs[count] = Instantiate(obj.Prefab, new Vector3(0, -1.5f * count + 1.0f, 0), Quaternion.identity);
                count++;
            }
        }
        alreadyCalculatedTrue = new int[Constant.MAX_IF_OBJECTS];
        alreadyCalculatedFalse = new int[Constant.MAX_IF_OBJECTS];
        foreach (int id in existIds)
        {
            foreach (IfObject obj in ifObjects)
            {
                if (obj.Id == id)
                {
                    obj.TrueSize = GetTrueSize(id);
                    obj.FalseSize = GetFalseSize(id);
                    Debug.Log("id:" + id + " true:" + obj.TrueSize + " false:" + obj.FalseSize);
                }
            }
        }
    }

    //ifで囲まれたlistをもらって、その中の最大のifのsizeを返す

    int GetSize(List<IfObject> list, int id)
    {
        //sizeは最大値の判定にのみ使用します。
        int size = 1;
        foreach (IfObject item in list)
        {
            if (item.Id == id)
            {
                return size;
            }
            else if (item is IfTrueObject)
            {
                if (size < item.Size)
                {
                    item.TrueSize = GetTrueSize(item.Id);
                    item.FalseSize = GetFalseSize(item.Id);
                    size = item.Size;
                }
            }
        }
        Debug.Log("error");
        alreadyCalculatedTrue[id] = -1;
        alreadyCalculatedFalse[id] = -1;
        return -1;
    }
    int GetTrueSize(int id)
    {
        if (alreadyCalculatedTrue[id] != 0)
        {
            return alreadyCalculatedTrue[id];
        }
        alreadyCalculatedTrue[id] = GetSize(GetListTrueToFalse(id), id);
        return alreadyCalculatedTrue[id];
    }
    int GetFalseSize(int id)
    {
        if (alreadyCalculatedFalse[id] != 0)
        {
            return alreadyCalculatedFalse[id];
        }
        alreadyCalculatedFalse[id] = GetSize(GetListFalseToEnd(id), id);
        return alreadyCalculatedFalse[id];
    }

    List<IfObject> GetListTrueToFalse(int id)
    {
        int start = 0;

        for (int i = 0;i<ifObjects.Count;i++)
        {
            if (ifObjects[i] is IfTrueObject && ifObjects[i].Id == id)
            {
                start = i;
            }
            else if (ifObjects[i] is IfFalseObject && ifObjects[i].Id == id)
            {
                int end = i;
                int size = end - start;
                //最初のifは含まないlistを返す
                Debug.Log("id:"+ id + " start:" + start + " end:" + end + " size:" + size);
                return ifObjects.GetRange(start+1, size);
            }
        }
        Debug.Log("error");
        return null;
    }
    List<IfObject> GetListFalseToEnd(int id)
    {
        int start = 0;

        for (int i = 0; i < ifObjects.Count; i++)
        {
            if (ifObjects[i] is IfFalseObject && ifObjects[i].Id == id)
            {
                start = i;
            }
            else if (ifObjects[i] is IfEndObject && ifObjects[i].Id == id)
            {
                int end = i;
                int size = end - start;
                //最初のifは含まないlistを返す
                Debug.Log("id:"+ id +" start:" + start + " end:" + end + " size:" + size);
                return ifObjects.GetRange(start+1, size);
            }
        }
        Debug.Log("error");
        return null;
    }
    void ResizeFlow()
    {
        //ifのsizeに応じてFlowのサイズを変更する
    }

    void Reload()
    {
        foreach (GameObject obj in objectPrefabs)
        {
            Destroy(obj);
        }
    }

    void Start()
    {
        Init();
        Test();
        Display();
    }
    void Init()
    {
        objectPrefabs = new GameObject[Constant.MAX_OBJECTS];
    }
    void Test()
    {
        AddNewObject(new IfTrueObject(3));
        AddNewObject(new IfTrueObject(4));
        AddNewObject(new IfFalseObject(4));
        AddNewObject(new IfTrueObject(2));
        AddNewObject(new IfFalseObject(2));
        AddNewObject(new IfEndObject(2));
        AddNewObject(new IfEndObject(4));
        AddNewObject(new IfFalseObject(3));
        AddNewObject(new IfTrueObject(1));
        AddNewObject(new IfFalseObject(1));
        AddNewObject(new IfEndObject(1));
        AddNewObject(new IfEndObject(3));
        //idは統一したものを使います
        existIds.Add(1);
        existIds.Add(2);
        existIds.Add(3);
        existIds.Add(4);
    }
}