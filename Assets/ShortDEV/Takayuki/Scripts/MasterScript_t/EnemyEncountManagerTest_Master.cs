using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyEncounterManager_Master : MonoBehaviour
{
    public static EnemyEncounterManager_Master instance; // シングルトンパターン
    public Transform player; // プレイヤーのTransform
    public Transform fieldCenter; // フィールドの中心
    public float encounterRadius = 10f; // 戦闘が発生する範囲の半径
    public float encounterTime = 5f; // 戦闘に移行するまでの待機時間（秒）

    private float timeInRange = 0f; // 指定範囲内に滞在している時間

    void Awake()
    {
        // シングルトンパターンの実装
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject); // 重複するオブジェクトを破棄
        }
    }

    void Update()
    {
        // playerとfieldCenterがnullでないことを確認
        if (player == null || fieldCenter == null)
        {
            Debug.LogWarning("Player or FieldCenter is null.");
            return;
        }

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
        SceneManager.LoadScene("BattleSceneTest_Master"); // 戦闘シーンに移行
    }

    private void OnDrawGizmosSelected()
    {
        // フィールド範囲を視覚的に表示するためのGizmo
        if (fieldCenter != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(fieldCenter.position, encounterRadius);
        }
    }
}
