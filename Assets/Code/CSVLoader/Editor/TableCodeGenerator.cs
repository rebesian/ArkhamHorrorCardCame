#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace CSVLoader.Editor
{
    public static class TableCodeGenerator
    {

        public static List<string> Generate(string csvPath , string outputPath = null)
        {
            var generatedFiles = new List<string>();
            return generatedFiles;
        }


    }
}