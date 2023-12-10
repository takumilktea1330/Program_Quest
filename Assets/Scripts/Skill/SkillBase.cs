using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SkillBase : ScriptableObject
{
    [SerializeField] string _name;
    public string Name { get => _name; }
}
