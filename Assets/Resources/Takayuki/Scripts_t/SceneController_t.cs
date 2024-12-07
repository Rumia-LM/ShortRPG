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
            LoadScene("BossScene_Master");
        }
        else if (other.CompareTag("LostVillage"))
        {
            LoadScene("FinalTown_Master");
        }
        else if (other.CompareTag("Home"))
        {
            LoadScene("FirstTown_Master");
        }
        else if (other.CompareTag("KingCastle"))
        {
            LoadScene("SecondCastle_Master");
        }
        else if (other.CompareTag("WorldMap"))
        {
            LoadScene("FieldTest_Master");
        }
    }

    public void LoadScene(string sceneName)
    {
        // 遷移先のシーンに応じてプレイヤーのスポーン位置を設定
        switch (sceneName)
        {
            case "FieldTest_Master":
                if (SceneManager.GetActiveScene().name == "FirstTown_Master")
                {
                    playerSpawnPosition = new Vector3(-29, -12, 0); // FieldTest_Masterからの遷移位置
                }
                else if (SceneManager.GetActiveScene().name == "FinalTown_Master")
                {
                    playerSpawnPosition = new Vector3(-22, 742, 0); // FinalTown_Masterからの遷移位置
                }
                else if (SceneManager.GetActiveScene().name == "SecondCastle_Master")
                {
                    playerSpawnPosition = new Vector3(26, 15, 0); // SecondCastle_Masterからの遷移位置
                }
                else if (SceneManager.GetActiveScene().name == "BossScene_Master")
                {
                    playerSpawnPosition = new Vector3(27, 144, 0); // BossScene_Masterからの遷移位置
                }
                break;
            case "FirstTown_Master":
                if (SceneManager.GetActiveScene().name == "FieldTest_Master")
                {
                    playerSpawnPosition = new Vector3(-29, -12, 0); // Scene1の初期位置
                }
                break;
            case "FinalTown_Master":
                if (SceneManager.GetActiveScene().name == "FieldTest_Master")
                {
                    playerSpawnPosition = new Vector3(-5, -5, 0); // Scene1の初期位置
                }
                break;
            case "SecondCastle_Master":
                if (SceneManager.GetActiveScene().name == "FieldTest_Master")
                {
                    playerSpawnPosition = new Vector3(-5, -5, 0); // Scene1の初期位置
                }
                break;
            case "BossScene_Master":
                if (SceneManager.GetActiveScene().name == "FieldTest_Master")
                {
                    playerSpawnPosition = new Vector3(-5, -5, 0); // Scene1の初期位置
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
