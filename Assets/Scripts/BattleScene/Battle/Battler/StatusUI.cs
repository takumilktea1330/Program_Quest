using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusUI : MonoBehaviour
{
    [SerializeField] private Text nameText;
    [SerializeField] private Text hpText;
    [SerializeField] private Text atkText;

    public void StatusUpdate(Battler playerBattler)
    {
        if (playerBattler == null) Debug.Log("funfun");
            nameText.text = "Name: " + playerBattler.Base.Name;
            hpText.text = "HP: " + playerBattler.CurrentHp.ToString();
            atkText.text = "ATK: " + playerBattler.AttackPower.ToString();
    }
}
