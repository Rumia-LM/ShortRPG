using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTest_Master : CharacterTest_Master
{
    public EnemyTest_Master(string name, int maxHP, int atk) : base(name, maxHP, atk) {}

    // ランダムターゲットを選ぶ
    public CharacterTest_Master SelectTarget(CharacterTest_Master[] targets)
    {
        int index = Random.Range(0, targets.Length);
        return targets[index];
    }
}