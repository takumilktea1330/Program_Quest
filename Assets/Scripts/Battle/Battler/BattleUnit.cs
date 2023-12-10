using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEditorInternal;
using UnityEngine;

public class BattleUnit
{
    Status status;
    BattleUnitBase _base;
    int level;
    int attackPower;
    int defensePower;
    int healingPower;
    int buffPower;
    int speed;
    int maxHp;
    int currentHp;

    public BattleUnitBase Base => _base;
    public int AttackPower { get => attackPower; set => attackPower = value; }
    public int DefensePower { get => defensePower; set => defensePower = value; }
    public int Speed { get => speed; set => speed = value; }
    public int CurrentHp { get => currentHp; set => currentHp = value; }
    public int HealingPower { get => healingPower; set => healingPower = value; }
    public int BuffPower { get => buffPower; set => buffPower = value; }
    public Status Status { get => status; set => status = value; }

    public void Init(BattleUnitBase _base)
    {
        this._base = _base;
        level = Base.InitLevel;
        AttackPower = Base.OffencePower;
        DefensePower = Base.DefensePower;
        BuffPower = Base.BuffPower;
        Speed = Base.Speed;
        maxHp = Base.MaxHp;
        CurrentHp = maxHp;
    }

    public string ReceiveDamage(int damage)
    {
        damage /= defensePower;
        if(currentHp-damage<0)
        {
            damage = currentHp;
            currentHp = 0;
        }
        else CurrentHp -= damage;
        return damage.ToString();
    }

    public string Recovery(int heal)
    {
        if(currentHp+heal > maxHp)
        {
            heal = maxHp - currentHp;
            currentHp = maxHp;
        }
        else currentHp += heal;
        return heal.ToString();
    }

    public string ExecuteSkill(BattleUnit target, Skill skill)
    {
        SkillBase _base = skill.Base;
        string dialogText = "";
        dialogText = "Error occured!";
        return dialogText;
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
