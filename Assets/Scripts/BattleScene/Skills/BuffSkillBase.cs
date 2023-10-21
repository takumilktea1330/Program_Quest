using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BuffSkillBase : SkillBase
{
    [SerializeField]int buffForAtk;
    [SerializeField] int buffForDef;
    [SerializeField] int buffForHealing;
    [SerializeField] int buffForSpeed;
    public BuffSkillBase() : base()
    {
        Type = "Buff";
    }

    public int BuffForAtk { get => buffForAtk; set => buffForAtk = value; }
    public int BuffForDef { get => buffForDef; set => buffForDef = value; }
    public int BuffForHealing { get => buffForHealing; set => buffForHealing = value; }
    public int BuffForSpeed { get => buffForSpeed; set => buffForSpeed = value; }
}
