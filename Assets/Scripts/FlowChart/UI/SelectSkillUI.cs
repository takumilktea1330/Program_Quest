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
    GameObject content;
    AsyncOperationHandle<GameObject> _skillButtonPrefabHandle;
    GameObject _skillButtonPrefab;
    Flow targetFlow;

    public UnityAction OnSettingSkillEnded;
    
    private void Start()
    {
        Init();
        Close();
    }
    private void Init()
    {
        Buttons = new();
        SkillList = new();
        _skillButtonPrefabHandle = Addressables.LoadAssetAsync<GameObject>("Prefabs/SkillButtonPrefab");
        _skillButtonPrefab = _skillButtonPrefabHandle.WaitForCompletion();
        Debug.Log("SelectSkillUI: Initialized!");
    }

    public void Open(Flow targetFlow)
    {
        //Init();
        content = transform.Find("Viewport/Content").gameObject;
        this.targetFlow = targetFlow;
        gameObject.SetActive(true);
        SkillList = SkillManager.GetSkillList();

        foreach(SkillManager.SkillKind skillKind in SkillList)
        {
            GameObject newSkillButton = Instantiate(_skillButtonPrefabHandle.Result, Vector3.zero, quaternion.identity);
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
        Close();
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
