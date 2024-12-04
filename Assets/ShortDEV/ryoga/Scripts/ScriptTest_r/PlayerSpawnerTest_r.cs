using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnerTest_r : MonoBehaviour
{
    public GameObject playerPrefab; //プレイヤープレハブ
    public Vector3 spawnPosition=new Vector3(0,0,0); //初期位置

    private GameObject currentPlayer; //現在のプレイヤー

    void Start()
    {
        if(playerPrefab!=null){
            currentPlayer=Instantiate(playerPrefab,spawnPosition,Quaternion.identity);
            currentPlayer.name="Player"; //プレイヤー名を設定
            Debug.Log("Player instantiated successfully.");
        }else{
            Debug.LogError("Player prefab is not assigned.");
        }
    }
}
