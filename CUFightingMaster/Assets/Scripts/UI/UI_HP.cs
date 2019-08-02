//---------------------------------------
// HPバー
//---------------------------------------
// 作成者:高野
// 作成日:2019.07.14
//--------------------------------------
// 更新履歴
// 2019.07.17 作成
// 2019.07.30 更新
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
<<<<<<< HEAD
=======
using System;

public class UI_HP : MonoBehaviour
{
	public GameObject[] hpObjects = new GameObject[5];

	public Image redImage;
	public RectTransform greenRect, redRrct, grayRect;

	public Action update;

	public bool isHit = false;

	public float alpha;

	public float transparentSpeed;
	
	public float redDeleteLimit; //この時間中にダメージを受けなければ
	public float autoHealLimit;   //この時間中にダメージを受けなければ灰色(3)が緑(4)になる

	public float hpBarWidth; //hpバーの長さ

	public float hp = 100.0f;
	public int damage;
	public int tempDamage;

	/// <summary>
	/// Hpゲージを更新する
	/// </summary>
	/// <param name="damage"></param>
	public void Call_UpdateHpGuage(int dmg)
	{
		damage += dmg;
		update = LowerHP;
		isHit = true;

		LowerHP();
	}

	/// <summary>
	/// 非ガード時のHP減少
	/// </summary>
	private void LowerHP()
	{
		greenRect.localPosition = new Vector3(CalcMove(hp , damage),0,0);
		grayRect.localPosition = new Vector3(CalcMove(hp , damage),0,0);

		Invoke("TransparentRedImage", 1.3f);
		//TransparentRedImage();
	}

	/// <summary>
	/// 赤いところを徐々に透明にする
	/// </summary>
	private void TransparentRedImage()
	{
		//while (alpha > 0.0f)
		{
			if (isHit == false)
			{
				//redRrct.localPosition = new Vector3(CalcMove(hp, damage), 0, 0);
				//isHit = false;
			}
			else
			{
				alpha -= transparentSpeed;
				redImage.color = new Color(redImage.color.r, redImage.color.g, redImage.color.b, alpha);
			}
		}
	}
	
	/// <summary>
	/// ダメージを受けたときに減らすHPバーの量を計算
	/// </summary>
	/// <returns></returns>
	private float CalcMove(float hp , int damage)
	{
		float temp = hp - damage;
		return hpBarWidth * (100 - temp) / 100;
	}
	
	private void Awake()
	{
		
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

		//debug-------------------------------
		if (Input.GetKeyDown("b"))
		{
			Call_UpdateHpGuage(10);
		}
	}
}
>>>>>>> 9d0ea753de905a0623dfe5b08a6652443d08599b
