

public class EventCharacterMidMenuText//EventStatic
{
    public int num;
    public EventCharacterMidMenuText(int n) => num = n;
}
//EventBus.Unsubscribe<EventCharacterMidMenuText>(CharacterMidMenuTextHandler);

public class EventCharacterTabStatLevelUp
{ 
    public int index, level;
    public EventCharacterTabStatLevelUp(int index, int level)
    { 
        this.index = index;
        this.level = level;
    }
}
//EventBus.Unsubscribe<EventCharacterTabStatLevelUp>(LevelUpEventHandler);

public class EventCharacterTabStatBtnByPt//EventStatic
{
    public int statPt;
    public EventCharacterTabStatBtnByPt(int n) => statPt = n;
}
//EventBus.Unsubscribe<EventCharacterTabStatBtnByPt>(BtnActiveByStatPtHandler);

public class EventCharacterTabAdvanceLvUp
{
    public int index, level;
    public EventCharacterTabAdvanceLvUp(int index, int level)
    {
        this.index = index;
        this.level = level;
    }
}
//EventBus.Unsubscribe<EventCharacterTabAdvanceLvUp>(LevelUpEventHandler);

public class EventCharacterTabAdvanceBtnByBeer//EventStatic
{
    public int beerPt;
    public EventCharacterTabAdvanceBtnByBeer(int n) => beerPt = n;
}
//EventBus.Unsubscribe<EventCharacterTabAdvanceBtnByBeer>(BtnActiveByBeerPtHandler);

public class EventCharacterTabAscentAllItemUpdate { }//EventStatic
//EventBus.Unsubscribe<EventCharacterTabAscentAllItemUpdate>(DataUpdateAfterAscentPromote);

public class EventCharacterMidMenuAscentUpdate { }
//EventBus.Unsubscribe<EventCharacterMidMenuAscentUpdate>(DataUpdateAfterAscentPromote);

