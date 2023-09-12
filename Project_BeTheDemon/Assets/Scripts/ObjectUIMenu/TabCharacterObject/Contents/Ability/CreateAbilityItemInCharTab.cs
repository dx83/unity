using UnityEngine;
using UnityEngine.UI;


public class CreateAbilityItemInCharTab
{
    GameObject abilityItemObject;

    public GameObject Create(Transform scrollContents)
    {
        // item object
        abilityItemObject = new GameObject("AbilityItem");
        UIStaticFunc.SetParentAndLayer(abilityItemObject, scrollContents, Constants.LAYER_UI);

        UIStaticFunc.SetRectTransformByNormal(abilityItemObject,
            new Vector2(0f, 1f), new Vector2(0f, 1f), new Vector2(0f, 1f),
            new Vector2(700f, 120f), Vector2.zero); // Vector2.zero => scrollcontroller 에서 제어

        abilityItemObject.AddComponent<CanvasRenderer>();

        Image image = abilityItemObject.AddComponent<Image>();
        image.sprite = SpriteSheetManager.GetSpriteByName("LoginPassword_Box");
        image.color = new Color(66 / 255f, 96 / 255f, 144 / 255f, 1f);

        CreateAbilityPortraitObject();
        CreateAbilityContentsObject();
        CreateAbilityExecuteObject();

        // scroll item script
        abilityItemObject.AddComponent<CharacterMainAbilityItem>();

        return abilityItemObject;
    }

    void CreateAbilityPortraitObject()
    {
        GameObject portraitObject = new GameObject("CharacterPortrait");
        UIStaticFunc.SetParentAndLayer(portraitObject, abilityItemObject.transform, Constants.LAYER_UI);

        UIStaticFunc.SetRectTransformByNormal(portraitObject,
            new Vector2(0f, 0.5f), new Vector2(0f, 0.5f), new Vector2(0.5f, 0.5f),
            new Vector2(100f, 100f), new Vector2(80f, 0f));

        GameObject imageObject = new GameObject("Image");
        UIStaticFunc.SetParentAndLayer(imageObject, portraitObject.transform, Constants.LAYER_UI);

        UIStaticFunc.SetRectTransformByPreset(imageObject,
            Vector2.zero, Vector2.one, new Vector2(0.5f, 0.5f));

        imageObject.AddComponent<CanvasRenderer>();

        Image image = imageObject.AddComponent<Image>();
        image.color = Color.white;
    }

    void CreateAbilityContentsObject()
    {
        GameObject contentsObject = new GameObject("AbilityContents");
        UIStaticFunc.SetParentAndLayer(contentsObject, abilityItemObject.transform, Constants.LAYER_UI);

        UIStaticFunc.SetRectTransformByNormal(contentsObject,
            new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f),
            new Vector2(300f, 100f), new Vector2(-30f, 0f));

        // ability contents object - title text object
        GameObject titileTextObject = new GameObject("Text");
        UIStaticFunc.SetParentAndLayer(titileTextObject, contentsObject.transform, Constants.LAYER_UI);

        UIStaticFunc.SetRectTransformByPreset(titileTextObject,
            new Vector2(0f, 1f), Vector2.one, Vector2.one,
            new Vector2(0f, 0f), new Vector2(0f, 40f), Vector2.zero);

        titileTextObject.AddComponent<CanvasRenderer>();

        Text textForTitleObject = titileTextObject.AddComponent<Text>();
        textForTitleObject.font = UIStaticFunc.legacyRuntimeFont;
        textForTitleObject.fontSize = 30;
        textForTitleObject.supportRichText = true;
        textForTitleObject.alignment = TextAnchor.UpperLeft;
        textForTitleObject.color = new Color(179 / 255f, 155 / 255f, 81 / 255f, 1f);

        // ability contents object - upper text object
        GameObject upperTextObject = new GameObject("Text");
        UIStaticFunc.SetParentAndLayer(upperTextObject, contentsObject.transform, Constants.LAYER_UI);

