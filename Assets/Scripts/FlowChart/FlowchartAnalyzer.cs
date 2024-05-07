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
        //beginnning at start flow
        Flow startFlow = ChartData.Flows.Find(flow => flow.Data.ID == ChartData.StartFlowID);
        if(startFlow == null)
        {
            messageTitle.text = "Error";
            messageText.text = "Start flow not found!";
            return;
        }
        resultText.text = $"----- Beginning of flowchart -----\n";
        messageTitle.text = "Processing...";
        messageText.text = "Flowchart analysis in progress...";
        Analyze(startFlow.Next);


        void Analyze(Flow currentFlow, int depth = 0)
        {
            if(currentFlow == null)
            {
                resultText.text += "----- End of flowchart -----\n";
                messageTitle.text = "Success";
                messageText.text = "Flowchart analysis completed!";
                return;
            }
            if (currentFlow is SkillFlow skillFlow)
            {
                resultText.text += $"SkillFlow: {skillFlow.Data.Name}\n";
                Analyze(currentFlow.Next);
            }
            else if (currentFlow is BranchFlow branchFlow)
            {
                resultText.text += $"BranchFlow: {branchFlow.Data.Name}\n";
                Analyze(branchFlow.Next, depth+1);
                Analyze(branchFlow.Branch, depth+1);
            }
            else
            {
                messageTitle.text = "Error";
                messageText.text = "Unknown flow type detected!";
            }
        }
    }
}
