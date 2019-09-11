using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UI_HP_New : MonoBehaviour
{
	public PlayerType playerType;

	private float hpBarWidth;    //hpバーの画像の長さ

	public float redAlpha;          //赤いところのAlpha値
	public float transparentSpeed = 0.015f;

	private int beforeHp = 100;
	public int maxHp = 100;

	private int reciveDamage;
	private int totalDamage;

	public bool isUpdateHp = false;

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
		currentUpdate = RedTransparentUpdate;
	}

	/// <summary>
	/// 赤が透明になり始める前までに攻撃をうけたら
	/// </summary>
	private void BeforeRedTransparentUpdate()
	{

	}

	/// <summary>
	/// 赤を透明にする
	/// </summary>
	private void RedTransparentUpdate()
	{
		//赤いところが透明になるまでに	
		if(redAlpha >= 0.0f)
		{
			//透明化は進む
			redAlpha -= transparentSpeed;
			//HPの更新があった場合
			if(isUpdateHp == true)
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
