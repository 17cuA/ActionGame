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
using System;

public class UI_HP : MonoBehaviour
{
	public GameObject[] hpObjects = new GameObject[5];

	private Action update;

	public Image redImage;
	public RectTransform green, red, gray;

	public float alpha;
	private float transparentSpeed;

	public int autoHealLimist;	//この時間中にダメージを受けなければ灰色(3)が緑(4)になる



	public float hpBarWidth; //hpバーの長さ

	//debug------------------
	public float hp = 100.0f;
	public int damage;

	/// <summary>
	/// Hpゲージを更新する
	/// </summary>
	/// <param name="damage"></param>
	public void UpdateHpGuage(int damage)
	{
		update = LowerHP;
	}

	/// <summary>
	/// 減らす
	/// </summary>
	private void LowerHP()
	{
		
	}

	/// <summary>
	/// 赤いところを徐々に透明にする
	/// </summary>
	private void TransparentRedImage()
	{
		redImage.color = new Color(1.0f, 1.0f, 1.0f, alpha);
		alpha -= transparentSpeed;
	}

	/// <summary>
	/// ダメージを受けたときに減らすHPバーの量を計算
	/// </summary>
	/// <returns></returns>
	private float CalcMove(float hp , int damage)
	{
		float temp = hp - (float)damage;
		return hpBarWidth * (100 - temp) / 100;
	}
	

	private void Awake()
	{
		redImage = hpObjects[1].GetComponent<Image>();
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
		update();

		hpObjects[4].GetComponent<RectTransform>().localPosition = new Vector3(CalcMove(hp , damage), 0, 0);
	}
}
