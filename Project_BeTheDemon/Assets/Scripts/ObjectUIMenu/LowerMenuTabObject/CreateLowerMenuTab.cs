using UnityEngine;
using UnityEngine.UI;


public class CreateLowerMenuTab
{
    GameObject lowerMenuBase;
    GameObject lowerContents;

    GameObject[] lowerContentsForTabs;
    GameObject lowerTabs;
    ToggleGroup togglesForlowerTabs;

    public CreateLowerMenuTab(GameObject lowerMenuBase, GameObject lowerContents)
    {
        this.lowerMenuBase = lowerMenuBase;
        this.lowerContents = lowerContents;
    }

    public void Create()
    {
        CreateLowerContentsForTabs();
        CreateLowerTabs();
    }

    void CreateLowerContentsForTabs()
    {
        int max = 7;
        lowerContentsForTabs = new GameObject[max];

        for (int i = 0; i < max; i++)
        {
            switch (i)
            {
                case 0:
                    //GenerateLowerContentsForTab(i, "Tab_Character", new Color(0f, 0f, 0f, 0f));
                    CreateCharacterTab tab = new CreateCharacterTab();
                    tab.Create(lowerContents.transform);
                    break;
                case 1:
                    GenerateLowerContentsForTab(i, "Tab_Items", new Color(42 / 255f, 1f, 0f, 1f));
                    break;
                case 2:
                    GenerateLowerContentsForTab(i, "Tab_Skills", new Color(0f, 1f, 222 / 255f, 1f));
                    break;
                case 3:
                    GenerateLowerContentsForTab(i, "Tab_Special", new Color(123 / 255f, 164 / 255f, 253 / 255f, 1f));
                    break;
                case 4:
                    GenerateLowerContentsForTab(i, "Tab_Shop", new Color(1f, 1f, 0f, 1f));
                    break;
                case 5:
                    GenerateLowerContentsForTab(i, "Tab_Menu", new Color(1f, 95 / 255f, 247 / 255f, 1f));
                    break;
                case 6:
                    GenerateLowerContentsForTab(i, "Tab_Idle", Color.white);
                    break;
            }
        }
    }

    void GenerateLowerContentsForTab(int index, string tabName, Color color)
    {
        lowerContentsForTabs[index] = new GameObject(tabName);
        GameObject obj = lowerContentsForTabs[index];

        UIStaticFunc.SetParentAndLayer(obj, lowerContents.transform, Constants.LAYER_UI);

        UIStaticFunc.SetRectTransformByNormal(obj,
            Vector2.zero, Vector2.one, new Vector2(0.5f, 0.5f));

        obj.AddComponent<Image>().color = color;

        obj.SetActive(false);
    }

    void CreateLowerTabs()
    {
        lowerTabs = new GameObject("LowerTabs");
        UIStaticFunc.SetParentAndLayer(lowerTabs, lowerMenuBase.transform, Constants.LAYER_UI);

        UIStaticFunc.SetRectTransformByNormal(lowerTabs,
            Vector2.zero, new Vector2(1f, 0.15f), new Vector2(0.5f, 0.5f));

        lowerTabs.AddComponent<CanvasRenderer>();
        togglesForlowerTabs = lowerTabs.AddComponent<ToggleGroup>();

        Image image = lowerTabs.AddComponent<Image>();
        image.color = new Color(29 / 255f, 37 / 255f, 51 / 255f, 1f);
        
        Canvas.ForceUpdateCanvases(); // Force all canvases to update their content.
        LowerMenuTabCont lowerMenuTabCont = lowerTabs.AddComponent<LowerMenuTabCont>();
        lowerMenuTabCont.AllowSwitchOff = true;
        lowerMenuTabCont.ContentTabMenu = lowerContents.GetComponent<RectTransform>();
        lowerMenuTabCont.BaseItem = LowerTabButton();
        lowerMenuTabCont.tabAmount = 6;
        lowerMenuTabCont.padding = new RectOffset(0, 0, 0, 0);
        lowerMenuTabCont.spacing = 5;
        lowerMenuTabCont.tabMorphWidth = 1f;
        lowerMenuTabCont.tabMorphHeight = 0.95f;
    }

