using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Felyca_Defense : AbstractSkill
{
    // スキル種別
    public override SkillManager.SkillKind SkillKind
    {
        get { return SkillManager.SkillKind.Felyca_Defense; }
    }

    public override void Execute(BattleUnit source, BattleUnit target)
    {
        Debug.Log("Felyca_Defense");
    }
}
