using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class SelectSkillUI : MonoBehaviour
{
    List<SkillManager.SkillKind> SkillList;
    List<GameObject> Buttons;
    [SerializeField] GameObject content;
    AsyncOperationHandle<GameObject> SkillButtonPrefabHandle;
    Flow targetFlow;

    public UnityAction OnSettingSkillEnded;
    
    private void Start()
    {
        Buttons = new();
        SkillList = new();
        StartCoroutine(Load());
        Close();
        Test();
    }

    private IEnumerator Load()
    {
        SkillButtonPrefabHandle = Addressables.LoadAssetAsync<GameObject>("Prefabs/SkillButtonPrefab");
        yield return SkillButtonPrefabHandle;
        yield break;
    }
    
    private void Test()
    {
        SkillList.Add(SkillManager.SkillKind.Patel_Crystalify);
        SkillList.Add(SkillManager.SkillKind.Felyca_Defense);
        SkillList.Add(SkillManager.SkillKind.Cryptical_Heal);
    }

    public void Open(Flow targetFlow)
    {
        targetFlow.ShowData();
        if (targetFlow == null) return; 
        this.targetFlow = targetFlow;
        gameObject.SetActive(true);
        foreach(SkillManager.SkillKind skillKind in SkillList)
        {
            GameObject newSkillButton = Instantiate(SkillButtonPrefabHandle.Result, Vector3.zero, quaternion.identity);
            Buttons.Add(newSkillButton);
            newSkillButton.transform.SetParent(content.transform, true); // contentの子にbuttonを追加
            Button button = newSkillButton.GetComponent<Button>();
            Text text = newSkillButton.GetComponentInChildren<Text>();
            text.text = skillKind.ToString();

            button.onClick.AddListener(() => SetSkill(skillKind)); // press button to set skill
            button.onClick.AddListener(targetFlow.Display); // press button to display flow name
            button.onClick.AddListener(Close); //press button to close SelectSkillsUI
        }
    }

    public void SetSkill(SkillManager.SkillKind skillKind)
    {
        targetFlow.Data.SkillName = skillKind.ToString();
        OnSettingSkillEnded.Invoke();
    }

    public void Close()
    {
        if (targetFlow != null) targetFlow.ShowData();
        foreach (GameObject button in Buttons) Destroy(button);
        Buttons.Clear();
        targetFlow = null;
        gameObject.SetActive(false);
    }
}
