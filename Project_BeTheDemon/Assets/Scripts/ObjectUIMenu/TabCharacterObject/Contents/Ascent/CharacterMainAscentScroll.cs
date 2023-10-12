using UnityEngine;


public class CharacterMainAscentScroll : ScrollController<ScrollAscentData>
{
    [Inject] GameData gd = new GameData();
    [Inject] UserData ud = new UserData();
    InjectionObj injectionObj = new InjectionObj();


    void LoadData()
    {
        injectionObj.Inject(this);
        scrollItemData = gd.InsertDataInCharacterMainAscent();
        scrollItemMax = scrollItemData.Count;
    }

    protected override void Start()
    {
        LoadData();
        base.Start();
        int curLoc = (ud.currentAscent == Constants.ASCENT_MAXIDX ? ud.currentAscent : ud.currentAscent + 1);
        ScrollItemsToLocate(curLoc);

        EventBus.Subscribe<EventCharacterTabAscentAllItemUpdate>(DataUpdateAfterAscentPromote);
    }

    bool initComp = false;
    protected void OnEnable()
    {
        if (initComp)
        {
            int curLoc = (ud.currentAscent == Constants.ASCENT_MAXIDX ? ud.currentAscent : ud.currentAscent + 1);
            ScrollItemsToLocate(curLoc);
        }
        initComp = true;
    }

    private EventCharacterMidMenuAscentUpdate ascentUpdate = new EventCharacterMidMenuAscentUpdate();

    private void DataUpdateAfterAscentPromote(EventCharacterTabAscentAllItemUpdate obj)
    {
        UpdateItemDataInScroll();
        EventBus.Publish(ascentUpdate);
    }
}
