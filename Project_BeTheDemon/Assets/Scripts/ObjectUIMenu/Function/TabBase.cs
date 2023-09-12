using UnityEngine;


[RequireComponent(typeof(RectTransform))]
public class TabBase : MonoBehaviour
{
    private RectTransform cachedRect;
    protected RectTransform CachedRect
    {
        get
        {
            if (cachedRect == null)
                cachedRect = this.GetComponent<RectTransform>();
            return cachedRect;
        }
    }
}
