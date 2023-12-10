using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class AttackSkillBase : SkillBase
{
    [SerializeField] float attackPower;

    public float AttackPower { get => attackPower;}
}
