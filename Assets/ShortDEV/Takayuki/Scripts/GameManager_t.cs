using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager_t : MonoBehaviour
{
    public GameObject playerPrefab; // プレイヤープレハブ
    private GameObject playerInstance;

    void Start()
    {
        if (PlayerData_t.position != Vector3.zero)
        {
            Vector3 spawnPosition = PlayerData_t.targetPosition != Vector3.zero ? PlayerData_t.targetPosition : PlayerData_t.position;
            playerInstance = Instantiate(playerPrefab, spawnPosition, Quaternion.identity);
            PlayerController_t playerController = playerInstance.GetComponent<PlayerController_t>();
            playerController.health = PlayerData_t.health;
            playerController.attack = PlayerData_t.attack;
        }
        else
        {
            playerInstance = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity); // 初期位置が設定されていない場合のデフォルト位置
        }
    }
}
