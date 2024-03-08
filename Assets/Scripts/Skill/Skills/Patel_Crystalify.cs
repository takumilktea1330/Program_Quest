using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patel_Crystalify : AbstractSkill
{
    // スキル種別
    public override SkillManager.SkillKind SkillKind
    {
        get { return SkillManager.SkillKind.Patel_Crystalify; }
    }

    public override void Execute(BattleUnit source, BattleUnit target)
    {
        Debug.Log("Patel_Crystalify");
    }
}
