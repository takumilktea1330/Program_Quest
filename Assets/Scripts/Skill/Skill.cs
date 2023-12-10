using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill
{
    public SkillBase Base { get; }
    public Skill(SkillBase _base)
    {
        Base = _base;
    }
    public virtual void Execute(BattleUnit turnUnit, BattleUnit targetUnit)
    {
        
    }
}
