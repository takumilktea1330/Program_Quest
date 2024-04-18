using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class SkillManager
{
    static readonly AbstractSkill[] skills =
    {
        new Patel_Crystalify(),
        new Felyca_Defense(),
        new Cryptical_Heal()
    };

    public enum SkillKind
    {
        Patel_Crystalify,
        Felyca_Defense,
        Cryptical_Heal,
    }

    // SkillKindを引数に、それに応じたスキルを返す
    public static AbstractSkill Create(SkillKind skillKind)
    {
        return skills.SingleOrDefault(skill => skill.SkillKind == skillKind);
    }

    public static SkillKind GetSkillKind(string skillName)
    {
        return (SkillKind)Enum.Parse(typeof(SkillKind), skillName);
    }

    public static List<SkillKind> GetSkillList()
    {
        return Enum.GetValues(typeof(SkillKind)).Cast<SkillKind>().ToList();
    }
}
