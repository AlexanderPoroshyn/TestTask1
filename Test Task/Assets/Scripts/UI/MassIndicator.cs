using UnityEngine;
using UnityEngine.UI;

public class MassIndicator : MonoBehaviour
{
    [SerializeField] private PlayerBallController playerBall;

    [Header("View")]
    [SerializeField] private Image massBar;
    [SerializeField] private Animator warningAnimator;

    private void OnEnable()
    {
        UpdateView();
    }

    private void Update()
    {
        UpdateView();
    }

    private void UpdateView()
    {
        float massPercentage = (playerBall.GetCurrentBallSize() - PlayerBallController.MIN_BALL_SIZE) / (PlayerBallController.START_BALL_SIZE - PlayerBallController.MIN_BALL_SIZE);
        massBar.fillAmount = massPercentage;

        if (massPercentage < 0.35f)
        {
            warningAnimator.Play("Warning");
        }
        else
        {
            warningAnimator.Play("Idle");
        }
    }
}
