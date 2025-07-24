using UnityEngine;

public class BallProjectile : MonoBehaviour
{
    private bool isCanMove;
    private float infectionRadius;

    [SerializeField] private GameObject projectile;
    [SerializeField] private GameObject radiusView;
    [SerializeField] private SphereCollider sphereCollider;

    private bool hasHit = false;
    private float halfEnemySize = 0.25f;

    private float maxSize = 0.45f;
    private float maxRadius = 1.25f;
    private float fromRadiusCircle = 2f;

    private Vector3 target = new Vector3(0, 0, 1);
    private float speed = 2f;

    public void SetSize(float scale)
    {
        float normalizedSize = Mathf.Clamp01((scale - 0.05f) / maxSize);
        infectionRadius = Mathf.SmoothStep(0f, maxRadius, normalizedSize);

        radiusView.transform.localScale = new Vector2(infectionRadius * fromRadiusCircle, infectionRadius * fromRadiusCircle);
        projectile.transform.localScale = new Vector3(scale, scale, scale);

        sphereCollider.radius = scale / 2f;
    }

    public void StartMove()
    {
        isCanMove = true;
    }

    private void FixedUpdate()
    {
        if (isCanMove == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, Time.fixedDeltaTime * speed);
            if (transform.position == target)
            {
                hasHit = true;
                isCanMove = false;

                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hasHit == false && other.CompareTag("Enemy"))
        {
            EnemyEntity enemy = other.GetComponent<EnemyEntity>();
            if (enemy != null && enemy.GetIsInfected() == false)
            {
                enemy.SetIsInfected(true);
                InfectNearbyEnemies(infectionRadius + halfEnemySize);

                Terminate();
            }
        }
    }

    private void Terminate()
    {
        hasHit = true;
        isCanMove = false;

        Destroy(gameObject);
    }

    private void InfectNearbyEnemies(float radius)
    {
        var activeEnemies = EnemyPool.Instance.GetActiveEnemies();

        foreach (EnemyEntity e in activeEnemies)
        {
            float distance = Vector3.Distance(transform.position, e.transform.position);
            if (distance <= radius && e.GetIsInfected() == false)
            {
                e.SetIsInfected(true);
            }
        }
    }
}
