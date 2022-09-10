

public class EventCharacterMidMenuText
{
    public int num;
    public EventCharacterMidMenuText(int num)
    {
        this.num = num;
    }
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

public class EventCharacterTabStatBtnByPt
{
    public int statPt;
    public EventCharacterTabStatBtnByPt(int statPt)
    {
        this.statPt = statPt;
    }
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

public class EventCharacterTabAdvanceBtnByBeer
{
    public int beerPt;
    public EventCharacterTabAdvanceBtnByBeer(int beerPt)
    {
        this.beerPt = beerPt;
    }
}
//EventBus.Unsubscribe<EventCharacterTabAdvanceBtnByBeer>(BtnActiveByBeerPtHandler);

public class EventCharacterTabAscentAllItemUpdate { }
public class EventCharacterMidMenuAscentUpdate { }

