using UnityEngine;


public class LowerMenuTabData
{
    public Sprite tabIconSprite;
    public string tabTitleText;
}


public class LowerMenuTabCont : TabController<LowerMenuTabData>
{
    [Inject] GameData gameData = new GameData();
    InjectionObj injectionObj = new InjectionObj();

    void LoadData()
    {
        injectionObj.Inject(this);
        tabItemData = gameData.InsertDataInLowerMenuTabCont(tabAmount);
    }

    protected override void Start()
    {
        LoadData();
        base.Start();
    }
}
