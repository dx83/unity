using UnityEngine;
using UnityEngine.UI;

public class CreateQuitWindow
{
    GameObject panel;
    GameObject window;

    QuitDataStrings uiText;
    QuitDataVariables variables;

    public CreateQuitWindow(QuitDataStrings uiText, QuitDataVariables variables)
    {
        this.uiText = uiText;
        this.variables = variables;
    }

    public delegate void QuitWindowFunc();
    public QuitWindowFunc[] funcArray;


    public GameObject Create(Transform parent)
    {
        CreateShadowPanel(parent);
        CreateQuitWindowObject(panel.transform);

        return panel;
    }

    // shadow background and click restrict
    void CreateShadowPanel(Transform parent)
    {
        panel = new GameObject("Panel");
        UIStaticFunc.SetParentAndLayer(panel, parent, Constants.LAYER_UI);

        UIStaticFunc.SetRectTransformByPreset(panel,
            Vector2.zero, Vector2.one, new Vector2(0.5f, 0.5f),
            new Vector2(-10f, -10f), new Vector2(10f, 10f), Vector2.zero);

        panel.AddComponent<CanvasRenderer>();

        Image image = panel.AddComponent<Image>();
        image.sprite = BuiltInSpritesLoad.UI_SKIN_BACKGROUND;
        image.color = new Color(0f, 0f, 0f, 200 / 255f);
        image.type = Image.Type.Sliced;
    }

    // quit window
    void CreateQuitWindowObject(Transform parent)
    {
        // window object
        window = new GameObject("Window");
        UIStaticFunc.SetParentAndLayer(window, parent, Constants.LAYER_UI);

        UIStaticFunc.SetRectTransformByPreset(window,
            new Vector2(0.2f, 0.3f), new Vector2(0.8f, 0.7f), new Vector2(0.5f, 0.5f));

        window.AddComponent<CanvasRenderer>();

        Image image = window.AddComponent<Image>();
        image.sprite = BuiltInSpritesLoad.UI_SKIN_BACKGROUND;
        image.color = new Color(42 / 255f, 52 / 255f, 71 / 255f, 1f);
        image.type = Image.Type.Sliced;
        image.fillCenter = true;

        CreateWindowDesign();
        CreateWindowContents();
    }

    void CreateWindowDesign()
    {
        // frame
        GameObject frameObject = new GameObject("Frame");
        UIStaticFunc.SetParentAndLayer(frameObject, window.transform, Constants.LAYER_UI);

        UIStaticFunc.SetRectTransformByPreset(frameObject,
            Vector2.zero, Vector2.one, new Vector2(0.5f, 0.5f),
            new Vector2(-1f, -1f), new Vector2(1f, 1f));

        frameObject.AddComponent<CanvasRenderer>();

        Image imageForFrame = frameObject.AddComponent<Image>();
        imageForFrame.sprite = SpriteSheetManager.GetSpriteByName("WindowStroke");
        imageForFrame.color = Color.white;
        imageForFrame.type = Image.Type.Sliced;

        // corner
        GameObject cornerObject = new GameObject("Corner");
        UIStaticFunc.SetParentAndLayer(cornerObject, window.transform, Constants.LAYER_UI);

        UIStaticFunc.SetRectTransformByPreset(cornerObject,
            Vector2.zero, Vector2.one, new Vector2(0.5f, 0.5f),
            new Vector2(-10f, -10f), new Vector2(10f, 10f));

        cornerObject.AddComponent<CanvasRenderer>();

        Image imageForCorner = cornerObject.AddComponent<Image>();
        imageForCorner.sprite = SpriteSheetManager.GetSpriteByName("CornersGold2");
        imageForCorner.color = Color.white;
        imageForCorner.type = Image.Type.Sliced;

        // title
        GameObject titleObject = new GameObject("Title");
        UIStaticFunc.SetParentAndLayer(titleObject, window.transform, Constants.LAYER_UI);

        UIStaticFunc.SetRectTransformByNormal(titleObject,
            new Vector2(0.5f, 1f), new Vector2(0.5f, 1f), new Vector2(0.5f, 0.5f),
            new Vector2(60f, 60f), new Vector2(0f, 0f), new Vector3(4f, 1f, 1f));

        titleObject.AddComponent<CanvasRenderer>();

        Image imageForTitle = titleObject.AddComponent<Image>();
        imageForTitle.sprite = SpriteSheetManager.GetSpriteByName("TitleBarGold");
        imageForTitle.color = Color.white;
        imageForTitle.type = Image.Type.Sliced;

        // text
        GameObject textObject = new GameObject("Text");
        UIStaticFunc.SetParentAndLayer(textObject, titleObject.transform, Constants.LAYER_UI);

        UIStaticFunc.SetRectTransformByNormal(textObject,
            new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f),
            new Vector2(160f, 50f), new Vector2(0f, 0f), new Vector3(0.25f, 1f, 1f));

