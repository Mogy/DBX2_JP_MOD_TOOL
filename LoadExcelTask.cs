using ExcelDataReader;
using System;
using System.Data;
using System.IO;
using System.Threading.Tasks;

namespace DBX2_JP_MOD_TOOL
{
    public static class LoadExcelTask
    {
        const string JA_COL_NAME = "日本語";
        const string JA_MSG = @"bin\jaMsg.txt";
        public static async Task analyzeExcelData(DataSet ds) {
            await Task.Run(() =>
            {
                // ファイルを削除
                if (File.Exists(JA_MSG))
                {
                    File.Delete(JA_MSG);
                }

                using (var sw = new StreamWriter(JA_MSG, true))
                {
                    foreach (DataTable table in ds.Tables)
                    {
                        // 翻訳データ以外は無視
                        if (!table.TableName.StartsWith("翻訳データ")) continue;
                        // カラムチェック
                        if (table.Columns[JA_COL_NAME] == null) continue;

                        var rowCount = table.Rows.Count;
                        for (var row = 0; row < rowCount; row++)
                        {
                            var line = table.Rows[row][JA_COL_NAME].ToString();
                            sw.WriteLine(line);
                        }
                    }
                }
            });
        }
        public static async Task<DataSet> readExcelData(string path)
        {
            return await Task.Run(() =>
            {
                try
                {
                    using (var fs = File.Open(path, FileMode.Open))
                    using (var reader = ExcelReaderFactory.CreateOpenXmlReader(fs))
                    {
                        var config = new ExcelDataSetConfiguration
                        {
                            ConfigureDataTable = _ => new ExcelDataTableConfiguration
                            {
                                UseHeaderRow = true
                            }
                        };
                        return reader.AsDataSet(config);
                    }
                }
                catch
                {
                    return null;
                }
            });
        }
    }
}
