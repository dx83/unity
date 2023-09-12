using UnityEngine;
using UnityEngine.UI;


public class CharacterMainAbilityContents
{
    GameObject abilityTabObject;
    GameObject scrollContents;


    public void Create(Transform parent)
    {
        // tab object
        abilityTabObject = new GameObject("Tab_Ability");
        UIStaticFunc.SetParentAndLayer(abilityTabObject, parent, Constants.LAYER_UI);

        UIStaticFunc.SetRectTransformByPreset(abilityTabObject,
            Vector2.zero, Vector2.one, new Vector2(0.5f, 0.5f));

        abilityTabObject.AddComponent<Mask>();
        abilityTabObject.AddComponent<CanvasRenderer>();

        Image image = abilityTabObject.AddComponent<Image>();
        image.color = new Color(32 / 255f, 43 / 255f, 61 / 255f, 1f);

        // scroll object
        scrollContents = new GameObject("ScrollContents");
        UIStaticFunc.SetParentAndLayer(scrollContents, abilityTabObject.transform, Constants.LAYER_UI);

        UIStaticFunc.SetRectTransformByNormal(scrollContents,
            new Vector2(0f, 1f), new Vector2(0f, 1f), new Vector2(0f, 1f),
            new Vector2(720f, 0f), Vector2.zero); // 720f 0f => vertical scroll에서 높이는 가변적

        // scroll rect
        ScrollRect scrollRect = abilityTabObject.AddComponent<ScrollRect>();
        scrollRect.content = scrollContents.GetComponent<RectTransform>();
        scrollRect.vertical = true;
        scrollRect.movementType = ScrollRect.MovementType.Elastic;

        // character main attribute scroll
        CharacterMainAbilityScroll scrollController = abilityTabObject.AddComponent<CharacterMainAbilityScroll>();
        scrollController.padding = new RectOffset(10, 10, 10, 10);
        scrollController.spacing = 10;
        scrollController.itemLength = 120;
        CreateAbilityItemInCharTab scrollItem = new CreateAbilityItemInCharTab();
        scrollController.baseItem = scrollItem.Create(scrollContents.transform);
    }
}
