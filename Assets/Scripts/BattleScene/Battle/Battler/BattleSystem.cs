using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSystem : MonoBehaviour
{
    Battler player;
    Battler enemy;

    [SerializeField] List<BattlerBase> EnemyBattlerBases;

    [SerializeField] PlayerController playerController;

    public void StartBattle()
    {
        player = playerController.PlayerBattler;
        enemy = new Battler();
        BattlerBase battlerBase = new(); 
        battlerBase = GetRandomBattlerBase();
        enemy.Init(battlerBase);
        StartCoroutine(SetupBattle());

    }

    public IEnumerator SetupBattle()
    {
        yield break;
    }

    private BattlerBase GetRandomBattlerBase()
    {
        return EnemyBattlerBases[Random.Range(0, EnemyBattlerBases.Count)];
    }
}
