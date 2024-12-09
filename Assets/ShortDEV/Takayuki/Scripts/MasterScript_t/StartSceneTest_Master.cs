using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class StartSceneTest_Master : MonoBehaviour
{
    public TMP_Text titleText;
    public TMP_Text buttonText;

    void Start()
    {
        // タイトルテキストを設定
        if (titleText != null)
        {
            titleText.text = "Compliance Communications. Development All the brave men and women. Collaborate Four Heavens";
        }

        // ボタンテキストを設定
        if (buttonText != null)
        {
            buttonText.text = "Game Start !";
        }
    }

    public void GoToFieldScene()
    {
        SceneManager.LoadScene("FirstTown_Master"); // フィールド画面に移動

        // シーンがロードされた後にプレイヤーの位置を設定するイベントを追加
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "FirstTown_Master")
        {
            // プレイヤーオブジェクトを取得
            GameObject player = GameObject.FindWithTag("Player");
            if (player != null)
            {
                // プレイヤーのスポーン位置を設定
                player.transform.position = new Vector3(5, 1, 0); // ここで希望の座標を指定
            }
            else
            {
                Debug.LogError("Player not found in the scene!");
            }

            // イベントの解除
            SceneManager.sceneLoaded -= OnSceneLoaded; // 次回のシーンロード時に再度呼ばれないようにする
        }
    }
}
