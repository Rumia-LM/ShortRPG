using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyEncounterManagerTest_r : MonoBehaviour
{
    public float encounterRadius = 10f; // 戦闘が発生する範囲（コライダーの設定に使用）
    public float encounterWaitTime = 5f; // 戦闘に移行するまでの待機時間（秒）
    public float encounterProbability=0.5f; //戦闘発生確率(0.0~1.0)

    private bool playerInRange=false; //プレイヤーが範囲内にいるか
    private Coroutine encounterCoroutine; //現在実行中のコルーチン
    private Transform player; // プレイヤーのTransform

    void Start()
    {
        if (player == null) return; // プレイヤーが存在しない場合は処理を中断

        // プレイヤーオブジェクトのTransformを取得
        if (PlayerDataManagerTest_r.Instance != null && PlayerDataManagerTest_r.Instance.playerPrefab != null)
        {
            player = PlayerDataManagerTest_r.Instance.playerPrefab.transform;
            DontDestroyOnLoad(PlayerDataManagerTest_r.Instance.playerPrefab); //プレイヤーを保持
            Debug.Log($"Player Transform assigned: {player.name}");
        }
        else
        {
            Debug.LogError("PlayerDataManagerTest_r.Instance or its currentPlayer is null!");
        }
    }

    void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player")){ //プレイヤーが範囲内に入ったら
            playerInRange=true;
            Debug.Log("Player entered the encounter area.");
            if(encounterCoroutine==null){
                encounterCoroutine=StartCoroutine(WaitForEncounter());
            }
        }    
    }

    void OnTriggerExit(Collider other) {
        if(other.CompareTag("Player")){ //プレイヤーが範囲外に出たら
            playerInRange=false;
            Debug.Log("Player exited the encounter area.");
             if(encounterCoroutine!=null){
                 StopCoroutine(encounterCoroutine);
                encounterCoroutine=null;
            }
        }
    }

    private IEnumerator WaitForEncounter(){
        yield return new WaitForSeconds(encounterWaitTime); //指定待機時間
        //プレイヤーが範囲内にいる場合のみ処理を実行
        if(playerInRange){
            Debug.Log("Checking for encounter...");
            float randomValue=Random.Range(0f,1f);
            if(randomValue<=encounterProbability){
                Debug.Log("Encounter triggered!");
                StartEncounter();
            }else{
                Debug.Log("No encounter this time.");
            }
        }
        encounterCoroutine=null; //コルーチンをリセット
    }

    /*void Update()
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
    }*/

    void StartEncounter()
    {
        Debug.Log("Enemy Encountered! Transitioning to Battle Scene...");
        SceneManager.LoadScene("BattleSceneTest_r"); // 戦闘シーンに移行
    }
}