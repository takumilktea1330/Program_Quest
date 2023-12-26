using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Flow Data Save with JsonUtility
/// </summary>
[System.Serializable]
public class FlowData
{
    int structId;
    int nextId1;
    int nextId2;

    int posX;
    int posY;

    string type = "skill";

    // skill property
    string skillName; //(skill name must be unique)    

    // condition (if, while)

    int formulaType;
    string lvalue1; // var name = (player, enemy) status, skill power or turn
    string calcToken; // +, -, *, /, %
    string lvalue2; // var name
    string compToken; // <, <=, >, >=, ==
    string rvalue; //varname or int


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
    /// true: 0
    /// false: 1
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
}
