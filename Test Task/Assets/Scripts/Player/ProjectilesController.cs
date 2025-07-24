using UnityEngine;
using System.Collections.Generic;

public class ProjectilesController : MonoBehaviour
{
    private List<BallProjectile> projectiles = new List<BallProjectile>();

    public void AddProjectiles(BallProjectile projectile)
    {
        if (!projectiles.Contains(projectile))
        {
            projectiles.Add(projectile);
        }
    }

    public void ClearAllProjectiles()
    {
        foreach (BallProjectile projectile in projectiles)
        {
            if (projectile != null)
                Destroy(projectile.gameObject);
        }

        projectiles.Clear();
    }
}