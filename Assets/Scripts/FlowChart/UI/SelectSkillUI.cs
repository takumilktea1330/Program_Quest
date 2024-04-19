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
    IList<Skill> skills;
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
        skills = SkillManager.Skills;

        foreach(Skill skill in skills)
        {
            GameObject newSkillButton = Instantiate(_skillButtonPrefabHandle.Result, Vector3.zero, quaternion.identity);
            Buttons.Add(newSkillButton);
            newSkillButton.transform.SetParent(content.transform, true); // contentの子にbuttonを追加
            Button button = newSkillButton.GetComponent<Button>();
            Text text = newSkillButton.GetComponentInChildren<Text>();
            text.text = skill.ToString();

            button.onClick.AddListener(() => SetSkill(skill)); // press button to set skill
        }
    }

    public void SetSkill(Skill skill)
    {
        targetFlow.Data.SkillName = skill.ToString();
        targetFlow.Display();
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
