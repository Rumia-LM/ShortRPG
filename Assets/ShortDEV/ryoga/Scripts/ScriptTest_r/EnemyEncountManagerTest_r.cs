using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyEncounterManager : MonoBehaviour
{
    public Transform player; // プレイヤーのTransform
    public Transform fieldCenter; // フィールドの中心
    public float encounterRadius = 10f; // 戦闘が発生する範囲の半径
    public float encounterTime = 5f; // 戦闘に移行するまでの待機時間（秒）

    private float timeInRange = 0f; // 指定範囲内に滞在している時間

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
        if(PlayerDataManagerTest_r.Instance==null){
            Debug.LogError("PlayerDataManagerTest_r.Instance is null. Cannot call HidePlayer().");
            return;
        }
        Debug.Log("Enemy Encountered! Transitioning to Battle Scene...");
        SceneManager.LoadScene("BattleSceneTest_r"); // 戦闘シーンに移行
        SceneManager.sceneLoaded+=OnBattleSceneLoaded; //ロード完了時にプレイヤーを非表示
    }

    private void OnBattleSceneLoaded(Scene scene,LoadSceneMode mode){
        if(scene.name=="BattleSceneTest_r"){
            PlayerDataManagerTest_r.Instance.HidePlayer();
            SceneManager.sceneLoaded-=OnBattleSceneLoaded;
        }
    }
}