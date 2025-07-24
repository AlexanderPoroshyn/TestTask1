using UnityEngine;

public class VictoryChecker : MonoBehaviour
{
    private Vector2 zoneX;
    [SerializeField] private PlayerBallController playerBall;

    public bool CheckVictoryZoneCleared()
    {
        float zoneXBallRadius = playerBall.GetCurrentBallSize() / 2f;

        zoneX = new Vector2(-zoneXBallRadius, zoneXBallRadius);
        EnemyEntity[] enemies = EnemyPool.Instance.GetActiveEnemies();

        foreach (EnemyEntity enemy in enemies)
        {
            if (enemy.GetIsInfected() == false)
            {
                Vector3 pos = enemy.transform.position;
                float enemyRadius = 0.2f;

                float minX = pos.x - enemyRadius;
                float maxX = pos.x + enemyRadius;

                if (maxX >= zoneX.x && minX <= zoneX.y)
                {
                    return false;
                }
            }
        }
        return true;
    }
}

