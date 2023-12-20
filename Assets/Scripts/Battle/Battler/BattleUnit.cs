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
    SkillManager skillManager;

    public void Init()
    {
        skillManager = new();
    }

    public void ExecuteSkill(BattleUnit source, BattleUnit target, SkillManager.SkillKind skillKind)
    {
        AbstractSkill skill = skillManager.Create(skillKind);
        skill.Execute(source, target);
    }
}
