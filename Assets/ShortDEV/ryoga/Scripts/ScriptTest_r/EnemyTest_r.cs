using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTest_r : CharacterTest_r
{
    public EnemyTest_r(string name, int maxHP, int atk) : base(name, maxHP, atk) {}

    // ランダムターゲットを選ぶ
    public CharacterTest_r SelectTarget(CharacterTest_r[] targets)
    {
        int index = Random.Range(0, targets.Length);
        return targets[index];
    }
}