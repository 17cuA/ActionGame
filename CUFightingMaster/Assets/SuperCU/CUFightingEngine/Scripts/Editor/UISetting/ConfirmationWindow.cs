using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ConfirmationWindow : EditorWindow
{
	public bool isOverwriting { get; private set; }
	public bool isContinue { get; private set; }
	public static ConfirmationWindow Open()
	{
		// エディターを生成
		var window = EditorWindow.GetWindow<ConfirmationWindow>("-変更がありました-");
		return window;
	}

	private void OnGUI()
	{
		EditorGUILayout.LabelField("以前保存したUIの情報から変更があります。");
		EditorGUILayout.LabelField("変更しますか？");
		if (GUILayout.Button("現在のものに上書きする"))
		{
			isOverwriting = true;
		}
		if (GUILayout.Button("保存したデータを読み込む"))
		{
			isContinue = true;
		}
	}
}
