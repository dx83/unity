using UnityEngine;
using UnityEngine.UI;


public class CharacterMainAbilityItem : ScrollItem<ScrollAbilityData>
{
    ItemAbilityVariables contents;

    Text btnText;

    [Inject] UserData ud = new UserData();
    InjectionObj injectionObj = new InjectionObj();

    void InitializeItemAbilityVariables()
    {
        contents = new ItemAbilityVariables();

        contents.abilityImage = this.transform.GetChild(0).GetChild(0).GetComponent<Image>();
        contents.abilityTitle = this.transform.GetChild(1).GetChild(0).GetComponent<Text>();
        contents.abilityGain = this.transform.GetChild(1).GetChild(1).GetComponent<Text>();
        contents.abilityMyth = this.transform.GetChild(1).GetChild(2).GetComponent<Text>();
        contents.abilityBtn = this.transform.GetChild(2).GetChild(0).GetComponent<Button>();
    }

    public override void InitializeItem(int dataIndex)
    {
        injectionObj.Inject(this);

        InitializeItemAbilityVariables();
        this.dataIndex = dataIndex;
        contents.abilityImage.useSpriteMesh = true;
        contents.abilityBtn.onClick.AddListener(AbilityButtonEvent);
        btnText = contents.abilityBtn.GetComponentInChildren<Text>();
    }

    public override void UpdateContent(ScrollAbilityData itemData)
    {
        contents.abilityImage.sprite = itemData.abilitySprite;
        contents.abilityTitle.text = itemData.abilityTitle;
        contents.abilityGain.text = $"{itemData.abilityGain}  {ud.abilityGain[dataIndex]} {itemData.abilityQtyText}";
        contents.abilityMyth.text = $"{itemData.abilityMyth}  {ud.abliityMyth[dataIndex]} {itemData.abilityQtyText}";
        btnText.text = itemData.btnText;

        AblBtnActiveFunc(ud.currentAsc + 1 > dataIndex);
        //### 레벨 제한 추가해야함
    }

    void AblBtnActiveFunc(bool on)
    {
        if (on)
        {
            contents.abilityBtn.interactable = true;
            contents.abilityBtn.image.color = new Color(1.0f, 1.0f, 1.0f);
            btnText.color = new Color(1.0f, 1.0f, 1.0f);
        }
        else
        {
            btnText.color = new Color(0.6f, 0.6f, 0.6f);
            contents.abilityBtn.image.color = new Color(0.4f, 0.4f, 0.4f);
            contents.abilityBtn.interactable = false;
        }
    }

    private void AbilityButtonEvent()
    {
    }
}
