using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ButtonSimpleAnimation : MonoBehaviour, IPointerDownHandler, IPointerClickHandler, IPointerExitHandler, IPointerUpHandler
{
    private float startSize;
    private float minSize;
    private float maxSize;
    private float speedAnimation;
    private float size, targetSize;
    [SerializeField] private bool isMiniAnimation;

    [Header("Color")]
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI text;

    private Color startColorImage;
    private Color startColorText;

    private Color targetColorImage;
    private Color targetColorText;

    private Color pointerDownColorImage;
    private Color pointerDownColorText;

    [SerializeField] private RectTransform anotherRectTransform;
    private RectTransform rectTransform;

    private void Awake()
    {
        if (anotherRectTransform == null)
        {
            rectTransform = GetComponent<RectTransform>();
        }
        else
        {
            rectTransform = anotherRectTransform;
        }

        startSize = rectTransform.localScale.x;
        if (isMiniAnimation == false)
        {
            minSize = startSize * 0.92f;
            maxSize = startSize * 1.08f;
            speedAnimation = startSize * 3.5f;
        }
        else
        {
            minSize = startSize * 0.98f;
            maxSize = startSize * 1.02f;
            speedAnimation = startSize;
        }

        if (image != null)
        {
            startColorImage = image.color;
        }
        if (text != null)
        {
            startColorText = text.color;
        }
    }

    private void OnEnable()
    {
        size = startSize;
        targetSize = size;

        pointerDownColorImage = new Color(startColorImage.r - 0.1f, startColorImage.g - 0.1f, startColorImage.b - 0.1f, startColorImage.a - 0.2f);
        targetColorImage = startColorImage;

        pointerDownColorText = new Color(startColorText.r - 0.3f, startColorText.g - 0.3f, startColorText.b - 0.3f, startColorText.a - 0.1f);
        targetColorText = startColorText;

        if (image != null)
        {
            image.color = startColorImage;
        }
        if (text != null)
        {
            text.color = startColorText;
        }
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        targetSize = minSize;

        targetColorImage = pointerDownColorImage;
        targetColorText = pointerDownColorText;
    }

    public virtual void OnPointerClick(PointerEventData eventData)
    {
        OnClick();
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        targetSize = startSize;
        targetColorImage = startColorImage;
        targetColorText = startColorText;
    }

    public virtual void OnPointerUp(PointerEventData eventData)
    {
        OnClick();
    }

    private void Update()
    {
        size = Mathf.MoveTowards(size, targetSize, Time.fixedDeltaTime * speedAnimation);
        if (size == maxSize)
        {
            targetSize = startSize;
        }

        rectTransform.localScale = new Vector2(size, size);

        if (image != null)
        {
            image.color = Color.Lerp(image.color, targetColorImage, Time.fixedDeltaTime * 10f);
        }
        if (text != null)
        {
            text.color = Color.Lerp(text.color, targetColorText, Time.fixedDeltaTime * 10f);
        }
    }

    public bool GetIsClicked()
    {
        if (targetSize != startSize)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void OnClick()
    {
        targetSize = maxSize;
        targetColorImage = startColorImage;
        targetColorText = startColorText;
    }
}
