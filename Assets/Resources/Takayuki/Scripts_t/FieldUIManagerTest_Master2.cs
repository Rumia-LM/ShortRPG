using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FieldUIManagerTest_Master2 : MonoBehaviour
{
    public TMP_Text PlayerStatusText; //ステータス表示用のTMPro

    void Start()
    {
        UpdatePlayerStatus();
    }

    //プレイヤーステータスをUIに表示
    public void UpdatePlayerStatus()
    {
        if(PlayerDataManagerTest_Master2.Instance!=null){
            int currentHP=PlayerDataManagerTest_Master2.Instance.CurrentHP;
            int maxHP=PlayerDataManagerTest_Master2.Instance.MaxHP;
            int atk=PlayerDataManagerTest_Master2.Instance.ATK;

            PlayerStatusText.text=$"Hero\nHP:{currentHP}/{maxHP}\nATK:{atk}";
        }
    }
}
