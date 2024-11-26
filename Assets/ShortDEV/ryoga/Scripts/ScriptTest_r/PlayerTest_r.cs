using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTest_r : CharacterTest_r
{
    public PlayerTest_r(string name, int maxHP, int atk) : base(name, maxHP, atk) {}

    // 回復スキル（回復量はランダム）
    public void HealParty(CharacterTest_r[] party)
    {
        foreach (CharacterTest_r member in party)
        {
            int healAmount = Random.Range(10, 30);
            member.HP += healAmount;
            if (member.HP > member.MaxHP) member.HP = member.MaxHP;
            Debug.Log($"{member.Name}は{healAmount}回復した！");
        }
    }

    // 逃走（成功確率50%）
    public bool TryEscape()
    {
        return Random.Range(0f, 1f) > 0.5f;
    }
}