using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterTest_Master2
{
    public string Name;
    public int HP;
    public int MaxHP;
    public int ATK;

    public CharacterTest_Master2(string name, int maxHP, int atk)
    {
        Name = name;
        MaxHP = maxHP;
        HP = maxHP;
        ATK = atk;
    }

    // ダメージを受ける
    public void TakeDamage(int damage)
    {
        HP -= damage;
        if (HP < 0) HP = 0;
    }

    // HPがゼロなら死亡
    public bool IsDead()
    {
        return HP <= 0;
    }
}
