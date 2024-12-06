using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneInitializer_t : MonoBehaviour
{
    void Start()
    {
        // 保存された目標位置を取得
        Vector2 targetPosition = new Vector2(
            PlayerPrefs.GetFloat("PlayerX", 0), // デフォルト値を 0 に設定
            PlayerPrefs.GetFloat("PlayerY", 0)
        );

        // プレイヤーを取得して位置を変更
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            player.transform.position = new Vector3(targetPosition.x, targetPosition.y, player.transform.position.z);
            Debug.Log("Player moved to target position: " + targetPosition);
        }
        else
        {
            Debug.LogError("Player object not found in the scene.");
        }
    }
}

