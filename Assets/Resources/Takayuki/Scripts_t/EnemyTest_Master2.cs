using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTest_Master2 : CharacterTest_Master2
{
    public EnemyTest_Master2(string name, int maxHP, int atk) : base(name, maxHP, atk) {}

    // ランダムターゲットを選ぶ
    public CharacterTest_Master2 SelectTarget(CharacterTest_Master2[] targets)
    {
        int index = Random.Range(0, targets.Length);
        return targets[index];
    }
}