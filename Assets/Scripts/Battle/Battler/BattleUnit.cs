using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEditorInternal;
using UnityEngine;

public abstract class BattleUnit
{
    Status status;
    public Status Status { get => status; set => status = value; }

    public void Init(){}

    public void ExecuteSkill(BattleUnit source, BattleUnit target, Skill skill)
    {
        skill.Execute(source, target);
    }
}
