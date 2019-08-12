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
using System;

public class UI_HP : MonoBehaviour
{
	public GameObject[] hpObjects = new GameObject[5];

	public Image redImage;
	public RectTransform greenRect, redRect, grayRect;

	public Action update;

	public bool isUpdate = false;							//HPゲージを更新しているか
	public bool isTransparentRed = false;				//赤いところを透明にするか
	public bool isDeleteRed = false;						//赤いところを消すか (透明にしている時に攻撃されたら、消す)

	public float redAlphaValue;				//赤いところのアルファ値
	public float transparentSpeed;			//赤いところを透明にする速度

	public float hpBarWidth;					//hpバーの画像の長さ

	public float hp = 100.0f;
	public float totalDamage = 0;
	public float beforeDamage = 0;

	public int cnt = 0;
	public int cntMax;

	/// <summary>
	/// Hpゲージを更新する
	/// </summary>
	/// <param name="damage"></param>
	public void Call_UpdateHpGuage(float dmg)
	{
		if (isUpdate == true && isTransparentRed == true)
		{
			isDeleteRed = true;
		}
		beforeDamage = totalDamage;
		totalDamage += dmg;
		isUpdate = true;
		LowerHP();
	}

	/// <summary>
	/// 非ガード時のHP減少
	/// </summary>
	private void LowerHP()
	{
		greenRect.localPosition = new Vector3(CalcMove(hp , totalDamage),0,0);
		grayRect.localPosition = new Vector3(CalcMove(hp , totalDamage),0,0);
	}

	/// <summary>
	/// 赤いところの操作
	/// </summary>
	private void OperateRed()
	{
		//赤いところが透明な時にHPの更新があった時の処理
		if  (isDeleteRed == true)
		{
			redAlphaValue = -1.0f;
			redImage.color = new Color(redImage.color.r, redImage.color.g, redImage.color.b, redAlphaValue);
			redRect.localPosition = new Vector3(CalcMove(hp, beforeDamage), 0, 0);
			isDeleteRed = false;
			isTransparentRed = false;
			redAlphaValue = 1.0f;
			redImage.color = new Color(redImage.color.r, redImage.color.g, redImage.color.b, redAlphaValue);
			cnt = 0;
			isUpdate = true;
		}
		//赤いところを透明にする処理
		else if (isTransparentRed == true)
		{
			redAlphaValue -= transparentSpeed;
			redImage.color = new Color(redImage.color.r, redImage.color.g, redImage.color.b, redAlphaValue);
		}
		//赤いところが完全に透明になったときの処理
		else if (redAlphaValue <= 0 )
		{
			redRect.localPosition = new Vector3(CalcMove(hp, totalDamage), 0, 0);
			isTransparentRed = false;
			redAlphaValue = 1.0f;
			redImage.color = new Color(redImage.color.r, redImage.color.g, redImage.color.b, redAlphaValue);
			cnt = 0;
			isUpdate = false;
		}
	}
	
	/// <summary>
	/// ダメージを受けたときに減らすHPバーの量を計算
	/// </summary>
	/// <returns></returns>
	private float CalcMove(float hp , float damage)
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
		if (isUpdate == true)
		{
			if (cnt <= cntMax)
			{
				isTransparentRed = false;
				cnt++;
			}
			else
			{
				isTransparentRed = true;
			}
		}
		OperateRed();

		//debug-------------------------------
		if (Input.GetKeyDown("b"))
		{
			Call_UpdateHpGuage(3);
			//test = flag(isHit);
		}
	}
}
