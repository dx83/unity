using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CharacterMidTabData
{
    public string tabTitleText;
    public GameObject[] tabObject;
}

public class CharacterMidUIText
{
    public int index;
    public string image;
    public string text;
    public string text2;
}

public class CharacterMidTabCont : TabController<CharacterMidTabData>
{
    [SerializeField] private Image characterPortrait;

    [SerializeField] private GameObject[] contentForTab;

    // Attribute / Advance
    [SerializeField] private Text characterLevel;
    [SerializeField] private Text levelPercentage;
    [SerializeField] private Slider sliderForLevel;
    // Attribute
    [SerializeField] private Text statsPtText;
    [SerializeField] private Text currentStats;
    // Advance
    [SerializeField] private Text fullMessage;
    [SerializeField] private Text currentBeers;
    // Ascent / Ability
    [SerializeField] private Text currentAscent;
    [SerializeField] private Text currentSTR;
    [SerializeField] private Text currentAttSpd;
    [SerializeField] private Button ascectBtn;
    [SerializeField] private Text textAscent;


    List<CharacterMidUIText> uiText;

    [Inject] GameData gd = new GameData();
    [Inject] UserData ud = new UserData();
    InjectionObj injectionObj = new InjectionObj();

    void LoadData()
    {
        injectionObj.Inject(this);

        // tab ---------------------------------------------
        uiText = gd.InsertDataInCharacterMidTabCont();
        tabItemData = gd.InsertDataInCharacterMidTabCont(tabAmount, contentForTab);

        // tab content -------------------------------------
        // Attribute / Advance
        characterPortrait.transform.parent.gameObject.SetActive(true);
        characterPortrait.useSpriteMesh = true;
        characterPortrait.sprite = SpriteSheetManager.GetSpriteByName(uiText[5 + ud.currentAscent].image);
        characterLevel.text = $"Lv.{ud.characterLevel} {ud.userId}";
        ExpPercentFunc();
        // Attribute
        statsPtText.text = uiText[0].text;
        currentStats.text = $"{ud.statsPoint}";
        // Advance
        BeerFullFunc();
        fullMessage.text = uiText[1].text;
        currentBeers.text = $"{ud.beersPoint}";
        // Ascent / Ability
        ud.AscentCurrentApply();
        currentAscent.text = uiText[5 + ud.currentAscent].text;
        currentSTR.text = $"{uiText[2].text} +{ud.ascStrPer}%";
        currentAttSpd.text = $"{uiText[3].text} +{ud.ascAtkSpd}%";

        textAscent.text = uiText[4].text;
        ascectBtn.onClick.AddListener(AscentButtonEvent);

        EventBus.Subscribe<EventCharacterMidMenuText>(CharacterMidMenuTextHandler);//EventStatic
        EventBus.Subscribe<EventCharacterMidMenuAscentUpdate>(DataUpdateAfterAscentPromote);
    }

    protected override void Start()
    {
        LoadData();
        base.Start();
    }

    protected override void TabSelectionInit(TabItem<CharacterMidTabData> tab)
    {
        tab.TabSelectionInit();
    }

    public void AscentButtonEvent()
    {
        ud.AscentPromoteEvent();
        EventStatic.UpdateAfterAscent();
        if (ud.currentAscent == Constants.ASCENT_MAXIDX)
        {
            textAscent.text = uiText[4].text2;
            textAscent.color = new Color(0.6f, 0.6f, 0.6f);
            ascectBtn.image.color = new Color(0.4f, 0.4f, 0.4f);
            ascectBtn.interactable = false;
        }
    }

    public void ExpPercentFunc()
    {
        levelPercentage.text = $"{ud.expPercent}%";
        sliderForLevel.value = ud.expPercent / 100;
    }

    public void BeerFullFunc()
    {
        if (ud.beersPoint >= 999999)
        {
            fullMessage.gameObject.SetActive(true);
            currentBeers.color = new Color(1.0f, 0.0f, 0.0f);
        }
        else
        {
            fullMessage.gameObject.SetActive(false);
            currentBeers.color = new Color(1.0f, 1.0f, 1.0f);
        }
    }

    private void CharacterMidMenuTextHandler(EventCharacterMidMenuText obj)
    {
        switch (obj.num)
        {
            case 0: characterLevel.text = $"Lv.{ud.characterLevel} {ud.userId}"; break;
            case 1: ExpPercentFunc(); break;
            case 2: currentStats.text = $"{ud.statsPoint}";   break;
            case 3: BeerFullFunc(); currentBeers.text = $"{ud.beersPoint}"; break;
            case 4:
                {
                    characterPortrait.sprite = SpriteSheetManager.GetSpriteByName(uiText[5 + ud.currentAscent].image);
                    currentAscent.text = uiText[5 + ud.currentAscent].text;
                    currentSTR.text = $"{uiText[2].text} +{ud.currentAscent * 10}%";
                    currentAttSpd.text = $"{uiText[3].text} +{ud.currentAscent * 5}%";
                    break;
                }
        }
    }

    private void DataUpdateAfterAscentPromote(EventCharacterMidMenuAscentUpdate obj)
    {
        characterPortrait.sprite = SpriteSheetManager.GetSpriteByName(uiText[5 + ud.currentAscent].image);
        currentAscent.text = uiText[5 + ud.currentAscent].text;
        currentSTR.text = $"{uiText[2].text} +{ud.ascStrPer}%";
        currentAttSpd.text = $"{uiText[3].text} +{ud.ascAtkSpd}%";
    }
}
