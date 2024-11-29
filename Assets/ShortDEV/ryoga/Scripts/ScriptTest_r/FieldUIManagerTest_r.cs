using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FieldUIManagerTest_r : MonoBehaviour
{
    public TMP_Text PlayerStatusText; //ステータス表示用のTMPro

    void Start()
    {
        UpdatePlayerStatus();
    }

    //プレイヤーステータスをUIに表示
    public void UpdatePlayerStatus()
    {
        if(PlayerDataManagerTest_r.Instance!=null){
            int currentHP=PlayerDataManagerTest_r.Instance.CurrentHP;
            int maxHP=PlayerDataManagerTest_r.Instance.MaxHP;
            int atk=PlayerDataManagerTest_r.Instance.ATK;

            PlayerStatusText.text=$"Hero\nHP:{currentHP}/{maxHP}\nATK:{atk}";
        }
    }
}
