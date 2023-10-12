using UnityEngine;
using UnityEngine.UI;


public class CharacterMainAttributeContents
{
    GameObject attributeTabObject;
    GameObject scrollContents;


    public void Create(Transform parent)
    {
        // tab object
        attributeTabObject = new GameObject("Tab_Attribute");
        UIStaticFunc.SetParentAndLayer(attributeTabObject, parent, Constants.LAYER_UI);

        UIStaticFunc.SetRectTransformByPreset(attributeTabObject,
            Vector2.zero, Vector2.one, new Vector2(0.5f, 0.5f));

        attributeTabObject.AddComponent<Mask>();
        attributeTabObject.AddComponent<CanvasRenderer>();

        Image image = attributeTabObject.AddComponent<Image>();
        image.color = new Color(32 / 255f, 43 / 255f, 61 / 255f, 1f);

        // scroll object
        scrollContents = new GameObject("ScrollContents");
        UIStaticFunc.SetParentAndLayer(scrollContents, attributeTabObject.transform, Constants.LAYER_UI);

        UIStaticFunc.SetRectTransformByNormal(scrollContents,
            new Vector2(0f, 1f), new Vector2(0f, 1f), new Vector2(0f, 1f),
            new Vector2(720f, 0f), Vector2.zero); // 720f 0f => vertical scroll에서 높이는 가변적

        // scroll rect
        ScrollRect scrollRect = attributeTabObject.AddComponent<ScrollRect>();
        scrollRect.content = scrollContents.GetComponent<RectTransform>();
        scrollRect.vertical = true;
        scrollRect.movementType = ScrollRect.MovementType.Elastic;

        // character main attribute scroll
        CharacterMainAttributeScroll scrollController = attributeTabObject.AddComponent<CharacterMainAttributeScroll>();
        scrollController.padding = new RectOffset(10, 10, 10, 10);
        scrollController.spacing = 10;
        scrollController.itemLength = 120;
        CreateAttributeItemInCharTab scrollItem = new CreateAttributeItemInCharTab();
        scrollController.baseItem = scrollItem.Create(scrollContents.transform);
    }
}

