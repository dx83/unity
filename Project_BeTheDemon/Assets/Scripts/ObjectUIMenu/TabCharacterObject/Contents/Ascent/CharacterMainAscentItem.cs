using UnityEngine;
using UnityEngine.UI;


public class CharacterMainAscentItem : ScrollItem<ScrollAscentData>
{
    ItemAscentVariables contents;

    GameObject btnPanel;
    Text btnText;

    [Inject] UserData ud = new UserData();
    InjectionObj injectionObj = new InjectionObj();

    void InitializeItemAscentVariables()
    {
        contents = new ItemAscentVariables();

        contents.ascentImage = this.transform.GetChild(0).GetChild(0).GetComponent<Image>();
        contents.ascentTitle = this.transform.GetChild(1).GetChild(0).GetComponent<Text>();
        contents.ascentStr = this.transform.GetChild(1).GetChild(1).GetComponent<Text>();
        contents.ascentSpd = this.transform.GetChild(1).GetChild(2).GetComponent<Text>();
        contents.ascentBtn = this.transform.GetChild(2).GetChild(0).GetComponent<Button>();
    }

    public override void InitializeItem(int dataIndex)
    {
        injectionObj.Inject(this);

        InitializeItemAscentVariables();
        this.dataIndex = dataIndex;
        contents.ascentImage.useSpriteMesh = true;
        contents.ascentBtn.onClick.AddListener(AscentButtonEvent);
        btnPanel = contents.ascentBtn.GetComponentInParent<RectTransform>().gameObject;
        btnText = contents.ascentBtn.GetComponentInChildren<Text>();
    }

    public override void UpdateContent(ScrollAscentData itemData)
    {
        contents.ascentImage.sprite = itemData.ascentSprite;
        contents.ascentTitle.text = itemData.ascentTitle;
        
        if (dataIndex < Constants.ASCENT_FACTOR - 1)
        {
            contents.ascentStr.text = $"{itemData.ascentStr} +{dataIndex * 10}%";
            contents.ascentSpd.text = $"{itemData.ascentSpd} +{dataIndex * 5}%";
        }
        else
        {
            contents.ascentStr.text = $"{itemData.ascentStr} +{dataIndex * 10 + 10}%";
            contents.ascentSpd.text = $"{itemData.ascentSpd} +{(Constants.ASCENT_FACTOR * 5) + (dataIndex - Constants.ASCENT_FACTOR) * 10 + 10}%";
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
            contents.ascentBtn.interactable = true;
            contents.ascentBtn.image.color = new Color(1.0f, 1.0f, 1.0f);
            btnText.color = new Color(1.0f, 1.0f, 1.0f);
        }
        else
        {
            btnText.color = new Color(0.6f, 0.6f, 0.6f);
            contents.ascentBtn.image.color = new Color(0.4f, 0.4f, 0.4f);
            contents.ascentBtn.interactable = false;
        }
    }
    
    private void AscentButtonEvent()
    {
        ud.AscentPromoteEvent();
        EventStatic.UpdateAfterAscent();
    }
}
