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
    //
    //ここからJsonUtilityで保存
    //
    int structId;
    int nextId1;
    int nextId2;

    string type = "skill";

    // skill property
    string skillName; //(skill name must be unique)    

    // condition (if, while)

    int formulaType;
    string lvalue1; // var name
    string calcToken; // +, -, *, /
    string lvalue2; // var name = (player, enemy) status, skill power or turn
    string compToken; // <, <=, >, >=, ==
    string rvalue; //varname or int


    //
    //ここまでをJsonUtilityで保存
    //

    Skill skill;
    Text displayText;
    // ids
    public int StructId { get => structId; set => structId = value; }
    public int NextId1 { get => nextId1; set => nextId1 = value; } // skill->next, ifflow(true)
    public int NextId2 { get => nextId2; set => nextId2 = value; } // is used only ifflow(false)

    // type = skill, if
    public string Type { get => type; set => type = value; }

    // skill property
    public string SkillName { get => skillName; set => skillName = value; }


    // condition formula property

    /// <summary>
    /// formulaType depending on this rule below
    /// unset: 0
    /// true: 1
    /// varname(compToken)int : 2
    /// varname(compToken)varname : 3
    /// varname(calcToken)varname(compToken)int : 4
    /// varname(calcToken)varname(compToken)varname : 5
    /// </summary>
    public int FormulaType { get => formulaType; set => formulaType = value; }
    public string Lvalue1 { get => lvalue1; set => lvalue1 = value; }
    public string CalcToken { get => calcToken; set => calcToken = value; }
    public string Lvalue2 { get => lvalue2; set => lvalue2 = value; }
    public string CompToken { get => compToken; set => compToken = value; }
    public string Rvalue { get => rvalue; set => rvalue = value; }
    public Skill Skill { get => skill; set => skill = value; }
    public Text DisplayText { get => displayText; set => displayText = value; }
    GameObject myCanvas;

    public UnityAction<Flow> OnSelectSkill;
    public UnityAction<Flow> OnOpenProperty;
    public UnityAction<Flow> OnSetCondition;
    Camera _camera;
    

    /// <summary>
    /// initialize this flow
    /// </summary>
    /// <param name="id">newStructId(it must be unique)</param>
    /// <param name="type">"skill", "if" </param>
    public void Init(int id, string type)
    {
        structId = id;
        this.type = type;
        formulaType = 0;
        _camera = Camera.main;
        displayText = GameObject.Find("Canvas/Text").GetComponent<Text>();
    }

    public void ConnectToFlow(Flow direction, bool isiffalse = false)
    {
        if(isiffalse)
        {
            NextId2 = direction.StructId;
        }
        else
        {
            NextId1 = direction.StructId;
        }
    }

    public void Display()
    {
        displayText.text = skillName;
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
}