using System;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    public event Action OnEnemyReturnSetNotActive;
    public static EnemyPool Instance { get; private set; }

    public EnemyEntity[] enemies;

    private void Awake()
    {
        Instance = this;
    }

    public EnemyEntity[] GetAllEnemies()
    {
        return enemies;
    }

    public EnemyEntity[] GetActiveEnemies()
    {
        return Array.FindAll(enemies, e => e.gameObject.activeInHierarchy);
    }

    public void OnEnemyWasDestroyed()
    {
        OnEnemyReturnSetNotActive?.Invoke();
    }
}