using System;
using UnityEngine;

[Serializable]
public class UserData
{
    public string userId;
    
    public int characterLevel;
    public float expPercent;
    public int statsPoint;
    public int beersPoint;
    public int currentAscent;
    // stat
    public int strLv;
    public int dexLv;
    public int agiLv;
    public int lukLv;
    // advance
    public int maxAtkLv;
    public int atkPerLv;
    public int accuracyLv;
    public int dodgeLv;
    public int atkSpdLv;
    public int movSpdLv;
    // ascent
    [HideInInspector]public int ascStrPer;
    [HideInInspector]public int ascAtkSpd;
    // ability
    public int currentAsc
    { 
        get { return currentAscent / 5; }
    }
    public int[] abilityGain = new int[Constants.ASCENT_TOTAL];
    public int[] abliityMyth = new int[Constants.ASCENT_TOTAL];

    // etc..
    public float heroSpeed;

    public void StatLevelUpEvent(int itemID, int up)
    {
        statsPoint -= up;
        EventStatic.CharacterMidTextUpdate(2);
        EventStatic.BtnActiveByStatPt(statsPoint);

        switch (itemID)
        {
            case 0: strLv += up; break;
            case 1: dexLv += up; break;
            case 2: agiLv += up; break;
            case 3: lukLv += up; break;
        }
    }

    public void AdvanceLvUpEvent(int itemID, int cost, int up)
    {
        beersPoint -= cost;
        EventStatic.CharacterMidTextUpdate(3);
        EventStatic.BtnActiveByBeerPt(beersPoint);

        int curLv = 0;
        switch (itemID)
        {
            case 0: maxAtkLv += up; curLv = maxAtkLv; break;
            case 1: atkPerLv += up; curLv = atkPerLv; break;
            case 2: accuracyLv += up; curLv = accuracyLv; break;
            case 3: dodgeLv += up; curLv = dodgeLv; break;
            case 4: atkSpdLv += up; curLv = atkSpdLv; break;
            case 5: movSpdLv += up; curLv = movSpdLv; break;
        }
    }

    public void AscentPromoteEvent()
    {
        if (currentAscent < Constants.ASCENT_MAXIDX)
            currentAscent++;

        AscentCurrentApply();
    }

    public void AscentCurrentApply()
    {
        if (currentAscent < Constants.ASCENT_FACTOR)
        {
            ascStrPer = currentAscent * 10;
            ascAtkSpd = currentAscent * 5;
        }
        else
        {
            ascStrPer = currentAscent * 10 + 10;
            ascAtkSpd = (Constants.ASCENT_FACTOR * 5) + (currentAscent - Constants.ASCENT_FACTOR) * 10 + 10;
        }
    }
}
