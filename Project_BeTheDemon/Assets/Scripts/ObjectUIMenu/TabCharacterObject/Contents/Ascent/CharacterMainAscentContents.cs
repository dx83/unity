using UnityEngine;
using UnityEngine.UI;


public class CharacterMainAscentContents
{
    GameObject ascentTabObject;
    GameObject scrollContents;


    public void Create(Transform parent)
    {
        // tab object
        ascentTabObject = new GameObject("Tab_Ascent");
        UIStaticFunc.SetParentAndLayer(ascentTabObject, parent, Constants.LAYER_UI);

        UIStaticFunc.SetRectTransformByPreset(ascentTabObject,
            Vector2.zero, Vector2.one, new Vector2(0.5f, 0.5f));

        ascentTabObject.AddComponent<Mask>();
        ascentTabObject.AddComponent<CanvasRenderer>();

        Image image = ascentTabObject.AddComponent<Image>();
        image.color = new Color(32 / 255f, 43 / 255f, 61 / 255f, 1f);

        // scroll object
        scrollContents = new GameObject("ScrollContents");
        UIStaticFunc.SetParentAndLayer(scrollContents, ascentTabObject.transform, Constants.LAYER_UI);

        UIStaticFunc.SetRectTransformByNormal(scrollContents,
            new Vector2(0f, 1f), new Vector2(0f, 1f), new Vector2(0f, 1f),
            new Vector2(720f, 0f), Vector2.zero); // 720f 0f => vertical scroll에서 높이는 가변적

        // scroll rect
        ScrollRect scrollRect = ascentTabObject.AddComponent<ScrollRect>();
        scrollRect.content = scrollContents.GetComponent<RectTransform>();
        scrollRect.vertical = true;
        scrollRect.movementType = ScrollRect.MovementType.Elastic;

        // character main attribute scroll
        CharacterMainAscentScroll scrollController = ascentTabObject.AddComponent<CharacterMainAscentScroll>();
        scrollController.padding = new RectOffset(10, 10, 10, 10);
        scrollController.spacing = 10;
        scrollController.itemLength = 120;
        CreateAscentItemInCharTab scrollItem = new CreateAscentItemInCharTab();
        scrollController.baseItem = scrollItem.Create(scrollContents.transform);
    }
}
