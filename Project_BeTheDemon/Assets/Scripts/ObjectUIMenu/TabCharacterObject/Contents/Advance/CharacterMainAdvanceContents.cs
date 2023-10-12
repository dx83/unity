using UnityEngine;
using UnityEngine.UI;


public class CharacterMainAdvanceContents
{
    GameObject advanceTabObject;
    GameObject scrollContents;


    public void Create(Transform parent)
    {
        // tab object
        advanceTabObject = new GameObject("Tab_Advance");
        UIStaticFunc.SetParentAndLayer(advanceTabObject, parent, Constants.LAYER_UI);

        UIStaticFunc.SetRectTransformByPreset(advanceTabObject,
            Vector2.zero, Vector2.one, new Vector2(0.5f, 0.5f));

        advanceTabObject.AddComponent<Mask>();
        advanceTabObject.AddComponent<CanvasRenderer>();

        Image image = advanceTabObject.AddComponent<Image>();
        image.color = new Color(32 / 255f, 43 / 255f, 61 / 255f, 1f);

        // scroll object
        scrollContents = new GameObject("ScrollContents");
        UIStaticFunc.SetParentAndLayer(scrollContents, advanceTabObject.transform, Constants.LAYER_UI);

        UIStaticFunc.SetRectTransformByNormal(scrollContents,
            new Vector2(0f, 1f), new Vector2(0f, 1f), new Vector2(0f, 1f),
            new Vector2(720f, 0f), Vector2.zero); // 720f 0f => vertical scroll에서 높이는 가변적

        // scroll rect
        ScrollRect scrollRect = advanceTabObject.AddComponent<ScrollRect>();
        scrollRect.content = scrollContents.GetComponent<RectTransform>();
        scrollRect.vertical = true;
        scrollRect.movementType = ScrollRect.MovementType.Elastic;

        // character main attribute scroll
        CharacterMainAdvanceScroll scrollController = advanceTabObject.AddComponent<CharacterMainAdvanceScroll>();
        scrollController.padding = new RectOffset(10, 10, 10, 10);
        scrollController.spacing = 10;
        scrollController.itemLength = 120;
        CreateAdvanceItemInCharTab scrollItem = new CreateAdvanceItemInCharTab();
        scrollController.baseItem = scrollItem.Create(scrollContents.transform);
    }
}
