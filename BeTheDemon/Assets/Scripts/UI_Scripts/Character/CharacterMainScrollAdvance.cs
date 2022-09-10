using UnityEngine;


public class ScrollAdvanceData
{
    public int itemID;
    public Sprite advSprite;
    public int advLevel;
    public string advTitle;
}

public class CharacterMainScrollAdvance : ScrollController<ScrollAdvanceData>
{
    [Inject] GameData gd = new GameData();
    [Inject] UserData ud = new UserData();
    InjectionObj injectionObj = new InjectionObj();

    void LoadData()
    {
        injectionObj.Inject(this);
        scrollItemMax = Constants.ADV_MAX;
        scrollItemData = gd.InsertDataInCharacterMainAdvance();

        scrollItemData[0].advLevel = ud.maxAtkLv;
        scrollItemData[1].advLevel = ud.atkPerLv;
        scrollItemData[2].advLevel = ud.accuracyLv;
        scrollItemData[3].advLevel = ud.dodgeLv;
        scrollItemData[4].advLevel = ud.atkSpdLv;
        scrollItemData[5].advLevel = ud.movSpdLv;
    }

    protected override void Start()
    {
        LoadData();
        base.Start();
        EventBus.Subscribe<EventCharacterTabAdvanceLvUp>(LevelUpEventHandler);
    }

    private void LevelUpEventHandler(EventCharacterTabAdvanceLvUp obj)
    {
        ScrollAdvanceData ad = scrollItemData[obj.index];

        int up = 0;
        int cost = (int)(ad.advLevel * Constants.BEER_FACTOR) + 5;

        int limit = 0;
        if (ad.itemID == 4)         limit = Constants.MAX_ATKSPD;
        else if (ad.itemID == 5)    limit = Constants.MAX_MOVSPD;

        if (obj.level == 1)
        {
            up = 1;
        }
        else if (obj.level == 10)
        {
            int cnt = ud.beersPoint / cost;
            if (cnt > 10) cnt = 10;

            if (limit != 0)
            {
                while (ad.advLevel + cnt > limit)
                    cnt--;
            }

            cost *= cnt;
            up = cnt;
        }
        else if (obj.level == 100)
        {
            int cnt = ud.beersPoint / cost;
            if (cnt > 100) cnt = 100;

            if (limit != 0)
            {
                while (ad.advLevel + cnt > limit)
                    cnt--;
            }

            cost *= cnt;
            up = cnt;
        }

        ud.AdvanceLvUpEvent(ad.itemID, cost, up);
        ad.advLevel += up;
    }    
}
