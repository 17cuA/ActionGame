//---------------------------------------
// ゲージ系のUI
//---------------------------------------
// 作成者:高野
// 作成日:2019.07.14
//--------------------------------------
// 更新履歴
// 2019.07.14 作成
//--------------------------------------
// 仕様
//----------------------------------------
// MEMO

//----------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UI_Gauge : MonoBehaviour
{
	public PlayerType playerType;

	public GameObject guage;

	public RectTransform guagePosition;

	private float guageWidth;

	private static int guageIndex = 3;

	public AnimationUIManager[] guageAnims = new AnimationUIManager[guageIndex];
	public AnimationUIManager pushEx;
	[SerializeField]private int valueMax = 0;
	public int value = 0;

	/// <summary>
	/// 増やす量を計算
	/// </summary>
	/// <returns></returns>
	private float CalcMove(float _valueMax, float damage)
	{
		float temp = _valueMax + damage;

		if (temp != 0)
		{
			if (playerType == PlayerType.P1)
			{
				return guageWidth * (_valueMax + _valueMax - temp) / _valueMax * -1;
			}
			return guageWidth * (_valueMax + _valueMax - temp) / _valueMax;
		}
		return 0;
	}

	/// <summary>
	/// ゲージの更新
	/// </summary>
	/// <param name="currentValue"></param>
	public void UpdateGuage(int currentValue)
	{
		guagePosition.localPosition = new Vector3(CalcMove(valueMax, currentValue), 0, 0);
	}

	public void SetValueMax(int _maxValue)
	{
		valueMax = _maxValue;
	}

	//ゲージが一定量溜まったらエフェクトでわかりやすいようにする
	private void GuageEfects()
	{
		var guageNum = value / (valueMax / guageIndex);
		//何本光らせるか処理
		for (int i = 0; i < guageIndex; i++)
		{
			if (i < guageNum)
			{
				if (guageAnims[i].isInvisible)
				{
					guageAnims[i].isInvisible = false;
				}
			}
			else
			{
				if (!guageAnims[i].isInvisible)
				{
					guageAnims[i].isInvisible = true;
				}
			}
			//PUSH EX 表示
			if (guageNum == guageIndex)	pushEx.isInvisible = false;
			else pushEx.isInvisible = true;
		}


	}
	private void Start()
	{
		guageWidth = guage.GetComponent<RectTransform>().sizeDelta.x;
	}

	private void Update()
	{
		UpdateGuage(value);
		GuageEfects();
	}
}
