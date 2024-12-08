using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public GameObject monsterPrefab; // モンスターのプレハブを指定
    public int numberOfMonsters = 5; // エンカウントさせるモンスターの数
    public Vector2 spawnAreaMin; // スポーンエリアの最小値（左下のコーナー）
    public Vector2 spawnAreaMax; // スポーンエリアの最大値（右上のコーナー）

    void Start()
    {
        SpawnMonsters();
    }

    void SpawnMonsters()
    {
        for (int i = 0; i < numberOfMonsters; i++)
        {
            // ランダムな位置を計算
            float spawnX = Random.Range(spawnAreaMin.x, spawnAreaMax.x);
            float spawnY = Random.Range(spawnAreaMin.y, spawnAreaMax.y);
            Vector2 spawnPosition = new Vector2(spawnX, spawnY);
            
            // モンスターをインスタンス化
            Instantiate(monsterPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
