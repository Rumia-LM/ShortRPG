using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleManagerTest_r : MonoBehaviour
{
    // UIコンポーネントの参照
    public Button AttackButton;
    public Button HealButton;
    public Button EscapeButton;
    public Slider EnemyHPBar;
    public TMP_Text BattleLogText;

    private PlayerTest_r player;
    private EnemyTest_r enemy;

    void Start()
    {
        // プレイヤーと敵を初期化
        player = new PlayerTest_r("勇者", 100, 20);
        enemy = new EnemyTest_r("スライム", 30, 10);

        // HPバーの初期化
        EnemyHPBar.maxValue = enemy.MaxHP;
        EnemyHPBar.value = enemy.HP;

        // ボタンに関数を登録
        AttackButton.onClick.AddListener(OnAttack);
        HealButton.onClick.AddListener(OnHeal);
        EscapeButton.onClick.AddListener(OnEscape);

        // 初期ログを表示
        UpdateBattleLog("戦闘開始！");
    }

    // 攻撃コマンド
    void OnAttack()
    {
        enemy.TakeDamage(player.ATK);
        UpdateBattleLog($"勇者は攻撃！スライムに{player.ATK}のダメージ！");
        UpdateEnemyHP();

        if (enemy.IsDead())
        {
            UpdateBattleLog("スライムを倒した！");
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

        UpdateBattleLog($"勇者は回復！{healAmount}のHPを回復！");

        EnemyTurn();
    }

    // 逃げるコマンド
    void OnEscape()
    {
        if (Random.Range(0f, 1f) > 0.5f)
        {
            UpdateBattleLog("勇者は逃げ出した！");
            EndBattle();
        }
        else
        {
            UpdateBattleLog("逃げるのに失敗した！");
            EnemyTurn();
        }
    }

    // 敵のターン
    void EnemyTurn()
    {
        player.TakeDamage(enemy.ATK);
        UpdateBattleLog($"スライムの攻撃！勇者に{enemy.ATK}のダメージ！");

        if (player.IsDead())
        {
            UpdateBattleLog("勇者は倒された...");
            EndBattle();
        }
    }

    // バトルログを更新
    void UpdateBattleLog(string message)
    {
        BattleLogText.text += message + "\n";
    }
    // 敵HPバー更新
    void UpdateEnemyHP()
    {
        EnemyHPBar.value = enemy.HP;
    }

    // バトル終了
    void EndBattle()
    {
        AttackButton.interactable = false;
        HealButton.interactable = false;
        EscapeButton.interactable = false;
    }
}