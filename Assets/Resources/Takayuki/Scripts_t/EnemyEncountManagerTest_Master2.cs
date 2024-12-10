using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyEncounterManager_Master2 : MonoBehaviour
{
    public Transform player; // プレイヤーのTransform
    public Transform fieldCenter; // フィールドの中心
    public float encounterRadius = 10f; // 戦闘が発生する範囲の半径
    public float encounterTime = 5f; // 戦闘に移行するまでの待機時間（秒）

    private float timeInRange = 0f; // 指定範囲内に滞在している時間
    

    void Start()
    {
        if (player == null) return; // プレイヤーが存在しない場合は処理を中断

        // プレイヤーオブジェクトのTransformを取得
        if (PlayerDataManagerTest_Master2.Instance != null && PlayerDataManagerTest_Master2.Instance.playerPrefab != null)
        {
            player = PlayerDataManagerTest_Master2.Instance.playerPrefab.transform;
            Debug.Log($"Player Transform assigned: {player.name}");
        }
        else
        {
            Debug.LogError("PlayerDataManagerTest_Master2.Instance or its currentPlayer is null!");
        }
        player.transform.position = new Vector3(0,0,0);
    }
    
    void Update()
    {
        // プレイヤーが範囲内にいるか確認
        float distance = Vector3.Distance(player.position, fieldCenter.position);
        if (distance <= encounterRadius)
        {
            timeInRange += Time.deltaTime;

            if (timeInRange >= encounterTime)
            {
                StartEncounter();
            }
        }
        else
        {
            // 範囲外に出たらタイマーをリセット
            timeInRange = 0f;
        }
    }

    void StartEncounter()
    {
        Debug.Log("Enemy Encountered! Transitioning to Battle Scene...");
        SceneManager.LoadScene("BattleSceneTest_Master2"); // 戦闘シーンに移行
    }

}