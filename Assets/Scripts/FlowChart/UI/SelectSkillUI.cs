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
    List<GameObject> Buttons = new();
    GameObject content;
    AsyncOperationHandle<GameObject> _skillButtonPrefabHandle;
    UIController uiController;
    Flow targetFlow;
    
    private void Init()
    {
        //foreach (GameObject button in Buttons) Destroy(button);
        //Buttons.Clear();

        targetFlow = null;
        content = transform.Find("Viewport/Content").gameObject;
        uiController = transform.parent.GetComponent<UIController>();
        _skillButtonPrefabHandle = Addressables.LoadAssetAsync<GameObject>("Prefabs/SkillButtonPrefab");
        _skillButtonPrefabHandle.WaitForCompletion();
        Debug.Log("SelectSkillUI: Initialized!");
    }

    private void Start()
    {
        Close();
    }

    public void Open(Flow targetFlow)
    {
        Init();
        gameObject.SetActive(true);
        uiController.ToProcessingMode();
        this.targetFlow = targetFlow;

        foreach(Skill skill in SkillManager.Skills)
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

    void SetSkill(Skill skill)
    {
        targetFlow.Data.SkillName = skill.ToString();
        targetFlow.Display();
        uiController.ToViewMode();
        Close();
    }

    void Close()
    {
        gameObject.SetActive(false);
        uiController.ToViewMode();
    }
}
