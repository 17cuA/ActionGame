using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
#if UNITY_EDITOR
using UnityEditor;
#endif

#if UNITY_EDITOR
[CustomEditor(typeof(UIFactory))]
public class UIFactoryInspector : Editor
{
	#region EDITOR_
	public static GameObject canvas = null;
	private bool isNext = false;

	public override void OnInspectorGUI()
	{
		// 読み込むCanvas
		canvas = EditorGUILayout.ObjectField("読み込むCanvasをセット→", canvas, typeof(GameObject), true) as GameObject;

		if (canvas == null)
		{
			EditorGUILayout.HelpBox("Canvasをドラックしてください。", MessageType.Error);
		}
		else
		{
			if (canvas != canvas.HasComponent<Canvas>())
			{
				EditorGUILayout.HelpBox("Canvasをドラックしてください。", MessageType.Error);
				isNext = false;
			}
			else
			{
				isNext = true;
			}
		}

		//else if (sceneAsset != null && scriptableDate != null)
		//{
		//	if (sceneAsset.name == scriptableDate.name)
		//	{
		//		isNext = true;
		//	}
		//	else
		//	{
		//		EditorGUILayout.HelpBox("シーンとUISceneDateの名前が違います。同じ名前のアセットで実行してください。", MessageType.Error);
		//	}
		//}
		if (GUILayout.Button("UI設定画面を開く") && isNext == true)
		{
			//UISettingEditor.Open(sceneAsset, scriptableDate);

			UISettingEditor.Open(canvas);
		}

		isNext = false;
	}
	#endregion
}
#endif

/// <summary>
/// GameObject 型の拡張メソッドを管理するクラス
/// </summary>
public static class GameObjectExtensions
{
	/// <summary>
	/// 指定されたコンポーネントがアタッチされているかどうかを返します
	/// </summary>
	public static GameObject HasComponent<T>(this GameObject self) where T : Component
	{
		if (self.GetComponent<T>() != null)
		{
			return self;
		}
		else
		{
			return null;
		}
	}
}