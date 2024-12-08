using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using System;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class BattleManagerTest_Master : MonoBehaviour
{
    // UIコンポーネントの参照
    public Button AttackButton;
    public Button HealButton;
    public Button EscapeButton;
    public Slider PlayerHPBar;
    public TMP_Text PlayerNameAndHPText;
    public Slider EnemyHPBar;
    public TMP_Text BattleLogText;
    public ScrollRect BattleLogScrollRect;
    public GameObject EnemySprite;
    public Image FadeImage; //フェードアウト用の黒いイメージ

    public GameObject playerCharacter; // プレイヤーキャラクターの参照を追加

    private SpriteRenderer enemySpriteRenderer; //敵スプライトのレンダラー
    private PlayerTest_Master player;
    private EnemyTest_Master enemy;

    private bool isProcessing=false; //処理中のフラグ
    private bool isBattleOver=false; //戦闘終了のフラグ

    public static PlayerDataManagerTest_Master Instance { get; private set; }

    void Awake()
    {
        // PlayerDataManagerTest_Masterが正しく初期化されているか確認
        if (PlayerDataManagerTest_Master.Instance == null)
        {
            Debug.LogError("PlayerDataManagerTest_Master.Instance is not initialized!");
            return;
        }

        // ここに初期化処理を追加
        InitializeBattle(); // 例: 戦闘の初期化メソッドを呼び出す
    }

    void InitializeBattle()
    {
        // 戦闘の初期化処理をここに実装
        Debug.Log("Battle initialized.");
    }

    void Start()
    {
        Debug.Log("Start method called");
        if (PlayerDataManagerTest_Master.Instance == null)
        {
            Debug.LogError("PlayerDataManagerTest_Master.Instance is not initialized!");
            return;
        }
        Debug.Log("PlayerDataManagerTest_Master.Instance initialized");
        InitializePlayer();

        Debug.Log("Player initialized");

        //PlayerDataManagerTest_Masterからプレイヤー情報を取得
        int maxHP=PlayerDataManagerTest_Master.Instance.MaxHP; //最大HP
        int currentHP=PlayerDataManagerTest_Master.Instance.CurrentHP; //現在のHP
        player=new PlayerTest_Master("Hero",maxHP,PlayerDataManagerTest_Master.Instance.ATK); //ATK
        player.HP=currentHP;
        //敵を初期化
        enemy = new EnemyTest_Master("Slime", 50, 10);

        // HPバーの初期化
        PlayerHPBar.maxValue=player.MaxHP;
        PlayerHPBar.value=player.HP;
        UpdatePlayerNameAndHPText();

        EnemyHPBar.maxValue = enemy.MaxHP;
        EnemyHPBar.value = enemy.HP;

        //敵スプライトのレンダラーを取得
        if(EnemySprite!=null){
            enemySpriteRenderer=EnemySprite.GetComponent<SpriteRenderer>();
        }

        // ボタンに関数を登録
        AttackButton.onClick.AddListener(()=>StartPlayerAction("attack"));
        HealButton.onClick.AddListener(()=>StartPlayerAction("heal"));
        EscapeButton.onClick.AddListener(()=>StartPlayerAction("escape"));

        // 初期ログを表示
        UpdateBattleLog("Battle Start!");

        // バトルシーンではプレイヤーキャラクターを無効化
        if (SceneManager.GetActiveScene().name == "BattleSceneTest_Master")
        {
            playerCharacter.SetActive(false);
        }
    }

    void InitializePlayer()
    {
        Debug.Log("InitializePlayer method called");
        if (PlayerDataManagerTest_Master.Instance == null)
        {
            Debug.LogError("PlayerDataManagerTest_Master.Instance is null!"); return;
        }
        // プレイヤー初期化コード
        int maxHP = PlayerDataManagerTest_Master.Instance.MaxHP;
        int currentHP = PlayerDataManagerTest_Master.Instance.CurrentHP;

        // playerがnullであれば、新たにインスタンスを作成します。
        if (player == null)
        {
            player = new PlayerTest_Master("Hero", maxHP, PlayerDataManagerTest_Master.Instance.ATK);
            Debug.Log("Player instance created");
        }
        player.HP = currentHP; Debug.Log("Player initialized successfully");
    }

    public void GoToFieldScene()
    {
        // PlayerDataManagerTest_Masterのインスタンスを確保
        if (PlayerDataManagerTest_Master.Instance == null)
        {
            Debug.LogError("PlayerDataManagerTest_Master.Instance is not initialized!");
            return;
        }

        SceneManager.LoadScene("FirstTown_Master"); // フィールド画面に移動
    }

    void OnDestroy()
    {
        // シーン遷移時にプレイヤーキャラクターを再度有効化
        if (playerCharacter != null)
        {
            playerCharacter.SetActive(true);
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
            enemy.TakeDamage(player.ATK);

            //ダメージログ表示とHP更新、スプライト点滅を同時に行う
            yield return LogAction($"Slime took {player.ATK} damage!",()=>{
                UpdateEnemyHP();

                StartCoroutine(FlashEnemySprite()); //スプライトを点滅

            });

            if(enemy.IsDead()){
                yield return LogAction("Slime defeated!");
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
                PlayerDataManagerTest_Master.Instance.UpdateHP(player.HP); //HPを保存

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
        yield return LogAction("Slime attacked!");
        player.TakeDamage(enemy.ATK);
        PlayerDataManagerTest_Master.Instance.UpdateHP(player.HP); //HPを保存

        //ダメージログ表示とHP更新を同時に行う
        yield return LogAction($"Hero took {enemy.ATK} damage!",()=>{
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
    IEnumerator LogAction(String message,System.Action onComplete=null){
        ClearBattleLog(); //ログを削除
        UpdateBattleLog(message); //新しいログを表示
        onComplete?.Invoke(); //完了時に指定された処理を実行
        yield return new WaitForSeconds(1.5f); //1.5秒間隔で次の処理へ進む
    }

    //プレイヤーUIを揺らして点滅させるコルーチン
    IEnumerator ShakeAndFlashPlayerUI(){
        //初期位置・色を保存
        Vector3 originalHPBarPosition=PlayerHPBar.transform.position;
        Vector3 originalTextPosition=PlayerNameAndHPText.transform.position;
        Color originalTextColor=PlayerNameAndHPText.color;

        int flashCount=3;
        float shakeMagnitude=20f; //揺れの強さ
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
        if(enemySpriteRenderer==null)yield break;

        //点滅回数と間隔を指定
        int flashCount=3;
        float flashInterval=0.1f;
        for(int i=0;i<flashCount;i++){
            enemySpriteRenderer.color=new Color(1f,1f,1f,0f); //透明
            yield return new WaitForSeconds(flashInterval);
            enemySpriteRenderer.color=new Color(1f,1f,1f,1f); //元に戻す
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

    // プレイヤーのHPバーと名前+HPを更新
    void UpdatePlayerHP()
    {
        PlayerHPBar.value = player.HP;
        UpdatePlayerNameAndHPText();
    }

    void UpdatePlayerNameAndHPText()
    {
        PlayerNameAndHPText.text = $"{player.Name}\nHP:{player.HP}/{player.MaxHP}"; // 例: "Hero  HP:50/100"
    }

    // 敵HPバー更新
    void UpdateEnemyHP()
    {
        EnemyHPBar.value = enemy.HP;
    }

    // 敵のスプライトとHPバーを非表示
    void HideEnemySpriteAndHPBar(){
        if(EnemySprite!=null){
            EnemySprite.SetActive(false); //スプライトを非表示
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
            PlayerDataManagerTest_Master.Instance.UpdateHP(player.HP); //現在のHPを保存
            StartCoroutine(FadeAndTransitionToScene("FieldTest_Master")); //勝利・逃げる成功→フィールドに移行
        }else if(result=="lose"){
            PlayerDataManagerTest_Master.Instance.ResetData(); //敗北時にデータをリセット
            StartCoroutine(FadeAndTransitionToScene("GameOverTest_Master")); //敗北→GameOver画面に移行
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
        float fadeDuration = 2.0f; // フェードアウトにかかる時間
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