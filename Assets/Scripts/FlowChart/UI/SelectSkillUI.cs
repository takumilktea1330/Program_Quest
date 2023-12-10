using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SelectSkillUI : MonoBehaviour
{
    List<Skill> SkillList;
    List<GameObject> Buttons;
    [SerializeField] GameObject content;
    [SerializeField] GameObject SkillPrefab;
    Flow targetFlow;

    public UnityAction OnSettingSkillEnded;
    private void Start()
    {
        Buttons = new();
        Close();
        Test();
    }
    private void Test()
    {
        SkillList = new();
        AttackSkillBase normalAttack = Resources.Load<AttackSkillBase>("Data/Skills/AttackSkills/NormalAttack");
        if(normalAttack == null)
        {
            Debug.Log("normalAttack is not existed");
        }
        SkillList.Add(new AttackSkill(normalAttack));
    }
    public void Open(Flow targetFlow)
    {
        if (targetFlow == null) return; 
        this.targetFlow = targetFlow;
        gameObject.SetActive(true);
        foreach(Skill skill in SkillList)
        {
            GameObject newSkillButton = Instantiate(SkillPrefab, Vector3.zero, quaternion.identity);
            Buttons.Add(newSkillButton);
            newSkillButton.transform.SetParent(content.transform, true);
            Button button = newSkillButton.GetComponent<Button>();
            Text text = newSkillButton.GetComponentInChildren<Text>();
            text.text = skill.Base.Name;
            button.onClick.AddListener(() => SetSkill(skill));
            button.onClick.AddListener(targetFlow.Display);
            button.onClick.AddListener(Close);
        }
    }
    public void SetSkill(Skill skill)
    {
        targetFlow.SkillName = skill.Base.Name;
        targetFlow.Skill = skill;
        OnSettingSkillEnded.Invoke();
    }
    public void Close()
    {
        foreach (GameObject button in Buttons) Destroy(button);
        Buttons.Clear();
        targetFlow = null;
        gameObject.SetActive(false);

    }
    public void UpdateUI()
    {
        
    }
}
