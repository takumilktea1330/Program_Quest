using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class SaveChartDataasJson
{
    public static void Save()
    {
        var wrapper = new Wrapper();
        foreach (var flow in ChartData.Flows)
        {
            Debug.Log("saving: " + flow.Data.Name);
            wrapper.Flows.Add(flow.Data);
        }
        string json = JsonUtility.ToJson(wrapper, true);
        File.WriteAllText("Assets/JsonData/ChartData.json", json);
        Debug.Log("Chart Data Saved as Json!");
    }

    public static List<FlowData> Load()
    {
        string json = File.ReadAllText("Assets/JsonData/ChartData.json");
        var wrapper = JsonUtility.FromJson<Wrapper>(json);
        if(wrapper == null)
        {
            Debug.Log("No data found!");
            return null;
        }
        Debug.Log("Chart Data Loaded from Json!");
        Debug.Log("Loaded " + wrapper.Flows.Count + " flows");
        return wrapper.Flows;
    }
}

[Serializable]
public class Wrapper
{
    public List<FlowData> Flows = new();
}
