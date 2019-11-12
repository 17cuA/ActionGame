using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;

[CreateAssetMenu(menuName = "CUSystem/Create UIFactory")]
[Serializable]
public class UIFactory : ScriptableObject
{
	public static UIFactory Create(string folder)
	{
		//ScriptableObject.CreateInstance()でインスタンスを生成
		// この時点ではアセット化はされていない
		var asset = CreateInstance<UIFactory>();
		// アセット化するにはAssetDatabase.CreateAsset()
		// 拡張子は必ず.assetとする
		AssetDatabase.SaveAssets();
		AssetDatabase.CreateAsset(asset, folder);
		AssetDatabase.Refresh();
		return asset;
	}
}
