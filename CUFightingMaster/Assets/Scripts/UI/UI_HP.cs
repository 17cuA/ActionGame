//---------------------------------------
// HPバー
//---------------------------------------
// 作成者:高野
// 作成日:2019.07.14
//--------------------------------------
// 更新履歴
// 2019.07.17 作成
//--------------------------------------
// 仕様
// 子オブジェクトに必要なもの
// ・Mask
// ・5:ダメージを受けたときにHpバーの色が変わるやつ
// ・4:Hpバーの緑のところ
// ・3:ブロックした時に色が変わるところ
// ・2:ダメージの赤
// ・1:一番後ろのbackground
//----------------------------------------
// MEMO
// 参照は手動です
//----------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_HP : MonoBehaviour
{
	public GameObject[] hpObjects = new GameObject[5];

	public float hp = 100.0f; //debug

	public float hpBarWidth; //hpバーの長さ

	public int damage;

	/// <summary>
	/// ダメージを受けたときに減らすHPバーの量を計算
	/// </summary>
	/// <returns></returns>
	private float CalcMove(float hp , int damage)
	{
		float temp = hp - (float)damage;
		return hpBarWidth * (100 - temp) / 100;
	}

	private void Start()
	{
		//画像の横のサイズを取得
		hpBarWidth = hpObjects[0].GetComponent<RectTransform>().sizeDelta.x;

		//描画順番を指定
		for (int i = 0; i < 5; i++)
		{
			hpObjects[i].transform.SetSiblingIndex(i);
		}
	}

	private void Update()
	{
		hpObjects[0].GetComponent<RectTransform>().localPosition = new Vector3(CalcMove(hp , damage), 0, 0);
	}
}
