using System.Collections.Generic;
using UnityEngine;

public class UIStringData   // ClosedXML 사용시 반드시 프로퍼티로 해야함
{
    public int index { get; set; }
    public string title_kor { get; set; }
    public string title_eng { get; set; }
    public string icon { get; set; }
}


public class ExcelLoader
{
    List<string> keys;
    Dictionary<string, List<UIStringData>> datas;
    int langSet;

    public void ExcelLoadFunc(int language)
    {
        langSet = language;
        ExcelConnector ec = new ExcelConnector();
        keys = ec.ImportKeyValuesInExcel(@"Assets\ExcelFiles\UI_Menu_Strings.xlsx", "keysheet");

        datas = ec.ImportUIStringInExcel<UIStringData>(keys, @"Assets\ExcelFiles\UI_Menu_Strings.xlsx", "sheet0");
    }


    List<UIStringData> dataList;

    public void ExcelDataFunc(string typeName)
    {
        dataList = datas[typeName];
    }
    public string UIiconName(int index)
    {
        return dataList[index].icon; 
    }

    public string titleByLang(int index)
    {
        switch (langSet)
        {
            case 0: return dataList[index].title_kor;
            case 1: return dataList[index].title_eng;
            default: return "";
        }
    }
}
