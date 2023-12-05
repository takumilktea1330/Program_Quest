using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Flow : MonoBehaviour
{
    int structId;
    int nextId1;
    int nextId2;

    string type = "skill";

    // skill property
    Skill skill; //(skill name must be unique)

    // condition (if, while)

    /// <summary>
    /// formulaType depending on this rule below
    /// unset: 0
    /// true: 1
    /// varname(compToken)int : 2
    /// varname(compToken)varname : 3
    /// varname(calcToken)varname(compToken)int : 4
    /// varname(calcToken)varname(compToken)varname : 5
    /// </summary>
    int formulaType;
    string lvalue1; // var name
    string calcToken; // +, -, *, /
    string lvalue2; // var name = (player, enemy) status, skill power or turn
    string compToken; // <, <=, >, >=, ==
    string rvalue; //varname or int

    // ids
    public int StructId { get => structId; set => structId = value; }
    public int NextId1 { get => nextId1; set => nextId1 = value; } // skill->next, ifflow(true)
    public int NextId2 { get => nextId2; set => nextId2 = value; } // is used only ifflow(false)

    // type = skill, if
    public string Type { get => type; set => type = value; }

    // skill property
    public Skill Skill { get => skill; set => skill = value; }


    // condition formula property
    public int FormulaType { get => formulaType; set => formulaType = value; }
    public string Lvalue1 { get => lvalue1; set => lvalue1 = value; }
    public string CalcToken { get => calcToken; set => calcToken = value; }
    public string Lvalue2 { get => lvalue2; set => lvalue2 = value; }
    public string CompToken { get => compToken; set => compToken = value; }
    public string Rvalue { get => rvalue; set => rvalue = value; }

    public Flow(int id, string type)
    {
        structId = id;
        this.type = type;
        formulaType = 0;
    }

    public void OpenProperty()
    {

    }

    public void SelectSkill()
    {
        
    }

    public void SetCondition()
    {

    }
}