        textObject.AddComponent<CanvasRenderer>();

        Text text = textObject.AddComponent<Text>();
        text.font = UIStaticFunc.legacyRuntimeFont;
        text.fontSize = 30;
        text.fontStyle = FontStyle.Bold;
        text.supportRichText = true;
        text.alignment = TextAnchor.MiddleCenter;
        text.color = new Color(50 / 255f, 50 / 255f, 50 / 255f, 1f);
        text.text = uiText.title;
    }

    void CreateWindowContents()
    {
        // contents object
        GameObject contentsObject = new GameObject("Contents");
        UIStaticFunc.SetParentAndLayer(contentsObject, window.transform, Constants.LAYER_UI);

        UIStaticFunc.SetRectTransformByPreset(contentsObject,
            Vector2.zero, Vector2.one, new Vector2(0.5f, 0.5f),
            new Vector2(20f, 60f), new Vector2(-20f, -60f));

        CreateTellQuitObject(contentsObject.transform);
        CreateButtonsObject(contentsObject.transform);
        CreateLanguageBtnObject(contentsObject.transform);
    }

    void CreateTellQuitObject(Transform parent)
    {
        GameObject quitTextObject = new GameObject("Text_Quit");
        UIStaticFunc.SetParentAndLayer(quitTextObject, parent, Constants.LAYER_UI);

        UIStaticFunc.SetRectTransformByPreset(quitTextObject,
            new Vector2(0f, 0.5f), new Vector2(1f, 0.5f), new Vector2(0.5f, 0.5f),
            new Vector2(20f, 0f), new Vector2(-20f, 120f), new Vector2(0f, 20f));

        quitTextObject.AddComponent<CanvasRenderer>();

        Text text = quitTextObject.AddComponent<Text>();
        text.font = UIStaticFunc.legacyRuntimeFont;
        text.fontSize = 30;
        text.alignment = TextAnchor.MiddleCenter;
        text.color = Color.white;
        text.text = uiText.tellQuit;
    }

    void CreateButtonsObject(Transform parent)
    {
        GameObject buttonsObject = new GameObject("Buttons");
        UIStaticFunc.SetParentAndLayer(buttonsObject, parent, Constants.LAYER_UI);

        UIStaticFunc.SetRectTransformByPreset(buttonsObject,
            Vector2.zero, new Vector2(1f, 0f), new Vector2(0.5f, 0.5f),
            new Vector2(0f, 0f), new Vector2(0f, 80f), new Vector2(0f, 40f));

        // no button object
        GameObject noButtonObject = new GameObject("Button_No");
        UIStaticFunc.SetParentAndLayer(noButtonObject, buttonsObject.transform, Constants.LAYER_UI);

        UIStaticFunc.SetRectTransformByNormal(noButtonObject,
            new Vector2(0f, 0.5f), new Vector2(0f, 0.5f), new Vector2(0.5f, 0.5f),
            new Vector2(160f, 80f), new Vector2(110f, 0f));

        noButtonObject.AddComponent<CanvasRenderer>();

        Image imageForNoButton = noButtonObject.AddComponent<Image>();
        imageForNoButton.sprite = SpriteSheetManager.GetSpriteByName("buttonGreen_simple");
        imageForNoButton.color = Color.white;

        Button buttonNo = noButtonObject.AddComponent<Button>();
        buttonNo.targetGraphic = imageForNoButton;
        buttonNo.transition = Selectable.Transition.ColorTint;
        buttonNo.colors = ColorForButtons();
        buttonNo.onClick.AddListener(funcArray[0].Invoke);

        // no button text object
        GameObject noBtnTextObject = new GameObject("Text");
        UIStaticFunc.SetParentAndLayer(noBtnTextObject, noButtonObject.transform, Constants.LAYER_UI);

        UIStaticFunc.SetRectTransformByNormal(noBtnTextObject,
            new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f),
            new Vector2(150f, 70f), new Vector2(0f, 0f));

        noBtnTextObject.AddComponent<CanvasRenderer>();

        Text btnNoText = noBtnTextObject.AddComponent<Text>();
        btnNoText.font = UIStaticFunc.legacyRuntimeFont;
        btnNoText.fontSize = 30;
        btnNoText.fontStyle = FontStyle.Bold;
        btnNoText.supportRichText = true;
        btnNoText.alignment = TextAnchor.MiddleCenter;
        btnNoText.color = Color.white;
        btnNoText.text = uiText.btnNo;

        // quit button object
        GameObject quitButtonObject = new GameObject("Button_Quit");
        UIStaticFunc.SetParentAndLayer(quitButtonObject, buttonsObject.transform, Constants.LAYER_UI);

        UIStaticFunc.SetRectTransformByNormal(quitButtonObject,
            new Vector2(0f, 0.5f), new Vector2(0f, 0.5f), new Vector2(0.5f, 0.5f),
            new Vector2(160f, 80f), new Vector2(290f, 0f));

        quitButtonObject.AddComponent<CanvasRenderer>();

        Image imageForQuitButton = quitButtonObject.AddComponent<Image>();
        imageForQuitButton.sprite = SpriteSheetManager.GetSpriteByName("buttonGreen_simple");
        imageForQuitButton.color = new Color(130 / 255f, 50 / 255f, 50 / 255f, 1f);

        Button buttonQuit = quitButtonObject.AddComponent<Button>();
        buttonQuit.targetGraphic = imageForQuitButton;
        buttonQuit.transition = Selectable.Transition.ColorTint;
        buttonQuit.colors = ColorForButtons();
        buttonQuit.onClick.AddListener(funcArray[1].Invoke);

        // no button text object
        GameObject quitBtnTextObject = new GameObject("Text");
        UIStaticFunc.SetParentAndLayer(quitBtnTextObject, quitButtonObject.transform, Constants.LAYER_UI);

        UIStaticFunc.SetRectTransformByNormal(quitBtnTextObject,
            new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f),
            new Vector2(150f, 70f), new Vector2(0f, 0f));

        quitBtnTextObject.AddComponent<CanvasRenderer>();

        Text btnQuitText = quitBtnTextObject.AddComponent<Text>();
        btnQuitText.font = UIStaticFunc.legacyRuntimeFont;
        btnQuitText.fontSize = 30;
        btnQuitText.fontStyle = FontStyle.Bold;
        btnQuitText.supportRichText = true;
        btnQuitText.alignment = TextAnchor.MiddleCenter;
        btnQuitText.color = new Color(153 / 255f, 153 / 255f, 153 / 255f, 1f);
        btnQuitText.text = uiText.btnQuit;
    }

    void CreateLanguageBtnObject(Transform parent)
    {
        GameObject buttonsObject = new GameObject("ChangeLanguage");
        UIStaticFunc.SetParentAndLayer(buttonsObject, parent, Constants.LAYER_UI);

        UIStaticFunc.SetRectTransformByPreset(buttonsObject,
            new Vector2(0f, 1f), Vector2.one, new Vector2(0f, 1f),
            new Vector2(0f, 0f), new Vector2(-150f, 0f), Vector2.zero);
        buttonsObject.GetComponent<RectTransform>().offsetMin = new Vector2(20f, -50f);

        // back image object (change language button event)
        GameObject backImageObject = new GameObject("Button_Back");
        UIStaticFunc.SetParentAndLayer(backImageObject, buttonsObject.transform, Constants.LAYER_UI);

        UIStaticFunc.SetRectTransformByPreset(backImageObject,
            Vector2.zero, Vector2.one, new Vector2(0.5f, 0.5f));

        backImageObject.AddComponent<CanvasRenderer>();

        Image imageForBack = backImageObject.AddComponent<Image>();
        imageForBack.sprite = SpriteSheetManager.GetSpriteByName("Frame_Square_Yellow_60px");
        imageForBack.color = new Color(71 / 255f, 71 / 255f, 71 / 255f, 1f);
        imageForBack.type = Image.Type.Sliced;

        Button languageButton = backImageObject.AddComponent<Button>();
        languageButton.targetGraphic = imageForBack;
        languageButton.transition = Selectable.Transition.ColorTint;
        languageButton.colors = ColorForButtons();
        languageButton.onClick.AddListener(funcArray[2].Invoke);

        // kor text in back image
        GameObject korTextObject = new GameObject("Text");
        UIStaticFunc.SetParentAndLayer(korTextObject, backImageObject.transform, Constants.LAYER_UI);

        UIStaticFunc.SetRectTransformByPreset(korTextObject,
            Vector2.zero, new Vector2(0.5f, 1f), new Vector2(0.5f, 0.5f));

        korTextObject.AddComponent<CanvasRenderer>();

        Text backKor = korTextObject.AddComponent<Text>();
        backKor.font = UIStaticFunc.legacyRuntimeFont;
        backKor.fontSize = 30;
        backKor.fontStyle = FontStyle.Bold;
        backKor.supportRichText = true;
        backKor.alignment = TextAnchor.MiddleCenter;
        backKor.color = new Color(30 / 255f, 30 / 255f, 30 / 255f, 1f);
        backKor.text = uiText.backKor;

        // eng text in back image
        GameObject engTextObject = new GameObject("Text");
        UIStaticFunc.SetParentAndLayer(engTextObject, backImageObject.transform, Constants.LAYER_UI);

        UIStaticFunc.SetRectTransformByPreset(engTextObject,
            new Vector2(0.5f, 0f), Vector2.one, new Vector2(0.5f, 0.5f));

        engTextObject.AddComponent<CanvasRenderer>();

        Text backEng = engTextObject.AddComponent<Text>();
        backEng.font = UIStaticFunc.legacyRuntimeFont;
        backEng.fontSize = 30;
        backEng.fontStyle = FontStyle.Bold;
        backEng.supportRichText = true;
        backEng.alignment = TextAnchor.MiddleCenter;
        backEng.color = new Color(30 / 255f, 30 / 255f, 30 / 255f, 1f);
        backEng.text = uiText.backEng;

        // selected image object
        GameObject selectedImageObject = new GameObject("Selected");
        UIStaticFunc.SetParentAndLayer(selectedImageObject, buttonsObject.transform, Constants.LAYER_UI);

        variables.selectedRect = UIStaticFunc.SetRectTransformByPreset(selectedImageObject,
            Vector2.zero, new Vector2(0.5f, 1f), new Vector2(0.5f, 0.5f));

        selectedImageObject.AddComponent<CanvasRenderer>();

        variables.selectedImage = selectedImageObject.AddComponent<Image>();
        variables.selectedImage.sprite = SpriteSheetManager.GetSpriteByName("Frame_Square_Yellow_60px");
        variables.selectedImage.color = new Color(0f, 128 / 255f, 1f, 1f);
        variables.selectedImage.type = Image.Type.Sliced;
        variables.selectedImage.raycastTarget = false;

        GameObject seletedLangTextObject = new GameObject("Text");
        UIStaticFunc.SetParentAndLayer(seletedLangTextObject, selectedImageObject.transform, Constants.LAYER_UI);

        UIStaticFunc.SetRectTransformByPreset(seletedLangTextObject,
            Vector2.zero, Vector2.one, new Vector2(0.5f, 0.5f));

        variables.selectedLang = seletedLangTextObject.AddComponent<Text>();
        variables.selectedLang.font = UIStaticFunc.legacyRuntimeFont;
        variables.selectedLang.fontSize = 30;
        variables.selectedLang.fontStyle = FontStyle.Bold;
        variables.selectedLang.supportRichText = true;
        variables.selectedLang.alignment = TextAnchor.MiddleCenter;
        variables.selectedLang.color = Color.white;
        variables.selectedLang.raycastTarget = false;
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
