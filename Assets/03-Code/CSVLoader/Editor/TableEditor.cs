#if UNITY_EDITOR

using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.PackageManager.UI;
using UnityEngine;

namespace CSVLoader.Editor
{
    public class TableEditor : EditorWindow
    {
        public static EditorWindow window;

        private Object csvFilePath;

        [MenuItem("Window/Tools/TableEditor")]
        public static void ShowWindow()
        {
            window = GetWindow<TableEditor>("csv추출기");
        }

        public void OnEnable()
        {
            TableCodeGenerator.OnProgress += progressOnText;
        }

        private void OnDisable()
        {
            // 구독 해제: 창 닫히면 연결 끊기
            TableCodeGenerator.OnProgress -= progressOnText;
        }

        private void OnGUI()
        {
            csvFilePath = EditorGUILayout.ObjectField("CSV 파일", csvFilePath, typeof(TextAsset), false);

            if (GUILayout.Button("생성"))
            {
               
                TableCodeGenerator.GenerateTableCode(AssetDatabase.GetAssetPath(csvFilePath));
            }

            EditorGUILayout.HelpBox("일단 csv파일을 .cs로더파일로 전환하는 툴입니다. 바이너리화는 나중이에요\nCSV파일 구간에 마우스 드래그해서 csv파일을 땡겨넣으세요,\n그 다음 생성 버튼을 누르시면됩니다.\n일단 csv파일만 넣게 되었으니 꼭 csv파일이름을 테이블이름으로만 해주세요", MessageType.Info);
        }

        public void progressOnText(string progressText)
        {
            Debug.Log(progressText);
        }
    }
}
#endif