
// UI 이외 스크립트에서 사용시
public class EventStatic
{
    static private EventCharacterTabStatBtnByPt btnActiveByStatPt = new EventCharacterTabStatBtnByPt(0);
    static public void BtnActiveByStatPt(int statPt)
    {
        btnActiveByStatPt.statPt = statPt;
        EventBus.Publish(btnActiveByStatPt);
    }

    static private EventCharacterMidMenuText characterMidTextUpdate = new EventCharacterMidMenuText(0);
    static public void CharacterMidTextUpdate(int num)
    {
        characterMidTextUpdate.num = num;
        EventBus.Publish(characterMidTextUpdate);
    }

    static private EventCharacterTabAdvanceBtnByBeer btnActiveByBeerPt = new EventCharacterTabAdvanceBtnByBeer(0);
    static public void BtnActiveByBeerPt(int beerPt)
    {
        btnActiveByBeerPt.beerPt = beerPt;
        EventBus.Publish(btnActiveByBeerPt);
    }

    static private EventCharacterTabAscentAllItemUpdate updateAfterAscent = new EventCharacterTabAscentAllItemUpdate();
    static public void UpdateAfterAscent()
    {
        EventBus.Publish(updateAfterAscent);
    }
}

