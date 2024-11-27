using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class MonsterData
{
    public List<Monster> monsters;
}

[System.Serializable]
public class Monster
{
    public string name;
    public int health;
    public int attack;
}

public class RandomEncounterFromJson_t : MonoBehaviour
{
    public string jsonFilePath = "Assets/ShortDEV/Takayuki/Scripts/data_t.json"; // JSONファイルのパス
    private List<Monster> monsters;
    private float encounterCooldown = 2.0f; // エンカウント判定の間隔（秒）
    private float lastEncounterTime;

    void Start()
    {
        LoadMonstersFromJson();
        Debug.Log("Monsters list count after load: " + (monsters != null ? monsters.Count.ToString() : "null"));
        lastEncounterTime = Time.time; // 最初のエンカウントタイムを初期化
    }

    void LoadMonstersFromJson()
    {
        string filePath = "Assets/ShortDEV/Takayuki/Scripts/data_t.json"; // 相対パスで指定
        Debug.Log("Loading JSON file from path: " + filePath);
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            Debug.Log("JSON content: " + json);
            MonsterData monsterData = JsonUtility.FromJson<MonsterData>(json);
            monsters = monsterData.monsters;
            Debug.Log("Monsters loaded: " + (monsters != null ? monsters.Count.ToString() : "null"));
        }
        else
        {
            Debug.LogError("JSON file not found at path: " + filePath);
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (Time.time - lastEncounterTime >= encounterCooldown)
            {
                Debug.Log("Player is in the encounter area.");
                if (Random.value < 0.2f) // エンカウント確率20%
                {
                    Debug.Log("Encounter triggered.");
                    TriggerEncounter();
                }
                lastEncounterTime = Time.time; // エンカウントタイムを更新
            }
        }
    }

    void TriggerEncounter()
    {
        Monster randomMonster = GetRandomMonster();
        if (randomMonster != null)
        {
            Debug.Log("Encountered Monster: " + randomMonster.name);
            SceneManager.LoadScene("TestScene_t"); // 実際のシーン名に変更
        }
        else
        {
            Debug.Log("No monster found for encounter.");
        }
    }

    Monster GetRandomMonster()
    {
        if (monsters != null && monsters.Count > 0)
        {
            int randomIndex = Random.Range(0, monsters.Count);
            return monsters[randomIndex];
        }
        return null;
    }
}
