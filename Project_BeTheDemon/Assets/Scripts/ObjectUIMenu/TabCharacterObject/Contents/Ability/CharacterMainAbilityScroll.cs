using UnityEngine;


public class CharacterMainAbilityScroll : ScrollController<ScrollAbilityData>
{
    [Inject] GameData gd = new GameData();
    InjectionObj injectionObj = new InjectionObj();

    void LoadData()
    {
        injectionObj.Inject(this);

        scrollItemMax = Constants.ASCENT_TOTAL;
        scrollItemData = gd.InsertDataInCharacterMainAbility();
        EventBus.Subscribe<EventCharacterTabAscentAllItemUpdate>(DataUpdateAfterAscentPromote);
    }
    
    protected override void Start()
    {
        LoadData();
        base.Start();
    }

    private EventCharacterMidMenuAscentUpdate ascentUpdate = new EventCharacterMidMenuAscentUpdate();

    private void DataUpdateAfterAscentPromote(EventCharacterTabAscentAllItemUpdate obj)
    {
        UpdateItemDataInScroll();
        EventBus.Publish(ascentUpdate);
    }
}
