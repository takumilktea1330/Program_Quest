using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public BattleUnit PlayerBattler{ get; set; }
    [SerializeField] Chart chart;
    [SerializeField] BattleUnitBase battlerBase;
    private void Awake() 
    {
        PlayerBattler = new BattleUnit();
        PlayerBattler.Init(battlerBase);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
