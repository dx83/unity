using UnityEngine;
using UnityEngine.UI;


// CharacterMainAttributeScroll
public class ScrollAttributeData
{
    public int itemID;
    public string statTitle;
    public string statUpper;
    public string statLower;
    public int level;
    public float upper;
    public float lower;
}

// CharacterMainAttributeItem
public class ItemAttributeVariables
{
    public Text attrTitle;
    public Text attrUpper;
    public Text attrLower;
    public Text level;
    public Button lvUpOne;
    public Button lvUpTen;
    public Button lvUpMax;
    public Text lvOne;
    public Text lvTen;
    public Text lvMax;
}

// CharacterMainAdvanceScroll
public class ScrollAdvanceData
{
    public int itemID;
    public Sprite advSprite;
    public int advLevel;
    public string advTitle;
}

// CharacterMainAdvanceItem
public class ItemAdvanceVariables
{
    public Image advImage;
    public Text advLvTitle;
    public Text advNumbers;
    public Text beerCost;
    public Button advOneBtn;
    public Button advTenBtn;
    public Button advHunBtn;
    public Text advOne;
    public Text advTen;
    public Text advHun;
}

// CharacterMainAscentScroll
public class ScrollAscentData
{
    public int itemID;
    public Sprite ascentSprite;
    public string ascentTitle;
    public string ascentStr;
    public string ascentSpd;
    public string btnCom;
    public string btnAsc;
}

// CharacterMainAscentItem
public class ItemAscentVariables
{
    public Image ascentImage;
    public Text ascentTitle;
    public Text ascentStr;
    public Text ascentSpd;
    public Button ascentBtn;
}

// CharacterMainAbilityScroll
public class ScrollAbilityData
{
    public Sprite abilitySprite;
    public string abilityTitle;
    public string abilityGain;
    public string abilityMyth;
    public string abilityQtyText;
    public string btnText;
}

// CharacterMainAbilityItem
public class ItemAbilityVariables
{
    public Image abilityImage;
    public Text abilityTitle;
    public Text abilityGain;
    public Text abilityMyth;
    public Button abilityBtn;
}

