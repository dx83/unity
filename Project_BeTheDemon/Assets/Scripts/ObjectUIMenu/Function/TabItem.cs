using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class TabItem<T> : TabBase, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] public Sprite tabIdle;
    [SerializeField] public Sprite tabActive;

    [HideInInspector] public Toggle toggle;
    [HideInInspector] public bool fakeTab;
    [HideInInspector] public Vector2 originSize, morphSize;

    public virtual void InitializeTab(RectTransform content, int index) { }
    public virtual void FillContent(T tabData) { }

    protected RectTransform contentTab;
    public virtual bool ContentActiveSelf() { return false; }
    public virtual void ContentSetActive(bool active) { }
    public virtual void TabSelectionInit() { }

    public virtual void ClickEvent(bool turn) { }

    public virtual void OnPointerDown(PointerEventData eventData) { }
    public virtual void OnPointerUp(PointerEventData eventData) { }
}
