using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController_t : MonoBehaviour
{
    private Transform player; // プレイヤーオブジェクトのTransformを指定
    private Vector3 playerSpawnPosition;
    public GameObject playerPrefab; // プレイヤーのプレハブを指定

    private void Start()
    {
        // プレイヤーオブジェクトを取得
        player = GameObject.FindWithTag("Player")?.transform; // タグを使ってプレイヤーを見つける
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // コライダーに触れたオブジェクトのタグを確認
        if (other.CompareTag("LastCastle"))
        {
            LoadScene("BossScene_Master2");
        }
        else if (other.CompareTag("LostVillage"))
        {
            LoadScene("FinalTown_Master2");
        }
        else if (other.CompareTag("Home"))
        {
            LoadScene("FirstTown_Master2");
        }
        else if (other.CompareTag("KingCastle"))
        {
            LoadScene("SecondCastle_Master2");
        }
        else if (other.CompareTag("WorldMap"))
        {
            LoadScene("FieldTest_Master2");
        }
    }

    public void LoadScene(string sceneName)
    {
        // 遷移先のシーンに応じてプレイヤーのスポーン位置を設定
        switch (sceneName)
        {
            case "FieldTest_Master2":
                if (SceneManager.GetActiveScene().name == "FirstTown_Master2")
                {
                    playerSpawnPosition = new Vector3(-26.28f, -12.45f, 0); // FieldTest_Master2からの遷移位置
                }
                else if (SceneManager.GetActiveScene().name == "FinalTown_Master2")
                {
                    playerSpawnPosition = new Vector3(-27.74f, 44.28f, 0); // FinalTown_Master2からの遷移位置
                }
                else if (SceneManager.GetActiveScene().name == "SecondCastle_Master2")
                {
                    playerSpawnPosition = new Vector3(26.78f, 13.16f, 0); // SecondCastle_Master2からの遷移位置
                }
                else if (SceneManager.GetActiveScene().name == "BossScene_Master2")
                {
                    playerSpawnPosition = new Vector3(28.12f, 43.86f, 0); // BossScene_Master2からの遷移位置
                }
                break;
            case "FirstTown_Master2":
                if (SceneManager.GetActiveScene().name == "FieldTest_Master2")
                {
                    playerSpawnPosition = new Vector3(0, 0, 0); // FieldTest_Master2の初期位置
                }
                break;
            case "FinalTown_Master2":
                if (SceneManager.GetActiveScene().name == "FieldTest_Master2")
                {
                    playerSpawnPosition = new Vector3(-5, -5, 0); // FieldTest_Master2の初期位置
                }
                break;
            case "SecondCastle_Master2":
                if (SceneManager.GetActiveScene().name == "FieldTest_Master2")
                {
                    playerSpawnPosition = new Vector3(-0.08f, -2.0f, 0); // FieldTest_Master2の初期位置
                }
                break;
            case "BossScene_Master2":
                if (SceneManager.GetActiveScene().name == "FieldTest_Master2")
                {
                    playerSpawnPosition = new Vector3(-0.16f, -1.57f, 0); // FieldTest_Master2の初期位置
                }
                break;
        }

        // シーンを遷移
        SceneManager.LoadScene(sceneName);

        // シーンがロードされた後にプレイヤーの位置を設定するイベント
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // プレイヤーオブジェクトを再取得
        player = GameObject.FindWithTag("Player")?.transform; // プレイヤーを見つける

        if (player == null)
        {
            // プレイヤーオブジェクトが見つからない場合、プレイヤーを生成
            if (playerPrefab != null) // プレハブが設定されているか確認
            {
                player = Instantiate(playerPrefab).transform; // プレイヤーを生成
                Debug.Log("Player instantiated in the new scene."); // 追加
            }
            else
            {
                Debug.LogError("Player prefab is not assigned!"); // エラーログを追加
                return; // プレハブが設定されていない場合、処理を中断
            }
        }

        // プレイヤーのスポーン位置を設定
        player.position = playerSpawnPosition; // ここでplayerSpawnPositionが正しいことを確認

        // イベントの解除
        SceneManager.sceneLoaded -= OnSceneLoaded; // 次回のシーンロード時に再度呼ばれないようにする
    }
}
