using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class LowerMenuTabItem : TabItem<LowerMenuTabData>
{
    [SerializeField] public Image iconImage;
    [SerializeField] public Text titleName;

    Color tabColorImage, tabColorName;

    public override void InitializeTab(RectTransform content, int index)
    {
        toggle = this.GetComponent<Toggle>();
        toggle.image.sprite = tabIdle;
        contentTab = content.GetChild(index).GetComponent<RectTransform>();
        iconImage.useSpriteMesh = true;
    }

    public override void FillContent(LowerMenuTabData tabData)
    {
        iconImage.sprite = tabData.tabIconSprite;
        titleName.text = tabData.tabTitleText;
    }

    public override bool ContentActiveSelf()
    { 
        return contentTab.gameObject.activeSelf;
    }

    public override void ContentSetActive(bool active)
    {
        contentTab.gameObject.SetActive(active);
    }


    public override void OnPointerDown(PointerEventData eventData)
    {
        float r, g, b;
        tabColorImage = iconImage.color;
        r = (tabColorImage.r - 0.4f) < 0.4f ? tabColorImage.r : (tabColorImage.r - 0.4f);
        g = (tabColorImage.g - 0.4f) < 0.4f ? tabColorImage.g : (tabColorImage.g - 0.4f);
        b = (tabColorImage.b - 0.4f) < 0.4f ? tabColorImage.b : (tabColorImage.b - 0.4f);
        iconImage.color = new Color(r, g, b);

        tabColorName = titleName.color;
        r = (tabColorName.r - 0.4f) < 0.4f ? tabColorName.r : (tabColorName.r - 0.4f);
        g = (tabColorName.g - 0.4f) < 0.4f ? tabColorName.g : (tabColorName.g - 0.4f);
        b = (tabColorName.b - 0.4f) < 0.4f ? tabColorName.b : (tabColorName.b - 0.4f);
        titleName.color = new Color(r, g, b);

        CachedRect.sizeDelta = new Vector2(originSize.x * morphSize.x, originSize.y * morphSize.y);
    }

    public override void ClickEvent(bool turn)
    {
        toggle.image.sprite = turn ? tabActive : tabIdle;
        CachedRect.sizeDelta = new Vector2(originSize.x * (turn ? morphSize.x : 1.0f),
                                           originSize.y * (turn ? morphSize.y : 1.0f));
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        iconImage.color = tabColorImage;
        titleName.color = tabColorName;
        CachedRect.sizeDelta = new Vector2(originSize.x, originSize.y);
    }
}
