using UnityEngine;
using UnityEditor;
using System.Text.RegularExpressions;

[InitializeOnLoad]
public class LRHighlight
{
    private static bool isBackgroundColorEnabled = true; // 背景色の変更を制御するためのブール変数

    static LRHighlight()
    {
        EditorApplication.hierarchyWindowItemOnGUI += OnHierarchyWindowItemOnGUI;
    }

    // コンテキストメニューに項目を追加し、背景色の変更をトグルする
    [MenuItem("GameObject/LRHighlight 切り替え", false, 49)]
    private static void ToggleBackgroundColor()
    {
        isBackgroundColorEnabled = !isBackgroundColorEnabled;
    }

    private static void OnHierarchyWindowItemOnGUI(int instanceID, Rect selectionRect)
    {
        if (!isBackgroundColorEnabled) return; // 背景色の変更が無効ならば何もしない

        GameObject obj = EditorUtility.InstanceIDToObject(instanceID) as GameObject;
        if (obj != null)
        {
            Color? backgroundColor = null;

            // "Left"を含むオブジェクトの背景色を青に設定
            if (Regex.IsMatch(obj.name, "(_L|\\.L)($|_[0-9]*|\\.[0-9]*|[0-9]+|\\()") || obj.name.Contains("Left"))
            {
                backgroundColor = new Color(0.0f, 0.0f, 1.0f, 0.1f); // 青色に変更
            }
            // "Right"を含むオブジェクトの背景色を赤に設定
            else if (Regex.IsMatch(obj.name, "(_R|\\.R)($|_[0-9]*|\\.[0-9]*|[0-9]+|\\()") || obj.name.Contains("Right"))
            {
                backgroundColor = new Color(1.0f, 0.0f, 0.0f, 0.1f); // 赤色に変更
            }

            if (backgroundColor.HasValue)
            {
                EditorGUI.DrawRect(selectionRect, backgroundColor.Value);
            }
        }
    }
}
