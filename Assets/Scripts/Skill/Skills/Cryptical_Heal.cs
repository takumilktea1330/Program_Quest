using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cryptical_Heal : AbstractSkill
{
    // スキル種別
    public override SkillManager.SkillKind SkillKind
    {
        get { return SkillManager.SkillKind.Cryptical_Heal; }
    }

    // スキル「ライトニング」の実行
    public override void Execute(BattleUnit source, BattleUnit target)
    {
        Debug.Log("Lightning!");
    }
}
