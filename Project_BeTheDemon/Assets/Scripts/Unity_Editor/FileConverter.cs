#if UNITY_EDITOR
using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;


public class FileConverter
{
    [MenuItem("MyMenu/StringInGame => DataFile")]
    static void SerializeExcel_StringInGame()
    {
        ExcelConnector ec = new ExcelConnector();
        List<string> sheets = ec.GetSheetNameInExcel(@"Assets\ExcelFiles\StringInGame.xlsx"); ;
        Dictionary<string, List<UIDatas>> datas = ec.ImportExcelBySheet<UIDatas>(@"Assets\ExcelFiles\StringInGame.xlsx", sheets);

        string textSheets = JsonConvert.SerializeObject(sheets);
        byte[] bytesSheets = Encoding.UTF8.GetBytes(textSheets);
        string codeSheets = Convert.ToBase64String(bytesSheets);
        File.WriteAllText($"Assets/Resources/Data000.txt", codeSheets);

        string textDatas = JsonConvert.SerializeObject(datas);
        byte[] bytesDatas = Encoding.UTF8.GetBytes(textDatas);
        string codeDatas = Convert.ToBase64String(bytesDatas);
        File.WriteAllText($"Assets/Resources/Data001.txt", codeDatas);

        Debug.Log("StringInGame Data file Created!!");
    }
#if UI_Menu_Strings
    [MenuItem("MyMenu/UI_Menu_Strings => DataFile")]
    static void SerializeExcel_UIMenuStrings()
    {
        ExcelConnector ec = new ExcelConnector();
        List<string> keys = ec.ImportKeyValuesInExcel(@"Assets\ExcelFiles\UI_Menu_Strings.xlsx", "keysheet");
        Dictionary<string, List<UIStringData>> datas = ec.ImportUIStringInExcel<UIStringData>(keys, @"Assets\ExcelFiles\UI_Menu_Strings.xlsx", "sheet0");

        string textKeys = JsonConvert.SerializeObject(keys);
        byte[] bytesKeys = Encoding.UTF8.GetBytes(textKeys);
        string codeKeys = Convert.ToBase64String(bytesKeys);
        File.WriteAllText($"Assets/Resources/Data002.txt", codeKeys);

        string textDatas = JsonConvert.SerializeObject(datas);
        byte[] bytesDatas = Encoding.UTF8.GetBytes(textDatas);
        string codeDatas = Convert.ToBase64String(bytesDatas);
        File.WriteAllText($"Assets/Resources/Data003.txt", codeDatas);

        Debug.Log("UI_Menu_Strings Data file Created!!");
    }
#endif
}
#endif

