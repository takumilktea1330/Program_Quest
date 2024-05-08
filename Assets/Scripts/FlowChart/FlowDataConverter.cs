using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FlowDataConverter
{
    public static Flow Convert(FlowData data)
    {
        Flow flow;
        if(data.Type == "Start")
        {
            flow = new StartFlow();
        }
        else if(data.Type == "Skill")
        {
            flow = new SkillFlow();
        }
        else if(data.Type == "Branch")
        {
            flow = new BranchFlow();
        }
        else
        {
            Debug.Log("Unknown Flow Type!");
            return null;
        }
        flow.Data = data;
        return flow;
    }
}
