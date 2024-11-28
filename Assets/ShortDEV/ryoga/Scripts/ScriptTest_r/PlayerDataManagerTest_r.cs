using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class PlayerDataManagerTest_r : MonoBehaviour
{
    public static PlayerDataManagerTest_r Instance{get;private set;} //シングルトンインスタンス

    public int MaxHP{get;private set;} //最大HP
    public int CurrentHP{get;private set;} //現在のHP
    public int ATK{get;private set;} //攻撃力

    private void Awake() {
        //シングルトンの初期化
        if(Instance==null){
            Instance=this;
            DontDestroyOnLoad(gameObject); //シーン切り替え時に破棄されない
        }else{
            Destroy(gameObject); //重複するインスタンスを破棄する
        }
    }

    //HPを更新するメソッド
    public void UpdateHP(int newHP)
    {
        CurrentHP=Mathf.Clamp(newHP,0,MaxHP); //0からMaxHPの範囲内に制限
    }

    //データをリセットするメソッド(ゲームオーバー時など)
    public void ResetData(){
        CurrentHP=MaxHP;
    }
}
