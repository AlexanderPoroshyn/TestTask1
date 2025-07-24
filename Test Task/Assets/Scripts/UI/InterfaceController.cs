using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class InterfaceController : MonoBehaviour
{
    public RectTransform menuScreen;
    public RectTransform gameplayScreen;

    [Header("MenuSettings")]
    [SerializeField] private Image actionButtonImage;
    [SerializeField] private Sprite actionButtonSpriteMain, actionButtonSpriteRepeat;
    [SerializeField] private TextMeshProUGUI statusTitleText;
    [SerializeField] private TextMeshProUGUI actionButtonText;

    public void ShowMenuScreen(MenuState state)
    {
        switch (state)
        {
            case MenuState.Start:
                actionButtonImage.sprite = actionButtonSpriteMain;
                statusTitleText.text = "Welcome!";
                actionButtonText.text = "Play";
                break;
            case MenuState.Win:
                actionButtonImage.sprite = actionButtonSpriteRepeat;
                statusTitleText.text = "You win!";
                actionButtonText.text = "Repeat";
                break;
            case MenuState.Lose:
                actionButtonImage.sprite = actionButtonSpriteRepeat;
                statusTitleText.text = "You lose.";
                actionButtonText.text = "Repeat";
                break;
        }
        menuScreen.gameObject.SetActive(true);
        gameplayScreen.gameObject.SetActive(false);
    }

    public void ShowGameplayScreen()
    {
        menuScreen.gameObject.SetActive(false);
        gameplayScreen.gameObject.SetActive(true);
    }

    public void HideAll()
    {
        menuScreen.gameObject.SetActive(false);
        gameplayScreen.gameObject.SetActive(false);
    }

    public enum MenuState
    {
        Start,
        Win,
        Lose
    }
}
