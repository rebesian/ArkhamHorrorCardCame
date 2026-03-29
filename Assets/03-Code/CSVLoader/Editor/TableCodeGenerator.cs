#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEditor.AdaptivePerformance.Editor;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.UIElements;

namespace CSVLoader.Editor
{
    public static class TableCodeGenerator
    {

        public static event Action<string> OnProgress;
 
        public static void GenerateTableCode(string csvPath , string outputPath = null)
        {
            StringBuilder txt = new StringBuilder();
            string tableName = Path.GetFileNameWithoutExtension(csvPath);

            txt.AppendLine("using MessagePack;");
            txt.AppendLine("using System;\n");

            txt.AppendLine("namespace Data");
            txt.AppendLine("{");
            txt.AppendLine("    [MessagePackObject]");
            txt.AppendLine($"    public class {tableName}");
            txt.AppendLine("    {\n");
            OnProgress.Invoke($"{tableName} ");

            if (File.Exists($"Assets/06-Data/{tableName}Data.cs"))
            {
                File.Delete($"Assets/06-Data/{tableName}Data.cs");
            }

            string currentCsv = File.ReadAllText(csvPath);
            string[] line = currentCsv.Replace("\r", "").Split(('\n'), StringSplitOptions.RemoveEmptyEntries);

            string[] colName = line[0].Split(',', StringSplitOptions.RemoveEmptyEntries);
            string[] colType = line[1].Split(',', StringSplitOptions.RemoveEmptyEntries);
            int MaxColum = line[0].Split(',', StringSplitOptions.RemoveEmptyEntries).Length;
            for (int i = 0; i < MaxColum; ++i)
            {
                txt.AppendLine($"       [Key({i})]");
                txt.Append($"       public {colType[i]} {colName[i]}");
                txt.AppendLine("    { get; set; }\n");
            }

            txt.AppendLine("    }");
            txt.AppendLine("}");
            File.WriteAllText($"Assets/06-Data/{tableName}Data.cs", txt.ToString());

            OnProgress.Invoke($"{tableName}Data 완료");
        }
    }
}
#endif