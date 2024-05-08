using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FlowchartAnalyzer : MonoBehaviour
{
    [SerializeField] TMP_Text resultText;
    [SerializeField] TMP_Text messageTitle;
    [SerializeField] TMP_Text messageText;

    public void StartAnalysing()
    {
        List<FlowData> flowDatasToAnalyze = SaveChartDataasJson.Load();
        List<FlowToAnalyze> flowsToAnalyze = Convert(flowDatasToAnalyze);
        FlowToAnalyze startFlow = flowsToAnalyze.Find(flow => flow.Data.Type == "Start");
        if(startFlow == null)
        {
            messageTitle.text = "Error";
            messageText.text = "Start flow not found!";
            return;
        }
        messageTitle.text = "Processing...";
        messageText.text = "Flowchart analysis in progress...";
        Analyze(startFlow.Next);

        void Analyze(FlowToAnalyze currentFlow, int depth = 0)
        {
            if(currentFlow == null)
            {
                return;
            }
            if (currentFlow.Data.Type == "Skill")
            {
                resultText.text += $"SkillFlow: {currentFlow.Data.Name}\n";
                Analyze(currentFlow.Next, depth);
            }
            else if (currentFlow.Data.Type == "Branch")
            {
                resultText.text += $"BranchFlow: {currentFlow.Data.Name}\n";
                Analyze(currentFlow.Next, depth+1);
                Analyze(currentFlow.Branch, depth+1);
            }
            else
            {
                messageTitle.text = "Error";
                messageText.text = "Unknown flow type detected!";
            }
        }
        messageTitle.text = "Success";
        messageText.text = "Flowchart analysis completed!";
    }
    private List<FlowToAnalyze> Convert(List<FlowData> flowDatas)
    {
        List<FlowToAnalyze> flows = new();
        foreach (FlowData flowData in flowDatas)
        {
            FlowToAnalyze flow = new()
            {
                Data = flowData,
                Next = null,
                Branch = null
            };
            flows.Add(flow);
        }
        foreach (FlowToAnalyze flow in flows)
        {
            if(flow.Data.Next != null)
            {
                flow.Next = flows.Find(f => f.Data.ID == flow.Data.Next);
            }
            if(flow.Data.Branch != null)
            {
                flow.Branch = flows.Find(f => f.Data.ID == flow.Data.Branch);
            }
        }
        return flows;
    }

    private class FlowToAnalyze
    {
        public FlowData Data { get; set; }
        public FlowToAnalyze Next { get; set; }
        public FlowToAnalyze Branch { get; set; }
    }
}
