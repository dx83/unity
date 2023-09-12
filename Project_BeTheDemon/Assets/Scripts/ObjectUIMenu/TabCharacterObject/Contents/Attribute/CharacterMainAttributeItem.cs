using UnityEngine;
using UnityEngine.UI;


public class CharacterMainAttributeItem : ScrollItem<ScrollAttributeData>
{
    ScrollAttributeData itemData;

    ItemAttributeVariables contents;

    void InitializeItemAttributeVariables()
    {
        contents = new ItemAttributeVariables();

        contents.attrTitle = this.transform.GetChild(0).GetChild(0).GetComponent<Text>();
        contents.attrUpper = this.transform.GetChild(1).GetChild(0).GetComponent<Text>();
        contents.attrLower = this.transform.GetChild(1).GetChild(1).GetComponent<Text>();

        contents.level = this.transform.GetChild(2).GetChild(0).GetComponent<Text>();
        
        Transform lvOneBtn = this.transform.GetChild(2).GetChild(1);
        contents.lvUpOne = lvOneBtn.GetComponent<Button>();
        Transform lvTenBtn = this.transform.GetChild(2).GetChild(2);
        contents.lvUpTen = lvTenBtn.GetComponent<Button>();
        Transform lvMaxBtn = this.transform.GetChild(2).GetChild(3);
        contents.lvUpMax = lvMaxBtn.GetComponent<Button>();
        
        contents.lvOne = lvOneBtn.GetChild(0).GetComponent<Text>();
        contents.lvOne.text = "+1";
        contents.lvTen = lvTenBtn.GetChild(0).GetComponent<Text>();
        contents.lvTen.text = "+10";
        contents.lvMax = lvMaxBtn.GetChild(0).GetComponent<Text>();
        contents.lvMax.text = "MAX";
    }

    public override void InitializeItem(int dataIndex)
    {
        InitializeItemAttributeVariables();
        this.dataIndex = dataIndex;
        contents.lvUpOne.onClick.AddListener(LevelUpOne);
        contents.lvUpTen.onClick.AddListener(LevelUpTen);
        contents.lvUpMax.onClick.AddListener(LevelUpMax);
        EventBus.Subscribe<EventCharacterTabStatBtnByPt>(BtnActiveByStatPtHandler);//EventStatic
    }

    public override void UpdateContent(ScrollAttributeData itemData)
    {
        this.itemData = itemData;

        contents.attrTitle.text = itemData.statTitle;

        if (itemData.itemID == 0)
        {
            itemData.upper = itemData.level * 4;
            itemData.lower = itemData.level * 6;
            contents.attrUpper.text = $"{itemData.statUpper}  +{itemData.upper}";
            contents.attrLower.text = $"{itemData.statLower}  +{itemData.lower}";
        }
        else if (itemData.itemID == 1)
        {
            itemData.upper = itemData.level * 0.1f;
            itemData.lower = itemData.level * 0.15f;
            contents.attrUpper.text = $"{itemData.statUpper}  +{itemData.upper}%";
            contents.attrLower.text = $"{itemData.statLower}  +{itemData.lower}%";
        }
        else if (itemData.itemID == 2)
        {
            itemData.upper = itemData.level * 0.05f;
            itemData.lower = itemData.level * 0.5f;
            contents.attrUpper.text = $"{itemData.statUpper}  +{itemData.upper}%";
            contents.attrLower.text = $"{itemData.statLower}  +{itemData.lower}";
        }
        else if (itemData.itemID == 3)
        {
            itemData.upper = itemData.level * 10;
            itemData.lower = itemData.level * 0.5f;
            contents.attrUpper.text = $"{itemData.statUpper}  +{itemData.upper}";
            contents.attrLower.text = $"{itemData.statLower}  +{itemData.lower}";
        }

        contents.level.text = $"Lv.{itemData.level}";
    }

    //이 스크립트에서만 사용, 유니티 UI는 단일 스레드 사용
    private EventCharacterTabStatLevelUp lvUpEvent = new EventCharacterTabStatLevelUp(0,0);
    private void LvUpEvent(int dataIndex, int level)
    {
        lvUpEvent.index = dataIndex;
        lvUpEvent.level = level;

        EventBus.Publish(lvUpEvent);
    }

    private void LevelUpOne()
    {
        LvUpEvent(dataIndex, 1);
        UpdateContent(itemData);
    }

    private void LevelUpTen()
    {
        LvUpEvent(dataIndex, 10);
        UpdateContent(itemData);
    }

    private void LevelUpMax()
    {
        LvUpEvent(dataIndex, 0);
        UpdateContent(itemData);
    }

    private void BtnActiveByStatPtHandler(EventCharacterTabStatBtnByPt obj)
    {
        contents.lvUpOne.interactable = true;
        contents.lvUpTen.interactable = true;
        contents.lvUpMax.interactable = true;
        contents.lvOne.color = new Color(1.0f, 1.0f, 1.0f);
        contents.lvTen.color = new Color(1.0f, 1.0f, 1.0f);
        contents.lvMax.color = new Color(1.0f, 1.0f, 1.0f);
        contents.lvUpOne.image.color = new Color(1.0f, 1.0f, 1.0f);
        contents.lvUpTen.image.color = new Color(1.0f, 1.0f, 1.0f);
        contents.lvUpMax.image.color = new Color(1.0f, 1.0f, 1.0f);

        if (obj.statPt < 1)
        {
            contents.lvUpOne.interactable = false;
            contents.lvUpTen.interactable = false;
            contents.lvUpMax.interactable = false;
            contents.lvOne.color = new Color(0.6f, 0.6f, 0.6f);
            contents.lvTen.color = new Color(0.6f, 0.6f, 0.6f);
            contents.lvMax.color = new Color(0.6f, 0.6f, 0.6f);
            contents.lvUpOne.image.color = new Color(0.4f, 0.4f, 0.4f);
            contents.lvUpTen.image.color = new Color(0.4f, 0.4f, 0.4f);
            contents.lvUpMax.image.color = new Color(0.4f, 0.4f, 0.4f);
        }
        else if (obj.statPt < 10)
        {
            contents.lvUpTen.interactable = false;
            contents.lvTen.color = new Color(0.6f, 0.6f, 0.6f);
            contents.lvUpTen.image.color = new Color(0.4f, 0.4f, 0.4f);
        }
    }
}

