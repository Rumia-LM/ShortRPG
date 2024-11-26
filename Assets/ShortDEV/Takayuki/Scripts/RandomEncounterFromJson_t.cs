using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

using System.Collections.Generic;
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
    public string jsonFilePath = "Assets/ShortDEV/takayuki/Scripts/data_t.json"; // JSONファイルのパス
    private List<Monster> monsters;

    void Start()
    {
        LoadMonstersFromJson();
        Debug.Log("Monsters list count after load: " + (monsters != null ? monsters.Count.ToString() : "null"));
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
            Debug.Log("Monsters loaded: " + monsters.Count);
        }
        else
        {
            Debug.LogError("JSON file not found at path: " + filePath);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("OnTriggerEnter2D called with: " + other.name);
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered the encounter area.");
            if (Random.value < 1.0f) // エンカウント確率100%（デバッグ用）
            {
                Debug.Log("Encounter triggered.");
                TriggerEncounter();
            }
        }
    }

    void TriggerEncounter()
    {
        Monster randomMonster = GetRandomMonster();
        if (randomMonster != null)
        {
            Debug.Log("Encountered Monster: " + randomMonster.name);

            // バトルシーンのロード
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
