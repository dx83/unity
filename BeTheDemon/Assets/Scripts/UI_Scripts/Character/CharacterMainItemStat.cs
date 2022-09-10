using UnityEngine;
using UnityEngine.UI;


public class CharacterMainItemStat : ScrollItem<ScrollStatData>
{
    [SerializeField] private Text statTitle;
    [SerializeField] private Text statUpper;
    [SerializeField] private Text statLower;
    [SerializeField] private Text level;
    [SerializeField] private Button lvUpOne;
    [SerializeField] private Button lvUpTen;
    [SerializeField] private Button lvUpMax;
    [SerializeField] Text lvOne;
    [SerializeField] Text lvTen;
    [SerializeField] Text lvMax;

    ScrollStatData itemData;

    public override void InitializeItem(int dataIndex)
    {
        this.dataIndex = dataIndex;
        lvUpOne.onClick.AddListener(LevelUpOne);
        lvUpTen.onClick.AddListener(LevelUpTen);
        lvUpMax.onClick.AddListener(LevelUpMax);
        EventBus.Subscribe<EventCharacterTabStatBtnByPt>(BtnActiveByStatPtHandler);//EventStatic
    }

    public override void UpdateContent(ScrollStatData itemData)
    {
        this.itemData = itemData;
        
        statTitle.text = itemData.statTitle;

        if (itemData.itemID == 0)
        {
            itemData.upper = itemData.level * 4;
            itemData.lower = itemData.level * 6;
            statUpper.text = $"{itemData.statUpper}  +{itemData.upper}";
            statLower.text = $"{itemData.statLower}  +{itemData.lower}";
        }
        else if (itemData.itemID == 1)
        {
            itemData.upper = itemData.level * 0.1f;
            itemData.lower = itemData.level * 0.15f;
            statUpper.text = $"{itemData.statUpper}  +{itemData.upper}%";
            statLower.text = $"{itemData.statLower}  +{itemData.lower}%";
        }
        else if (itemData.itemID == 2)
        {
            itemData.upper = itemData.level * 0.05f;
            itemData.lower = itemData.level * 0.5f;
            statUpper.text = $"{itemData.statUpper}  +{itemData.upper}%";
            statLower.text = $"{itemData.statLower}  +{itemData.lower}";
        }
        else if (itemData.itemID == 3)
        {
            itemData.upper = itemData.level * 10;
            itemData.lower = itemData.level * 0.5f;
            statUpper.text = $"{itemData.statUpper}  +{itemData.upper}";
            statLower.text = $"{itemData.statLower}  +{itemData.lower}";
        }

        level.text = $"Lv.{itemData.level}";
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
        lvUpOne.interactable = true;
        lvUpTen.interactable = true;
        lvUpMax.interactable = true;
        lvOne.color = new Color(1.0f, 1.0f, 1.0f);
        lvTen.color = new Color(1.0f, 1.0f, 1.0f);
        lvMax.color = new Color(1.0f, 1.0f, 1.0f);
        lvUpOne.image.color = new Color(1.0f, 1.0f, 1.0f);
        lvUpTen.image.color = new Color(1.0f, 1.0f, 1.0f);
        lvUpMax.image.color = new Color(1.0f, 1.0f, 1.0f);

        if (obj.statPt < 1)
        {
            lvUpOne.interactable = false;
            lvUpTen.interactable = false;
            lvUpMax.interactable = false;
            lvOne.color = new Color(0.6f, 0.6f, 0.6f);
            lvTen.color = new Color(0.6f, 0.6f, 0.6f);
            lvMax.color = new Color(0.6f, 0.6f, 0.6f);
            lvUpOne.image.color = new Color(0.4f, 0.4f, 0.4f);
            lvUpTen.image.color = new Color(0.4f, 0.4f, 0.4f);
            lvUpMax.image.color = new Color(0.4f, 0.4f, 0.4f);
        }
        else if (obj.statPt < 10)
        {
            lvUpTen.interactable = false;
            lvTen.color = new Color(0.6f, 0.6f, 0.6f);
            lvUpTen.image.color = new Color(0.4f, 0.4f, 0.4f);
        }
    }
}
