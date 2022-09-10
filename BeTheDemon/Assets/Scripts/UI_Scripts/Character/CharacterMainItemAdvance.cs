using UnityEngine;
using UnityEngine.UI;


public class CharacterMainItemAdvance : ScrollItem<ScrollAdvanceData>
{
    [SerializeField] private Image advImage;
    [SerializeField] private Text advLvTitle;
    [SerializeField] private Text advNumbers;
    [SerializeField] private Text beerCost;
    [SerializeField] private Button advOne;
    [SerializeField] private Button advTen;
    [SerializeField] private Button advHun;
    [SerializeField] private Text advOneBtnText;
    [SerializeField] private Text advTenBtnText;
    [SerializeField] private Text advHunBtnText;

    ScrollAdvanceData itemData;

    [Inject] GameData gd = new GameData();
    [Inject] UserData ud = new UserData();
    InjectionObj injectionObj = new InjectionObj();

    public override void InitializeItem(int dataIndex)
    {
        injectionObj.Inject(this);

        this.dataIndex = dataIndex;
        advOne.onClick.AddListener(AdvanceOneExcute);
        advTen.onClick.AddListener(AdvanceTenExcute);
        advHun.onClick.AddListener(AdvanceHunExcute);

        advOneBtnText.text = gd.UIDataInCharacterMainAdvance();
        EventBus.Subscribe<EventCharacterTabAdvanceBtnByBeer>(BtnActiveByBeerPtHandler);
    }

    public override void UpdateContent(ScrollAdvanceData itemData)
    {
        this.itemData = itemData;
        advImage.sprite = itemData.advSprite;
        advLvTitle.text = "Lv." + itemData.advLevel + " " + itemData.advTitle;
        advNumbers.text = AdvNumber(itemData.advLevel);
        beerCost.text = $"{(int)(itemData.advLevel * Constants.BEER_FACTOR) + 5}";
        
        EventStatic.BtnActiveByBeerPt(ud.beersPoint);
    }

    string AdvNumber(int level)
    {
        int currInt = 0, nextInt = 0;
        float currFloat = 0.0f, nextFloat= 0.0f;
        switch(dataIndex)
        {
            case 0: // Max Damage
                currInt = level * 5;
                nextInt = (level + 1) * 5;
                return $"{currInt} ⇒ {nextInt}";

            case 1: // Damage(%)
                currFloat = (float)level / 10;
                nextFloat = (float)(level + 1) / 10;
                return $"{currFloat} ⇒ {nextFloat}";

            case 2: // Accuracy
                nextInt = level + 1;
                return $"{level} ⇒ {nextInt}";

            case 3: // Dodge
                nextInt = level + 1;
                return $"{level} ⇒ {nextInt}";

            case 4: // Attack Speed
                currFloat = (float)level / 10;
                nextFloat = (float)(level + 1) / 10;
                if (level == Constants.MAX_ATKSPD)
                {
                    AdvBtnActiveFunc(false);
                    return $"{currFloat} (MAX)";
                }
                else
                    return $"{currFloat} ⇒ {nextFloat}";

            case 5: // Move Speed
                currFloat = (float)level / 10;
                nextFloat = (float)(level + 1) / 10;
                if (level == Constants.MAX_MOVSPD)
                {
                    AdvBtnActiveFunc(false);
                    return $"{currFloat} (MAX)";
                }
                else
                    return $"{currFloat} ⇒ {nextFloat}";

            default:
                return "";
        }
    }

    private void BtnActiveByBeerPtHandler(EventCharacterTabAdvanceBtnByBeer obj)
    {
        if (itemData.itemID == 4 && itemData.advLevel == Constants.MAX_ATKSPD)
        {
            AdvBtnActiveFunc(false);
            return;
        }
        else if (itemData.itemID == 5 && itemData.advLevel == Constants.MAX_MOVSPD)
        {
            AdvBtnActiveFunc(false);
            return;
        }

        int cost = (int)(itemData.advLevel * Constants.BEER_FACTOR) + 5;
        if (obj.beerPt < cost)
            AdvBtnActiveFunc(false);
        else
            AdvBtnActiveFunc(true);
    }

    private void AdvBtnActiveFunc(bool on)
    {
        if (on)
        {
            advOne.interactable = true;
            advTen.interactable = true;
            advHun.interactable = true;
            advOneBtnText.color = new Color(1.0f, 1.0f, 1.0f);
            advTenBtnText.color = new Color(1.0f, 1.0f, 1.0f);
            advHunBtnText.color = new Color(1.0f, 1.0f, 1.0f);
            advOne.image.color = new Color(1.0f, 1.0f, 1.0f);
            advTen.image.color = new Color(1.0f, 1.0f, 1.0f);
            advHun.image.color = new Color(1.0f, 1.0f, 1.0f);
        }
        else
        {
            advOneBtnText.color = new Color(0.6f, 0.6f, 0.6f);
            advTenBtnText.color = new Color(0.6f, 0.6f, 0.6f);
            advHunBtnText.color = new Color(0.6f, 0.6f, 0.6f);
            advOne.image.color = new Color(0.4f, 0.4f, 0.4f);
            advTen.image.color = new Color(0.4f, 0.4f, 0.4f);
            advHun.image.color = new Color(0.4f, 0.4f, 0.4f);
            advOne.interactable = false;
            advTen.interactable = false;
            advHun.interactable = false;
        }
    }

    //이 스크립트에서만 사용, 유니티 UI는 단일 스레드 사용
    private EventCharacterTabAdvanceLvUp lvUpEvent = new EventCharacterTabAdvanceLvUp(0, 0);
    private void LvUpEvent(int dataIndex, int level)
    {
        lvUpEvent.index = dataIndex;
        lvUpEvent.level = level;

        EventBus.Publish(lvUpEvent);
    }

    private void AdvanceOneExcute()
    {
        LvUpEvent(dataIndex, 1);
        UpdateContent(itemData);
    }
    private void AdvanceTenExcute()
    {
        LvUpEvent(dataIndex, 10);
        UpdateContent(itemData);
    }
    private void AdvanceHunExcute()
    {
        LvUpEvent(dataIndex, 100);
        UpdateContent(itemData);
    }
}
