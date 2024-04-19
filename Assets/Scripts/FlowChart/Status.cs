using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{
    string _name;
    int hp; //体力
    int attack; //攻撃力
    int defense; //防御力

    public string Name { get => _name; set => _name = value; }
    public int Hp { get => hp; set => hp = value; }
    public int Attack { get => attack; set => attack = value; }
    public int Defense { get => defense; set => defense = value; }
}
