using UnityEngine;
using UnityEngine.UI;


public class HeaderObjectInCharTab
{
    GameObject headerObject;

    public void Create(Transform parent)
    {
        // header block
        headerObject = new GameObject("Header");
        UIStaticFunc.SetParentAndLayer(headerObject, parent, Constants.LAYER_UI);

        UIStaticFunc.SetRectTransformByNormal(headerObject,
            new Vector2(0f, 0.9f), Vector2.one, new Vector2(0.5f, 0.5f));

        // image object
        GameObject imageBackground = new GameObject("Image");
        UIStaticFunc.SetParentAndLayer(imageBackground, headerObject.transform, Constants.LAYER_UI);

        UIStaticFunc.SetRectTransformByPreset(imageBackground,
            new Vector2(0f, 0.9f), Vector2.one, new Vector2(0.5f, 0.5f),
            new Vector2(-500, -55), new Vector2(500, 5));

        imageBackground.AddComponent<CanvasRenderer>();

        Image image = imageBackground.AddComponent<Image>();
        image.sprite = SpriteSheetManager.GetSpriteByName("IRONY_TITLE_Large");

        CreateTestButton(new Vector2(0f, 0.5f), new Vector2(0f, 0.5f), new Vector2(0f, 0.5f), new Vector2(140, 30), new Vector2(20f, 0f));
        CreateTestButton(new Vector2(1f, 0.5f), new Vector2(1f, 0.5f), new Vector2(1f, 0.5f), new Vector2(120, 30), new Vector2(-300f, 0f));
        CreateTestButton(new Vector2(1f, 0.5f), new Vector2(1f, 0.5f), new Vector2(1f, 0.5f), new Vector2(120, 30), new Vector2(-160f, 0f));
        CreateTestButton(new Vector2(1f, 0.5f), new Vector2(1f, 0.5f), new Vector2(1f, 0.5f), new Vector2(120, 30), new Vector2(-20f, 0f));

    }

    void CreateTestButton(Vector2 anchorMin, Vector2 anchorMax, Vector2 pivot, Vector2 sizeDelta, Vector2 position)
    {
        Canvas.ForceUpdateCanvases();

        GameObject button = new GameObject("Button");
        UIStaticFunc.SetParentAndLayer(button, headerObject.transform, Constants.LAYER_UI);

        UIStaticFunc.SetRectTransformByNormal(button,
            anchorMin, anchorMax, pivot, sizeDelta, position);

        button.AddComponent<CanvasRenderer>();

        Image btnImage = button.AddComponent<Image>();
        btnImage.sprite = SpriteSheetManager.GetSpriteByName("header_button");
        btnImage.color = new Color(224 / 255f, 181 / 255f, 0f, 1f);

        Button btnObj = button.AddComponent<Button>();
        btnObj.transition = Selectable.Transition.ColorTint;
        btnObj.targetGraphic = btnImage;

        // text
        GameObject textObject = new GameObject("Text");
        UIStaticFunc.SetParentAndLayer(textObject, button.transform, Constants.LAYER_UI);

        UIStaticFunc.SetRectTransformByPreset(textObject,
            Vector2.zero, Vector2.one, new Vector2(0.5f, 0.5f));

        textObject.AddComponent<CanvasRenderer>();

        Text textForTextObject = textObject.AddComponent<Text>();
        textForTextObject.font = UIStaticFunc.sebangGothicBoldFont;
        textForTextObject.fontSize = 14;
        textForTextObject.supportRichText = true;
        textForTextObject.alignment = TextAnchor.MiddleCenter;
        textForTextObject.color = new Color(80 / 255f, 80 / 255f, 80 / 255f, 1f);
        textForTextObject.text = "- - -";
        textForTextObject.resizeTextForBestFit = true;
        textForTextObject.resizeTextMinSize = 10;
        textForTextObject.resizeTextMaxSize = 40;
    }
}
