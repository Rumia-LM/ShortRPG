using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneChanger_t : MonoBehaviour
{
    [SerializeField] private string sceneName; // 切り替えたいシーンの名前
    [SerializeField] private Vector3 targetPosition; // 切り替え後のプレイヤーの目標位置

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // プレイヤーの現在位置と状態を保存
            PlayerController_t player = other.GetComponent<PlayerController_t>();
            PlayerData_t.SaveData(player.transform.position, player.health, player.attack);
            PlayerData_t.SaveTargetPosition(targetPosition);

            Debug.Log("Player entered the scene change area.");
            SceneManager.LoadScene(sceneName);
        }
    }
}
