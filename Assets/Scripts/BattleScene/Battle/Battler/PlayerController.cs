using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Battler PlayerBattler{ get; set; }
    [SerializeField] Chart chart;
    [SerializeField] BattlerBase battlerBase;
    private void Awake() 
    {
        PlayerBattler = new Battler();
        PlayerBattler.Init(battlerBase);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
