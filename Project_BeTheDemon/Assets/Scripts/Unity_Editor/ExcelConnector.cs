#if UNITY_EDITOR
using System;
using System.Linq;
using System.Collections.Generic;
using ClosedXML.Excel;


public class ExcelConnector
{
    public List<string> GetSheetNameInExcel(string excelFilePath)
    {
        List<string> sheetName = new List<string>();

        using (XLWorkbook workbook = new XLWorkbook(excelFilePath))
        {
            foreach (IXLWorksheet worksheet in workbook.Worksheets)
                sheetName.Add(worksheet.Name);
        }
        return sheetName;
    }

    public Dictionary<string, List<T>> ImportExcelBySheet<T>(string excelFilePath, List<string> sheets)
    {
        Dictionary<string, List<T>> datas = new Dictionary<string, List<T>>();

        Type typeOfObject = typeof(T);

        using (XLWorkbook workbook = new XLWorkbook(excelFilePath))
        {
            for (int i = 0; i < sheets.Count; i++)
            {
                datas.Add(sheets[i], new List<T>());

                var worksheet = workbook.Worksheets.Where(w => w.Name == sheets[i]).First();
                var properties = typeOfObject.GetProperties();
                // header column texts
                var columns = worksheet.FirstRow().Cells().Select((v, i) => new { Value = v.Value, Index = i + 1 });// 1 부터 시작

                foreach (IXLRow row in worksheet.RowsUsed().Skip(1))    // 필드명이 있는 첫행은 스킵
                {
                    T obj = (T)Activator.CreateInstance(typeOfObject);

                    foreach (var prop in properties)
                    {
                        var res = columns.SingleOrDefault(c => c.Value.ToString() == prop.Name.ToString());
                        if (res == null) continue;

                        var val = row.Cell(res.Index).Value;
                        var type = prop.PropertyType;
                        Console.WriteLine(type.Name);
                        prop.SetValue(obj, Convert.ChangeType(val, type));
                    }

                    datas[worksheet.Name].Add(obj);
                }
            }
        }
        return datas;
    }

    public List<string> ImportKeyValuesInExcel(string excelFilePath, string sheetName)
    {
        List<string> keyValues = new List<string>();

        using (XLWorkbook workbook = new XLWorkbook(excelFilePath))
        {
            var worksheet = workbook.Worksheets.Where(w => w.Name == sheetName).First();
            var columns = worksheet.FirstRow().Cells().Select((v, i) => new { Value = v.Value, Index = i + 1 });// 1부터 시작

            foreach (IXLRow row in worksheet.RowsUsed().Skip(1)) // 필드명이 있는 첫행은 스킵
            {
                var val = row.Cell(1).Value;

                if (keyValues.Contains((string)val) == false)
                    keyValues.Add((string)val);
            }
        }

        return keyValues;
    }

    public Dictionary<string, List<T>> ImportUIStringInExcel<T>(List<string> keys, string excelFilePath, string sheetName)
    {
        Dictionary<string, List<T>> datas = new Dictionary<string, List<T>>();

        foreach (var k in keys)
            datas.Add(k, new List<T>());

        Type typeOfObject = typeof(T);

        using (XLWorkbook workbook = new XLWorkbook(excelFilePath))
        {
            var worksheet = workbook.Worksheets.Where(w => w.Name == sheetName).First();
            var properties = typeOfObject.GetProperties();
            //header column texts
            var columns = worksheet.FirstRow().Cells().Select((v, i) => new { Value = v.Value, Index = i + 1 });// 1부터 시작

            List<T> data = new List<T>();

            foreach (IXLRow row in worksheet.RowsUsed().Skip(1)) // 필드명이 있는 첫행은 스킵
            {
                T obj = (T)Activator.CreateInstance(typeOfObject);

                foreach (var prop in properties)
                {
                    int colIndex = columns.SingleOrDefault(c => c.Value.ToString() == prop.Name.ToString()).Index;
                    var val = row.Cell(colIndex).Value;
                    var type = prop.PropertyType;
                    prop.SetValue(obj, Convert.ChangeType(val, type));
                }

                string key = row.Cell(1).Value.ToString();
                datas[key].Add(obj);
            }
        }

        return datas;
    }
}
#endif
