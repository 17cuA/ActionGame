using UnityEngine;
using UnityEditor;
using System.IO;

public class UI_Editor : EditorWindow
{
	[MenuItem("Editor/UI")]
	private static void Create()
	{
		// 生成
		GetWindow<UI_Editor>("サンプル");
	}
	
	private ScriptableObject_UI _sample;
	private const string ASSET_PATH = "Assets/#HONAGA/Scripts/ScriptableObject_UI.asset";

	private void OnGUI()
	{
		if (_sample == null)
		{
			_sample = CreateInstance<ScriptableObject_UI>();
		}

		using (new GUILayout.VerticalScope())
		{
			using (new GUILayout.HorizontalScope())
			{
				_sample.ImageSizeWidth = EditorGUILayout.FloatField("横", _sample.ImageSizeWidth);
				_sample.ImageSizeHeight = EditorGUILayout.FloatField("縦", _sample.ImageSizeHeight);
			}	
			//_sample.Us = EditorGUILayout.
		}
		using (new GUILayout.HorizontalScope())
		{
			// 書き込みボタン
			if (GUILayout.Button("書き込み"))
			{
				Export();
			}
		}
	}

	private void Export()
	{
		// 新規の場合は作成
		if (!AssetDatabase.Contains(_sample as UnityEngine.Object))
		{
			string directory = Path.GetDirectoryName(ASSET_PATH);
			if (!Directory.Exists(directory))
			{
				Directory.CreateDirectory(directory);
			}
			// アセット作成
			AssetDatabase.CreateAsset(_sample, ASSET_PATH);
		}
		// インスペクターから設定できないようにする
		_sample.hideFlags = HideFlags.NotEditable;
		// 更新通知
		EditorUtility.SetDirty(_sample);
		// 保存
		AssetDatabase.SaveAssets();
		// エディタを最新の状態にする
		AssetDatabase.Refresh();
	}
}