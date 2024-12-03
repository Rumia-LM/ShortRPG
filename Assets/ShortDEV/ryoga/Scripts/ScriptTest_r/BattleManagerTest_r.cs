using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.IO;

public class BattleManagerTest_r : MonoBehaviour
{
    // UIコンポーネントの参照
    public Button AttackButton;
    public Button HealButton;
    public Button EscapeButton;
    public Slider PlayerHPBar;
    public TMP_Text PlayerNameAndHPText;
    public Slider EnemyHPBar;
    public TMP_Text BattleLogText; //テキスト表示エリア
    public ScrollRect BattleLogScrollRect;
    public Image FadeImage; //フェードアウト用の黒いイメージ
    public EnemyDataTest_r currentEnemy; //現在の敵
    public Image EnemyImage; //敵画像を表示するUI

    private PlayerTest_r player;
    private List<EnemyDataTest_r> enemyList;
    
    private bool isProcessing=false; //処理中のフラグ
    private bool isBattleOver=false; //戦闘終了のフラグ

    void Start()
    {
        //敵リストを初期化（JSONから読み込む）
        LoadEnemiesFromJSON();

        //ランダムに敵を選択
        SelectRandomEnemy();

        //シングルトンから敵データを取得
        currentEnemy=EnemyDataManagerTest_r.Instance.currentEnemy;

        //プレイヤーと敵を初期化
        InitializaPlayer();
              
        //HPバーの初期化
        InitializeHPBars();

        //戦闘ログに敵の名前を表示
        Debug.Log($"A wild {currentEnemy.name} appears!");

        // ボタンに関数を登録
        AttackButton.onClick.AddListener(()=>StartPlayerAction("attack"));
        HealButton.onClick.AddListener(()=>StartPlayerAction("heal"));
        EscapeButton.onClick.AddListener(()=>StartPlayerAction("escape"));

        // 初期ログを表示
        StartCoroutine(LogAction($"{currentEnemy.name} has appeared!"));
    }

    //プレイヤーの初期化
    private void InitializaPlayer(){
        player=new PlayerTest_r("Hero",PlayerDataManagerTest_r.Instance.MaxHP,PlayerDataManagerTest_r.Instance.ATK);
        player.HP=PlayerDataManagerTest_r.Instance.CurrentHP;
    }

    //HPバーを初期化
    void InitializeHPBars(){
        if(PlayerHPBar!=null){
            PlayerHPBar.maxValue=player.MaxHP;
            PlayerHPBar.value=player.HP;
            UpdatePlayerNameAndHPText();
        }
        if(EnemyHPBar!=null){
            EnemyHPBar.maxValue=currentEnemy.hp;
            EnemyHPBar.value=currentEnemy.hp;
        }else{
            Debug.LogError("EnemyHPBar is not assigned in the Inspector!");
        }
    }

    //JSONファイルから敵リストを読み込む
    private void LoadEnemiesFromJSON(){
        string filePath=Application.dataPath+"/ShortDEV/ryoga/Scripts/JSONFiles/monstersList_r.json";
        if(File.Exists(filePath)){
            string jsonText=File.ReadAllText(filePath);
            MonsterList monsterList=JsonUtility.FromJson<MonsterList>(jsonText);
            
            if(monsterList!=null&&monsterList.monsters!=null){
                enemyList=monsterList.monsters;
                Debug.Log($"Loaded {enemyList.Count} enemies from JSON!");
            }else{
                Debug.LogError("Failed to parse JSON or no enemies found");
            }
        }else{
            Debug.LogError($"JSON file not found at path:{filePath}");
        }
    }
    //ランダムに敵を選択
    private void SelectRandomEnemy(){
        if(enemyList!=null&&enemyList.Count>0){
            int randomIndex=UnityEngine.Random.Range(0,enemyList.Count);
            currentEnemy=enemyList[randomIndex];

            //敵画像を読み込む
            Sprite enemySprite=Resources.Load<Sprite>($"ryoga/Images/{currentEnemy.image}");
            if(enemySprite!=null){
                EnemyImage.sprite=enemySprite; //UIに画像を設定
                EnemyImage.enabled=true; //UIを表示
            }else{
                Debug.LogError($"Enemy image not found:{currentEnemy.image}");
                EnemyImage.enabled=false; //UIを非表示
            }
            
            //シングルトンに保存
            EnemyDataManagerTest_r.Instance.currentEnemy=currentEnemy;
            Debug.Log($"Selected Enemy:{currentEnemy.name},HP:{currentEnemy.hp},ATK:{currentEnemy.atk}");
        }else{
            Debug.LogError("No enemies available to select!");
        }
    }

