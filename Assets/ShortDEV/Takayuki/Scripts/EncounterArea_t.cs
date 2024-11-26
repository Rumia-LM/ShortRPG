using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EncounterArea_t : MonoBehaviour
{
    public float encounterChance = 1.0f; // エンカウント確率（20%）
    public GameObject encounterPrefab;   // モンスターのプレハブ
    private List<Monster> monsters;

    void Start()
    {
        LoadMonstersFromJson();
    }

    void LoadMonstersFromJson()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, "data_t.json");
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            MonsterData monsterData = JsonUtility.FromJson<MonsterData>(json);
            monsters = monsterData.monsters;
            Debug.Log("Monsters loaded: " + monsters.Count);
        }
        else
        {
            Debug.LogError("JSON file not found");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (Random.value < encounterChance)
            {
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
            SceneManager.LoadScene("TestScene_t");
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
