using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SkillBase : ScriptableObject
{
    public string Type { get; set;}
    [SerializeField] string _name;
    [SerializeField] int code;
    [SerializeField] int skillBasePower;
    [SerializeField] int learnableLevel;

    public string Name { get => _name; set => _name = value; }
    public int Code { get => code; set => code = value; }
    public int SkillBasePower { get => skillBasePower; set => skillBasePower = value; }
    public int LearnableLevel { get => learnableLevel; set => learnableLevel = value; }
}
