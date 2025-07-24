using System;
using UnityEngine;

public class PlayerBallController : MonoBehaviour
{
    public event Action OnBallProjectileCreated;
    public event Action OnPlayerMassDepleted;
    private bool isCanShoot;
    private bool isCanDefeated;

    [Header("Main")]
    [SerializeField] private PlayerBallInput playerBallInput;
    [SerializeField] private GameObject ballObject;
    [SerializeField] private GameObject road;
    [SerializeField] private Animator ballAnimator;
    [SerializeField] private ExplosionEffect explosionEffect;

    [Header("Projectiles")]
    [SerializeField] private BallProjectile projectilePrefab;
    [SerializeField] private ProjectilesController projectilesController;
    [SerializeField] private ParticleSystem creationEffect;
    private BallProjectile currentProjectile;

    //Charging
    private float chargeTime = 0f;
    private bool isCharging = false;
    private float timeForFullBallShoot = 5f;

    //Sizes
    public const float START_BALL_SIZE = 1.3f;
    public const float MIN_BALL_SIZE = 0.26f;
    private float currentBallSize = 1.3f;
    private float ballSize = 1.3f;

    private void Awake()
    {
        playerBallInput.OnPlayerPressed += OnPointerDown;
        playerBallInput.OnPlayerReleased += OnPointerUp;
    }

    public void StartGameplay()
    {
        isCanShoot = true;
        ballSize = START_BALL_SIZE;

        isCanDefeated = true;
        isCharging = false;
        chargeTime = 0f;
    }

    public void ResetView()
    {
        ballAnimator.Play("Idle");
        SetBallSize(START_BALL_SIZE);
    }

    public void StopGameplay()
    {
        isCanShoot = false;
    }

    private void OnPointerDown()
    {
        if (isCanShoot == true)
        {
            isCharging = true;
            CreateProjectile();
            chargeTime = 0.1f;
        }
    }

    private void OnPointerUp()
    {
        if (isCharging == true)
        {
            isCharging = false;
            ShootProjectile();
        }
    }

    private void FixedUpdate()
    {
        if (isCanShoot == true)
        {
            if (isCharging == true)
            {
                chargeTime += Time.fixedDeltaTime / timeForFullBallShoot;
                UpdateVisualCharge();
            }
        }
    }

    private void CreateProjectile()
    {
        creationEffect.Play();

        currentProjectile = Instantiate(projectilePrefab, new Vector3(0, 0, -4.6f), Quaternion.identity);
        currentProjectile.gameObject.transform.parent = projectilesController.gameObject.transform;
        projectilesController.AddProjectiles(currentProjectile);
    }

    private void UpdateVisualCharge()
    {
        currentProjectile.SetSize(chargeTime);
        SetBallSize(ballSize - chargeTime);
    }

    private void ShootProjectile()
    {
        ballSize -= chargeTime;
        SetBallSize(ballSize);

        creationEffect.Stop();
        currentProjectile.StartMove();
        if (isCanDefeated == true)
        {
            OnBallProjectileCreated?.Invoke();
        }
    }

    private void SetBallSize(float size)
    {
        currentBallSize = size;
        ballObject.transform.localScale = new Vector3(size, size, size);
        ballObject.transform.localPosition = new Vector3(0, (size - 1f) / 2, 0f);

        road.transform.localScale = new Vector3(size, 9, 1);

        if (size <= MIN_BALL_SIZE && isCanDefeated == true)
        {
            isCanDefeated = false;
            OnPointerUp();
            Instantiate(explosionEffect, ballObject.transform.position, Quaternion.identity);
            OnPlayerMassDepleted?.Invoke();
        }
    }

    public void PlayAnimationVictory()
    {
        ballAnimator.Play("JumpingFinish");
    }

    public void PlayAnimationDefeat()
    {
        ballAnimator.Play("Disappear");
    }

    public float GetCurrentBallSize()
    {
        return currentBallSize;
    }
}
