using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CharacterMidMenu_InitButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private Button initButton;
    private Image buttonIcon;

    private Color iconColor;

    private void Start()
    {
        initButton = GetComponent<Button>();
        buttonIcon = transform.GetChild(0).GetComponentInChildren<Image>();

        initButton.onClick.AddListener(ClickEvent);
        iconColor = buttonIcon.color;
    }

    public void ClickEvent()
    {
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        buttonIcon.color = new Color(iconColor.r - 0.1f, iconColor.g - 0.1f, iconColor.b - 0.1f);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        buttonIcon.color = iconColor;
    }
}
