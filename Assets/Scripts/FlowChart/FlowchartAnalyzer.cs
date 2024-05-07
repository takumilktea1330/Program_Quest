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
        Analyze(startFlow.Next);

        void Analyze(Flow currentFlow)
        {
            if(currentFlow == null)
            {
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
                Analyze(branchFlow.Next);
                Analyze(branchFlow.Branch);
            }
            else
            {

            }
        }
    }
}