        UIStaticFunc.SetRectTransformByPreset(upperTextObject,
            new Vector2(0f, 1f), Vector2.one, Vector2.one,
            new Vector2(0f, 0f), new Vector2(0f, 30f), new Vector2(0f, -40f));

        upperTextObject.AddComponent<CanvasRenderer>();

        Text textForUpperObject = upperTextObject.AddComponent<Text>();
        textForUpperObject.font = UIStaticFunc.legacyRuntimeFont;
        textForUpperObject.fontSize = 22;
        textForUpperObject.supportRichText = true;
        textForUpperObject.alignment = TextAnchor.MiddleLeft;
        textForUpperObject.color = Color.white;

        // ability contents object - lower text object
        GameObject lowerTextObject = new GameObject("Text");
        UIStaticFunc.SetParentAndLayer(lowerTextObject, contentsObject.transform, Constants.LAYER_UI);

        UIStaticFunc.SetRectTransformByPreset(lowerTextObject,
            new Vector2(0f, 1f), Vector2.one, Vector2.one,
            new Vector2(0f, 0f), new Vector2(0f, 30f), new Vector2(0f, -70f));

        lowerTextObject.AddComponent<CanvasRenderer>();

        Text textForLowerObject = lowerTextObject.AddComponent<Text>();
        textForLowerObject.font = UIStaticFunc.legacyRuntimeFont;
        textForLowerObject.fontSize = 22;
        textForLowerObject.supportRichText = true;
        textForLowerObject.alignment = TextAnchor.MiddleLeft;
        textForLowerObject.color = Color.white;
    }

    void CreateAbilityExecuteObject()
    {
        // ability excute object
        GameObject excuteObject = new GameObject("AbilityExcute");
        UIStaticFunc.SetParentAndLayer(excuteObject, abilityItemObject.transform, Constants.LAYER_UI);

        UIStaticFunc.SetRectTransformByNormal(excuteObject,
            new Vector2(1f, 0.5f), new Vector2(1f, 0.5f), new Vector2(0.5f, 0.5f),
            new Vector2(180f, 100f), new Vector2(-110f, 0f));

        // button object
        GameObject buttonObject = new GameObject("Ability_Button");
        UIStaticFunc.SetParentAndLayer(buttonObject, excuteObject.transform, Constants.LAYER_UI);

        UIStaticFunc.SetRectTransformByNormal(buttonObject,
            new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f),
            new Vector2(160f, 80f), Vector2.zero);

        buttonObject.AddComponent<CanvasRenderer>();

        Image image = buttonObject.AddComponent<Image>();
        image.sprite = SpriteSheetManager.GetSpriteByName("buttonGreen_simple");
        image.color = Color.white;

        Button button = buttonObject.AddComponent<Button>();
        button.targetGraphic = image;
        button.transition = Selectable.Transition.ColorTint;
        button.colors = ColorForButtons();

        // button object - text object
        GameObject textObject = new GameObject("Text");
        UIStaticFunc.SetParentAndLayer(textObject, buttonObject.transform, Constants.LAYER_UI);

        UIStaticFunc.SetRectTransformByPreset(textObject,
            Vector2.zero, Vector2.one, new Vector2(0.5f, 0.5f),
            new Vector2(5f, 5f), new Vector2(-5f, -5f));

        textObject.AddComponent<CanvasRenderer>();

        Text text = textObject.AddComponent<Text>();
        text.font = UIStaticFunc.legacyRuntimeFont;
        text.fontSize = 30;
        text.supportRichText = true;
        text.alignment = TextAnchor.MiddleCenter;
        text.color = Color.white;
    }

    ColorBlock ColorForButtons()
    {
        ColorBlock colorBlock = new ColorBlock();
        colorBlock.pressedColor = new Color(200 / 255f, 200 / 255f, 200 / 255f, 1f);
        colorBlock.normalColor = colorBlock.highlightedColor =
            colorBlock.selectedColor = colorBlock.disabledColor = Color.white;
        colorBlock.colorMultiplier = 1;
        colorBlock.fadeDuration = 0.1f;

        return colorBlock;
    }
}
