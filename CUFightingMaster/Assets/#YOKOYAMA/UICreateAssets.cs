using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEditorInternal;
using System.IO;
using System.Linq;

namespace SuperCU.FightingEngine
{
#if UNITY_EDITOR
	public class UICreateAssets : EditorWindow
	{
		public static UICreateAssets window; //ウィンドウ
		string sceneName = "";

		[MenuItem("スーパーCU格ゲーエン人17号/シーン作成", false,0)]
		static void OpenTitel()
		{
			// エディターを生成
			window = EditorWindow.GetWindow<UICreateAssets>("Scene作成");
		}

		private void OnGUI()
		{
			sceneName = EditorGUILayout.TextField("Scene名", sceneName);
			EditorGUILayout.LabelField("作成するもの");
			EditorGUILayout.LabelField("1：シーン");
			EditorGUILayout.LabelField("2：シーンに配置するUIを保存するアセット");
			EditorGUILayout.LabelField("※既に同じ名前がある場合は作成を中断します");
			EditorGUILayout.LabelField("※ボタンを押した後、Sceneは手動で");
			EditorGUILayout.LabelField(" 「保存先」「フォルダ名」入力してください");

			// ボタンを押して作成
			if (GUILayout.Button("シーン作成", GUILayout.Width(100)))
			{
				// 名前入力をチェック
				if(sceneName != null)
				{
					// シーンを作る
					// ※シーンを作成はするが、保存はされていない
					SetupScene();
					// シーンのUIを保存するScriptableを作成
					SetupMedia();
					window.Close();
				}
			}
		}

		/// <summary>
		/// パスがディレクトリ内にあるか確認する
		/// </summary>
		/// <param name="_path"></param>
		/// <returns></returns>
		private bool CheckAvailability(string _path)
		{
			return Directory.Exists(_path);
		}

		/// <summary>
		/// フォルダーを探す
		/// </summary>
		/// <param name="_path">アセットが格納されているフォルダーパス</param>
		/// <returns></returns>
		private bool SearchFiles(string _path)
		{
			string[] vs = System.IO.Directory.GetFiles(_path);
			return vs.Any();
		}

		/// <summary>
		/// さらに絞り込んだパスの検索をする
		/// </summary>
		/// <param name="_path">アセットが格納されているフォルダーパス</param>
		/// <param name="_searchInFolders">さらに絞り込んだパス(オブジェクトの拡張子まで含む)</param>
		/// <returns></returns>
		private bool SearchFiles(string _path, string _searchInFolders)
		{
			string[] vs = System.IO.Directory.GetFiles(_path, _searchInFolders);
			return vs.Any();
		}

		/// <summary>
		/// Sceneの作成
		/// </summary>
		private void CreateScene()
		{
			// シーンを作成
			Scene temp = EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects, NewSceneMode.Additive);
			// ※hierarchy上には生成されるが保存はされていない
			// 保存はEditorSceneManager.SaveSceneで手動で行う
			temp.name = sceneName;
			EditorSceneManager.SaveScene(temp);

			AssetDatabase.Refresh();
		}

		/// <summary>
		/// ScriptableObjectAssetの作成
		/// </summary>
		/// <returns></returns>
		private UIProperty CreateMedia()
		{
			return UIProperty.Create("Assets/SceneUIDate/" + sceneName + ".asset");
		}

		/// <summary>
		/// 保存するフォルダを作成
		/// Sceneの生成
		/// </summary>
		void SetupScene()
		{
			//保存先のファルダーがあるか確認
			if (CheckAvailability("Assets/Scenes") == false)
			{
				// folderを生成
				AssetDatabase.CreateFolder("Assets", "Scenes");//フォルダ作成
			}
			// 新規シーン
			if (SearchFiles("Assets/Scenes", sceneName + ".unity") == false)
			{
				CreateScene();
			}
			else
			{
				Debug.Log("同じ名前のシーンがあるため、新しいシーンは作れません。");
			}
		}

		/// <summary>
		/// 保存するフォルダを作成
		/// UIを保存するScriptableObjectの生成
		/// </summary>
		void SetupMedia()
		{
			if (CheckAvailability("Assets/SceneUIDate/") == false)
			{
				// folderを生成
				AssetDatabase.CreateFolder("Assets", "SceneUIDate");        //フォルダ作成
			}
			if (SearchFiles("Assets/SceneUIDate", sceneName + ".asset") == false)
			{
				CreateMedia();
			}
			else
			{
				Debug.Log("同じ名前のUIDataがあるため、新しいUIDataは作れません。");
			}
		}
	}
}
#endif