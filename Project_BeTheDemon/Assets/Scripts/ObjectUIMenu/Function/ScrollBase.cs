using UnityEngine;


[RequireComponent(typeof(RectTransform))]
public class ScrollBase : MonoBehaviour
{
    private RectTransform cachedRect;
    public RectTransform CachedRect
    {
        get
        {
            if (cachedRect == null)
                cachedRect = this.GetComponent<RectTransform>();
            return cachedRect;
        }
    }
}
