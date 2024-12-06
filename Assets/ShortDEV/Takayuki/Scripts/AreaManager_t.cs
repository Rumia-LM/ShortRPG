using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class AreaManager : MonoBehaviour
{
    private Dictionary<string, Dictionary<string, Vector2>> areaTransitions = new Dictionary<string, Dictionary<string, Vector2>>
    {
        { "FieldTest_Master", new Dictionary<string, Vector2>
            {
                { "Town1", new Vector2(0, 0) },
                { "Town2", new Vector2(5, 0) }
            }
        },
        { "FirstTown_Master", new Dictionary<string, Vector2>
            {
                { "FieldTest_Master", new Vector2(0, 0) }
            }
        },
        { "SecondCastle_Master", new Dictionary<string, Vector2>
            {
                { "FieldTest_Master", new Vector2(5, 0) }
            }
        },
        { "FinalTown_Master", new Dictionary<string, Vector2>
            {
                { "FieldTest_Master", new Vector2(10, 0) }
            }
        },
        { "BossScene_Master", new Dictionary<string, Vector2>
            {
                { "FieldTest_Master", new Vector2(15, 0) }
            }
        }
    };

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            string currentScene = SceneManager.GetActiveScene().name;
            string targetScene = GetTargetSceneName(currentScene); // 遷移先のシーン名を取得

            if (!string.IsNullOrEmpty(targetScene))
            {
                ChangeScene(targetScene);
            }
        }
    }

    public void ChangeScene(string sceneName)
    {
        if (areaTransitions.ContainsKey(SceneManager.GetActiveScene().name) &&
            areaTransitions[SceneManager.GetActiveScene().name].ContainsKey(sceneName))
        {
            Vector2 spawnPosition = areaTransitions[SceneManager.GetActiveScene().name][sceneName];
            PlayerPrefs.SetFloat("SpawnX", spawnPosition.x);
            PlayerPrefs.SetFloat("SpawnY", spawnPosition.y);
        }
        
        SceneManager.LoadScene(sceneName);
    }

    private string GetTargetSceneName(string currentScene)
    {
        // 遷移先のシーン名を取得するためのロジックを実装
        // 例: 現在のシーンに基づいて次のシーン名を返す
        // 必要に応じて、他のロジックを追加してください
        if (areaTransitions.ContainsKey(currentScene))
        {
            // ここで遷移先のシーン名を選択するロジックを実装
            // 例えば、次のシーン名を選択するための条件を追加
            // ここでは単純化のため、最初のキーを返しています
            foreach (var target in areaTransitions[currentScene])
            {
                return target.Key; // 最初の遷移先を返す
            }
        }
        return null;
    }
}