    //プレイヤー行動の開始（攻撃、回復、にげる）
    void StartPlayerAction(string action){
        if(isProcessing||isBattleOver)return; //処理中・戦闘終了時はボタン入力を無効化
        StartCoroutine(PlayerActionCoroutine(action));
    }

    //プレイヤーのターン（コルーチン）
    IEnumerator PlayerActionCoroutine(string action){
        isProcessing=true; // 処理中フラグを設定
        SetButtonsInteractable(false); //ボタンを無効化

        // 攻撃コマンド
        if(action=="attack"){
            yield return LogAction("Hero attacked!");
            currentEnemy.hp-=player.ATK;

            //ダメージログ表示とHP更新、スプライト点滅を同時に行う
            yield return LogAction($"{currentEnemy.name} took {player.ATK} damage!",()=>{
                UpdateEnemyHP();
                
                StartCoroutine(FlashEnemySprite()); //スプライトを点滅
                
            });

            if(currentEnemy.hp<=0){
                yield return LogAction($"{currentEnemy.name} defeated!");
                HideEnemySpriteAndHPBar(); //敵スプライトとHPバーを非表示
                EndBattle("win"); //勝利処理
            }else{
                yield return EnemyTurn(); //敵のターンへ
            }

        // 回復コマンド
        }else if(action=="heal"){
            if(player.HP>=player.MaxHP){
                yield return LogAction("Hero healed!"); //HP満タン時、回復失敗
                yield return LogAction("But Hero's HP is full...");
            }else{
                int healAmount=UnityEngine.Random.Range(10,30);
                if(player.HP+healAmount>player.MaxHP){
                    healAmount=player.MaxHP-player.HP; //最大HPを超えない
                }
                player.HP+=healAmount;
                PlayerDataManagerTest_r.Instance.UpdateHP(player.HP); //HPを保存

                //回復ログ表示とHP更新を同時に行う
                yield return LogAction($"Hero healed! Recover {healAmount} HP!",()=>{
                    UpdatePlayerHP();
                });
            }
            yield return EnemyTurn(); //敵のターンへ

        // 逃げるコマンド
        }else if(action=="escape"){
            yield return LogAction("Hero escaped!");
            if(UnityEngine.Random.Range(0f,1f)>0.5f){
                yield return LogAction("Hero managed to escape!");
                EndBattle("escape"); //逃げる成功
            }else{
                yield return LogAction("Hero couldn't escape...");
                yield return EnemyTurn(); //敵のターンへ
            }
        }

        //戦闘が続いている場合
        if(!isBattleOver){
            SetButtonsInteractable(true); //ボタンを再度有効化
        }
        isProcessing=false; //処理中フラグを解除
    }

    // 敵のターン(コルーチン)
    IEnumerator EnemyTurn()
    {   
        yield return LogAction($"{currentEnemy.name} attacked!");
        player.TakeDamage(currentEnemy.atk);
        PlayerDataManagerTest_r.Instance.UpdateHP(player.HP); //HPを保存

        //ダメージログ表示とHP更新を同時に行う
        yield return LogAction($"Hero took {currentEnemy.atk} damage!",()=>{
            UpdatePlayerHP();
            StartCoroutine(ShakeAndFlashPlayerUI()); //プレイヤーUIの揺れ＆点滅
        });

        if (player.IsDead())
        {
            UpdateBattleLog("Hero was defeated...");
            EndBattle("lose"); //敗北処理
        }
    }

    //ログを1アクションずつ表示(コルーチン)
    private IEnumerator LogAction(String message,System.Action onComplete=null){
        ClearBattleLog(); //ログを削除
        yield return StartCoroutine(DisplayText(message)); //１文字づつ表示
        UpdateBattleLog(message); //新しいログを表示
        onComplete?.Invoke(); //完了時に指定された処理を実行
        yield return new WaitForSeconds(1.5f); //1.5秒間隔で次の処理へ進む
    }

    //テキストを１文字づつ表示するコルーチン
    private IEnumerator DisplayText(string message){
        BattleLogText.text=""; //表示をクリア
        foreach(char c in message){
            BattleLogText.text+=c; //１文字追加
            yield return new WaitForSeconds(0.05f); //一定時間待つ
        }
    }

