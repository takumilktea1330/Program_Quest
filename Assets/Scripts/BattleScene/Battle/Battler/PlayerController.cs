using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Battler PlayerBattler{ get; set; }
    [SerializeField] BattlerBase battlerBase;
    void Start()
    {
        PlayerBattler = new Battler();
        PlayerBattler.Init(battlerBase);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
