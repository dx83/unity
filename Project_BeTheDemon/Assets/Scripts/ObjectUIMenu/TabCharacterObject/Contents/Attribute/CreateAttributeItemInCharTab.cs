using UnityEngine;
using UnityEngine.UI;


public class CreateAttributeItemInCharTab
{
    GameObject attributeItemObject;

    public GameObject Create(Transform scrollContents)
    {
        // item object
        attributeItemObject = new GameObject("AttributeItem");
        UIStaticFunc.SetParentAndLayer(attributeItemObject, scrollContents, Constants.LAYER_UI);

        UIStaticFunc.SetRectTransformByNormal(attributeItemObject,
            new Vector2(0f, 1f), new Vector2(0f, 1f), new Vector2(0f, 1f),
            new Vector2(680f, 130f), Vector2.zero); // Vector2.zero => scrollcontroller 에서 제어

        attributeItemObject.AddComponent<CanvasRenderer>();

        Image image = attributeItemObject.AddComponent<Image>();
        image.sprite = SpriteSheetManager.GetSpriteByName("LoginPassword_Box");
        image.color = new Color(66 / 255f, 96 / 255f, 144 / 255f, 1f);

        CreateAttributeTitleObject();
        CreateAttributeContentsObject();
        CreateAttributeLevelUpObject();

        // scroll item script
        attributeItemObject.AddComponent<CharacterMainAttributeItem>();

        return attributeItemObject;
    }

    void CreateAttributeTitleObject()
    {
        GameObject titleObject = new GameObject("AttributeTitle");
        UIStaticFunc.SetParentAndLayer(titleObject, attributeItemObject.transform, Constants.LAYER_UI);

        UIStaticFunc.SetRectTransformByNormal(titleObject,
            new Vector2(0f, 0.5f), new Vector2(0f, 0.5f), new Vector2(0.5f, 0.5f),
            new Vector2(100f, 100f), new Vector2(80f, 0f));

        GameObject textObject = new GameObject("Text");
        UIStaticFunc.SetParentAndLayer(textObject, titleObject.transform, Constants.LAYER_UI);

        UIStaticFunc.SetRectTransformByPreset(textObject,
            Vector2.zero, Vector2.one, new Vector2(0.5f, 0.5f));

        textObject.AddComponent<CanvasRenderer>();

        Text text = textObject.AddComponent<Text>();
        text.font = UIStaticFunc.legacyRuntimeFont;
        text.fontSize = 40;
        text.supportRichText = true;
        text.alignment = TextAnchor.MiddleCenter;
        text.color = new Color(179 / 255f, 155 / 255f, 81 / 255f, 1f);
    }

    void CreateAttributeContentsObject()
    {
        GameObject contentsObject = new GameObject("AttributeContents");
        UIStaticFunc.SetParentAndLayer(contentsObject, attributeItemObject.transform, Constants.LAYER_UI);

        UIStaticFunc.SetRectTransformByNormal(contentsObject,
            new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f),
            new Vector2(300f, 100f), new Vector2(-45f, 0f));

        // attribute contents object - upper text object
        GameObject upperTextObject = new GameObject("Text");
        UIStaticFunc.SetParentAndLayer(upperTextObject, contentsObject.transform, Constants.LAYER_UI);

        UIStaticFunc.SetRectTransformByPreset(upperTextObject,
            new Vector2(0f, 0.5f), new Vector2(1f, 0.5f), new Vector2(0.5f, 0.5f),
            new Vector2(0f, 0f), new Vector2(0f, 50f), new Vector2(0f, 25f));

        upperTextObject.AddComponent<CanvasRenderer>();

        Text textForUpperObject = upperTextObject.AddComponent<Text>();
        textForUpperObject.font = UIStaticFunc.legacyRuntimeFont;
        textForUpperObject.fontSize = 25;
        textForUpperObject.supportRichText = true;
        textForUpperObject.alignment = TextAnchor.MiddleLeft;
        textForUpperObject.color = Color.white;

        // attribute contents object - lower text object
        GameObject lowerTextObject = new GameObject("Text");
        UIStaticFunc.SetParentAndLayer(lowerTextObject, contentsObject.transform, Constants.LAYER_UI);

        UIStaticFunc.SetRectTransformByPreset(lowerTextObject,
            new Vector2(0f, 0.5f), new Vector2(1f, 0.5f), new Vector2(0.5f, 0.5f),
            new Vector2(0f, 0f), new Vector2(0f, 50f), new Vector2(0f, -25f));

