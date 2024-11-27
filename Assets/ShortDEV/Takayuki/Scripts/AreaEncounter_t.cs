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
    public int experience;
    public int gold;
}

public class AreaEncounter_t : MonoBehaviour
{
    public string jsonFilePath = "Assets/ShortDEV/Takayuki/Scripts/area1_monsters.json"; // 各エリアごとのJSONファイル
    private List<Monster> monsters;
    private float encounterCooldown = 2.0f; // エンカウント判定の間隔（秒）
    private float lastEncounterTime;
    public Transform playerTransform; // プレイヤーのTransform
    public float movementThreshold = 0.1f; // 移動速度のしきい値
    private Vector3 lastPlayerPosition;

    void Start()
    {
        LoadMonstersFromJson();
        Debug.Log("Monsters list count after load: " + (monsters != null ? monsters.Count.ToString() : "null"));
        lastEncounterTime = Time.time; // 最初のエンカウントタイムを初期化
        lastPlayerPosition = playerTransform.position; // 初期プレイヤー位置を記憶
    }

    void LoadMonstersFromJson()
    {
        string filePath = jsonFilePath; // 相対パスで指定
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
            Vector3 currentPlayerPosition = playerTransform.position;
            float distanceMoved = Vector3.Distance(lastPlayerPosition, currentPlayerPosition);

            if (distanceMoved >= movementThreshold)
            {
                if (Time.time - lastEncounterTime >= encounterCooldown)
                {
                    Debug.Log("Player is moving in the encounter area.");
                    if (Random.value < 0.2f) // エンカウント確率20%
                    {
                        Debug.Log("Encounter triggered.");
                        TriggerEncounter();
                    }
                    lastEncounterTime = Time.time; // エンカウントタイムを更新
                }
            }

            lastPlayerPosition = currentPlayerPosition; // プレイヤーの位置を更新
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
