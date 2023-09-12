using UnityEngine;
using UnityEngine.UI;


public class MidContentsInCharTab
{
    public CharacterMidVariables UIObject;

    public void Create(Transform parent)
    {
        UIObject = new CharacterMidVariables();
        UIObject.contentForTab = new GameObject[6];

        CreatePortraitBox(parent);
        CreateExperienceBox(parent);
        CreateOtherBox(parent);
    }

    //1
    void CreatePortraitBox(Transform parent)
    {
        // portrait box
        GameObject portraitObject = new GameObject("Box_Portrait");
        UIStaticFunc.SetParentAndLayer(portraitObject, parent, Constants.LAYER_UI);

        UIStaticFunc.SetRectTransformByPreset(portraitObject,
            Vector2.zero, new Vector2(0.2f, 1f), new Vector2(0.5f, 0.5f));

        // frame
        GameObject portraitFrame = new GameObject("Portrait_Frame");
        UIStaticFunc.SetParentAndLayer(portraitFrame, portraitObject.transform, Constants.LAYER_UI);

        UIStaticFunc.SetRectTransformByNormal(portraitFrame,
            new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f),
            new Vector2(75f, 75f), Vector2.zero);

        portraitFrame.AddComponent<CanvasRenderer>();

        Image frameImage = portraitFrame.AddComponent<Image>();
        frameImage.sprite = SpriteSheetManager.GetSpriteByName("panel_vertical");
        frameImage.color = new Color(31 / 255f, 38 / 255f, 52 / 255f, 1f);

        // image
        GameObject characterPortrait = new GameObject("Portrait");
        UIStaticFunc.SetParentAndLayer(characterPortrait, portraitFrame.transform, Constants.LAYER_UI);

