using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battler
{
    BattlerBase _base;
    int level;
    int attackPower;
    int defensePower;
    int speed;
    int maxHp;
    int currentHp;

    public BattlerBase Base => _base;

    public void Init(BattlerBase _base)
    {
        this._base = _base;
        level = Base.InitLevel;
        attackPower = Base.OffencePower;
        defensePower = Base.DefensePower;
        speed = Base.Speed;
        maxHp = Base.MaxHp;
        currentHp = maxHp;
    }

    public void ReceiveDamage(int damage)
    {
        currentHp -= damage;
    }

    public SkillBase GetRandomSkillBase()
    {
        int index = Random.Range(0, Base.LearnableSkills.Count);
        return Base.LearnableSkills[index];
    }
    public Skill GetRandomSkill()
    {
        int index = Random.Range(0, Base.LearnableSkills.Count);
        return new Skill(Base.LearnableSkills[index]);
    }
}
