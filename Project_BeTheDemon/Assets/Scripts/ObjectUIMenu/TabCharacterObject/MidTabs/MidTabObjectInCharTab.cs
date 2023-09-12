using UnityEngine;
using UnityEngine.UI;


public class MidTabObjectInCharTab
{
    GameObject middleObject;
    GameObject midTabButtons;
    GameObject midTabContents;
    ToggleGroup toggleGroupForMidTab;

    public void Create(Transform parent)
    {
        // middle part
        middleObject = new GameObject("Middle");
        UIStaticFunc.SetParentAndLayer(middleObject, parent, Constants.LAYER_UI);

        UIStaticFunc.SetRectTransformByPreset(middleObject,
            new Vector2(0f, 0.6f), new Vector2(1f, 0.9f), new Vector2(0.5f, 0.5f));

        // buttons in middle
        midTabButtons = new GameObject("MidTabs");
        UIStaticFunc.SetParentAndLayer(midTabButtons, middleObject.transform, Constants.LAYER_UI);
        
        UIStaticFunc.SetRectTransformByPreset(midTabButtons,
            new Vector2(0.1f, 0.6f), new Vector2(0.9f, 1f), new Vector2(0.5f, 0.5f));

        toggleGroupForMidTab = midTabButtons.AddComponent<ToggleGroup>();

        // contents in middle
        midTabContents = new GameObject("Contents");
        UIStaticFunc.SetParentAndLayer(midTabContents, middleObject.transform, Constants.LAYER_UI);

        UIStaticFunc.SetRectTransformByPreset(midTabContents,
            Vector2.zero, new Vector2(1f, 0.6f), new Vector2(0.5f, 0.5f));

        // line between middle and main contents
        GameObject LineObject = new GameObject("Line");
        UIStaticFunc.SetParentAndLayer(LineObject, middleObject.transform, Constants.LAYER_UI);

        UIStaticFunc.SetRectTransformByPreset(LineObject,
            Vector2.zero, new Vector2(1f, 0f), new Vector2(0.5f, 0.5f),
            new Vector2(-500f, -20f), new Vector2(500f, 0f), Vector2.zero);

        LineObject.AddComponent<CanvasRenderer>();

        Image image = LineObject.AddComponent<Image>();
        image.sprite = SpriteSheetManager.GetSpriteByName("Division_line");
        image.color = new Color(67 / 255f, 87 / 255f, 123 / 255f, 1f);
    }

    public void CreateMidTabButtons(RectTransform targetObject)
    {
        MidContentsInCharTab midContentsInCharTab = new MidContentsInCharTab();
        midContentsInCharTab.Create(midTabContents.transform);
        
        CharacterMidTabCont characterMidTabCont = midTabButtons.AddComponent<CharacterMidTabCont>();
        characterMidTabCont.AllowSwitchOff = false;
        characterMidTabCont.ContentTabMenu = targetObject;
        characterMidTabCont.BaseItem = MidTabButton();
        characterMidTabCont.tabAmount = 4;
        characterMidTabCont.padding = new RectOffset(0, 0, 0, 0);
        characterMidTabCont.spacing = 10;
        characterMidTabCont.tabMorphWidth = 1f;
        characterMidTabCont.tabMorphHeight = 1f;
        characterMidTabCont.InitializeContentsDatas(midContentsInCharTab.UIObject);
    }

    // like Prefab for Tab(Button)
    GameObject MidTabButton()
    {
        // button object
        GameObject midTabObject = new GameObject("Tab");
        UIStaticFunc.SetParentAndLayer(midTabObject, midTabButtons.transform, Constants.LAYER_UI);

        UIStaticFunc.SetRectTransformByNormal(midTabObject,
            new Vector2(0f, 1f), new Vector2(0f, 1f), new Vector2(0f, 1f),
            new Vector2(120f, 60f), Vector2.zero);

        // nomal button image
        GameObject buttonImage = new GameObject("ButtonImage");
        UIStaticFunc.SetParentAndLayer(buttonImage, midTabObject.transform, Constants.LAYER_UI);

        UIStaticFunc.SetRectTransformByPreset(buttonImage,
            Vector2.zero, Vector2.one, new Vector2(0.5f, 0.5f));

        buttonImage.AddComponent<CanvasRenderer>();

        Image btnImage = buttonImage.AddComponent<Image>();
        btnImage.sprite = SpriteSheetManager.GetSpriteByName("panel_horizontal");
        btnImage.color = new Color(29 / 255f, 37 / 255f, 51 / 255f, 1f);

        // button select image
        GameObject selectImage = new GameObject("SelectImage");
        UIStaticFunc.SetParentAndLayer(selectImage, midTabObject.transform, Constants.LAYER_UI);

        UIStaticFunc.SetRectTransformByPreset(selectImage,
            Vector2.zero, Vector2.one, new Vector2(0.5f, 0.5f));

        selectImage.AddComponent<CanvasRenderer>();

        Image sltImage = selectImage.AddComponent<Image>();
        sltImage.sprite = SpriteSheetManager.GetSpriteByName("panel_horizontal_edge");
        sltImage.color = new Color(230 / 255f, 230 / 255f, 0f, 1f);

        // text object
        GameObject textObject = new GameObject("Text");
        UIStaticFunc.SetParentAndLayer(textObject, midTabObject.transform, Constants.LAYER_UI);

        UIStaticFunc.SetRectTransformByPreset(textObject,
            Vector2.zero, Vector2.one, new Vector2(0.5f, 0.5f),
            new Vector2(15f, 15f), new Vector2(-15f, -15f));

        textObject.AddComponent<CanvasRenderer>();

        Text textForTextObject = textObject.AddComponent<Text>();
        textForTextObject.font = UIStaticFunc.legacyRuntimeFont;
        textForTextObject.fontSize = 25;
        textForTextObject.supportRichText = true;
        textForTextObject.alignment = TextAnchor.MiddleCenter;
        textForTextObject.color = Color.white;
        textForTextObject.resizeTextForBestFit = true;
        textForTextObject.resizeTextMinSize = 10;
        textForTextObject.resizeTextMaxSize = 30;

        // middle TabObject component settings
        CharacterMidTabItem characterMidTabItem = midTabObject.AddComponent<CharacterMidTabItem>();

        characterMidTabItem.tabIdle = SpriteSheetManager.GetSpriteByName("panel_horizontal");
        characterMidTabItem.tabActive = SpriteSheetManager.GetSpriteByName("panel_horizontal");

        Toggle toggle = midTabObject.AddComponent<Toggle>();
        toggle.interactable = true;
        toggle.transition = Selectable.Transition.ColorTint;
        toggle.targetGraphic = btnImage;

        ColorBlock colorForToggle = new ColorBlock();
        colorForToggle.normalColor = colorForToggle.highlightedColor = colorForToggle.pressedColor =
            colorForToggle.selectedColor = colorForToggle.disabledColor = Color.white;
        colorForToggle.colorMultiplier = 1;
        colorForToggle.fadeDuration = 0.1f;

        toggle.colors = colorForToggle;
        toggle.group = toggleGroupForMidTab;

        return midTabObject;
    }
}
