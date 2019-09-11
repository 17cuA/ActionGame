//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using System;

//public class UI_HP_New : MonoBehaviour
//{
//	public PlayerType playerType;

//	private float hpBarWidth;                    //hpバーの画像の長さ

//	private int beforeHp = 100;
//	public int maxHp = 100;

//	private int reciveDamage;
//	private int totalDamage;

//	public RectTransform maskPos;
//	public RectTransform redPos;
//	public RectTransform reactionRedPos;

//	private Action currentUpdate;

//	public GameObject[] hpObjects = new GameObject[4];

//	/// <summary>
//	/// キャラの最初のHpを取得
//	/// </summary>
//	/// <param name="hp"></param>
//	public void SetHpMax(int hp)
//	{
//		maxHp = hp;
//		beforeHp = hp;
//	}

//	/// <summary>
//	/// Hpゲージを更新される
//	/// </summary>
//	/// <param name="currentHp"></param>
//	public void Call_UpdateHpGuage( int currentHp)
//	{
//		if(beforeHp != currentHp)
//		{
//			reciveDamage = beforeHp - currentHp;
//			totalDamage += reciveDamage;
//			currentUpdate = LowerHP;
//		}
//	}

//	/// <summary>
//	/// HPゲージを減少させる
//	/// </summary>
//	private void LowerHP()
//	{
//		maskPos.localPosition = new Vector3(CalcMove(maxHp, totalDamage), 0, 0);
//		currentUpdate = ReceiveMoveRed; 
//	}

//	/// <summary>
//	/// 赤いところを受けたダメージ分出す
//	/// </summary>
//	private void ReceiveMoveRed()
//	{
//		redPos.localPosition = new Vector3(totalDamage - 1, 0, 0);
//		currentUpdate = RedUpdate();h
//	}

//	private RedUpdate()
//	{

//	}

//	/// <summary>
//	/// ダメージを受けたときに減らすHPバーの量を計算
//	/// </summary>
//	/// <returns></returns>
//	private float CalcMove(float hp, float damage)
//	{
//		float temp = hp + damage;

//		if (playerType == PlayerType.P1)
//		{
//			return hpBarWidth * (hp - temp) / hp;
//		}
//		return hpBarWidth * (hp - temp) / hp * -1;
//	}

//	// Start is called before the first frame update
//	void Start()
//    {
//		//画像の横のサイズを取得
//		hpBarWidth = hpObjects[0].GetComponent<RectTransform>().sizeDelta.x;

//		//描画順番を指定
//		for (int i = 0; i < 3	; i++)
//		{
//			hpObjects[i].transform.SetSiblingIndex(i);
//		}
//	}

//    // Update is called once per frame
//    void Update()
//    {
//		currentUpdate?.Invoke();

//		if (Input.GetKeyDown("x"))
//		{
//			Call_UpdateHpGuage(50);
//		}
//    }
//}