    //プレイヤーUIを揺らして点滅させるコルーチン
    IEnumerator ShakeAndFlashPlayerUI(){
        //初期位置・色を保存
        Vector3 originalHPBarPosition=PlayerHPBar.transform.position;
        Vector3 originalTextPosition=PlayerNameAndHPText.transform.position;
        Color originalTextColor=PlayerNameAndHPText.color;

        int flashCount=3;
        float shakeMagnitude=40f; //揺れの強さ
        float flashInterval=0.1f;

        for(int i=0;i<flashCount;i++){
            //揺らす処理
            PlayerHPBar.transform.position=originalHPBarPosition+(Vector3.right*UnityEngine.Random.Range(-shakeMagnitude,shakeMagnitude));
            PlayerNameAndHPText.transform.position=originalTextPosition+(Vector3.up*UnityEngine.Random.Range(-shakeMagnitude,shakeMagnitude));

            //点滅処理
            PlayerNameAndHPText.color=Color.red; //赤色に変更
            yield return new WaitForSeconds(flashInterval);

            //位置をリセット
            PlayerHPBar.transform.position=originalHPBarPosition;
            PlayerNameAndHPText.transform.position=originalTextPosition;

            //元の色に戻す
            PlayerNameAndHPText.color=originalTextColor; 
            yield return new WaitForSeconds(flashInterval);
        }
        //最後に位置と色を完全にリセット
        PlayerHPBar.transform.position=originalHPBarPosition;
        PlayerNameAndHPText.transform.position=originalTextPosition;
        PlayerNameAndHPText.color=originalTextColor;
    }

    //スプライトを点滅させるコルーチン
    IEnumerator FlashEnemySprite(){
        if(EnemyImage==null){
            Debug.LogError("EnemyImage is not assigned!");
            yield break;
        }

        //点滅回数と間隔を指定
        int flashCount=3;
        float flashInterval=0.1f;
        for(int i=0;i<flashCount;i++){
            EnemyImage.color=new Color(1f,1f,1f,0f); //透明
            yield return new WaitForSeconds(flashInterval);
            EnemyImage.color=new Color(1f,1f,1f,1f); //元に戻す
            yield return new WaitForSeconds(flashInterval);
        }
    }

    // バトルログを更新
    void UpdateBattleLog(string message)
    {
        // ログを更新(1アクションのみ表示)
        BattleLogText.text=message;
        // Canvasの更新を強制して、UIを即座に反映
        Canvas.ForceUpdateCanvases();
        // ScrollRectのスクロール位置を最下部に設定
        BattleLogScrollRect.verticalNormalizedPosition = 0f;
    }

    //ログを削除
    void ClearBattleLog(){
        BattleLogText.text=""; //ログをクリア
    }

    //ボタンの有効・無効化
    void SetButtonsInteractable(bool state){
        AttackButton.interactable=state;
        HealButton.interactable=state;
        EscapeButton.interactable=state;
    }

    // プレイヤーのHPバーを更新
    void UpdatePlayerHP()
    {
        PlayerHPBar.value = player.HP;
        UpdatePlayerNameAndHPText();
    }

    //「名前+HP」のテキストを更新
    void UpdatePlayerNameAndHPText()
    {
        PlayerNameAndHPText.text = $"{player.Name}\nHP:{player.HP}/{player.MaxHP}"; // 例: "Hero  HP:50/100"
    }

    // 敵HPバー更新
    void UpdateEnemyHP()
    {
        EnemyHPBar.value = currentEnemy.hp;
    }

    // 敵のスプライトとHPバーを非表示
    void HideEnemySpriteAndHPBar(){
        if(EnemyImage!=null){
            EnemyImage.enabled=false; //スプライトを非表示
        }
        if(EnemyHPBar!=null){
            EnemyHPBar.gameObject.SetActive(false); //HPバーを非表示
        }
    }

    // バトル終了
    void EndBattle(string result)
    {
        isBattleOver=true; //戦闘終了フラグを立てる
        SetButtonsInteractable(false); //ボタンを無効化

        //戦闘結果に応じたシーンの切り替え
        if(result=="win"||result=="escape"){
            PlayerDataManagerTest_r.Instance.UpdateHP(player.HP); //現在のHPを保存
            StartCoroutine(FadeAndTransitionToScene("FieldTest_r")); //勝利・逃げる成功→フィールドに移行
        }else if(result=="lose"){
            PlayerDataManagerTest_r.Instance.ResetData(); //敗北時にデータをリセット
            StartCoroutine(FadeAndTransitionToScene("GameOverTest_r")); //敗北→GameOver画面に移行
        }
    }

    //シーン遷移コルーチン
    IEnumerator FadeAndTransitionToScene(string sceneName){
        yield return new WaitForSeconds(1f); //1秒待ってから
        yield return StartCoroutine(FadeOut()); //フェードアウト開始
        SceneManager.LoadScene(sceneName);
    }

    //フェードアウト処理
    IEnumerator FadeOut()
    {
        float fadeDuration = 1.5f; // フェードアウトにかかる時間
        float elapsedTime = 0f;

        Color fadeColor = FadeImage.color;

        // アルファ値を徐々に1.0に近づける
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            fadeColor.a = Mathf.Clamp01(elapsedTime / fadeDuration);
            FadeImage.color = fadeColor;
            yield return null;
        }
    }
}