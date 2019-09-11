using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class UI_HP_New : MonoBehaviour
{
	public PlayerType playerType;

	private float hpBarWidth;    //hpバーの画像の長さ

	public float redAlpha = 1.0f;          //赤いところのAlpha値
	public float transparentSpeed = 0.015f;

	private int beforeHp = 100;
	public int maxHp = 100;

	private int reciveDamage;
	private int totalDamage;

	public int frameCnt = 0;
	public int startTransRedFrame = 90;	//赤が透明になりはじめるまでのフレーム

	public bool isUpdateHp = false;

	public Image redImage;

	public RectTransform maskPos;
	public RectTransform redPos;
	public RectTransform reactionRedPos;

	private Action currentUpdate;

	public GameObject[] hpObjects = new GameObject[4];

	/// <summary>
	/// キャラの最初のHpを取得
	/// </summary>
	/// <param name="hp"></param>
	public void SetHpMax(int hp)
	{
		maxHp = hp;
		beforeHp = hp;
	}

	/// <summary>
	/// Hpゲージを更新される
	/// </summary>
	/// <param name="currentHp"></param>
	public void Call_UpdateHpGuage(int currentHp)
	{
		if (beforeHp != currentHp)
		{
			reciveDamage = beforeHp - currentHp;
			totalDamage += reciveDamage;
			currentUpdate = LowerHP;
		}
	}

	/// <summary>
	/// HPゲージを減少させる
	/// </summary>
	private void LowerHP()
	{
		isUpdateHp = true;
		maskPos.localPosition = new Vector3(CalcMove(maxHp, totalDamage), 0, 0);
		currentUpdate = ReceiveMoveRed;
	}

	/// <summary>
	/// 赤いところを受けたダメージ分押し出す
	/// </summary>
	private void ReceiveMoveRed()
	{
		redPos.localPosition = new Vector3(totalDamage - 1, 0, 0);
		currentUpdate = BeforeRedTransparentUpdate;
	}

	/// <summary>
	/// 赤が透明になり始める前までに攻撃をうけたら
	/// </summary>
	private void BeforeRedTransparentUpdate()
	{
		frameCnt++;
		//設定されたフレーム数を過ぎたら透明にしはじめる
		if(frameCnt < startTransRedFrame)
		{
			currentUpdate = RedTransparentUpdate;
		}
		//攻撃をうけたら
		if(isUpdateHp == true)
		{
			frameCnt = 0;
			currentUpdate = ReceiveMoveRed;
		}
	}

	/// <summary>
	/// 赤を透明にする
	/// </summary>
	private void RedTransparentUpdate()
	{
		frameCnt = 0;

		//赤いところが透明になるまでに	
		if (redAlpha >= 0.0f)
		{
			//透明化は進む
			redAlpha -= transparentSpeed;
			redImage.color = new Color(redImage.color.r, redImage.color.g, redImage.color.b, redAlpha);

			//HPの更新があった場合
			if (isUpdateHp == true)
			{

				currentUpdate = MoveRedToGreen;
			}
		}
		//赤いところが透明になったら
		else
		{
			currentUpdate = MoveRedToGreen;
		}
	}

	/// <summary>
	/// 赤を緑まで移動させる
	/// </summary>
	private void MoveRedToGreen()
	{
		//実体化する
		redAlpha = 1.0f;
		redImage.color = new Color(redImage.color.r, redImage.color.g, redImage.color.b, redAlpha);
		//移動させる
		redPos.localPosition = maskPos.localPosition;
	}

	/// <summary>
	/// ダメージを受けたときに減らすHPバーの量を計算
	/// </summary>
	/// <returns></returns>
	private float CalcMove(float hp, float damage)
	{
		float temp = hp + damage;

		if (playerType == PlayerType.P1)
		{
			return hpBarWidth * (hp - temp) / hp;
		}
		return hpBarWidth * (hp - temp) / hp * -1;
	}

	// Start is called before the first frame update
	void Start()
	{
		//画像の横のサイズを取得
		hpBarWidth = hpObjects[0].GetComponent<RectTransform>().sizeDelta.x;

		//描画順番を指定
		for (int i = 0; i < 3; i++)
		{
			hpObjects[i].transform.SetSiblingIndex(i);
		}
	}

	// Update is called once per frame
	void Update()
	{
		currentUpdate?.Invoke();

		if (Input.GetKeyDown("x"))
		{
			Call_UpdateHpGuage(50);
		}
	}
}
