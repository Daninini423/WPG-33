using System;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static int aliveEnemies = 0;
    public static Action<int> OnAliveEnemiesChanged;


    public static void ResetManager()
    {
        aliveEnemies = 0;
        OnAliveEnemiesChanged?.Invoke(aliveEnemies);
    }
}


