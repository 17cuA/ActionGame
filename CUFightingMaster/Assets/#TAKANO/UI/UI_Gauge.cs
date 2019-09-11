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

	[SerializeField]private int valueMax = 0;
	public int value = 0;

	/// <summary>
	/// 増やす量を計算
	/// </summary>
	/// <returns></returns>
	private float CalcMove(float valueMax, float damage)
	{
		float temp = valueMax + damage;

		if (temp != 0)
		{
			if (playerType == PlayerType.P1)
			{
				return guageWidth * (valueMax + valueMax - temp) / valueMax * -1;
			}
			return guageWidth * (valueMax + valueMax - temp) / valueMax;
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
		if ((value / (valueMax / guageIndex)) != 0)
		{
			Debug.Log(value / (valueMax / guageIndex));
			for (int i = 0;i < guageIndex;i++)
			{
				if (i < guageNum)
				{
					if (guageAnims[i].isStart == false)
					{
						guageAnims[i].isStart = true;
						guageAnims[i].isLoop = true;
					}
				}
				else
				{
					if (guageAnims[i].isStart == true)
					{
						guageAnims[i].isLoop = false;
					}
				}
			}
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
