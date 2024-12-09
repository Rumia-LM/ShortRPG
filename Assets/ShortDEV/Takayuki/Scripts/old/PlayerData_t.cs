using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData_t
{
    public static Vector3 position;
    public static Vector3 targetPosition; // 追加
    public static int health;
    public static int attack;

    public static void SaveData(Vector3 pos, int hp, int atk)
    {
        position = pos;
        health = hp;
        attack = atk;
    }

    public static void SaveTargetPosition(Vector3 targetPos)
    {
        targetPosition = targetPos;
    }

    internal static Vector3 GetTargetPosition()
    {
        throw new NotImplementedException();
    }
}

