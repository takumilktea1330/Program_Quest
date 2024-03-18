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
    [SerializeField] SkillManager skillManager;
    List<GameObject> Buttons;
    [SerializeField] GameObject content;
    AsyncOperationHandle<GameObject> SkillButtonPrefabHandle;
    Flow targetFlow;

    public UnityAction OnSettingSkillEnded;
    
    private void Start()
    {
        Buttons = new();
        SkillList = new();
        Load();
        Close();
    }

    private void Load()
    {
        Addressables.LoadAssetAsync<GameObject>("Prefabs/SkillButtonPrefab").Completed += handle =>
        {
            SkillButtonPrefabHandle = handle;
        };
    }

    public void Open(Flow targetFlow)
    {
        try
        {
            targetFlow.ShowData(); //to debug
        }
        catch
        {
            throw new System.Exception();
        }
        this.targetFlow = targetFlow;
        gameObject.SetActive(true);
        SkillList = skillManager.GetSkillList();

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
