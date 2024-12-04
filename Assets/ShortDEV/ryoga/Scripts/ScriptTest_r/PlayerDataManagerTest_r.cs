using UnityEngine;

public class PlayerDataManagerTest_r : MonoBehaviour
{
    public static PlayerDataManagerTest_r Instance{get;private set;} //シングルトンインスタンス
    public GameObject playerPrefab; //プレイヤープレハブ
    public Vector3 spawnPosition=new Vector3(0,0,0); //初期位置

    public int MaxHP{get;private set;}=100; //最大HP
    public int CurrentHP{get;private set;}=100; //現在のHP
    public int ATK{get;private set;}=20; //攻撃力

    private GameObject currentPlayer; //現在のプレイヤー

    private void Awake() {
        //シングルトンの初期化
        if(Instance==null){
            Instance=this;
            DontDestroyOnLoad(gameObject); //シーン切り替え時に破棄されない
            Debug.Log($"PlayerDataManagerTest_r instance created: {gameObject.name}");
            
            //プレイヤーの生成
            if(playerPrefab!=null&&currentPlayer==null){
                currentPlayer=Instantiate(playerPrefab,spawnPosition,Quaternion.identity);
                currentPlayer.name="Player"; //プレイヤー名を設定
                Debug.Log("Player instantiated successfully.");
            }else if(playerPrefab==null){
                Debug.LogError("Player prefab is not assigned.");
            }
        }else{
            Debug.LogWarning($"Duplicate PlayerDataManagerTest_r instance detected: {gameObject.name}. Destroying this instance.");
            Destroy(gameObject); //重複するインスタンスを破棄する
        }
    }


    //HPを更新するメソッド
    public void UpdateHP(int newHP)
    {
        CurrentHP=Mathf.Clamp(newHP,0,MaxHP); //0からMaxHPの範囲内に制限
    }

    //データをリセットするメソッド(ゲームオーバー時など)
    public void ResetData(){
        CurrentHP=MaxHP;
    }
}
