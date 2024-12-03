using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyDataTest_r
{
    public string name;
    public int hp;
    public int atk;
    public int exp;
    public int gold;
    public string image; //画像名（"Slime"）

    //コンストラクタ（必要に応じて追加)
    public EnemyDataTest_r(string name,int hp,int atk,int exp,int gold)
    {
        this.name=name;
        this.hp=hp;
        this.atk=atk;
        this.exp=exp;
        this.gold=gold;
    }
}

[System.Serializable]
public class MonsterList{
    public List<EnemyDataTest_r> monsters; //敵のリスト
}