        UIStaticFunc.SetRectTransformByNormal(characterPortrait,
            new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f),
            new Vector2(75f, 75f), Vector2.zero);

        characterPortrait.AddComponent<CanvasRenderer>();

        Image portraitImage = characterPortrait.AddComponent<Image>();
        portraitImage.color = Color.white;

        UIObject.characterPortrait = portraitImage;
    }

    //2
    void CreateExperienceBox(Transform parent)
    {
        // experience box
        GameObject experienceObject = new GameObject("Box_Experience");
        UIStaticFunc.SetParentAndLayer(experienceObject, parent, Constants.LAYER_UI);

        UIStaticFunc.SetRectTransformByPreset(experienceObject,
            new Vector2(0.2f, 0f), new Vector2(0.65f, 1f), new Vector2(0.5f, 0.5f));

        // sub object
        CreateExpBoxAttributeAdvance(experienceObject.transform);
        CreateExpBoxAscentAbility(experienceObject.transform);
    }

    void CreateExpBoxAttributeAdvance(Transform parent)
    {
        // mid contents experience box attribute / advance
        GameObject attributeAdvanceObject = new GameObject("Tab_Attribute/Advance");
        UIStaticFunc.SetParentAndLayer(attributeAdvanceObject, parent, Constants.LAYER_UI);

        UIStaticFunc.SetRectTransformByPreset(attributeAdvanceObject,
            Vector2.zero, Vector2.one, new Vector2(0.5f, 0.5f));

        // text object
        GameObject textObject = new GameObject("Text");
        UIStaticFunc.SetParentAndLayer(textObject, attributeAdvanceObject.transform, Constants.LAYER_UI);

        UIStaticFunc.SetRectTransformByPreset(textObject,
            new Vector2(0f, 0.5f), new Vector2(1f, 0.5f), new Vector2(0f, 1f),
            new Vector2(5f, 0f), new Vector2(-5f, 30f));// horizontal stretch

        textObject.AddComponent<CanvasRenderer>();

        Text characterLevel = textObject.AddComponent<Text>();
        characterLevel.font = UIStaticFunc.legacyRuntimeFont;
        characterLevel.fontSize = 25;
        characterLevel.supportRichText = true;
        characterLevel.alignment = TextAnchor.MiddleLeft;
        characterLevel.color = Color.white;
        UIObject.characterLevel = characterLevel;

        // slider Object
        GameObject sliderObject = new GameObject("Slider");
        UIStaticFunc.SetParentAndLayer(sliderObject, attributeAdvanceObject.transform, Constants.LAYER_UI);

        UIStaticFunc.SetRectTransformByPreset(sliderObject,
            new Vector2(0f, 0.5f), new Vector2(1f, 0.5f), new Vector2(0f, 1f),
            new Vector2(0f, 0f), new Vector2(0f, 40f), Vector2.zero);
        
        // slider Object - background object
        GameObject backgroundObject = new GameObject("Background");
        UIStaticFunc.SetParentAndLayer(backgroundObject, sliderObject.transform, Constants.LAYER_UI);

        UIStaticFunc.SetRectTransformByPreset(backgroundObject,
            new Vector2(0f, 0.2f), new Vector2(1f, 0.8f), new Vector2(0.5f, 0.5f));

        backgroundObject.AddComponent<CanvasRenderer>();

        Image backgroundImage = backgroundObject.AddComponent<Image>();
        backgroundImage.sprite = BuiltInSpritesLoad.UI_SKIN_BACKGROUND;
        backgroundImage.color = new Color(27 / 255f, 34 / 255f, 48 / 255f, 1f);
        backgroundImage.type = Image.Type.Sliced;

        // slider Object - fillarea object
        GameObject fillareaObject = new GameObject("Fill_Area");
        UIStaticFunc.SetParentAndLayer(fillareaObject, sliderObject.transform, Constants.LAYER_UI);

        UIStaticFunc.SetRectTransformByPreset(fillareaObject,
            new Vector2(0f, 0.2f), new Vector2(1f, 0.8f), new Vector2(0.5f, 0.5f));

        // slider Object - fillarea object - fill object
        GameObject fillObject = new GameObject("Fill");
        UIStaticFunc.SetParentAndLayer(fillObject, fillareaObject.transform, Constants.LAYER_UI);

        UIStaticFunc.SetRectTransformByPreset(fillObject,
            Vector2.zero, new Vector2(0f, 1f), new Vector2(0.5f, 0.5f));

        fillObject.AddComponent<CanvasRenderer>();

        Image fillImage = fillObject.AddComponent<Image>();
        fillImage.sprite = BuiltInSpritesLoad.UI_SKIN_UISPRITE;
        fillImage.color = new Color(241 / 255f, 149 / 255f, 0f, 1f);
        fillImage.type = Image.Type.Sliced;

        // slider Object - fillarea object - text object
        GameObject textInSlider = new GameObject("Text");
        UIStaticFunc.SetParentAndLayer(textInSlider, fillareaObject.transform, Constants.LAYER_UI);

        UIStaticFunc.SetRectTransformByNormal(textInSlider,
            new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f),
            new Vector2(160f, 30f), Vector2.zero);

        textInSlider.AddComponent<CanvasRenderer>();

        Text levelPercentage = textInSlider.AddComponent<Text>();
        levelPercentage.font = UIStaticFunc.legacyRuntimeFont;
        levelPercentage.fontSize = 25;
        levelPercentage.supportRichText = true;
        levelPercentage.alignment = TextAnchor.MiddleCenter;
        levelPercentage.color = Color.white;
        UIObject.levelPercentage = levelPercentage;

        // slider object - slider component
        Slider slider = sliderObject.AddComponent<Slider>();
        slider.transition = Selectable.Transition.ColorTint;
        slider.targetGraphic = fillImage;
        slider.fillRect = fillObject.GetComponent<RectTransform>();
        slider.direction = Slider.Direction.LeftToRight;
        slider.minValue = 0;
        slider.maxValue = 1;

        ColorBlock colorForToggle = new ColorBlock();
        colorForToggle.normalColor = colorForToggle.highlightedColor = colorForToggle.pressedColor =
            colorForToggle.selectedColor = colorForToggle.disabledColor = Color.white;
        colorForToggle.colorMultiplier = 1;
        colorForToggle.fadeDuration = 0.1f;

        slider.colors = colorForToggle;

        UIObject.sliderForLevel = slider;

        // to array
        UIObject.contentForTab[0] = attributeAdvanceObject;
    }

    void CreateExpBoxAscentAbility(Transform parent)
    {
        // mid contents experience box ascent / ability
        GameObject ascentAbilityObject = new GameObject("Tab_Ascent/Ability");
        UIStaticFunc.SetParentAndLayer(ascentAbilityObject, parent, Constants.LAYER_UI);

        UIStaticFunc.SetRectTransformByPreset(ascentAbilityObject,
            Vector2.zero, Vector2.one, new Vector2(0.5f, 0.5f));

        // ascent text object
        GameObject ascentTextObject = new GameObject("TextAscent");
        UIStaticFunc.SetParentAndLayer(ascentTextObject, ascentAbilityObject.transform, Constants.LAYER_UI);

        UIStaticFunc.SetRectTransformByPreset(ascentTextObject,
            new Vector2(0f, 1f), new Vector2(0.8f, 1f), Vector2.one,
            new Vector2(0f, 0f), new Vector2(0, 35f), Vector2.zero);

        ascentTextObject.AddComponent<CanvasRenderer>();

        Text currentAscent = ascentTextObject.AddComponent<Text>();
        currentAscent.font = UIStaticFunc.legacyRuntimeFont;
        currentAscent.fontSize = 30;
        currentAscent.supportRichText = true;
        currentAscent.alignment = TextAnchor.UpperLeft;
        currentAscent.color = new Color(179 / 255f, 155 / 255f, 81 / 255f, 1f);
        UIObject.currentAscent = currentAscent;

        // str text object
        GameObject strTextObject = new GameObject("TextSTR");
        UIStaticFunc.SetParentAndLayer(strTextObject, ascentAbilityObject.transform, Constants.LAYER_UI);

        UIStaticFunc.SetRectTransformByPreset(strTextObject,
            Vector2.zero, new Vector2(0.8f, 0f), Vector2.one,
            new Vector2(0f, 0f), new Vector2(0f, 30f), new Vector2(0f, 65f));

        strTextObject.AddComponent<CanvasRenderer>();

        Text currentSTR = strTextObject.AddComponent<Text>();
        currentSTR.font = UIStaticFunc.legacyRuntimeFont;
        currentSTR.fontSize = 22;
        currentSTR.supportRichText = true;
        currentSTR.alignment = TextAnchor.MiddleLeft;
        currentSTR.color = Color.white;
        UIObject.currentSTR = currentSTR;

        // speed text object
        GameObject speedTextObject = new GameObject("TextSpeed");
        UIStaticFunc.SetParentAndLayer(speedTextObject, ascentAbilityObject.transform, Constants.LAYER_UI);

        UIStaticFunc.SetRectTransformByPreset(speedTextObject,
            Vector2.zero, new Vector2(0.8f, 0f), Vector2.one,
            new Vector2(0f, 0f), new Vector2(0f, 30f), new Vector2(0f, 35f));

        speedTextObject.AddComponent<CanvasRenderer>();

        Text currentAtkSpd = speedTextObject.AddComponent<Text>();
        currentAtkSpd.font = UIStaticFunc.legacyRuntimeFont;
        currentAtkSpd.fontSize = 22;
        currentAtkSpd.supportRichText = true;
        currentAtkSpd.alignment = TextAnchor.MiddleLeft;
        currentAtkSpd.color = Color.white;
        UIObject.currentAtkSpd = currentAtkSpd;

        // to array
        UIObject.contentForTab[1] = ascentAbilityObject;
    }

    //3
    void CreateOtherBox(Transform parent)
    {
        // other box
        GameObject PromotionObject = new GameObject("Box_Promotion");
        UIStaticFunc.SetParentAndLayer(PromotionObject, parent, Constants.LAYER_UI);

        UIStaticFunc.SetRectTransformByPreset(PromotionObject,
            new Vector2(0.65f, 0f), Vector2.one, new Vector2(0.5f, 0.5f));

        // sub object
        CreateOtherBoxAttributeAdvance(PromotionObject.transform);
        CreateOtherBoxAscentAbility(PromotionObject.transform);
    }

    void CreateOtherBoxAttributeAdvance(Transform parent)
    {
        // mid contents other box attribute / advance
        GameObject attributeAdvanceObject = new GameObject("Tab_Attribute/Advance");
        UIStaticFunc.SetParentAndLayer(attributeAdvanceObject, parent, Constants.LAYER_UI);

        UIStaticFunc.SetRectTransformByPreset(attributeAdvanceObject,
            Vector2.zero, Vector2.one, new Vector2(0.5f, 0.5f));

        CreateOtherBoxAttributeCase(attributeAdvanceObject.transform);
        CreateOtherBoxAdvanceCase(attributeAdvanceObject.transform);
        CreateOtherBoxSharedButton(attributeAdvanceObject.transform);

        // to array
        UIObject.contentForTab[2] = attributeAdvanceObject;
    }

    void CreateOtherBoxAttributeCase(Transform parent)
    {
        // mid contents other box attribute
        GameObject attributeObject = new GameObject("Tab_Attribute");
        UIStaticFunc.SetParentAndLayer(attributeObject, parent, Constants.LAYER_UI);

        UIStaticFunc.SetRectTransformByPreset(attributeObject,
            Vector2.zero, Vector2.one, new Vector2(0.5f, 0.5f));

        // text object - title
        GameObject titleTextObject = new GameObject("Text");
        UIStaticFunc.SetParentAndLayer(titleTextObject, attributeObject.transform, Constants.LAYER_UI);

        UIStaticFunc.SetRectTransformByNormal(titleTextObject,
            new Vector2(0f, 0.5f), new Vector2(0f, 0.5f), new Vector2(0.5f, 0.5f),
            new Vector2(120f, 30f), new Vector2(70f, 10f));

        titleTextObject.AddComponent<CanvasRenderer>();

        Text statsPointTitle = titleTextObject.AddComponent<Text>();
        statsPointTitle.font = UIStaticFunc.legacyRuntimeFont;
        statsPointTitle.fontSize = 20;
        statsPointTitle.supportRichText = true;
        statsPointTitle.alignment = TextAnchor.MiddleRight;
        statsPointTitle.color = Color.white;
        UIObject.statsPtText = statsPointTitle;

        // text object - stat value
        GameObject valueTextObject = new GameObject("Text");
        UIStaticFunc.SetParentAndLayer(valueTextObject, attributeObject.transform, Constants.LAYER_UI);

        UIStaticFunc.SetRectTransformByNormal(valueTextObject,
            new Vector2(0f, 0.5f), new Vector2(0f, 0.5f), new Vector2(0.5f, 0.5f),
            new Vector2(120f, 30f), new Vector2(70f, -15f));

        valueTextObject.AddComponent<CanvasRenderer>();

        Text currentStatsPoints = valueTextObject.AddComponent<Text>();
        currentStatsPoints.font = UIStaticFunc.legacyRuntimeFont;
        currentStatsPoints.fontSize = 20;
        currentStatsPoints.supportRichText = true;
        currentStatsPoints.alignment = TextAnchor.MiddleRight;
        currentStatsPoints.color = Color.white;
        UIObject.currentStats = currentStatsPoints;

        // to array
        UIObject.contentForTab[4] = attributeObject;
    }

    void CreateOtherBoxAdvanceCase(Transform parent)
    {
        // mid contents other box advance
        GameObject advanceObject = new GameObject("Tab_Advance");
        UIStaticFunc.SetParentAndLayer(advanceObject, parent, Constants.LAYER_UI);

        UIStaticFunc.SetRectTransformByPreset(advanceObject,
            Vector2.zero, Vector2.one, new Vector2(0.5f, 0.5f));

        // text object - message
        GameObject messageTextObject = new GameObject("Text_FullMessage");
        UIStaticFunc.SetParentAndLayer(messageTextObject, advanceObject.transform, Constants.LAYER_UI);

        UIStaticFunc.SetRectTransformByNormal(messageTextObject,
            new Vector2(0f, 0.5f), new Vector2(0f, 0.5f), new Vector2(0.5f, 0.5f),
            new Vector2(120f, 30f), new Vector2(70f, 10f));

        messageTextObject.AddComponent<CanvasRenderer>();

        Text beerFullMessage = messageTextObject.AddComponent<Text>();
        beerFullMessage.font = UIStaticFunc.legacyRuntimeFont;
        beerFullMessage.fontSize = 20;
        beerFullMessage.supportRichText = true;
        beerFullMessage.alignment = TextAnchor.MiddleRight;
        beerFullMessage.color = Color.white;
        UIObject.fullMessage = beerFullMessage;

        // background object
        GameObject textBackgroundObject = new GameObject("Text_Background");
        UIStaticFunc.SetParentAndLayer(textBackgroundObject, advanceObject.transform, Constants.LAYER_UI);

        UIStaticFunc.SetRectTransformByNormal(textBackgroundObject,
            new Vector2(0f, 0.5f), new Vector2(0f, 0.5f), new Vector2(0.5f, 0.5f),
            new Vector2(100f, 30f), new Vector2(80f, -20f));

        textBackgroundObject.AddComponent<CanvasRenderer>();

        Image imageFortxtBack = textBackgroundObject.AddComponent<Image>();
        imageFortxtBack.sprite = BuiltInSpritesLoad.UI_SKIN_BACKGROUND;
        imageFortxtBack.color = new Color(23 / 255f, 29 / 255f, 40 / 255f, 1f);
        imageFortxtBack.type = Image.Type.Sliced;

        // background object - beer text object
        GameObject beerTextObject = new GameObject("Text_BeerValue");
        UIStaticFunc.SetParentAndLayer(beerTextObject, textBackgroundObject.transform, Constants.LAYER_UI);

        UIStaticFunc.SetRectTransformByPreset(beerTextObject,
            new Vector2(0.3f, 0f), Vector2.one, new Vector2(0.5f, 0.5f));

        beerTextObject.AddComponent<CanvasRenderer>();

        Text currentBeersValue = beerTextObject.AddComponent<Text>();
        currentBeersValue.font = UIStaticFunc.legacyRuntimeFont;
        currentBeersValue.fontSize = 20;
        currentBeersValue.supportRichText = true;
        currentBeersValue.alignment = TextAnchor.MiddleRight;
        currentBeersValue.color = Color.white;
        UIObject.currentBeers = currentBeersValue;

        // background object - beer image object
        GameObject beerImageObject = new GameObject("Text_BeerImage");
        UIStaticFunc.SetParentAndLayer(beerImageObject, textBackgroundObject.transform, Constants.LAYER_UI);

        UIStaticFunc.SetRectTransformByNormal(beerImageObject,
            Vector2.zero, new Vector2(0.3f, 1f), new Vector2(0.5f, 0.5f));

        beerImageObject.AddComponent<CanvasRenderer>();

        Image imageForBeerImage = beerImageObject.AddComponent<Image>();
        imageForBeerImage.sprite = SpriteSheetManager.GetSpriteByName("Icon_wealth_beer");
        imageForBeerImage.color = Color.white;

        // to array
        UIObject.contentForTab[5] = advanceObject;
    }

    void CreateOtherBoxSharedButton(Transform parent)
    {
        // mid contents other box advance
        GameObject sharedButton = new GameObject("Button_Object");
        UIStaticFunc.SetParentAndLayer(sharedButton, parent, Constants.LAYER_UI);

        UIStaticFunc.SetRectTransformByNormal(sharedButton,
            new Vector2(1f, 0.5f), new Vector2(1f, 0.5f), new Vector2(0.5f, 0.5f),
            new Vector2(65f, 65f), new Vector2(-65f, 0f));

        sharedButton.AddComponent<CanvasRenderer>();

        // button bright
        Image imageForSharedBtn = sharedButton.AddComponent<Image>();
        imageForSharedBtn.color = new Color(214 / 255f, 198 / 255f, 135 / 255f, 1f);

        // button object
        GameObject buttonObject = new GameObject("Button");
        UIStaticFunc.SetParentAndLayer(buttonObject, sharedButton.transform, Constants.LAYER_UI);

        UIStaticFunc.SetRectTransformByNormal(buttonObject,
            new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f),
            new Vector2(65f, 65f), new Vector2(2f, -2f));

        buttonObject.AddComponent<CanvasRenderer>();

        Image imageForBtnObj = buttonObject.AddComponent<Image>();
        imageForBtnObj.sprite = SpriteSheetManager.GetSpriteByName("panel_vertical");
        imageForBtnObj.color = new Color(179 / 255f, 153 / 255f, 79 / 255f, 220 / 255f);

        Button button = buttonObject.AddComponent<Button>();
        button.targetGraphic = imageForBtnObj;
        button.transition = Selectable.Transition.ColorTint;

        ColorBlock colorForToggle = new ColorBlock();
        colorForToggle.pressedColor = new Color(200 / 255f, 200 / 255f, 200 / 255f, 1f);
        colorForToggle.normalColor = colorForToggle.highlightedColor =
            colorForToggle.selectedColor = colorForToggle.disabledColor = Color.white;
        colorForToggle.colorMultiplier = 1;
        colorForToggle.fadeDuration = 0.1f;

        button.colors = colorForToggle;

        // button reset image
        GameObject resetImage = new GameObject("Reset_Image");
        UIStaticFunc.SetParentAndLayer(resetImage, buttonObject.transform, Constants.LAYER_UI);

        UIStaticFunc.SetRectTransformByPreset(resetImage,
            Vector2.zero, Vector2.one, new Vector2(0.5f, 0.5f),
            new Vector2(3f, 7f), new Vector2(-7f, -3f));

        resetImage.AddComponent<CanvasRenderer>();

        Image imageForResetImg = resetImage.AddComponent<Image>();
        imageForResetImg.sprite = SpriteSheetManager.GetSpriteByName("reset_image");
        imageForResetImg.color = new Color(127 / 255f, 105 / 255f, 48 / 255f, 1f);
        imageForResetImg.raycastTarget = false;
        imageForResetImg.maskable = false;

        resetImage.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0f, 0f, -90f);
    }

    void CreateOtherBoxAscentAbility(Transform parent)
    {
        // mid contents other box ascent / ability
        GameObject ascentAbilityObject = new GameObject("Tab_Ascent/Ability");
        UIStaticFunc.SetParentAndLayer(ascentAbilityObject, parent, Constants.LAYER_UI);

        UIStaticFunc.SetRectTransformByPreset(ascentAbilityObject,
            Vector2.zero, Vector2.one, new Vector2(0.5f, 0.5f));

        // ascent button
        GameObject ascentButtonObject = new GameObject("Ascent_Button");
        UIStaticFunc.SetParentAndLayer(ascentButtonObject, ascentAbilityObject.transform, Constants.LAYER_UI);

        UIStaticFunc.SetRectTransformByNormal(ascentButtonObject,
            new Vector2(1f, 0.5f), new Vector2(1f, 0.5f), new Vector2(0.5f, 0.5f),
            new Vector2(160f, 60f), new Vector2(-130f, 0f));

        ascentButtonObject.AddComponent<CanvasRenderer>();

        Image image = ascentButtonObject.AddComponent<Image>();
        image.sprite = SpriteSheetManager.GetSpriteByName("buttonGreen_simple");
        image.color = Color.white;

        Button ascentButton = ascentButtonObject.AddComponent<Button>();
        ascentButton.targetGraphic = image;
        ascentButton.transition = Selectable.Transition.ColorTint;
        UIObject.ascentBtn = ascentButton;

        ColorBlock colorForToggle = new ColorBlock();
        colorForToggle.pressedColor = new Color(200 / 255f, 200 / 255f, 200 / 255f, 1f);
        colorForToggle.normalColor = colorForToggle.highlightedColor =
            colorForToggle.selectedColor = colorForToggle.disabledColor = Color.white;
        colorForToggle.colorMultiplier = 1;
        colorForToggle.fadeDuration = 0.1f;

        ascentButton.colors = colorForToggle;

        GameObject textObject = new GameObject("Text");
        UIStaticFunc.SetParentAndLayer(textObject, ascentButtonObject.transform, Constants.LAYER_UI);

        UIStaticFunc.SetRectTransformByPreset(textObject,
            Vector2.zero, Vector2.one, new Vector2(0.5f, 0.5f));

        textObject.AddComponent<CanvasRenderer>();

        Text ascentText = textObject.AddComponent<Text>();
        ascentText.font = UIStaticFunc.legacyRuntimeFont;
        ascentText.fontSize = 25;
        ascentText.supportRichText = true;
        ascentText.alignment = TextAnchor.MiddleCenter;
        ascentText.color = Color.white;
        UIObject.ascentText = ascentText;

        // to array
        UIObject.contentForTab[3] = ascentAbilityObject;
    }
}
