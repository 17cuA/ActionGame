using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

#if UNITY_EDITOR
[CustomEditor(typeof(UIFactory))]
public class UIFactoryInspector : Editor
{
	#region EDITOR_
#if UNITY_EDITOR
	public static GameObject canvas = null;
	private bool isNext = false;

	public override void OnInspectorGUI()
	{
		// 読み込むCanvasをセットする(ドラック＆ドロップ)
		canvas = EditorGUILayout.ObjectField("読み込むCanvasをセット→", canvas, typeof(GameObject), true) as GameObject;

		// Canvasをセット
		if (canvas != null)
		{
			// セットされたのがCanvasじゃない場合
			if (canvas != canvas.HasComponent<Canvas>())
			{
				EditorGUILayout.HelpBox("Canvasをドラックしてください。", MessageType.Error);
				isNext = false;
			}
			// Canvasをセット
			else
			{
				isNext = true;
			}
		}

		else
		{
			EditorGUILayout.HelpBox("Canvasをドラックしてください。", MessageType.Error);
		}

		if (GUILayout.Button("UI設定画面を開く") && isNext == true)
		{
			UISettingEditor.Open(canvas);
		}

		isNext = false;
	}
#endif
	#endregion
}
#endif

/// <summary>
/// GameObject型の拡張メソッドを管理するクラス
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
