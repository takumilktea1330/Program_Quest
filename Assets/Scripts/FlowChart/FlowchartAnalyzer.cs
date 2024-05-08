using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FlowchartAnalyzer : MonoBehaviour
{
    [SerializeField] TMP_Text resultText;
    [SerializeField] TMP_Text messageText;

    public void StartAnalysing()
    {
        List<FlowData> flowDatasToAnalyze = SaveChartDataasJson.Load();
        List<FlowToAnalyze> flowsToAnalyze = Convert(flowDatasToAnalyze);
        List<FlowToAnalyze> flowsAnalyzed = new();
        FlowToAnalyze startFlow = flowsToAnalyze.Find(flow => flow.Data.Type == "Start");
        if(startFlow == null)
        {
            messageText.text = "Error: Start flow not found\n";
            return;
        }
        messageText.text = "Processing: Flowchart analysis in progress...\n";
        resultText.text = "";
        analyze(startFlow.Next);

        void analyze(FlowToAnalyze currentFlow, int depth = 0)
        {
            if(currentFlow == null)
            {
                return;
            }
            if(flowsAnalyzed.Contains(currentFlow))
            {
                resultText.text += $"^ Loop detected at\n";
                messageText.text += "Error: Loop detected\n";
                return;
            }
            else
            {
                flowsAnalyzed.Add(currentFlow);
            }
            if (currentFlow.Data.Type == "Skill")
            {
                insertBlank(depth);
                resultText.text += $"> {currentFlow.Data.Name}\n";
                analyze(currentFlow.Next, depth);
            }
            else if (currentFlow.Data.Type == "Branch")
            {
                insertBlank(depth);
                resultText.text += $"> If: {currentFlow.Data.Name}\n";
                analyze(currentFlow.Next, depth+1);
                insertBlank(depth);
                resultText.text += $"> Else: {currentFlow.Data.Name}\n";
                analyze(currentFlow.Branch, depth+1);
            }
            else
            {
                messageText.text += "Error: Unknown flow type detected\n";
                return;
            }
        }
        
        void insertBlank(int depth)
        {
            for (int i = 0; i < depth; i++)
            {
                resultText.text += "  ";
            }
        }
        messageText.text += "Finished: Flowchart analysis completed\n";
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
