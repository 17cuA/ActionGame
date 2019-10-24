using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEditor;
using System.IO;

public class UI_Editor : EditorWindow
{
    [MenuItem("Editor/UI")]
    private static void Create()
    {
        // 生成
        GetWindow<UI_Editor>("画像配置の設定");
    }
    private List<bool> isOpen = new List<bool>();
    private ScriptableObject_UI _obj, _saveTempObj;

    private const string ASSET_PATH = "Assets/#HONAGA/Scripts/ScriptableObject_UI.asset";

    private void OnGUI()
    {
        if (_obj == null)
        {
            _obj = CreateInstance<ScriptableObject_UI>();
        }
        if (_saveTempObj == null)
        {
            _saveTempObj = CreateInstance<ScriptableObject_UI>();
        }
        if (_saveTempObj.Ui.Count > isOpen.Count)
        {
            while(_saveTempObj.Ui.Count - isOpen.Count > 0)
            {
                isOpen.Add(false);
            }
        }
        if (GUILayout.Button("イメージ作成", GUILayout.Width(100), GUILayout.Height(30)))
        {
            _saveTempObj.Ui.Add(new UIImageClass());
            isOpen.Add(false);
        }
        // 要素数の数だけ追加
        using (new GUILayout.VerticalScope())
        {
            for (int i = 0; i < _saveTempObj.Ui.Count; i++)
            {
                isOpen[i] = EditorGUILayout.Foldout(isOpen[i], _saveTempObj.Ui[i].name);
				bool removeFrag = false;
				if (isOpen[i])
                {
					if (GUILayout.Button("要素削除", GUILayout.Width(80)))
					{
						removeFrag = true;
					}
					_saveTempObj.Ui[i].name = EditorGUILayout.TextField("名前", _saveTempObj.Ui[i].name);
                    _saveTempObj.Ui[i].imageSetInterval = EditorGUILayout.FloatField("配置する間隔", _saveTempObj.Ui[i].imageSetInterval);
                    _saveTempObj.Ui[i].ImageSizeHeight = EditorGUILayout.FloatField("縦幅", _saveTempObj.Ui[i].ImageSizeHeight);
                    _saveTempObj.Ui[i].ImageSizeWidth = EditorGUILayout.FloatField("横幅", _saveTempObj.Ui[i].ImageSizeWidth);
                    _saveTempObj.Ui[i].image = EditorGUILayout.ObjectField("画像", _saveTempObj.Ui[i].image, typeof(Texture2D), false) as Texture2D;
                }
				if (removeFrag)
				{
					ScriptableObject_UI temp = AssetDatabase.LoadAssetAtPath<ScriptableObject_UI>(ASSET_PATH);
					temp.Ui.RemoveAt(i);
					_saveTempObj.Ui.RemoveAt(i);
				}
				 
			}
		}
        using (new GUILayout.HorizontalScope())
        {
            if (GUILayout.Button("読み込み"))
            {
                Import();
            }
            // 書き込みボタン
            if (GUILayout.Button("書き込み"))
            {
                Export();
            }
        }
    }

    private void Import()
    {
        if (_obj == null)
        {
            _obj = CreateInstance<ScriptableObject_UI>();
        }
        if (_saveTempObj == null)
        {
            _saveTempObj = CreateInstance<ScriptableObject_UI>();
        }
        ScriptableObject_UI temp = AssetDatabase.LoadAssetAtPath<ScriptableObject_UI>(ASSET_PATH);
        //ファイルが存在しない場合作成する
        if (temp == null)
        {
            temp = CreateInstance<ScriptableObject_UI>();
            AssetDatabase.CreateAsset(temp, ASSET_PATH);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            return;
        }
        if (temp != null)
        {
            // 要素数が少なかったら増やす
            if (_saveTempObj.Ui.Count < temp.Ui.Count)
            {
                while(temp.Ui.Count - _saveTempObj.Ui.Count > 0)
                {
                    _saveTempObj.Ui.Add(new UIImageClass());
                }
            }
            for (int i = 0; i < _saveTempObj.Ui.Count; i++)
            {
                _saveTempObj.Ui[i].Copy(temp.Ui[i]);
            }
        }
    }

    private void Export()
    {
        if(_saveTempObj.Ui.Count > _obj.Ui.Count)
        {
            while(_saveTempObj.Ui.Count - _obj.Ui.Count > 0)
            {
                _obj.Ui.Add(new UIImageClass());
            }
        }
        for (int i = 0; i < _saveTempObj.Ui.Count; i++)
        {
            _obj.Ui[i].Copy(_saveTempObj.Ui[i]);
        }
        ScriptableObject_UI temp = AssetDatabase.LoadAssetAtPath<ScriptableObject_UI>(ASSET_PATH);

        if (temp == null)
        {
            temp = CreateInstance<ScriptableObject_UI>();
        }

        // 新規の場合は作成
        if (!AssetDatabase.Contains(_obj as UnityEngine.Object))
        {
            string directory = Path.GetDirectoryName(ASSET_PATH);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            // アセット作成
            AssetDatabase.CreateAsset(_obj, ASSET_PATH);
        }

        if (_obj.Ui.Count > temp.Ui.Count)
        {
            while( _obj.Ui.Count - temp.Ui.Count > 0)
            {
                temp.Ui.Add(new UIImageClass());
            }
        }
        for (int i = 0; i < _obj.Ui.Count; i++)
        {
            temp.Ui[i].Copy(_obj.Ui[i]);
        }

		// インスペクターから設定できないようにする
		_obj.hideFlags = HideFlags.NotEditable;
		// 更新通知
		EditorUtility.SetDirty(_obj);
        // 保存
        AssetDatabase.SaveAssets();
        // エディタを最新の状態にする
        AssetDatabase.Refresh();
    }
}