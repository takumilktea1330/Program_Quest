using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SkillFlow : MonoBehaviour
{
    [SerializeField]List<FlowChartObject> flow = new();
    [SerializeField]List<FlowChartObject> objectsList = new();

    public List<FlowChartObject> Flow { get => flow; set => flow = value; }
    public List<FlowChartObject> ObjectsList { get => objectsList; set => objectsList = value; }
}
