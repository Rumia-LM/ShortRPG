using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDataManagerTest_Master : MonoBehaviour
{
    public static EnemyDataManagerTest_Master Instance{get;private set;}
    public EnemyDataTest_r currentEnemy; //敵データを保持する変数

    private void Awake() {
        if(Instance==null){
            Instance=this;
            DontDestroyOnLoad(gameObject); //シーン切り替え時に破棄されない
        }else{
            Destroy(gameObject); //重複するインスタンスを削除
        }
    }
}
