using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEditorInternal;
using UnityEngine;

public class Battler
{
    BattlerBase _base;
    int level;
    int attackPower;
    int defensePower;
    int healingPower;
    int buffPower;
    int speed;
    int maxHp;
    int currentHp;

    public BattlerBase Base => _base;
    public int AttackPower { get => attackPower; set => attackPower = value; }
    public int DefensePower { get => defensePower; set => defensePower = value; }
    public int Speed { get => speed; set => speed = value; }
    public int CurrentHp { get => currentHp; set => currentHp = value; }
    public int HealingPower { get => healingPower; set => healingPower = value; }
    public int BuffPower { get => buffPower; set => buffPower = value; }

    public void Init(BattlerBase _base)
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

    public string ExecuteSkill(Battler target, Skill skill)
    {
        SkillBase _base = skill.Base;
        string dialogText = "";
        if (_base is AggressiveSkillBase){
            dialogText = Attack(target, skill);
        }
        else if (_base is HealingSkillBase)
        {
            dialogText = Healing(target, skill);
        }
        else if (_base is BuffSkillBase)
        {
            dialogText = Buff(target, skill);
        }
        else dialogText = "Error occured!";
        return dialogText;
    }

    public string Attack(Battler target, Skill skill)
    {
        int damage = attackPower * skill.Base.SkillBasePower;
        return Base.Name + "は" + target.Base.Name + "に" + target.ReceiveDamage(damage) + "ダメージ与えた\n";
    }
    public string Healing(Battler target, Skill skill)
    {
        int heal = HealingPower * skill.Base.SkillBasePower;
        return Base.Name + "は" + target.Base.Name + "に" + target.Recovery(heal) + "ダメージ回復した\n";
    }
    public string Buff(Battler target, Skill skill)
    {
        string dialogText = "";
        BuffSkillBase buffBase = skill.Base as BuffSkillBase;
        if(buffBase.BuffForAtk != 0)
        {
            float buff = buffBase.BuffForAtk * buffPower;
            attackPower = (int)(attackPower * buff);
            dialogText += Base.Name + "は" + target.Base.Name + "の攻撃力を" + buff.ToString() + "倍した\n";
        }
        if (buffBase.BuffForDef != 0)
        {
            float buff = buffBase.BuffForDef * buffPower;
            defensePower = (int)(defensePower * buff);
            dialogText += Base.Name + "は" + target.Base.Name + "の防御力を" + buff.ToString() + "倍した\n";
        }
        if (buffBase.BuffForHealing != 0)
        {
            float buff = buffBase.BuffForHealing * buffPower;
            healingPower = (int)(healingPower * buff);
            dialogText += Base.Name + "は" + target.Base.Name + "の回復力を" + buff.ToString() + "倍した\n";
        }
        if (buffBase.BuffForSpeed != 0)
        {
            float buff = buffBase.BuffForSpeed * buffPower;
            speed = (int)(speed * buff);
            dialogText += Base.Name + "は" + target.Base.Name + "の素早さを" + buff.ToString() + "倍した\n";
        }
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
