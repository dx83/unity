using UnityEngine;
using UnityEngine.UI;


public class CharacterMidTabData
{
    public string tabTitleText;
    public GameObject[] tabObject;
}

public class CharacterMidUIText
{
    public int index;
    public string image;
    public string text;
    public string text2;
}

public class CharacterMidVariables
{
    public Image characterPortrait;

    public GameObject[] contentForTab;

    // Attribute / Advance
    public Text characterLevel;
    public Text levelPercentage;
    public Slider sliderForLevel;

    // Attribute
    public Text statsPtText;
    public Text currentStats;

    // Advance
    public Text fullMessage;
    public Text currentBeers;

    // Ascent / Ability
    public Text currentAscent;
    public Text currentSTR;
    public Text currentAtkSpd;
    public Button ascentBtn;
    public Text ascentText;
}
