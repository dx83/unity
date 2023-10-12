using UnityEngine;


public class CharacterMainAttributeScroll : ScrollController<ScrollAttributeData>
{
    [Inject] GameData gd = new GameData();
    [Inject] UserData ud = new UserData();
    InjectionObj injectionObj = new InjectionObj();
    
    void LoadData()
    {
        injectionObj.Inject(this);
        scrollItemMax = Constants.STAT_MAX;
        scrollItemData = gd.InsertDataInCharacterMainStat();

        scrollItemData[0].level = ud.strLv;
        scrollItemData[1].level = ud.dexLv;
        scrollItemData[2].level = ud.agiLv;
        scrollItemData[3].level = ud.lukLv;
    }
    
    protected override void Start()
    {
        LoadData();
        base.Start();
        EventStatic.BtnActiveByStatPt(ud.statsPoint);
        EventBus.Subscribe<EventCharacterTabStatLevelUp>(LevelUpEventHandler);
    }

    private void LevelUpEventHandler(EventCharacterTabStatLevelUp obj)
    {
        ScrollAttributeData sd = scrollItemData[obj.index];
        int up = obj.level == 0 ? ud.statsPoint : obj.level;

        ud.StatLevelUpEvent(sd.itemID, up);
        sd.level += up;
    }
}
