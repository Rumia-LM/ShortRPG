using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class PlayerDataManagerTest_Master2 : MonoBehaviour
{
    public static PlayerDataManagerTest_Master2 Instance{get;private set;} //シングルトンインスタンス
    public GameObject playerPrefab; //プレイヤープレハブ
    public Vector3 spawnPosition=new Vector3(0,0,0); //初期位置

    public int MaxHP{get;private set;}=100; //最大HP
    public int CurrentHP{get;private set;}=100; //現在のHP
    public int ATK{get;private set;}=20; //攻撃力

    private GameObject currentPlayer; //現在のプレイヤー

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

    //プレイヤーと全ての子孫オブジェクトを非表示にするメソッド
    public void HidePlayer(){
        if(playerPrefab!=null){
            playerPrefab.SetActive(false); //プレイヤーを非表示
            HideChildrenRecursively(playerPrefab.transform); //階層全体を非表示にする
            Debug.Log("Player has been hidden.");
        }else{
            Debug.LogWarning("Cannot hide Player because it doesn't exit.");
        }
    }

    //再起的に子オブジェクトを非表示にする
    private void HideChildrenRecursively(Transform parent){
        foreach(Transform child in parent){
            child.gameObject.SetActive(false); //子オブジェクトを非表示
            
            //再起的に処理
            if(child.childCount>0){
                HideChildrenRecursively(child);
            }
        }
    }

    //プレイヤーと全ての子孫オブジェクトを再表示するメソッド
    public void ShowPlayer(){
        if(playerPrefab!=null){
            playerPrefab.SetActive(true); //プレイヤーを表示
            ShowChildrenRecursively(playerPrefab.transform); //階層全体を非表示にする
            Debug.Log("Player has been shown.");
        }else{
            Debug.LogWarning("Cannot show Player because it doesn't exit.");
        }
    }

    //再起的に子オブジェクトを際表示にする
    private void ShowChildrenRecursively(Transform parent){
        foreach(Transform child in parent){
            child.gameObject.SetActive(true); //子オブジェクトを非表示
            
            //再起的に処理
            if(child.childCount>0){
                ShowChildrenRecursively(child);
            }
        }
    }
    
    //データをリセットするメソッド(ゲームオーバー時など)
    public void ResetData(){
        CurrentHP=MaxHP;
    }

}
