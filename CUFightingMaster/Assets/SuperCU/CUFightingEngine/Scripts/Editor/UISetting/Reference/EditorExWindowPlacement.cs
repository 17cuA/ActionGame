/*------------------------------------------------------------------------------------------------------------------------------------
 * BeginHorizontal～EndHorizontalとBeginVertical～EndVerticalは、
 * このメソッドで囲った範囲をまとめて横並び／縦並びにするメソッドです。
 * 縦にズラッと並んでわかりにくいのが軽減できそうですね。
 * 
 * GUILayoutにも同様のメソッドがありますが特に理由がない限りは,
 * EditorGUILayout版を使うようにしています（機能的に違いはなさそうですが）。
 * 
----------------------------------------------------チェックポイント！----------------------------------------------------
 * ・横並びにしたい場合はBeginHorizontal～EndHorizontalで囲う。
 * ・Horizontal内で縦並びにしたい場合はBeginVertical～EndVerticalで囲う。
 * ・基本は縦並び
 ------------------------------------------------------------------------------------------------------------------------------------*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EditorExWindowPlacement : ScriptableWizard
{
	public static EditorExWindowPlacement window;
	[MenuItem("見本/Placement")]
	static void Open()
	{
		if (window == null)
		{
			window = DisplayWizard<EditorExWindowPlacement>("Placement");
		}
	}

	void OnGUI()
	{
		// 基本的には縦並び
		EditorGUILayout.LabelField("ようこそ！　Unityエディタ拡張の沼へ！");

		// 見た目わかりやすくするため、Boxで囲う。
		// GUIStyleについては別途解説します。
		// 何も指定しなければ背景が変わりません。
		EditorGUILayout.BeginHorizontal(GUI.skin.box);
		{
			// ここの範囲は横並び

			// わかりやすくするために{}で囲ってます。
			// 実際は不要で、このスコープは並びに対して何も機能的効果を持っていません。

			EditorGUILayout.BeginVertical(GUI.skin.box);
			{
				// ここの範囲は縦並び

				EditorGUILayout.LabelField("左側");
				EditorGUILayout.LabelField("Labelとか", "いろいろ置いたり。");
				if (GUILayout.Button("Buttonとか"))
				{
					Debug.Log("Buttonとか押したよ");
				}
			}
			EditorGUILayout.EndVertical();

			EditorGUILayout.BeginVertical(GUI.skin.box);
			{
				// ここの範囲は縦並び

				EditorGUILayout.LabelField("右側");

				EditorGUILayout.BeginHorizontal(GUI.skin.box);
				{
					// ここの範囲は横並び

					EditorGUILayout.PrefixLabel("一列に並べる");

					if (GUILayout.Button("Button1"))
					{
						Debug.Log("Button1押したよ");
					}

					if (GUILayout.Button("Button2"))
					{
						Debug.Log("Button2押したよ");
					}

					if (GUILayout.Button("Button3"))
					{
						Debug.Log("Button3押したよ");
					}
				}
				EditorGUILayout.EndHorizontal();
			}
			EditorGUILayout.EndVertical();
		}
		EditorGUILayout.EndHorizontal();
	}
}
