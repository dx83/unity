using UnityEngine;
using UnityEngine.AddressableAssets;


public static class UIStaticFunc
{
    private static Font _LegacyRuntimeTTFFont;
    public static Font legacyRuntimeFont
    {
        get
        {
            if (_LegacyRuntimeTTFFont == null)
                _LegacyRuntimeTTFFont = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
            return _LegacyRuntimeTTFFont;
        }
    }

    private static Font _SEBANG_Gothic_Bold_Font;
    public static Font sebangGothicBoldFont
    {
        get
        {
            if (_SEBANG_Gothic_Bold_Font == null)
            {
                var loadCall = Addressables.LoadAssetAsync<Font>("Assets/Fonts/SEBANG Gothic Bold.ttf");
                _SEBANG_Gothic_Bold_Font = loadCall.WaitForCompletion();
            }
            return _SEBANG_Gothic_Bold_Font;
        }
    }

    public static void SetParentAndLayer(GameObject obj, Transform parent, string layer)
    {
        obj.transform.SetParent(parent);
        obj.layer = LayerMask.NameToLayer(layer);
    }

    public static void SetRectTransformByNormal(GameObject gameObject,
        Vector2 anchorMin, Vector2 anchorMax, Vector2 pivot)
        => SetRectTransformByNormal(gameObject, anchorMin, anchorMax, pivot, Vector2.zero, Vector2.zero);

    public static void SetRectTransformByNormal(GameObject gameObject,
        Vector2 anchorMin, Vector2 anchorMax, Vector2 pivot,
        Vector2 sizeDelta, Vector2 anchoredPos)
    {
        RectTransform rectTransform = gameObject.AddComponent<RectTransform>();
        rectTransform.localPosition = Vector3.zero;
        rectTransform.localScale = Vector3.one;
        rectTransform.anchorMin = anchorMin;
        rectTransform.anchorMax = anchorMax;
        rectTransform.pivot = pivot;
        rectTransform.offsetMin = Vector2.zero;
        rectTransform.offsetMax = Vector2.zero;
        rectTransform.sizeDelta = sizeDelta;    // width height
        rectTransform.anchoredPosition = anchoredPos;
    }

    public static void SetRectTransformByNormal(GameObject gameObject,
        Vector2 anchorMin, Vector2 anchorMax, Vector2 pivot,
        Vector2 sizeDelta, Vector2 anchoredPos, Vector3 scale)
    {
        RectTransform rectTransform = gameObject.AddComponent<RectTransform>();
        rectTransform.localPosition = Vector3.zero;
        rectTransform.localScale = scale;
        rectTransform.anchorMin = anchorMin;
        rectTransform.anchorMax = anchorMax;
        rectTransform.pivot = pivot;
        rectTransform.offsetMin = Vector2.zero;
        rectTransform.offsetMax = Vector2.zero;
        rectTransform.sizeDelta = sizeDelta;    // width height
        rectTransform.anchoredPosition = anchoredPos;
    }

    public static RectTransform SetRectTransformByPreset(GameObject gameObject,
       Vector2 anchorMin, Vector2 anchorMax, Vector2 pivot)
       => SetRectTransformByPreset(gameObject, anchorMin, anchorMax, pivot, Vector2.zero, Vector2.zero);

    public static RectTransform SetRectTransformByPreset(GameObject gameObject,
        Vector2 anchorMin, Vector2 anchorMax, Vector2 pivot,
        Vector2 offsetMin, Vector2 offsetMax)
    {
        RectTransform rectTransform = gameObject.AddComponent<RectTransform>();
        rectTransform.localPosition = Vector3.zero;
        rectTransform.localScale = Vector3.one;
        rectTransform.anchorMin = anchorMin;
        rectTransform.anchorMax = anchorMax;
        rectTransform.pivot = pivot;
        //offset : 안쪽 패딩 개념
        rectTransform.offsetMin = offsetMin;    // left     bottom
        rectTransform.offsetMax = offsetMax;    // -right   -top

        return rectTransform;
    }

    public static RectTransform SetRectTransformByPreset(GameObject gameObject,
        Vector2 anchorMin, Vector2 anchorMax, Vector2 pivot,
        Vector2 offsetMin, Vector2 offsetMax, Vector2 anchoredPos)
    {
        RectTransform rectTransform = gameObject.AddComponent<RectTransform>();
        rectTransform.localPosition = Vector3.zero;
        rectTransform.localScale = Vector3.one;
        rectTransform.anchorMin = anchorMin;
        rectTransform.anchorMax = anchorMax;
        rectTransform.pivot = pivot;
        //offset : 안쪽 패딩 개념
        rectTransform.offsetMin = offsetMin;    // left
        rectTransform.offsetMax = offsetMax;    // -right   height
        rectTransform.anchoredPosition = anchoredPos;   // PosY
        
        return rectTransform;
    }
}
