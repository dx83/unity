using UnityEngine;
using UnityEngine.UI;


public class CharacterMainItemAscent : ScrollItem<ScrollAscentData>
{
    [SerializeField] private Image ascentImage;
    [SerializeField] private Text ascentTitle;
    [SerializeField] private Text ascentStr;
    [SerializeField] private Text ascentSpd;
    [SerializeField] private Button ascentBtn;
    GameObject btnPanel;
    Text btnText;

    [Inject] UserData ud = new UserData();
    InjectionObj injectionObj = new InjectionObj();

    public override void InitializeItem(int dataIndex)
    {
        injectionObj.Inject(this);

        this.dataIndex = dataIndex;
        ascentImage.useSpriteMesh = true;
        ascentBtn.onClick.AddListener(AscentButtonEvent);
        btnPanel = ascentBtn.GetComponentInParent<RectTransform>().gameObject;
        btnText = ascentBtn.GetComponentInChildren<Text>();
    }

    public override void UpdateContent(ScrollAscentData itemData)
    {
        ascentImage.sprite = itemData.ascentSprite;
        ascentTitle.text = itemData.ascentTitle;
        
        if (dataIndex < Constants.ASCENT_FACTOR - 1)
        {
            ascentStr.text = $"{itemData.ascentStr} +{dataIndex * 10}%";
            ascentSpd.text = $"{itemData.ascentSpd} +{dataIndex * 5}%";
        }
        else
        {
            ascentStr.text = $"{itemData.ascentStr} +{dataIndex * 10 + 10}%";
            ascentSpd.text = $"{itemData.ascentSpd} +{(Constants.ASCENT_FACTOR * 5) + (dataIndex - Constants.ASCENT_FACTOR) * 10 + 10}%";
        }
        
        if (dataIndex == 0)
            btnPanel.SetActive(false);
        else
        {
            btnPanel.SetActive(true);
            if (ud.currentAscent + 1 > dataIndex)
            {
                AscBtnActiveFunc(false);
                btnText.text = itemData.btnCom;
            }
            else if (ud.currentAscent + 1 == dataIndex)
            {
                AscBtnActiveFunc(true);
                btnText.text = itemData.btnAsc;
            }
            else if (ud.currentAscent + 1 < dataIndex)
            {
                AscBtnActiveFunc(false);
                btnText.text = itemData.btnAsc;
            }
            //### 레벨 제한 추가해야함
        }
    }

    void AscBtnActiveFunc(bool on)
    {
        if (on)
        {
            ascentBtn.interactable = true;
            ascentBtn.image.color = new Color(1.0f, 1.0f, 1.0f);
            btnText.color = new Color(1.0f, 1.0f, 1.0f);
        }
        else
        {
            btnText.color = new Color(0.6f, 0.6f, 0.6f);
            ascentBtn.image.color = new Color(0.4f, 0.4f, 0.4f);
            ascentBtn.interactable = false;
        }
    }
    
    private void AscentButtonEvent()
    {
        ud.AscentPromoteEvent();
        EventStatic.UpdateAfterAscent();
    }
}
