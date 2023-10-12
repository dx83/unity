using UnityEngine;


public class ScrollItem<T> : ScrollBase
{
    public virtual void InitializeItem(int dataIndex) { }
    public virtual void UpdateContent(T itemData) { }
    public float itemLength { get; set; }
    public int dataIndex { get; set; }
    
    public float top
    {
        get { return CachedRect.anchoredPosition.y; }
        set { CachedRect.anchoredPosition = new Vector2(CachedRect.anchoredPosition.x, value); }
    }
    
    public float bottom
    {
        get { return CachedRect.anchoredPosition.y - itemLength; }
        set { CachedRect.anchoredPosition = new Vector2(CachedRect.anchoredPosition.x, value); }
    }
}
