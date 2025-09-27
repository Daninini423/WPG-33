using System;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static int aliveEnemies = 0;
    public static Action<int> OnAliveEnemiesChanged;

    public static void ChangeAlive(int delta)
    {
        aliveEnemies += delta;
        if (aliveEnemies < 0) aliveEnemies = 0;
        OnAliveEnemiesChanged?.Invoke(aliveEnemies);
    }
}
