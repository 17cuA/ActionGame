/*------------------------------------------------------------------------------------------------------------------------------------------------------
 * 作成日：	2019/05/09
 * 作成者：	横山凌
 * 
 * シーン上に生成するUIのデータを保存する
 * ゲーム実行時は、ここからデータを読み込み、シーン上のUIたちを配置する
 ------------------------------------------------------------------------------------------------------------------------------------------------------*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;

[CreateAssetMenu(menuName = "CUSystem/Create UIProperty")]
public class UIProperty : ScriptableObject
{
#if UNITY_EDITOR
	[SerializeField]
	public List<Sprite> sprite = new List<Sprite>();                // 画像
	public List<string> nameSprite = new List<string>();      // 画像の名前
	public List<Color> color = new List<Color>();                   // 画像の色

	// RectTransform関係
	[SerializeField]
	public List<Vector3> rectPos = new List<Vector3>();     // 座標(RectTransform)
	public List<Vector2> pivot = new List<Vector2>();           // pivot座標
	public List<Vector2> anchorMax = new List<Vector2>();   // Canvasからみた座標(最低値)
	public List<Vector2> anchorMin = new List<Vector2>();   // Canvasからみた座標(最大値)
	public List<Vector2> scale = new List<Vector2>();           // Canvasからみた座標(最大値)

	public static UIProperty Create(string folder)
	{
		//ScriptableObject.CreateInstance()でインスタンスを生成
		// この時点ではアセット化はされていない
		var asset = CreateInstance<UIProperty>();
		// アセット化するにはAssetDatabase.CreateAsset()
		// 拡張子は必ず.assetとする
		AssetDatabase.SaveAssets();
		AssetDatabase.CreateAsset(asset, folder);
		AssetDatabase.Refresh();
		return asset;
	}
#endif
}
