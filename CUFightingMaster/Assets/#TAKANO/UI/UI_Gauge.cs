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
	public Image waku_Image;
	public Image effect_Image;
	public Image pushEx_Image;
	public Image back_Image;

	public RectTransform guagePosition;

	private float guageWidth;

	public AnimationUIManager guageAnim;
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
		//光らせる処理とPUSH EX 表示
		if (value < valueMax)
		{
			guageAnim.isInvisible = true;
			pushEx.isInvisible = true;
		}
		else
		{
			guageAnim.isInvisible = false;
			pushEx.isInvisible = false;
		}
	}

	/// <summary>
	/// UIの表示を消す
	/// </summary>
	public void Call_HideImage()
	{
		guage.GetComponent<Image>().enabled = false;
		waku_Image.enabled = false;
		effect_Image.enabled = false;
		pushEx.enabled = false;
		back_Image.enabled = false;
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
