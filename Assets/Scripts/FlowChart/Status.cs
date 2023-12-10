using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{
    string _name;
    int hp; //体力
    int attack; //攻撃力
    int defense; //防御力
    int resilience; //回復力
    int agility; //敏捷性
    int dexterity; //命中率
    int evasion; //回避率
    int critical; //会心率

    public string Name { get => _name; set => _name = value; }
    public int Hp { get => hp; set => hp = value; }
    public int Attack { get => attack; set => attack = value; }
    public int Defense { get => defense; set => defense = value; }
    public int Resilience { get => resilience; set => resilience = value; }
    public int Agility { get => agility; set => agility = value; }
    public int Dexterity { get => dexterity; set => dexterity = value; }
    public int Evasion { get => evasion; set => evasion = value; }
    public int Critical { get => critical; set => critical = value; }
}
