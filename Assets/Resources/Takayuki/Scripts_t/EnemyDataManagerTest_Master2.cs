using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDataManagerTest_Master2 : MonoBehaviour
{
    public static EnemyDataManagerTest_Master2 Instance{get;private set;}
    public EnemyDataTest_Master2 currentEnemy; //敵データを保持する変数

    private void Awake() {
        if(Instance==null){
            Instance=this;
            DontDestroyOnLoad(gameObject); //シーン切り替え時に破棄されない
        }else{
            Destroy(gameObject); //重複するインスタンスを削除
        }
    }
}
