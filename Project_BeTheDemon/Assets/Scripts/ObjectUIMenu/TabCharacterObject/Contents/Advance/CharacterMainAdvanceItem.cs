using UnityEngine;
using UnityEngine.UI;


public class CharacterMainAdvanceItem : ScrollItem<ScrollAdvanceData>
{
    ScrollAdvanceData itemData;

    ItemAdvanceVariables contents;

    [Inject] GameData gd = new GameData();
    [Inject] UserData ud = new UserData();
    InjectionObj injectionObj = new InjectionObj();

    void InitializeItemAdvanceVariables()
    {
        contents = new ItemAdvanceVariables();

        contents.advImage = this.transform.GetChild(0).GetChild(0).GetComponent<Image>();
        contents.advLvTitle = this.transform.GetChild(1).GetChild(0).GetComponent<Text>();
        contents.advNumbers = this.transform.GetChild(1).GetChild(1).GetComponent<Text>();
        
        contents.beerCost = this.transform.GetChild(2).GetChild(0).GetChild(1).GetComponent<Text>();
        
        Transform lvOneBtn = this.transform.GetChild(2).GetChild(1);
        contents.advOneBtn = lvOneBtn.GetComponent<Button>();
        Transform lvTenBtn = this.transform.GetChild(2).GetChild(2);
        contents.advTenBtn = lvTenBtn.GetComponent<Button>();
        Transform lvMaxBtn = this.transform.GetChild(2).GetChild(3);
        contents.advHunBtn = lvMaxBtn.GetComponent<Button>();
        
        contents.advOne = lvOneBtn.GetChild(0).GetComponent<Text>();
        contents.advTen = lvTenBtn.GetChild(0).GetComponent<Text>();
        contents.advTen.text = "+10";
        contents.advHun = lvMaxBtn.GetChild(0).GetComponent<Text>();
        contents.advHun.text = "+100";
    }

    public override void InitializeItem(int dataIndex)
    {
        injectionObj.Inject(this);

        InitializeItemAdvanceVariables();
        this.dataIndex = dataIndex;
        contents.advOneBtn.onClick.AddListener(AdvanceOneExcute);
        contents.advTenBtn.onClick.AddListener(AdvanceTenExcute);
        contents.advHunBtn.onClick.AddListener(AdvanceHunExcute);

        contents.advOne.text = gd.UIDataInCharacterMainAdvance();
        EventBus.Subscribe<EventCharacterTabAdvanceBtnByBeer>(BtnActiveByBeerPtHandler);//static
    }

    public override void UpdateContent(ScrollAdvanceData itemData)
    {
        this.itemData = itemData;
        contents.advImage.sprite = itemData.advSprite;
        contents.advLvTitle.text = "Lv." + itemData.advLevel + " " + itemData.advTitle;
        contents.advNumbers.text = AdvNumber(itemData.advLevel);
        contents.beerCost.text = $"{(int)(itemData.advLevel * Constants.BEER_FACTOR) + 5}";
        
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
            contents.advOneBtn.interactable = true;
            contents.advTenBtn.interactable = true;
            contents.advHunBtn.interactable = true;
            contents.advOne.color = new Color(1.0f, 1.0f, 1.0f);
            contents.advTen.color = new Color(1.0f, 1.0f, 1.0f);
            contents.advHun.color = new Color(1.0f, 1.0f, 1.0f);
            contents.advOneBtn.image.color = new Color(1.0f, 1.0f, 1.0f);
            contents.advTenBtn.image.color = new Color(1.0f, 1.0f, 1.0f);
            contents.advHunBtn.image.color = new Color(1.0f, 1.0f, 1.0f);
        }
        else
        {
            contents.advOne.color = new Color(0.6f, 0.6f, 0.6f);
            contents.advTen.color = new Color(0.6f, 0.6f, 0.6f);
            contents.advHun.color = new Color(0.6f, 0.6f, 0.6f);
            contents.advOneBtn.image.color = new Color(0.4f, 0.4f, 0.4f);
            contents.advTenBtn.image.color = new Color(0.4f, 0.4f, 0.4f);
            contents.advHunBtn.image.color = new Color(0.4f, 0.4f, 0.4f);
            contents.advOneBtn.interactable = false;
            contents.advTenBtn.interactable = false;
            contents.advHunBtn.interactable = false;
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
