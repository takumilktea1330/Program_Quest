using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class Flow : MonoBehaviour
{
    public FlowData Data;
    public UnityAction<Flow> OnSelectSkill;
    public UnityAction<Flow> OnOpenProperty;
    public UnityAction<Flow> OnSetCondition;
    Text displayText;
    Camera _camera;
    

    /// <summary>
    /// initialize this flow
    /// </summary>
    /// <param name="id">newStructId(it must be unique)</param>
    /// <param name="type">"skill", "if" </param>
    public void Init(int id, string type)
    {
        Data = new()
        {
            StructId = id,
            Type = type,
            FormulaType = 0
        };
        _camera = Camera.main;
        displayText = GetComponentInChildren<Text>();
    }

    public void Display()
    {
        displayText.text = Data.SkillName;
    }

    public void OpenProperty()
    {
        OnOpenProperty.Invoke(this);
    }

    public void SelectSkill()
    {
        OnSelectSkill.Invoke(this);
    }

    public void SetCondition()
    {
        OnSetCondition.Invoke(this);
    }

    private void OnMouseDrag()
    {
        transform.position = (Vector2)_camera.ScreenToWorldPoint(Input.mousePosition);
    }

    public void ShowData()
    {
        if(Data.Type == "skill")
        Debug.Log($"This is Flow(Struct ID: {Data.StructId}) Data\n"+
        $"Position: {transform.position}, Skill Name: {Data.SkillName}\n");
    }
}