using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{
    string _name;
    int hp; //体力
    int attack; //攻撃力
    int critical; //会心率
    int defense; //防御力
    int dexterity; //命中率
    int evasion; //回避率

    public string Name { get => _name; set => _name = value; }
    public int Hp { get => hp; set => hp = value; }
    public int Attack { get => attack; set => attack = value; }
    public int Defense { get => defense; set => defense = value; }
    public int Dexterity { get => dexterity; set => dexterity = value; }
    public int Evasion { get => evasion; set => evasion = value; }
    public int Critical { get => critical; set => critical = value; }
}
