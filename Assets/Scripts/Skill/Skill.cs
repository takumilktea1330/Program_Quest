using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "Skill", menuName = "CreateSkill")]
public class Skill: ScriptableObject
{
    public enum Type
    {
        Attack,
        Guard, // Guard from specific magical attacks (guard rate is higher)
        Buff, // 
    }
    public enum MagicalAttribution
    {
        Normal,
        Fire,
        Aqua,
        Frozen,
        Thunder,
    }
    [SerializeField]Type _skillType;
    [SerializeField]MagicalAttribution _attribution;
    [SerializeField]float _rate = 0;
    [SerializeField]string _name = "";
    [SerializeField]string _description = "";
    [SerializeField]GameObject _sourceEffect = null;
    [SerializeField]GameObject _targetEffect = null;
    [SerializeField] Sprite _displaySprite = null;

    public Type SkillType { get => _skillType; }
    public MagicalAttribution Attribution { get => _attribution; }
    public float Rate { get => _rate;  }
    public string Name { get => _name; }
    public string Description { get => _description; }
    public GameObject SourceEffect { get => _sourceEffect; }
    public GameObject TargetEffect { get => _targetEffect; }
    public Sprite DisplaySprite { get => _displaySprite; }

    public void Execute(BattleUnit source, BattleUnit target)
    {
        Debug.Log($"{source.Status.Name} uses {Name} to {target.Status.Name}");
    }
}
