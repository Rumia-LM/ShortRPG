using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleManagerTest_r : MonoBehaviour
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

    private PlayerTest_r player;
    private EnemyTest_r enemy;

    void Start()
    {
        // プレイヤーと敵を初期化
        player = new PlayerTest_r("Hero", 100, 20);
        enemy = new EnemyTest_r("Slime", 30, 10);

        // HPバーの初期化
        PlayerHPBar.maxValue=player.MaxHP;
        PlayerHPBar.value=player.HP;
        UpdatePlayerNameAndHPText();

        EnemyHPBar.maxValue = enemy.MaxHP;
        EnemyHPBar.value = enemy.HP;

        // ボタンに関数を登録
        AttackButton.onClick.AddListener(OnAttack);
        HealButton.onClick.AddListener(OnHeal);
        EscapeButton.onClick.AddListener(OnEscape);

        // 初期ログを表示
        UpdateBattleLog("Battle Start!");
    }

    // 攻撃コマンド
    void OnAttack()
    {
        enemy.TakeDamage(player.ATK);
        UpdateBattleLog($"Hero attacked! Slime took {player.ATK} damage!");
        UpdateEnemyHP();

        if (enemy.IsDead())
        {
            UpdateBattleLog("Slime defeated!");
            HideEnemySprite(); // 敵のスプライトを非表示
            EndBattle();
        }
        else
        {
            EnemyTurn();
        }
    }

    // 回復コマンド
    void OnHeal()
    {
        int healAmount = Random.Range(10, 30);
        player.HP += healAmount;
        if (player.HP > player.MaxHP) player.HP = player.MaxHP;

        UpdateBattleLog($"Hero healed {healAmount} HP!");
        UpdatePlayerHP();
        EnemyTurn();
    }

    // 逃げるコマンド
    void OnEscape()
    {
        if (Random.Range(0f, 1f) > 0.5f)
        {
            UpdateBattleLog("Hero successfully escaped!");
            EndBattle();
        }
        else
        {
            UpdateBattleLog("Escape failed!");
            EnemyTurn();
        }
    }

    // 敵のターン
    void EnemyTurn()
    {
        player.TakeDamage(enemy.ATK);
        UpdateBattleLog($"Slime attacked! Hero took {enemy.ATK} damage!");
        UpdatePlayerHP();

        if (player.IsDead())
        {
            UpdateBattleLog("Hero was defeated...");
            EndBattle();
        }
    }

    // バトルログを更新(スクロール対応)
    void UpdateBattleLog(string message)
    {
        // ログに新しいメッセージを追加
        BattleLogText.text += message + "\n";
        // テキストの親オブジェクト（Content）の高さを再計算
        LayoutRebuilder.ForceRebuildLayoutImmediate(BattleLogText.rectTransform);
        // Canvasの更新を強制して、UIを即座に反映
        Canvas.ForceUpdateCanvases();
        // ScrollRectのスクロール位置を最下部に設定
        BattleLogScrollRect.verticalNormalizedPosition = 1f;
    }

    // プレイヤーのHPバーと名前+HPを更新
    void UpdatePlayerHP()
    {
        PlayerHPBar.value = player.HP;
        UpdatePlayerNameAndHPText();
    }

    void UpdatePlayerNameAndHPText()
    {
        PlayerNameAndHPText.text = $"{player.Name}  HP:{player.HP}/{player.MaxHP}"; // 例: "Hero  HP:50/100"
    }

    // 敵HPバー更新
    void UpdateEnemyHP()
    {
        EnemyHPBar.value = enemy.HP;
    }

    // 敵のスプライトを非表示
    void HideEnemySprite(){
        EnemySprite.SetActive(false);
    }

    // バトル終了
    void EndBattle()
    {
        AttackButton.interactable = false;
        HealButton.interactable = false;
        EscapeButton.interactable = false;
    }
}