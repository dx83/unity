using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;


public class GameData
{
    int languageSet;
    List<string> sheets;
    Dictionary<string, List<UIDatas>> datas;

    public void GameDataLoad(int language)
    {
        languageSet = language;
        string codeSheets = Resources.Load<TextAsset>("Data000").text;
        byte[] bytesSheets = Convert.FromBase64String(codeSheets);
        string jDataSheets = Encoding.UTF8.GetString(bytesSheets);
        sheets = JsonConvert.DeserializeObject<List<string>>(jDataSheets);
        
        string codeDatas = Resources.Load<TextAsset>("Data001").text;
        byte[] bytesDatas = Convert.FromBase64String(codeDatas);
        string jDatas = Encoding.UTF8.GetString(bytesDatas);
        datas = JsonConvert.DeserializeObject<Dictionary<string, List<UIDatas>>>(jDatas);
    }

    public bool isNull()
    {
        return datas == null;
    }

    List<UIDatas> dataList;
    public void ExcelDataFunc(int sheetNum)
    {
        dataList = datas[sheets[sheetNum]];
    }

    public string UIimageName(int index)
    {
        return dataList[index].Image;
    }

    public string StringByLang(int cell, int set)
    {
        switch(languageSet)
        {
            case 0:
                if (set == 0)       return dataList[cell].Kor;
                else if (set == 1)  return dataList[cell].Kor1;
                else if (set == 2)  return dataList[cell].Kor2;
                break;

            case 1:
                if (set == 0)       return dataList[cell].Eng;
                else if (set == 1)  return dataList[cell].Eng1;
                else if (set == 2)  return dataList[cell].Eng2;
                break;
        }
        return "";
    }

    int DataCodeToIndex(string code) => dataList.Where(d => d.Code == code).First().Index;


    public LoadingSceneDataStrings InsertDataLoadingScene()
    {
        ExcelDataFunc(0);
        LoadingSceneDataStrings datas = new LoadingSceneDataStrings();

        datas.loading = StringByLang(DataCodeToIndex("cbload"), 0);
        datas.start = StringByLang(DataCodeToIndex("cbgstart"), 0);

        return datas;
    }


    public QuitDataStrings InsertDataInQuitWindow()
    {
        ExcelDataFunc(0);
        QuitDataStrings datas = new QuitDataStrings();

        datas.title = StringByLang(DataCodeToIndex("cbcon"), 0);
        datas.tellQuit = StringByLang(DataCodeToIndex("tellquit"), 0);
        datas.btnNo = StringByLang(DataCodeToIndex("cbno"), 0);
        datas.btnQuit = StringByLang(DataCodeToIndex("cbquit"), 0);

        datas.backKor = StringByLang(DataCodeToIndex("cblkor"), 0);
        datas.backEng = StringByLang(DataCodeToIndex("cbleng"), 0);

        return datas;
    }


    public List<LowerMenuTabData> InsertDataInLowerMenuTabCont(int tabAmount)
    {
        ExcelDataFunc(0);
        List<LowerMenuTabData> list = new List<LowerMenuTabData>();

        int idx = dataList.Where(d => d.Code == "mchar").First().Index;

        for (int i = 0; i < tabAmount; i++)
        {
            list.Add(new LowerMenuTabData
            {
                tabIconSprite = SpriteSheetManager.GetSpriteByName(UIimageName(idx + i)),
                tabTitleText = StringByLang(idx + i, 0),
            });
        }
        return list;
    }

    public List<CharacterMidTabData> InsertDataInCharacterMidTabCont(int tabAmount, GameObject[] contentForTab)
    {
        ExcelDataFunc(0);
        List<CharacterMidTabData> list = new List<CharacterMidTabData>();

        int idx = dataList.Where(d => d.Code == "mattr").First().Index;

        for (int i = 0; i < tabAmount; i++)
        {
            list.Add(new CharacterMidTabData
            {
                tabTitleText = StringByLang(idx + i, 0),
                tabObject = contentForTab
            });
        }

        return list;
    }

    public List<CharacterMidUIText> InsertDataInCharacterMidTabCont()
    {
        ExcelDataFunc(0);
        List<CharacterMidUIText> list = new List<CharacterMidUIText>();

        for (int i = 0; i < 5; i++)
        {
            int idx = 0, btn = 0;
            if (i == 0) idx = dataList.Where(d => d.Code == "ctpoint").First().Index;
            else if (i == 1) idx = dataList.Where(d => d.Code == "ctfull").First().Index;
            else if (i == 2) idx = dataList.Where(d => d.Code == "ctstr").First().Index;
            else if (i == 3) idx = dataList.Where(d => d.Code == "satkspd").First().Index;
            else if (i == 4)
            {
                idx = dataList.Where(d => d.Code == "masc").First().Index;
                btn = dataList.Where(d => d.Code == "ctcomp").First().Index;
            }

            list.Add(new CharacterMidUIText
            {
                index = dataList[idx].Index,
                image = dataList[idx].Image,
                text = StringByLang(idx, 0),
                text2 = btn == 0 ? "" : StringByLang(btn, 0)
            });
        }

        int start = dataList.Where(d => d.Code == "titlestart").First().Index;
        int end = dataList.Where(d => d.Code == "titleend").First().Index;
        for (int i = start; i < end + 1; i++)
        {
            list.Add(new CharacterMidUIText
            {
                index = dataList[i].Index,
                image = dataList[i].Image,
                text = StringByLang(i, 0)
            });
        }

        return list;
    }

