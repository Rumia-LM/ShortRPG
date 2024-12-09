using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO; //ファイル操作用

public class EnemyManagerTest_Master2 : MonoBehaviour
{
    private List<EnemyDataTest_Master2> enemies; //敵のリスト

    void Start()
    {
        enemies = new List<EnemyDataTest_Master2>(); // 初期化する

        //外部ファイルJSONデータを読み込み
        ParseJSONFromFile();

        //敵リストの内容を確認
        foreach(var enemy in enemies){
            Debug.Log($"Name:{enemy.name},HP:{enemy.hp},ATK:{enemy.atk},EXP:{enemy.exp},Gold:{enemy.gold}");
        }
    }

    //外部ファイルからJSONデータを読み込んで解析
    public void ParseJSONFromFile()
    {   
        //ファイルパスを指定
        string filePath = Application.dataPath + "/Resources/Takayuki/Scripts_t/Json/monstersList_Master2.json"; //""内に指定するJSONファイルの相対パスを入力（Assets以下のフォルダ名）
        if(File.Exists(filePath)){
            string jsonText=File.ReadAllText(filePath); //ファイル内容を読み込み
            MonsterList_Master2 monsterList=JsonUtility.FromJson<MonsterList_Master2>(jsonText); //JSONデータをMonsterListクラスに変換
            enemies=monsterList.monsters; //モンスターリストを格納
        }else{
            Debug.LogError($"JSON file not found at path:{filePath}!");
        }
        
    }

    //敵リストを取得するメソッド
    public List<EnemyDataTest_Master2> GetEnemies(){
        return enemies;
    }
}
