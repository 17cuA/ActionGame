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

#if UNITY_EDITOR
public class UICreateAssets
{
	///// <summary>
	///// Canvasの中にあるImageオブジェクトを取得する
	///// </summary>
	//public void Preparation(GameObject _canvas)
	//{
	//	List<Transform> canvasChilds = SubTransform.ChildClass(_canvas.transform);
	//	SetEditorLoadHierarchy(canvasChilds);
	//}

	///// <summary>
	///// Imageオブジェクトをシーン上に生成する
	///// Canvasの子供に生成
	///// </summary>
	///// <param name="_name">画像の名前をImageオブジェクトに引き継ぐ</param>
	///// <returns></returns>
	//Image CreateImage(string _name,ref GameObject _canvas)
	//{
	//	// hierarchyWindowに生成する
	//	var canvasObject = new GameObject(_name);        // (”オブジェクトの名前”)
	//													 // Imageオブジェクトに変換(正しくはImageの特性を加える)
	//	var tempImage = canvasObject.AddComponent<Image>();
	//	// Carnvasの子供に設定
	//	tempImage.transform.SetParent(_canvas.transform);
	//	// 画面中央に生成する
	//	tempImage.GetComponent<RectTransform>().localPosition = Vector3.zero;

	//	return tempImage;
	//}

	///// <summary>
	///// Hierarchy上に存在しているImageオブジェクトの情報を取得し、
	///// エディターに反映させる
	///// </summary>
	///// <param name="_canvasChilds">Canvas内に存在しているImageオブジェクト配列(List)</param>
	//void SetEditorLoadHierarchy(List<Transform> _canvasChilds)
	//{
	//	if (_canvasChilds != null)
	//	{
	//		// typeで指定した型の全てのオブジェクトを配列で取得し,その要素数分繰り返す.
	//		foreach (Transform obj in _canvasChilds)
	//		{
	//			var tempImage = obj.GetComponent<Image>();
	//			// エディターに反映
	//			AddEditorAssets(tempImage);
	//		}
	//	}
	//}

	///// <summary>
	///// 新しく作るUIを生成する
	///// </summary>
	///// <param name="_sprite">UI作成に使用する画像データ</param>
	//void CreateNewAsset(Sprite _sprite)
	//{
	//	Image tempImage = CreateImage(_sprite.name);
	//	tempImage.sprite = _sprite;                                 // 画像を適用

	//	// 画像のXスケールを1とし、(X,Y)の比率をImageオブジェクトに適応させる
	//	Vector2 tempScale = _sprite.bounds.size;
	//	tempScale.y /= tempScale.x;         // Yスケールの比率を計算
	//	tempScale.x /= tempScale.x;         // Xスケールの比率を1にする
	//	tempImage.GetComponent<RectTransform>().localScale = tempScale;

	//	// エディターに反映
	//	AddEditorAssets(tempImage);

	//	// セットされた画像を空にし、再利用できるようにする
	//	tempSprite = null;
	//}

	///// <summary>
	///// 生成したImageをエディター用変数に格納する
	///// </summary>
	///// <param name="_image">新しく用意されたImageオブジェクト</param>
	//void AddEditorAssets(Image _image)
	//{
	//	RectTransform tempRectTransform = _image.GetComponent<RectTransform>();

	//	// UIの情報を構造体に格納
	//	UIAsset temp = new UIAsset
	//	{
	//		Image = _image,
	//		RectTransform = tempRectTransform,
	//		ScaleDef = tempRectTransform.localScale
	//	};

	//	// 構造体をリストに追加
	//	editorImage.Add(temp);                                                // Image
	//	assets.Add(temp);
	//}

	#region
	//public static UICreateAssets window; //ウィンドウ
	//string sceneName = "";

	//[MenuItem("スーパーCU格ゲーエン人17号/シーン作成", false,0)]
	//static void OpenTitel()
	//{
	//	// エディターを生成
	//	window = EditorWindow.GetWindow<UICreateAssets>("Scene作成");
	//}

