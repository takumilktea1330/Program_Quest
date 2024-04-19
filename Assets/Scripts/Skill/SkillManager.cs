using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public static class SkillManager
{
    public static IList<Skill> Skills { get => skills; }
    static IList<Skill> skills;

    public static IEnumerator Init()
    {
        List<string> labels = new(){ "Skill" };
        var handle = Addressables.LoadAssetsAsync<Skill>(labels, null, Addressables.MergeMode.Union);
        yield return handle;
        skills = handle.Result;
        yield break;
    }

    public static Skill GetSkill(string name)
    {
        return skills.FirstOrDefault(skill => skill.ToString() == name);
    }
}
