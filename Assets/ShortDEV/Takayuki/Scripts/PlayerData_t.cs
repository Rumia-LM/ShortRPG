using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData_t : MonoBehaviour
{
    public static Vector3 position;
    public static int health;
    public static int attack;

    public static void SaveData(Vector3 pos, int hp, int atk)
    {
        position = pos;
        health = hp;
        attack = atk;
    }

    internal static void SaveData(Vector3 position, object health, object attack)
    {
        throw new NotImplementedException();
    }
}

