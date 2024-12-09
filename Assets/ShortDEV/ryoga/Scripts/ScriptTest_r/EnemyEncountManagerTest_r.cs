using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyEncounterManagerTest_r : MonoBehaviour
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
        if (PlayerDataManagerTest_r.Instance != null && PlayerDataManagerTest_r.Instance.playerPrefab != null)
        {
            player = PlayerDataManagerTest_r.Instance.playerPrefab.transform;
            Debug.Log($"Player Transform assigned: {player.name}");
        }
        else
        {
            Debug.LogError("PlayerDataManagerTest_r.Instance or its currentPlayer is null!");
        }
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
        SceneManager.LoadScene("BattleSceneTest_r"); // 戦闘シーンに移行
    }

    //戦闘シーンロード完了後の処理
    /*private void OnBattleSceneLoaded(){
        PlayerDataManagerTest_r.Instance.HidePlayer();  
        Debug.Log("Player has been hidden after transitioning to BattleSceneTest_r.");
    }*/
}