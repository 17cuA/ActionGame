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
//※一時的なプログラムです※
// スライダーを使う場合は使える
//----------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UI_Gauge : MonoBehaviour
{
    private int gaugeMax = 0;
    private int currentGuageValue= 0;

    private Slider mySlider;

	/// <summary>
	/// このスライダーを最大値にする
	/// </summary>
	/// <param name="maxValie"></param>
    public void SetSliderMaxValue(int maxValie)
    {
        mySlider.maxValue = maxValie;
    }

	/// <summary>
	/// スライダーの値を更新
	/// </summary>
	/// <param name="currentValue"></param>
    public void UpdateSliderValue(int currentValue)
    {
        mySlider.value = currentValue;
    }

    private void Awake()
    {
        mySlider = gameObject.GetComponent<Slider>();
    }
}


