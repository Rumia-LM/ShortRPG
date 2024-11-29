using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManagerTest_r : MonoBehaviour
{
    //JSONデータ（コード貼り付け）
    private string jsonDate=@"{
    ""monsters"": [
        {
            ""name"": ""Slime"",
            ""hp"": 20,
            ""atk"": 3,
            ""exp"": 5,
            ""gold"": 1
        },
        {
            ""name"": ""Goblin"",
            ""hp"": 30,
            ""atk"": 5,
            ""exp"": 10,
            ""gold"": 3
        },
        {
            ""name"": ""Zombie"",
            ""hp"": 40,
            ""atk"": 7,
            ""exp"": 12,
            ""gold"": 2
        }
    ]
}";

    private List<EnemyDataTest_r> enemies; //敵のリスト

    void Start()
    {
        //JSONデータを解析して敵リストを生成
        ParseJSONDate();

        //敵リストの内容を確認
        foreach(var enemy in enemies){
            Debug.Log($"Name:{enemy.name},HP:{enemy.hp},ATK:{enemy.atk},EXP:{enemy.exp},Gold:{enemy.gold}");
        }
    }

    //JSONデータを解析してリストを生成
    public void ParseJSONDate()
    {
        //JSONデータをMonsterListクラスに変換
        MonsterList monsterList=JsonUtility.FromJson<MonsterList>(jsonDate);
        enemies=monsterList.monsters;
    }

    //敵リストを取得するメソッド
    public List<EnemyDataTest_r> GetEnemies(){
        return enemies;
    }
}