	//private void OnGUI()
	//{
	//	sceneName = EditorGUILayout.TextField("Scene名", sceneName);
	//	EditorGUILayout.LabelField("作成するもの");
	//	EditorGUILayout.LabelField("1：シーン");
	//	EditorGUILayout.LabelField("2：シーンに配置するUIを保存するアセット");
	//	EditorGUILayout.LabelField("※既に同じ名前がある場合は作成を中断します");
	//	EditorGUILayout.LabelField("※ボタンを押した後、Sceneは手動で");
	//	EditorGUILayout.LabelField(" 「保存先」「フォルダ名」入力してください");

	//	// ボタンを押して作成
	//	if (GUILayout.Button("シーン作成", GUILayout.Width(100)))
	//	{
	//		// 名前入力をチェック
	//		if(sceneName != null)
	//		{
	//			// シーンを作る
	//			// ※シーンを作成はするが、保存はされていない
	//			SetupScene();
	//			// シーンのUIを保存するScriptableを作成
	//			SetupMedia();
	//			window.Close();
	//		}
	//	}
	//}

	///// <summary>
	///// パスがディレクトリ内にあるか確認する
	///// </summary>
	///// <param name="_path"></param>
	///// <returns></returns>
	//private bool CheckAvailability(string _path)
	//{
	//	return Directory.Exists(_path);
	//}

	///// <summary>
	///// フォルダーを探す
	///// </summary>
	///// <param name="_path">アセットが格納されているフォルダーパス</param>
	///// <returns></returns>
	//private bool SearchFiles(string _path)
	//{
	//	string[] vs = System.IO.Directory.GetFiles(_path);
	//	return vs.Any();
	//}

	///// <summary>
	///// さらに絞り込んだパスの検索をする
	///// </summary>
	///// <param name="_path">アセットが格納されているフォルダーパス</param>
	///// <param name="_searchInFolders">さらに絞り込んだパス(オブジェクトの拡張子まで含む)</param>
	///// <returns></returns>
	//private bool SearchFiles(string _path, string _searchInFolders)
	//{
	//	string[] vs = System.IO.Directory.GetFiles(_path, _searchInFolders);
	//	return vs.Any();
	//}

	///// <summary>
	///// Sceneの作成
	///// </summary>
	//private void CreateScene()
	//{
	//	// シーンを作成
	//	Scene temp = EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects, NewSceneMode.Additive);
	//	// ※hierarchy上には生成されるが保存はされていない
	//	// 保存はEditorSceneManager.SaveSceneで手動で行う
	//	temp.name = sceneName;
	//	EditorSceneManager.SaveScene(temp);

	//	AssetDatabase.Refresh();
	//}

	///// <summary>
	///// ScriptableObjectAssetの作成
	///// </summary>
	///// <returns></returns>
	//private UIProperty CreateMedia()
	//{
	//	return UIProperty.Create("Assets/SceneUIDate/" + sceneName + ".asset");
	//}

	///// <summary>
	///// 保存するフォルダを作成
	///// Sceneの生成
	///// </summary>
	//void SetupScene()
	//{
	//	//保存先のファルダーがあるか確認
	//	if (CheckAvailability("Assets/Scenes") == false)
	//	{
	//		// folderを生成
	//		AssetDatabase.CreateFolder("Assets", "Scenes");//フォルダ作成
	//	}
	//	// 新規シーン
	//	if (SearchFiles("Assets/Scenes", sceneName + ".unity") == false)
	//	{
	//		CreateScene();
	//	}
	//	else
	//	{
	//		Debug.Log("同じ名前のシーンがあるため、新しいシーンは作れません。");
	//	}
	//}

	///// <summary>
	///// 保存するフォルダを作成
	///// UIを保存するScriptableObjectの生成
	///// </summary>
	//void SetupMedia()
	//{
	//	if (CheckAvailability("Assets/SceneUIDate/") == false)
	//	{
	//		// folderを生成
	//		AssetDatabase.CreateFolder("Assets", "SceneUIDate");        //フォルダ作成
	//	}
	//	if (SearchFiles("Assets/SceneUIDate", sceneName + ".asset") == false)
	//	{
	//		CreateMedia();
	//	}
	//	else
	//	{
	//		Debug.Log("同じ名前のUIDataがあるため、新しいUIDataは作れません。");
	//	}
	//}
	#endregion
}
#endif