    // Like Prefab for Tab
    GameObject LowerTabButton()
    {
        GameObject lowerTabObject = new GameObject("Tab");
        UIStaticFunc.SetParentAndLayer(lowerTabObject, lowerTabs.transform, Constants.LAYER_UI);

        UIStaticFunc.SetRectTransformByNormal(lowerTabObject,
            new Vector2(0f, 1f), new Vector2(0f, 1f), new Vector2(0f, 1f),
            new Vector2(100f, 100f), Vector2.zero);

        //Image Object
        GameObject imageObject = new GameObject("Image");
        UIStaticFunc.SetParentAndLayer(imageObject, lowerTabObject.transform, Constants.LAYER_UI);

        UIStaticFunc.SetRectTransformByNormal(imageObject,
            Vector2.zero, Vector2.one, new Vector2(0.5f, 0.5f));

        imageObject.AddComponent<CanvasRenderer>();
        Image imageForImageObject = imageObject.AddComponent<Image>();

        //IconRect Object
        GameObject iconRectObject = new GameObject("IconRect");
        UIStaticFunc.SetParentAndLayer(iconRectObject, lowerTabObject.transform, Constants.LAYER_UI);

        UIStaticFunc.SetRectTransformByPreset(iconRectObject,
            new Vector2(0f, 0.5f), Vector2.one, new Vector2(0.5f, 0.5f),
            new Vector2(10f, 0f), new Vector2(-10f, -10f));

        GameObject iconObject = new GameObject("Icon");
        UIStaticFunc.SetParentAndLayer(iconObject, iconRectObject.transform, Constants.LAYER_UI);

        UIStaticFunc.SetRectTransformByNormal(iconObject,
            new Vector2(0.3f, 0f), new Vector2(0.7f, 1f), new Vector2(0.5f, 0.5f));

        iconObject.AddComponent<CanvasRenderer>();
        Image imageForIconObject = iconObject.AddComponent<Image>();
        imageForIconObject.color = Color.white;

        //Text Object
        GameObject textObject = new GameObject("TextRect");
        UIStaticFunc.SetParentAndLayer(textObject, lowerTabObject.transform, Constants.LAYER_UI);

        UIStaticFunc.SetRectTransformByPreset(textObject,
            Vector2.zero, new Vector2(1f, 0.5f), new Vector2(0.5f, 0.5f),
            new Vector2(10f, 10f), new Vector2(-10f, 0f));

        textObject.AddComponent<CanvasRenderer>();

        Text textForTextObject = textObject.AddComponent<Text>();
        textForTextObject.font = UIStaticFunc.legacyRuntimeFont;
        textForTextObject.fontSize = 25;
        textForTextObject.supportRichText = true;
        textForTextObject.alignment = TextAnchor.MiddleCenter;
        textForTextObject.color = new Color(40 / 255f, 40 / 255f, 40 / 255f, 1f);

        //lowerTabObject component settings
        LowerMenuTabItem lowerMenuTabItem = lowerTabObject.AddComponent<LowerMenuTabItem>();

        lowerMenuTabItem.tabIdle = SpriteSheetManager.GetSpriteByName("buttonSquare_blue");
        lowerMenuTabItem.tabActive = SpriteSheetManager.GetSpriteByName("buttonSquare_brown_pressed");

        lowerMenuTabItem.iconImage = imageForIconObject;
        lowerMenuTabItem.titleName = textForTextObject;

        Toggle toggle = lowerTabObject.AddComponent<Toggle>();
        toggle.interactable = true;
        toggle.transition = Selectable.Transition.ColorTint;
        toggle.targetGraphic = imageForImageObject;

        ColorBlock colorForToggle = new ColorBlock();
        colorForToggle.pressedColor = new Color(180 / 255f, 180 / 255f, 180 / 255f, 1f);
        colorForToggle.normalColor = colorForToggle.highlightedColor =
            colorForToggle.selectedColor = colorForToggle.disabledColor = Color.white;
        colorForToggle.colorMultiplier = 1;
        colorForToggle.fadeDuration = 0.1f;

        toggle.colors = colorForToggle;
        toggle.group = togglesForlowerTabs;

        return lowerTabObject;
    }
}
