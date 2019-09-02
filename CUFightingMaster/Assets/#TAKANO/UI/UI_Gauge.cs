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

	private int valueMax;
	private int value;

	/// <summary>
	/// 増やす量を計算
	/// </summary>
	/// <returns></returns>
	private float CalcMove(float valueMax, float damage)
	{
		float temp = valueMax + damage;

		if (playerType == PlayerType.P1)
		{
			return guageWidth * (valueMax + valueMax - temp) / valueMax;
		}
		return guageWidth * (valueMax + valueMax - temp) / valueMax * -1;
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

	private void Start()
	{
		guageWidth = guage.GetComponent<RectTransform>().sizeDelta.x;
		// guagePosition.localPosition += new Vector3(valueMax, 0, 0);
	}

	private void Update()
	{
		//if(Input.GetKeyDown("x"))
		//{
		//    UpdateGuage();
		//}
	}
}
