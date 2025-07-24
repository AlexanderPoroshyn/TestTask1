using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private InterfaceController interfaceController;
    [SerializeField] private ButtonPlay buttonPlay;

    [Header("Controllers")]
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private ProjectilesController projectilesController;
    [SerializeField] private PlayerBallController playerBallController;

    [Header("Gameplay")]
    [SerializeField] private Door door;
    [SerializeField] private VictoryChecker victoryChecker;

    [Header("Pools")]
    [SerializeField] private EnemyPool enemyPool;

    private bool isGameActive = false;

    private void Awake()
    {
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0;
    }

    private void Start()
    {
        enemySpawner.OnEnemiesSpawned += StartGame;

        playerBallController.OnBallProjectileCreated += CheckVictory;
        enemyPool.OnEnemyReturnSetNotActive += CheckVictory;

        playerBallController.OnPlayerMassDepleted += OnDefeat;

        buttonPlay.OnClick += CreateLevel;
        interfaceController.ShowMenuScreen(InterfaceController.MenuState.Start);
    }

    private void CreateLevel()
    {
        if (isGameActive == false)
        {
            isGameActive = true;

            interfaceController.HideAll();
            playerBallController.ResetView();
            door.SetState(false);
            enemySpawner.SpawnEnemies();
        }
    }

    private void StartGame()
    {
        playerBallController.StartGameplay();
        interfaceController.ShowGameplayScreen();
    }

    private void StopGame()
    {
        interfaceController.HideAll();

        isGameActive = false;
        playerBallController.StopGameplay();
        projectilesController.ClearAllProjectiles();
    }

    private void CheckVictory()
    {
        if (victoryChecker.CheckVictoryZoneCleared())
        {
            OnVictory();
        }
    }

    private void OnVictory()
    {
        if (isGameActive == true)
        {
            StopGame();
            playerBallController.PlayAnimationVictory();

            door.SetState(true);
            StartCoroutine(ShowMenuScreen(2.5f, InterfaceController.MenuState.Win));
        }
    }

    private void OnDefeat()
    {
        if (isGameActive == true)
        {
            StopGame();
            playerBallController.PlayAnimationDefeat();

            StartCoroutine(ShowMenuScreen(1f, InterfaceController.MenuState.Lose));
        }
    }

    private IEnumerator ShowMenuScreen(float timer, InterfaceController.MenuState menuState)
    {
        yield return new WaitForSeconds(timer);
        interfaceController.ShowMenuScreen(menuState);
    }
}
