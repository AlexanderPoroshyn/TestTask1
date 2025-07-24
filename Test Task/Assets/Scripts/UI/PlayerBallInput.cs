using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerBallInput : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    public event Action OnPlayerPressed;
    public event Action OnPlayerReleased;

    public void OnPointerDown(PointerEventData eventData)
    {
        OnPlayerPressed?.Invoke();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        PlayerStopPressing();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        PlayerStopPressing();
    }

    private void PlayerStopPressing()
    {
        OnPlayerReleased?.Invoke();
    }
}
