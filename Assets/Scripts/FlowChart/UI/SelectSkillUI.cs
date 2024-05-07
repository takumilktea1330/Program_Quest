using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Security.Cryptography.X509Certificates;

public class SelectSkillUI : MonoBehaviour
{
    List<GameObject> Buttons = new();
    GameObject content;
    AsyncOperationHandle<GameObject> _skillButtonPrefabHandle;
    UIController uiController;
    Flow targetFlow;


    public IEnumerator Init()
    {
        yield return content = transform.Find("Viewport/Content").gameObject;
        yield return uiController = transform.parent.GetComponent<UIController>();
        yield return _skillButtonPrefabHandle = Addressables.LoadAssetAsync<GameObject>("Prefabs/SkillButtonPrefab");
        yield return ButtonMake();
        Debug.Log("SelectSkillUI: Initialized!");
        Close();
    }

    IEnumerator ButtonMake()
    {
        foreach (Skill skill in SkillManager.Skills)
        {
            GameObject newSkillButton = Instantiate(_skillButtonPrefabHandle.Result, Vector3.zero, quaternion.identity);
            Buttons.Add(newSkillButton);
            newSkillButton.transform.SetParent(content.transform, true); // contentの子にbuttonを追加
            Button button = newSkillButton.GetComponent<Button>();
            Text text = newSkillButton.GetComponentInChildren<Text>();
            text.text = skill.ToString();

            button.onClick.AddListener(() => SetSkill(skill)); // press button to set skill
        }
        yield break;
    }

    public void Open(Flow targetFlow)
    {
        gameObject.SetActive(true);
        uiController.ToProcessingMode();
        this.targetFlow = targetFlow;
    }

    void SetSkill(Skill skill)
    {
        targetFlow.Data.SkillName = skill.ToString();
        targetFlow.Display();
        uiController.ToViewMode();
        Close();
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
}
