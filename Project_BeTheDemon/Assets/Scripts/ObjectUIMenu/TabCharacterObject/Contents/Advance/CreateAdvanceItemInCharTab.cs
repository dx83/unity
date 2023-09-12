using UnityEngine;
using UnityEngine.UI;


public class CreateAdvanceItemInCharTab
{
    GameObject advanceItemObject;

    public GameObject Create(Transform scrollContents)
    {
        // item object
        advanceItemObject = new GameObject("AdvanceItem");
        UIStaticFunc.SetParentAndLayer(advanceItemObject, scrollContents, Constants.LAYER_UI);

        UIStaticFunc.SetRectTransformByNormal(advanceItemObject,
            new Vector2(0f, 1f), new Vector2(0f, 1f), new Vector2(0f, 1f),
            new Vector2(700f, 120f), Vector2.zero); // Vector2.zero => scrollcontroller 에서 제어

        advanceItemObject.AddComponent<CanvasRenderer>();

        Image image = advanceItemObject.AddComponent<Image>();
        image.sprite = SpriteSheetManager.GetSpriteByName("LoginPassword_Box");
        image.color = new Color(66 / 255f, 96 / 255f, 144 / 255f, 1f);

        CreateAdvanceIconObject();
        CreateAdvanceContentsObject();
        CreateAdvanceExecuteObject();

        // scroll item script
        advanceItemObject.AddComponent<CharacterMainAdvanceItem>();

        return advanceItemObject;
    }

    void CreateAdvanceIconObject()
    {
        GameObject iconObject = new GameObject("AdvanceIcon");
        UIStaticFunc.SetParentAndLayer(iconObject, advanceItemObject.transform, Constants.LAYER_UI);

        UIStaticFunc.SetRectTransformByNormal(iconObject,
            new Vector2(0f, 0.5f), new Vector2(0f, 0.5f), new Vector2(0.5f, 0.5f),
            new Vector2(100f, 100f), new Vector2(80f, 0f));

        GameObject imageObject = new GameObject("Image");
        UIStaticFunc.SetParentAndLayer(imageObject, iconObject.transform, Constants.LAYER_UI);

        UIStaticFunc.SetRectTransformByPreset(imageObject,
            Vector2.zero, Vector2.one, new Vector2(0.5f, 0.5f));

        imageObject.AddComponent<CanvasRenderer>();

        Image image = imageObject.AddComponent<Image>();
        image.sprite = SpriteSheetManager.GetSpriteByName("Icon_stat_max_attack");
        image.color = Color.white;
    }

    void CreateAdvanceContentsObject()
    {
        GameObject contentsObject = new GameObject("AdvanceContents");
        UIStaticFunc.SetParentAndLayer(contentsObject, advanceItemObject.transform, Constants.LAYER_UI);

        UIStaticFunc.SetRectTransformByNormal(contentsObject,
            new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f),
            new Vector2(250f, 100f), new Vector2(-70f, 0f));

        // advance contents object - upper text object
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
        textForUpperObject.color = new Color(179 / 255f, 155 / 255f, 81 / 255f, 1f);

        // advance contents object - lower text object
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

    void CreateAdvanceExecuteObject()
    {
        // advance excute object
        GameObject excuteObject = new GameObject("AdvanceExcute");
        UIStaticFunc.SetParentAndLayer(excuteObject, advanceItemObject.transform, Constants.LAYER_UI);

        UIStaticFunc.SetRectTransformByNormal(excuteObject,
            new Vector2(1f, 0.5f), new Vector2(1f, 0.5f), new Vector2(0.5f, 0.5f),
            new Vector2(250f, 100f), new Vector2(-150f, 0f));

        CreateBeerObject(excuteObject.transform);
        CreateAdvanceUpOneButton(excuteObject.transform);
        CreateAdvanceUpTenButton(excuteObject.transform);
        CreateAdvanceUpHunButton(excuteObject.transform);
    }

    void CreateBeerObject(Transform parent)
    {
        // beer object
        GameObject beerObject = new GameObject("Beer");
        UIStaticFunc.SetParentAndLayer(beerObject, parent, Constants.LAYER_UI);

        UIStaticFunc.SetRectTransformByPreset(beerObject,
            new Vector2(0f, 0.5f), new Vector2(1f, 0.5f), new Vector2(0.5f, 0.5f),
            new Vector2(0f, 0f), new Vector2(0f, 50f), new Vector2(0f, 25f));

        // image
        GameObject imageObject = new GameObject("Image");
        UIStaticFunc.SetParentAndLayer(imageObject, beerObject.transform, Constants.LAYER_UI);

        UIStaticFunc.SetRectTransformByNormal(imageObject,
            new Vector2(0f, 0.5f), new Vector2(0f, 0.5f), new Vector2(0f, 0.5f),
            new Vector2(40f, 40f), new Vector2(5f, 0f));

        imageObject.AddComponent<CanvasRenderer>();

        Image image = imageObject.AddComponent<Image>();
        image.sprite = SpriteSheetManager.GetSpriteByName("Icon_wealth_beer");
        image.color = Color.white;

        // text
        GameObject textObject = new GameObject("Text");
        UIStaticFunc.SetParentAndLayer(textObject, beerObject.transform, Constants.LAYER_UI);

        UIStaticFunc.SetRectTransformByNormal(textObject,
            new Vector2(0f, 0.5f), new Vector2(0f, 0.5f), new Vector2(0f, 0.5f),
            new Vector2(70f, 50f), new Vector2(50f, 0f));

        textObject.AddComponent<CanvasRenderer>();

        Text text = textObject.AddComponent<Text>();
        text.font = UIStaticFunc.legacyRuntimeFont;
        text.fontSize = 20;
        text.supportRichText = true;
        text.alignment = TextAnchor.MiddleCenter;
        text.color = Color.white;
    }

    void CreateAdvanceUpOneButton(Transform parent)
    {
        // button object
        GameObject buttonObject = new GameObject("AdvanceUp_One_Button");
        UIStaticFunc.SetParentAndLayer(buttonObject, parent, Constants.LAYER_UI);

        UIStaticFunc.SetRectTransformByNormal(buttonObject,
            new Vector2(0.5f, 0f), new Vector2(0.5f, 0f), new Vector2(0.5f, 0.5f),
            new Vector2(110f, 50f), new Vector2(-65f, 25f));

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

    void CreateAdvanceUpTenButton(Transform parent)
    {
        // button object
        GameObject buttonObject = new GameObject("AdvanceUp_Ten_Button");
        UIStaticFunc.SetParentAndLayer(buttonObject, parent, Constants.LAYER_UI);

        UIStaticFunc.SetRectTransformByNormal(buttonObject,
            new Vector2(0.5f, 0f), new Vector2(0.5f, 0f), new Vector2(0.5f, 0.5f),
            new Vector2(50f, 50f), new Vector2(30f, 25f));

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
        text.fontSize = 17;
        text.supportRichText = true;
        text.alignment = TextAnchor.MiddleCenter;
        text.color = Color.white;
    }

    void CreateAdvanceUpHunButton(Transform parent)
    {
        // button object
        GameObject buttonObject = new GameObject("AdvanceUp_Hun_Button");
        UIStaticFunc.SetParentAndLayer(buttonObject, parent, Constants.LAYER_UI);

        UIStaticFunc.SetRectTransformByNormal(buttonObject,
            new Vector2(0.5f, 0f), new Vector2(0.5f, 0f), new Vector2(0.5f, 0.5f),
            new Vector2(50f, 50f), new Vector2(95f, 25f));

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
        text.fontSize = 17;
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
