using UnityEngine;


public class ScrollAbilityData
{
    public Sprite abilitySprite;
    public string abilityTitle;
    public string abilityGain;
    public string abilityMyth;
    public string abilityQtyText;
    public string btnText;
}


public class CharacterMainScrollAbility : ScrollController<ScrollAbilityData>
{
    [Inject] GameData gd = new GameData();
    InjectionObj injectionObj = new InjectionObj();

    void LoadData()
    {
        injectionObj.Inject(this);

        scrollItemMax = Constants.ASCENT_TOTAL;
        scrollItemData = gd.InsertDataInCharacterMainAbility();
    }
    
    protected override void Start()
    {
        LoadData();
        base.Start();
    }
}
