using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CharacterMidTabCont : TabController<CharacterMidTabData>
{
    List<CharacterMidUIText> uiText;

    [Inject] GameData gd = new GameData();
    [Inject] UserData ud = new UserData();
    InjectionObj injectionObj = new InjectionObj();

    CharacterMidVariables contents;

    public void InitializeContentsDatas(CharacterMidVariables contents) => this.contents = contents;

    void LoadData()
    {
        injectionObj.Inject(this);

        // tab ---------------------------------------------
        uiText = gd.InsertDataInCharacterMidTabCont();
        tabItemData = gd.InsertDataInCharacterMidTabCont(tabAmount, contents.contentForTab);
        // tab content -------------------------------------
        // Attribute / Advance
        contents.characterPortrait.transform.parent.gameObject.SetActive(true);
        contents.characterPortrait.useSpriteMesh = true;
        contents.characterPortrait.sprite = SpriteSheetManager.GetSpriteByName(uiText[5 + ud.currentAscent].image);
        contents.characterLevel.text = $"Lv.{ud.characterLevel} {ud.userId}";
        ExpPercentFunc();
        // Attribute
        contents.statsPtText.text = uiText[0].text;
        contents.currentStats.text = $"{ud.statsPoint}";
        // Advance
        BeerFullFunc();
        contents.fullMessage.text = uiText[1].text;
        contents.currentBeers.text = $"{ud.beersPoint}";
        // Ascent / Ability
        ud.AscentCurrentApply();
        contents.currentAscent.text = uiText[5 + ud.currentAscent].text;
        contents.currentSTR.text = $"{uiText[2].text} +{ud.ascStrPer}%";
        contents.currentAtkSpd.text = $"{uiText[3].text} +{ud.ascAtkSpd}%";

        contents.ascentText.text = uiText[4].text;
        contents.ascentBtn.onClick.AddListener(AscentButtonEvent);

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
            contents.ascentText.text = uiText[4].text2;
            contents.ascentText.color = new Color(0.6f, 0.6f, 0.6f);
            contents.ascentBtn.image.color = new Color(0.4f, 0.4f, 0.4f);
            contents.ascentBtn.interactable = false;
        }
    }

    public void ExpPercentFunc()
    {
        contents.levelPercentage.text = $"{ud.expPercent}%";
        contents.sliderForLevel.value = ud.expPercent / 100;
    }

    public void BeerFullFunc()
    {
        if (ud.beersPoint >= 999999)
        {
            contents.fullMessage.gameObject.SetActive(true);
            contents.currentBeers.color = new Color(1.0f, 0.0f, 0.0f);
        }
        else
        {
            contents.fullMessage.gameObject.SetActive(false);
            contents.currentBeers.color = new Color(1.0f, 1.0f, 1.0f);
        }
    }

    private void CharacterMidMenuTextHandler(EventCharacterMidMenuText obj)
    {
        switch (obj.num)
        {
            case 0: contents.characterLevel.text = $"Lv.{ud.characterLevel} {ud.userId}"; break;
            case 1: ExpPercentFunc(); break;
            case 2: contents.currentStats.text = $"{ud.statsPoint}";   break;
            case 3: BeerFullFunc(); contents.currentBeers.text = $"{ud.beersPoint}"; break;
            case 4:
                {
                    contents.characterPortrait.sprite = SpriteSheetManager.GetSpriteByName(uiText[5 + ud.currentAscent].image);
                    contents.currentAscent.text = uiText[5 + ud.currentAscent].text;
                    contents.currentSTR.text = $"{uiText[2].text} +{ud.currentAscent * 10}%";
                    contents.currentAtkSpd.text = $"{uiText[3].text} +{ud.currentAscent * 5}%";
                    break;
                }
        }
    }

    private void DataUpdateAfterAscentPromote(EventCharacterMidMenuAscentUpdate obj)
    {
        contents.characterPortrait.sprite = SpriteSheetManager.GetSpriteByName(uiText[5 + ud.currentAscent].image);
        contents.currentAscent.text = uiText[5 + ud.currentAscent].text;
        contents.currentSTR.text = $"{uiText[2].text} +{ud.ascStrPer}%";
        contents.currentAtkSpd.text = $"{uiText[3].text} +{ud.ascAtkSpd}%";
    }
}
