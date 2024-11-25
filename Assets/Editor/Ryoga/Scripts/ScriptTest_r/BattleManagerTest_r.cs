using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManagerTest_r : MonoBehaviour
{
    private PlayerTest_r player;
    private EnemyTest_r[] enemies;
    private CharacterTest_r[] party;

    void Start()
    {
        // 味方と敵の初期化
        player = new PlayerTest_r("勇者", 100, 20);
        enemies = new EnemyTest_r[]
        {
            new EnemyTest_r("スライムA", 30, 10),
            new EnemyTest_r("スライムB", 30, 10)
        };
        party = new CharacterTest_r[] { player };

        Debug.Log("戦闘開始！");
        StartBattle();
    }

    void StartBattle()
    {
        while (true)
        {
            // 味方ターン
            PlayerTurn();

            // 敵ターン
            if (AllEnemiesDead()) break;
            EnemyTurn();

            // 状態確認
            DisplayStatus();

            if (AllEnemiesDead())
            {
                Debug.Log("敵を倒した！");
                break;
            }

            if (AllPartyDead())
            {
                Debug.Log("味方が全滅した...");
                break;
            }
        }
    }

    void PlayerTurn()
    {
        Debug.Log("味方のターン！");
        // 仮実装：戦うコマンドだけ実行
        foreach (EnemyTest_r enemy in enemies)
        {
            if (!enemy.IsDead())
            {
                enemy.TakeDamage(player.ATK);
                Debug.Log($"敵 {enemy.Name} に {player.ATK} ダメージ！");
                break;
            }
        }
    }

    void EnemyTurn()
    {
        Debug.Log("敵のターン！");
        foreach (EnemyTest_r enemy in enemies)
        {
            if (!enemy.IsDead())
            {
                CharacterTest_r target = enemy.SelectTarget(party);
                target.TakeDamage(enemy.ATK);
                Debug.Log($"敵 {enemy.Name} が {target.Name} に {enemy.ATK} ダメージ！");
            }
        }
    }

    void DisplayStatus()
    {
        Debug.Log($"味方：{player.Name} - HP: {player.HP}/{player.MaxHP}");
        foreach (EnemyTest_r enemy in enemies)
        {
            Debug.Log($"敵：{enemy.Name} - HP: {enemy.HP}/{enemy.MaxHP}");
        }
    }

    bool AllEnemiesDead()
    {
        foreach (EnemyTest_r enemy in enemies)
        {
            if (!enemy.IsDead()) return false;
        }
        return true;
    }

    bool AllPartyDead()
    {
        foreach (CharacterTest_r member in party)
        {
            if (!member.IsDead()) return false;
        }
        return true;
    }
}