using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonPlay : MonoBehaviour, IPointerClickHandler
{
    public event Action OnClick;

    public virtual void OnPointerClick(PointerEventData eventData)
    {
        OnClick?.Invoke();
    }
}
