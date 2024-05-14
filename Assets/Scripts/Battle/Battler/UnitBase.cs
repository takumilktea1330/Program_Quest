using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "UnitData", menuName = "UnitData")]
public class UnitBase : ScriptableObject
{
    [SerializeField] string _name;
    [SerializeField] int _hp;
    [SerializeField] int _attack;
    [SerializeField] int _defense;
    [SerializeField] Sprite _sprite;
    
    public string Name { get => _name; }
    public int Hp { get => _hp; }
    public int Attack { get => _attack; }
    public int Defense { get => _defense; }
    public Sprite Sprite { get => _sprite; }
}
