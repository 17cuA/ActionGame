/*------------------------------------------------------------------------------------------------------------------------------------------------------
 * 作成日：	2019/05/09
 * 作成者：	横山凌
 * 
 * シーン上に生成するUI(Image)を簡易的に生成、編集できるようにする
 * 選択されたSceneViewを生成し、保存されていたUIを生成する
 * 
 *					＿人人人人人人人人人人人人人人人人人人人人人人人人人人人人人人＿
 *					＞　MenuItem と void Awake と void OnEnable の実行処理順は　＜
 *					＞　　　void Awake → void OnEnable → MenuItemである			＜
 *					￣Y^Y^Y^Y^Y^Y^Y^Y^Y^Y^Y^Y^Y^Y^Y^Y^Y^Y^Y^Y^Y^Y^Y^Y^Y^Y^Y^Y^￣
 ------------------------------------------------------------------------------------------------------------------------------------------------------*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEditorInternal;

#if UNITY_EDITOR
public class UISettingParameter : ScriptableSingleton<UISettingParameter>
{
	public UISettingEditor editor = null;             // UIを編集するエディター
	public UIView view = null;                        // UIを編集する専用のシーン
}

public class UISettingEditor : EditorWindow
{
	/*-----------------------------------------------
	 * 変数宣言
	-----------------------------------------------*/
	public enum Step
	{
		step1,
		step2
	}
	
	// UIプロパティ関連
	[SerializeField]
	private GameObject canvas = null;
	private List<Image> editorImage = new List<Image>();            // 実際に表示しているImage変数を格納
	private List<UIAsset> assets = new List<UIAsset>();

	Step step = Step.step1;
	// 制限数値関連
	float pivotMin = 0.0f;      // pivotの上限
	float pivotMax = 1.0f;      // pivotの下限
	int anchorMax = 100;        // anchorの上限
	int anchorMin = 0;          // anchorの上限

	// UIを作るために必要な画像をアタッチする変数
	private Sprite tempSprite = null;
	Vector2 scrollAssetsPos = Vector2.zero;

	/// <summary>
	/// edirorとViewをインスタンス化
	/// </summary>
	/// <param name="_sceneAsset">ドラックしたシーン</param>
	/// <param name="_property">ドラックしたUIData</param>
	public static void Open(GameObject _canvas)
	{
		if (UISettingParameter.instance.editor == null)
		{
			// インスタンス化
			UISettingParameter.instance.editor = (UISettingEditor)CreateInstance(typeof(UISettingEditor));
			// Editor生成
			UISettingParameter.instance.editor = EditorWindow.GetWindow<UISettingEditor>(_canvas.name);
			UISettingParameter.instance.editor.canvas = _canvas; 
		}
		// 専用Viewを生成
		if (UISettingParameter.instance.view == null)
		{
			UISettingParameter.instance.editor.canvas = _canvas;
		}
	}

	private void OnGUI()
	{
		switch (step)
		{
			case Step.step1:
				Preparation();
				step++;
				break;
			case Step.step2:
				Drawing();
				break;
			default:
				break;
		}
	}

	void Preparation()
	{
		List<Transform> canvasChilds = SubTransform.ChildClass(canvas.transform);
		SetEditorLoadHierarchy(canvasChilds);
	}
	/// <summary>
	/// エディターに編集画面を描画する
	/// </summary>
	void Drawing()
	{
		#region 新しい画像を生成
		// 画像をアタッチする領域を設定
		tempSprite = EditorGUILayout.ObjectField("新しいUIを作る 画像をセット→", tempSprite, typeof(Sprite), false) as Sprite;
		// 画像がセットされたら新しくUIを生成する
		if (tempSprite)
		{
			CreateNewAsset(tempSprite);
		}
		#endregion

		// UIがあるならエディター上に表示
		if (assets.Count > 0)
		{
			//ここから下に書かれるものはエディター上でスクロール範囲に含まれるできる
			scrollAssetsPos = EditorGUILayout.BeginScrollView(scrollAssetsPos, GUI.skin.box);			//エディターをスクロールさせる(開始位置)-------------------------------------------------

			EditorGUILayout.BeginVertical(GUI.skin.box, GUILayout.Width(500));							// 縦表示開始---------------------------------------------------------------------------
			for (int i = 0; i < assets.Count; i++)
			{
				#region hierarchyでエディターを使わずに削除
				// hierarchy上でImageを削除(任意で消した場合)
				if (editorImage[i] == null)
				{
					// エディター用の変数を消す(エディターに反映)
					DeleteAssetDateOnEditor(i);
					continue;
				}
				#endregion

				// ImageのRectTransformを取得
				RectTransform tempRectTrans = editorImage[i].GetComponent<RectTransform>();

				EditorGUILayout.BeginVertical(GUI.skin.box);                                            // 縦表示開始---------------------------------------------------------------------------
				EditorGUILayout.BeginHorizontal();                                                      // 横表示開始---------------------------------------------------------------------------

				// UIに使用する画像を表示する(Spriteで取得)
				EditorGUILayout.BeginVertical(GUILayout.Width(100));                                    // 縦表示開始---------------------------------------------------------------------------

				// 画像の名前を変更可能にする
				assets[i].NameSprite = GUILayout.TextArea(editorImage[i].name);
				EditorGUIUtility.labelWidth = 70;
				assets[i].Sprite = EditorGUILayout.ObjectField("", editorImage[i].sprite, typeof(Sprite), true) as Sprite;
				// 画像の色を取得
				assets[i].Color = EditorGUILayout.ColorField("色変更", editorImage[i].color);
				EditorGUILayout.EndVertical();                                                                  // 縦表示終了---------------------------------------------------------------------------

				EditorGUILayout.BeginVertical();                                                                // 縦表示開始---------------------------------------------------------------------------
																												// 画像の座標を取得する(※Transformではなく、RectTransformで表示)
				assets[i].RectPos = EditorGUILayout.Vector3Field("Rect座標", tempRectTrans.localPosition);

				#region pivot定義
				// 画像のpivotを取得する(※上記はCanvasのアンカー座標、ここでは画像そのもののアンカー座標(ギズモまたはpivot))
				// スライダーで座標を変更できるようにする
				Vector2 tempPivot = tempRectTrans.pivot;
				tempPivot.x = EditorGUILayout.Slider("画像のギズモ設定X(0～1) ", tempPivot.x, pivotMin, pivotMax);
				tempPivot.y = EditorGUILayout.Slider("画像のギズモ設定Y(0～1) ", tempPivot.y, pivotMin, pivotMax);
				#endregion

				#region anchor定義
				EditorGUILayout.LabelField("画面比率(〇％～□％)の位置に配置する");
				// 画面比率対応配置(Anchor設定)
				Vector2 tempAnchorMin = tempRectTrans.anchorMin * 100.0f;
				tempAnchorMin = EditorGUILayout.Vector2Field("画像の左下端を〇％にする", tempAnchorMin);
				Vector2 tempAnchorMax = tempRectTrans.anchorMax * 100.0f;
				tempAnchorMax = EditorGUILayout.Vector2Field("画像の右上端を□％にする", tempAnchorMax);
				#endregion

				#region 制限処理
				// anchor数値に制限をかける
				tempAnchorMin = LimitAnchor(tempAnchorMin);					// アンカー座標の数値に制限をかける(0~100)
				tempAnchorMax = LimitAnchor(tempAnchorMax);					// アンカー座標の数値に制限をかける(0~100)
				LimitSpriteSize(i, tempAnchorMin, tempAnchorMax);			// アンカー座標の数値が同じ場合、Image画像の比率をデフォルトにする
				#endregion

				// エディターで編集した数値をImageオブジェクトに対応させる
				UpdateEditorAsset(i, tempPivot, tempAnchorMin, tempAnchorMax);

				#region 削除ボタン
				// エディター側から削除(削除ボタンを押した場合)
				if (GUILayout.Button("削除", GUILayout.Width(100)))
				{
					// hierarchy上(Scene)のImageオブジェクトを消す
					DestroyImmediate(editorImage[i].gameObject);
					// エディター用の変数を消す(エディターに反映)
					DeleteAssetDateOnEditor(i);
				}
				#endregion

				EditorGUILayout.EndVertical();                                                              // 縦表示終了---------------------------------------------------------------------------
				EditorGUILayout.EndHorizontal();                                                            // 横表示終了---------------------------------------------------------------------------
				EditorGUILayout.EndVertical();                                                              // 縦表示終了---------------------------------------------------------------------------
			}
			EditorGUILayout.EndVertical();                                                                  // 縦表示終了---------------------------------------------------------------------------

			//ここから上に書かれるものはエディター上でスクロール範囲に含まれる
			EditorGUILayout.EndScrollView();                                                                //エディターをスクロールさせる(終了位置)---------------------------------------------
		}
	}

	/// <summary>
	/// Imageオブジェクトをシーン上に生成する
	/// Canvasの子供に生成
	/// </summary>
	/// <param name="_name">画像の名前をImageオブジェクトに引き継ぐ</param>
	/// <returns></returns>
	Image CreateImage(string _name)
	{
		// hierarchyWindowに表示するオブジェクトを生成する
		var canvasObject = new GameObject(_name);        // (”オブジェクトの名前”)
														 // Imageオブジェクトに変換(正しくはImageの特性を加える)
		var tempImage = canvasObject.AddComponent<Image>();
		// Carnvasの子供に設定
		tempImage.transform.SetParent(GameObject.Find("Canvas").transform);
		// 画面中央に生成する
		tempImage.GetComponent<RectTransform>().localPosition = Vector3.zero;

		return tempImage;
	}

	/// <summary>
	/// Hierarchy上に存在しているImageオブジェクトの情報を取得し、
	/// エディターに反映させる
	/// </summary>
	/// <param name="_canvasChilds">Canvas内に存在しているImageオブジェクト配列(List)</param>
	void SetEditorLoadHierarchy(List<Transform> _canvasChilds)
	{
		if (_canvasChilds != null)
		{
			// typeで指定した型の全てのオブジェクトを配列で取得し,その要素数分繰り返す.
			foreach (Transform obj in _canvasChilds)
			{
				var tempImage = obj.GetComponent<Image>();
				// エディターに反映
				AddEditorAssets(tempImage);
			}
		}
	}

	/// <summary>
	/// 0~100の数値内に制限をかける
	/// </summary>
	/// <param name="_anchor">エディター上で操作した数値</param>
	Vector2 LimitAnchor(Vector2 _anchor)
	{
		if (_anchor.x >= anchorMax)
		{
			_anchor.x = anchorMax;
		}
		else if (_anchor.x <= anchorMin)
		{
			_anchor.x = anchorMin;
		}

		if (_anchor.y >= anchorMax)
		{
			_anchor.y = anchorMax;
		}
		else if (_anchor.y <= anchorMin)
		{
			_anchor.y = anchorMin;
		}

		return _anchor;
	}

	/// <summary>
	/// アンカーを設置した位置に画像の端が来るように調整する
	/// </summary>
	/// <param name="_num">Imageオブジェクトの要素番号</param>
	/// <param name="_anchorMin">エディター上で操作可能な数値(最小値)</param>
	/// <param name="_anchorMax">エディター上で操作可能な数値(最大値)</param>
	void LimitSpriteSize(int _num, Vector2 _anchorMin, Vector2 _anchorMax)
	{
		if (_anchorMin.x == _anchorMax.x)
		{
			editorImage[_num].GetComponent<RectTransform>().sizeDelta = new Vector2(100, 100);
		}
		if (_anchorMin.y == _anchorMax.y)
		{
			editorImage[_num].GetComponent<RectTransform>().sizeDelta = new Vector2(100, 100);
		}

		else
		{
			editorImage[_num].GetComponent<RectTransform>().offsetMin = Vector2.zero;
			editorImage[_num].GetComponent<RectTransform>().offsetMax = Vector2.zero;
		}
	}

	/// <summary>
	/// 新しく作るUIを生成する
	/// </summary>
	/// <param name="_sprite">UI作成に使用する画像データ</param>
	void CreateNewAsset(Sprite _sprite)
	{
		Image tempImage = CreateImage(_sprite.name);
		tempImage.sprite = _sprite;                                 // 画像を適用

		// 画像のXスケールを1とし、(X,Y)の比率をImageオブジェクトに適応させる
		Vector2 tempScale = _sprite.bounds.size;
		tempScale.y /= tempScale.x;         // Yスケールの比率を計算
		tempScale.x /= tempScale.x;         // Xスケールの比率を1にする
		tempImage.GetComponent<RectTransform>().localScale = tempScale;

		// エディターに反映
		AddEditorAssets(tempImage);

		// セットされた画像を空にし、再利用できるようにする
		tempSprite = null;
	}

	/// <summary>
	/// 生成したImageをエディター用変数に格納する
	/// </summary>
	/// <param name="_image">新しく用意されたImageオブジェクト</param>
	void AddEditorAssets(Image _image)
	{
		RectTransform tempRectTransform = _image.GetComponent<RectTransform>();
		// アンカー数値を100％表示に変換
		Vector2 tempAnchorMin = _image.GetComponent<RectTransform>().anchorMin * 100.0f;
		Vector2 tempAnchorMax = _image.GetComponent<RectTransform>().anchorMax * 100.0f;

		//// Image
		editorImage.Add(_image);                                                // Image
		// UIの情報を構造体に格納
		UIAsset temp = new UIAsset
		{
			Sprite = _image.sprite,					// 画像
			NameSprite = _image.name,				// 画像の名前
			Color = _image.color,					// RGB
			RectPos = tempRectTransform.position,	// 座標
			Pivot = tempRectTransform.pivot,		// ピボット
			AnchorMax = tempAnchorMax,				// アンカーMax
			AnchorMin = tempAnchorMin,				// アンカーMin
			Scale = tempRectTransform.localScale	// スケール
		};

		// 構造体をリストに追加
		assets.Add(temp);
	}


	/// <summary>
	/// エディター上の数値をオブジェクトと同期させる
	/// </summary>
	/// <param name="_Index">配列(List)要素番号</param>
	/// <param name="_pivot">pivot数値で画像の中心点を決める</param>
	void UpdateEditorAsset(int _Index, Vector2 _pivot, Vector2 _anchorMin, Vector2 _anchorMax)
	{
		RectTransform tempRectTransform = editorImage[_Index].GetComponent<RectTransform>();

		// エディターに適応
		assets[_Index].Pivot = _pivot;
		assets[_Index].AnchorMin = _anchorMin;
		assets[_Index].AnchorMax = _anchorMax;
		assets[_Index].Scale = tempRectTransform.localScale;

		// Imageオブジェクトに適応
		editorImage[_Index] = editorImage[_Index];                                                                                                  // オブジェクト更新
		editorImage[_Index].name = assets[_Index].NameSprite;                                                                                   // 名前を更新
		editorImage[_Index].sprite = assets[_Index].Sprite;                                                                                 // 画像更新
		editorImage[_Index].color = assets[_Index].Color;                                                                                       // 色更新

		tempRectTransform.localPosition = assets[_Index].RectPos;                 // 座標更新
		tempRectTransform.anchorMin = assets[_Index].AnchorMin / 100.0f;      // anchor座標更新
		tempRectTransform.anchorMax = assets[_Index].AnchorMax / 100.0f;  // anchor座標更新
		tempRectTransform.pivot = assets[_Index].Pivot;                                   // pivot更新
		tempRectTransform.localScale = assets[_Index].Scale;                                // scale更新
	}

	/// <summary>
	/// hierarchy上に存在しているImageと保存しているImageの情報が違う場合
	/// データの上書きをするのか、保存したものを開くのか確認する
	/// </summary>
	void SearchImageObj(GameObject _canvas)
	{
		// Canvasの子供を格納
		List<Transform> canvasChilds = SubTransform.ChildClass(_canvas.transform);
		Debug.Log(canvasChilds);
		SetEditorLoadHierarchy(canvasChilds);
	}

	/// <summary>
	/// シーン上に生成されているCanvas内のオブジェクトを消す
	/// </summary>
	/// <param name="_child">Canvasの子供の配列</param>
	void DeleteImageOnTheHierarchy(List<Transform> _child)
	{
		for (int i = 0; i < _child.Count; ++i)
		{
			DestroyImmediate(_child[i].gameObject);
		}
	}

	/// <summary>
	/// UIの情報を消す
	/// </summary>
	/// <param name="_Index">消すオブジェクトの要素番号</param>
	void DeleteAssetDateOnEditor(int _Index)
	{
		editorImage.RemoveAt(_Index);
		assets.RemoveAt(_Index);
	}

	/// <summary>
	/// エディターが消されたときに動作する
	/// </summary>
	private void OnDestroy()
	{
		//リフレッシュすることによりアセットがインポートされる
		AssetDatabase.Refresh();
		Resources.UnloadUnusedAssets();
		// 現状のシーン情報を保存する
		EditorSceneManager.SaveScene(SceneManager.GetActiveScene());

		// 現在アクティブなシーンを閉じる
		EditorSceneManager.CloseScene(SceneManager.GetActiveScene(), true);
		// SceneViewを閉じる
		UISettingParameter.instance.view.Close();
	}
}
#endif