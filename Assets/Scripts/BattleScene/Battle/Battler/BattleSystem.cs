using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSystem : MonoBehaviour
{
    Battler player;
    Battler enemy;

    [SerializeField] List<BattlerBase> EnemyBattlerBases;

    [SerializeField] PlayerController playerController;
    [SerializeField] StatusUI statusUI;
    [SerializeField] BattleDialogUI battleDialogUI;

    void Start()
    {
        StartBattle();
    }

    public void StartBattle()
    {
        enemy = new Battler();
        BattlerBase battlerBase = GetRandomBattlerBase();
        enemy.Init(battlerBase);
        player = playerController.PlayerBattler;
        SetupBattle(player, enemy);
    }

    public void SetupBattle(Battler player, Battler enemy)
    {
        Debug.Log("Start Battle");
        statusUI.StatusUpdate(player);
        StartCoroutine(TakeTurn());
    }

    private IEnumerator TakeTurn()
    {
        yield return battleDialogUI.DialogUpdate($"Take Turn()");
        yield return new WaitForSeconds(1);
        Skill playerSkill = player.GetRandomSkill();
        Debug.Log(playerSkill.Base.Name);
        yield return battleDialogUI.AddToDialog($"{player.ExecuteSkill(enemy, player.GetRandomSkill())}");
        statusUI.StatusUpdate(player);
        yield break;
    }

    private BattlerBase GetRandomBattlerBase()
    {
        return EnemyBattlerBases[Random.Range(0, EnemyBattlerBases.Count)];
    }
}