        lowerTextObject.AddComponent<CanvasRenderer>();

        Text textForLowerObject = lowerTextObject.AddComponent<Text>();
        textForLowerObject.font = UIStaticFunc.legacyRuntimeFont;
        textForLowerObject.fontSize = 25;
        textForLowerObject.supportRichText = true;
        textForLowerObject.alignment = TextAnchor.MiddleLeft;
        textForLowerObject.color = Color.white;
    }

    void CreateAttributeLevelUpObject()
    {
        // attribute level object
        GameObject levelObject = new GameObject("LevelUP");
        UIStaticFunc.SetParentAndLayer(levelObject, attributeItemObject.transform, Constants.LAYER_UI);

        UIStaticFunc.SetRectTransformByNormal(levelObject,
            new Vector2(1f, 0.5f), new Vector2(1f, 0.5f), new Vector2(0.5f, 0.5f),
            new Vector2(200f, 100f), new Vector2(-120f, 0f));

        // attribute level object - level text object
        GameObject levelTextObject = new GameObject("Text");
        UIStaticFunc.SetParentAndLayer(levelTextObject, levelObject.transform, Constants.LAYER_UI);

        UIStaticFunc.SetRectTransformByPreset(levelTextObject,
            new Vector2(0f, 0.5f), new Vector2(1f, 0.5f), new Vector2(0.5f, 0.5f),
            new Vector2(0f, 0f), new Vector2(0f, 50f), new Vector2(0f, 25f));

        levelTextObject.AddComponent<CanvasRenderer>();

        Text textForlevelObject = levelTextObject.AddComponent<Text>();
        textForlevelObject.font = UIStaticFunc.legacyRuntimeFont;
        textForlevelObject.fontSize = 30;
        textForlevelObject.supportRichText = true;
        textForlevelObject.alignment = TextAnchor.MiddleCenter;
        textForlevelObject.color = Color.white;

        CreateLevelUpOneButton(levelObject.transform);
        CreateLevelUpTenButton(levelObject.transform);
        CreateLevelUpMaxButton(levelObject.transform);
    }

    void CreateLevelUpOneButton(Transform parent)
    {
        // button object
        GameObject buttonObject = new GameObject("LevelUp_One_Button");
        UIStaticFunc.SetParentAndLayer(buttonObject, parent, Constants.LAYER_UI);

        UIStaticFunc.SetRectTransformByNormal(buttonObject,
            new Vector2(0.5f, 0f), new Vector2(0.5f, 0f), new Vector2(0.5f, 0.5f),
            new Vector2(50f, 50f), new Vector2(-65f, 25f));

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
        text.font = UIStaticFunc.sebangGothicBoldFont;
        text.fontSize = 20;
        text.supportRichText = true;
        text.alignment = TextAnchor.MiddleCenter;
        text.color = Color.white;
    }

    void CreateLevelUpTenButton(Transform parent)
    {
        // button object
        GameObject buttonObject = new GameObject("LevelUp_Ten_Button");
        UIStaticFunc.SetParentAndLayer(buttonObject, parent, Constants.LAYER_UI);

        UIStaticFunc.SetRectTransformByNormal(buttonObject,
            new Vector2(0.5f, 0f), new Vector2(0.5f, 0f), new Vector2(0.5f, 0.5f),
            new Vector2(50f, 50f), new Vector2(0f, 25f));

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
        text.font = UIStaticFunc.sebangGothicBoldFont;
        text.fontSize = 20;
        text.supportRichText = true;
        text.alignment = TextAnchor.MiddleCenter;
        text.color = Color.white;
    }

    void CreateLevelUpMaxButton(Transform parent)
    {
        // button object
        GameObject buttonObject = new GameObject("LevelUp_Ten_Button");
        UIStaticFunc.SetParentAndLayer(buttonObject, parent, Constants.LAYER_UI);

        UIStaticFunc.SetRectTransformByNormal(buttonObject,
            new Vector2(0.5f, 0f), new Vector2(0.5f, 0f), new Vector2(0.5f, 0.5f),
            new Vector2(50f, 50f), new Vector2(65f, 25f));

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
        text.font = UIStaticFunc.sebangGothicBoldFont;
        text.fontSize = 20;
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

