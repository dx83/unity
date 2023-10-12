using UnityEngine;


public class ApplicationQuit
{
    int language;

    static GameObject panel;

    [Inject] GameData gd = new GameData();
    InjectionObj injectionObj = new InjectionObj();

    QuitDataStrings uiText;
    QuitDataVariables variables;

    public static void VisibleWindow() => panel.gameObject.SetActive(true);

    public void Create(Transform parent)
    {
        language = PlayerPrefs.HasKey("Language") ? PlayerPrefs.GetInt("Language") : 0;

        injectionObj.Inject(this);
        uiText = gd.InsertDataInQuitWindow();

        variables = new QuitDataVariables();

        CreateQuitWindow quitWindowObject = new CreateQuitWindow(uiText, variables);
        quitWindowObject.funcArray = new CreateQuitWindow.QuitWindowFunc[3];
        quitWindowObject.funcArray[0] = NoButtonClickEvent;
        quitWindowObject.funcArray[1] = QuitButtonClickEvent;
        quitWindowObject.funcArray[2] = ChangeLanguageButtonEvent;
        panel = quitWindowObject.Create(parent);

        LangaugeButtonController();

        panel.gameObject.SetActive(false);
    }


    void NoButtonClickEvent()
    {
        panel.gameObject.SetActive(false);
    }

    void QuitButtonClickEvent()
    {
        PlayerPrefs.SetInt("Language", language);
        Application.Quit();
    }

    void ChangeLanguageButtonEvent()
    {
        language = (language == 0 ? 1 : 0);
        LangaugeButtonController();
    }

    void LangaugeButtonController()
    {
        if (language == 0)
        {
            variables.selectedRect.anchorMin = Vector2.zero;
            variables.selectedRect.anchorMax = new Vector2(0.5f, 1f);
            variables.selectedImage.color = new Color(0f, 128 / 255f, 1f, 1f);
            variables.selectedLang.text = uiText.backKor;
        }
        else if (language == 1)
        {
            variables.selectedRect.anchorMin = new Vector2(0.5f, 0f);
            variables.selectedRect.anchorMax = Vector2.one;
            variables.selectedImage.color = new Color(142 / 255f, 42 / 255f, 27 / 255f, 1f);
            variables.selectedLang.text = uiText.backEng;
        }
    }
}
