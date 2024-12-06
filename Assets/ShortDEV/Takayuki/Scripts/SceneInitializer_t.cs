using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneInitializer_t : MonoBehaviour
{
    void Start()
    {
        // 保存された目標位置を取得
        Vector3 targetPosition = PlayerData_t.GetTargetPosition();

        // プレイヤーを目標位置に移動
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            player.transform.position = targetPosition;
        }
        else
        {
            Debug.LogError("Player object not found in the scene.");
        }
    }
}
