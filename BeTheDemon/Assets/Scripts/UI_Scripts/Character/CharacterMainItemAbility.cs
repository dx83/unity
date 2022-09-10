using UnityEngine;
using UnityEngine.UI;


public class CharacterMainItemAbility : ScrollItem<ScrollAbilityData>
{
    [SerializeField] private Image abilityImage;
    [SerializeField] private Text abilityTitle;
    [SerializeField] private Text abilityGain;
    [SerializeField] private Text abilityMyth;
    [SerializeField] private Button abilityBtn;
    Text btnText;

    [Inject] UserData ud = new UserData();
    InjectionObj injectionObj = new InjectionObj();

    public override void InitializeItem(int dataIndex)
    {
        injectionObj.Inject(this);

        this.dataIndex = dataIndex;
        abilityImage.useSpriteMesh = true;
        abilityBtn.onClick.AddListener(AbilityButtonEvent);
        btnText = abilityBtn.GetComponentInChildren<Text>();
    }

    public override void UpdateContent(ScrollAbilityData itemData)
    {
        abilityImage.sprite = itemData.abilitySprite;
        abilityTitle.text = itemData.abilityTitle;
        abilityGain.text = $"{itemData.abilityGain}  {ud.abilityGain[dataIndex]} {itemData.abilityQtyText}";
        abilityMyth.text = $"{itemData.abilityMyth}  {ud.abliityMyth[dataIndex]} {itemData.abilityQtyText}";
        btnText.text = itemData.btnText;

        AblBtnActiveFunc(ud.currentAsc + 1 > dataIndex);
        //### 레벨 제한 추가해야함
    }

    void AblBtnActiveFunc(bool on)
    {
        if (on)
        {
            abilityBtn.interactable = true;
            abilityBtn.image.color = new Color(1.0f, 1.0f, 1.0f);
            btnText.color = new Color(1.0f, 1.0f, 1.0f);
        }
        else
        {
            btnText.color = new Color(0.6f, 0.6f, 0.6f);
            abilityBtn.image.color = new Color(0.4f, 0.4f, 0.4f);
            abilityBtn.interactable = false;
        }
    }

    private void AbilityButtonEvent()
    {
    }
}
