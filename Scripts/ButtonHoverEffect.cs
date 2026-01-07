using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Hover Settings")]
    [SerializeField] private float scaleAmount = 1.08f;
    [SerializeField] private float animationSpeed = 0.3f;

    [Header("Colors")]
    [SerializeField] private Color normalColor;
    [SerializeField] private Color hoverColor;

    private Image buttonImage;
    private Vector3 originalScale;
    private bool isHovered = false;

    void Start()
    {
        buttonImage = GetComponent<Image>();
        originalScale = transform.localScale;
        normalColor = buttonImage.color;
    }

    void Update()
    {
        Vector3 targetScale = isHovered ? originalScale * scaleAmount : originalScale;
        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime / animationSpeed);

        Color targetColor = isHovered ? hoverColor : normalColor;
        buttonImage.color = Color.Lerp(buttonImage.color, targetColor, Time.deltaTime / animationSpeed);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovered = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovered = false;
    }
}