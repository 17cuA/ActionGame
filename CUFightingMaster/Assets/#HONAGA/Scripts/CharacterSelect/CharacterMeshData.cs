using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// MonoBehaviour継承
public class CharacterMeshData : MonoBehaviour
{
	public SkinnedMeshRenderer[] mesh;		// キャラモデルのメッシュデータ
	public Material[] normalMaterials;			// カラー１(マテリアルの数 = 要素数)
	public Material[] anotherColorMaterials;	// カラー２(マテリアルの数 = 要素数)
	public int colorNumber = 1;					// カラーナンバー(1で初期化)

	/// <summary>
	/// マテリアルを変更する(入力ごとに変更するようにする予定)
	/// </summary>
	public void ChangeMaterials()
	{
		if (colorNumber == 2)
		{
			for (int i = 0; i < normalMaterials.Length; i++)
			{
				mesh[i].material = normalMaterials[i];
			}
			colorNumber = 1;
		}
		else if(colorNumber == 1)
		{
			for (int i = 0; i < anotherColorMaterials.Length; i++)
			{
				mesh[i].material = anotherColorMaterials[i];
			}
			colorNumber = 2;
		}
	}
}
