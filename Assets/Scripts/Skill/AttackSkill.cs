using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class AttackSkill : Skill
{
    public AttackSkillBase attackSkillBase;
    public AttackSkill(AttackSkillBase _base) : base(_base)
    {
        attackSkillBase = _base;
    }

    public override void Execute(BattleUnit turnUnit, BattleUnit targetUnit)
    {
        // turnunit attack targetunit
        // base.Execute(turnUnit, targetUnit);
        Debug.Log($"Execute Skill: {Base.Name}");
        targetUnit.Status.Hp -= (int)(turnUnit.Status.Attack * attackSkillBase.AttackPower);
    }
}
