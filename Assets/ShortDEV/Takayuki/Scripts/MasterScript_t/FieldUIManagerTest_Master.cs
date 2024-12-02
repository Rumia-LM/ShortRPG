using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FieldUIManagerTest_Master : MonoBehaviour
{
    public TMP_Text PlayerStatusText; //ステータス表示用のTMPro

    void Start()
    {
        UpdatePlayerStatus();
    }

    //プレイヤーステータスをUIに表示
    public void UpdatePlayerStatus()
    {
        if(PlayerDataManagerTest_Master.Instance!=null){
            int currentHP=PlayerDataManagerTest_Master.Instance.CurrentHP;
            int maxHP=PlayerDataManagerTest_Master.Instance.MaxHP;
            int atk=PlayerDataManagerTest_Master.Instance.ATK;

            PlayerStatusText.text=$"Hero\nHP:{currentHP}/{maxHP}\nATK:{atk}";
        }
    }
}
