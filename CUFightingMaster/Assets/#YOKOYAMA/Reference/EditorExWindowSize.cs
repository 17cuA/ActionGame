/*------------------------------------------------------------------------------------------------------------------------------------
 * GUILayoutOptionでサイズ指定
 * GUILayout、EditorGUILayout関係でほとんどのメソッドがparams GUILayoutOption[] optionsという引数を持っており、
 * こちらにサイズなどを指定することでいろいろ調整できるようになっています。
 * 指定方法は、GUILayoutにあるWidthなどを使用します。
 * 
 * 今回はわかりやすくするためにButtonに適用してみましたが、GUILayoutOptionを受け付けるメソッドであれば何でも適用できます。
 * たとえば前回のVerticalやHorizontalなども固定サイズにしたほうが見栄え良くなることもありますしね。
 * もちろん、Widthだけ指定したり、MinWidth/MaxWidthとExpandHeightを組み合わせてみたりと組み合わせも自由にできます。
 * 
 * ----------------------------------------------------チェックポイント！----------------------------------------------------
 * ・サイズ指定はWidth/Height
 * ・ゆるく範囲指定したい場合はMinWidth/MaxWidth/MinHeight/MaxHeight
 * ・全体に広げたい場合はExpandWidth/ExpandHeight
 * ・これらを使い分けて綺麗なレイアウトを組んでいく
 ------------------------------------------------------------------------------------------------------------------------------------*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class EditorExWindowSize : ScriptableWizard
{
	public static EditorExWindowSize window;
	[MenuItem("見本/Size")]
	static void Open()
	{
		if (window == null)
		{
			window = DisplayWizard<EditorExWindowSize>("Size");
		}
	}

	Vector2 buttonSize = new Vector2(100, 20);

	Vector2 buttonMinSize = new Vector2(100, 20);
	Vector2 buttonMaxSize = new Vector2(1000, 200);

	bool expandWidth = true;
	bool expandHeight = true;

	void OnGUI()
	{
		EditorGUILayout.LabelField("ようこそ！　Unityエディタ拡張の沼へ！");

		// 直接サイズを指定する場合は、
		// GUILayout.Width/Heightを使う。
		buttonSize = EditorGUILayout.Vector2Field("ButtonSize", buttonSize);

		if (GUILayout.Button("サイズ指定ボタン", GUILayout.Width(buttonSize.x), GUILayout.Height(buttonSize.y)))
		{
			Debug.Log("サイズ指定ボタン");
		}

		// 自動的にサイズ変更される範囲を指定する場合は
		// GUILayout.MinWidth/MaxWidth/MinHeight/MaxHeightを使う。
		buttonMinSize = EditorGUILayout.Vector2Field("ButtonMinSize", buttonMinSize);
		buttonMaxSize = EditorGUILayout.Vector2Field("ButtonMaxSize", buttonMaxSize);


		if (GUILayout.Button("最小最大指定ボタン",
							  GUILayout.MinWidth(buttonMinSize.x), GUILayout.MinHeight(buttonMinSize.y),
							  GUILayout.MaxWidth(buttonMaxSize.x), GUILayout.MaxHeight(buttonMaxSize.y)))
		{
			Debug.Log("最小最大指定ボタン");
		}

		// 有効範囲内全体に広げるかどうかは
		// GUILayout.ExpandWidth/ExpandHeightで指定する。
		expandWidth = EditorGUILayout.Toggle("ExpandWidth", expandWidth);
		expandHeight = EditorGUILayout.Toggle("ExpandHeight", expandHeight);
		if (GUILayout.Button("Expandボタン", GUILayout.ExpandWidth(expandWidth), GUILayout.ExpandHeight(expandHeight)))
		{
			Debug.Log("Expandボタン");
		}
	}
}