    public List<ScrollAttributeData> InsertDataInCharacterMainStat()
    {
        ExcelDataFunc(0);
        List<ScrollAttributeData> list = new List<ScrollAttributeData>();

        int title = 0, upper = 0, lower = 0;
        for (int i = 0; i < Constants.STAT_MAX; i++)
        {
            if (i == 0)
            {
                title = dataList.Where(d => d.Code == "ctstr").First().Index;
                upper = dataList.Where(d => d.Code == "smin").First().Index;
                lower = dataList.Where(d => d.Code == "smax").First().Index;
            }
            else if (i == 1)
            {
                title = dataList.Where(d => d.Code == "ctdex").First().Index;
                upper = dataList.Where(d => d.Code == "satk").First().Index;
                lower = dataList.Where(d => d.Code == "scriatk").First().Index;
            }
            else if (i == 2)
            {
                title = dataList.Where(d => d.Code == "ctagi").First().Index;
                upper = dataList.Where(d => d.Code == "scrir").First().Index;
                lower = dataList.Where(d => d.Code == "sdod").First().Index;
            }
            else if (i == 3)
            {
                title = dataList.Where(d => d.Code == "ctluk").First().Index;
                upper = dataList.Where(d => d.Code == "slife").First().Index;
                lower = dataList.Where(d => d.Code == "sacc").First().Index;
            }

            list.Add(new ScrollAttributeData
            {
                itemID = i,
                statTitle = StringByLang(title, 0),
                statUpper = StringByLang(upper, 0),
                statLower = StringByLang(lower, 0)
            });
        }

        return list;
    }

    public List<ScrollAdvanceData> InsertDataInCharacterMainAdvance()
    {
        ExcelDataFunc(0);
        List<ScrollAdvanceData> list = new List<ScrollAdvanceData>();

        int idx = 0;
        for (int i = 0; i < Constants.ADV_MAX; i++)
        {
            if (i == 0)         idx = dataList.Where(d => d.Code == "smax").First().Index;
            else if (i == 1)    idx = dataList.Where(d => d.Code == "satkp").First().Index;
            else if (i == 2)    idx = dataList.Where(d => d.Code == "sacc").First().Index;
            else if (i == 3)    idx = dataList.Where(d => d.Code == "sdod").First().Index;
            else if (i == 4)    idx = dataList.Where(d => d.Code == "satkspdp").First().Index;
            else if (i == 5)    idx = dataList.Where(d => d.Code == "smove").First().Index;

            list.Add(new ScrollAdvanceData
            {
                itemID = i,
                advSprite = SpriteSheetManager.GetSpriteByName(UIimageName(idx)),
                advTitle = StringByLang(idx, 0)
            });
        }

        return list;
    }

    public string UIDataInCharacterMainAdvance()
    {
        ExcelDataFunc(0);
        int idx = dataList.Where(d => d.Code == "madv").First().Index;
        return StringByLang(idx, 0);
    }

    public List<ScrollAscentData> InsertDataInCharacterMainAscent()
    {
        ExcelDataFunc(0);
        List<ScrollAscentData> list = new List<ScrollAscentData>();

        int start = dataList.Where(d => d.Code == "titlestart").First().Index;
        int end = dataList.Where(d => d.Code == "titleend").First().Index;
        int str = dataList.Where(d => d.Code == "ctstr").First().Index;
        int spd = dataList.Where(d => d.Code == "satkspd").First().Index;
        int comp = dataList.Where(d => d.Code == "ctcomp").First().Index;
        int asc = dataList.Where(d => d.Code == "masc").First().Index;

        int num = 0;
        for (int i = start; i < end + 1; i++)
        {
            list.Add(new ScrollAscentData
            {
                itemID = num++,
                ascentSprite = SpriteSheetManager.GetSpriteByName(UIimageName(i)),
                ascentTitle = StringByLang(i, 0),
                ascentStr = StringByLang(str, 0),
                ascentSpd = StringByLang(spd, 0),
                btnCom = StringByLang(comp, 0),
                btnAsc = StringByLang(asc, 0),
            });
        }

        return list;
    }

    public List<ScrollAbilityData> InsertDataInCharacterMainAbility()
    {
        ExcelDataFunc(0);
        List<ScrollAbilityData> list = new List<ScrollAbilityData>();

        int start = dataList.Where(d => d.Code == "title_s").First().Index;
        int end = dataList.Where(d => d.Code == "title_e").First().Index;
        int gain = dataList.Where(d => d.Code == "cttot").First().Index;
        int myth = dataList.Where(d => d.Code == "ctmythic").First().Index;
        int qty = dataList.Where(d => d.Code == "uqty").First().Index;
        int btn = dataList.Where(d => d.Code == "mabi").First().Index;

        for (int i = start; i < end + 1; i++)
        {
            list.Add(new ScrollAbilityData
            {
                abilitySprite = SpriteSheetManager.GetSpriteByName(UIimageName(i)),
                abilityTitle = StringByLang(i, 0),
                abilityGain = StringByLang(gain, 0),
                abilityMyth = StringByLang(myth, 0),
                abilityQtyText = StringByLang(qty, 0),
                btnText = StringByLang(btn, 0),
            });
        }

        return list;
    }
}

public class UIDatas
{
    public string Info { get; set; }
    public int KeyCode { get; set; }
    public int Index { get; set; }
    public string Image { get; set; }
    public float Bonus { get; set; }

    public string Code { get; set; }
    public string Kor { get; set; }
    public string Kor1 { get; set; }
    public string Kor2 { get; set; }
    public string Eng { get; set; }
    public string Eng1 { get; set; }
    public string Eng2 { get; set; }

    public int Step { get; set; }
    public int Option { get; set; }
    public int Option1 { get; set; }
    public int Option2 { get; set; }

    public float DelayTime { get; set; }
}
