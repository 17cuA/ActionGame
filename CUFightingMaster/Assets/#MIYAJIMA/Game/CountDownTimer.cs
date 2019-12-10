//---------------------------------------
// カウントダウンをするタイマー
//---------------------------------------
// 作成者:宮島
// 作成日:2019
//--------------------------------------
// 更新履歴
// 2019.07.18 コメント追記 by takano
// 2019.12.09 ミリ秒を表示するためのプログラムを追加　by gotou
// 2019.12.10 内部電源、外部電源の表記を表示するためのプログラムを追加 by gotou
//--------------------------------------
// 仕様
// 
//----------------------------------------
// MEMO
// 
//----------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDownTimer : MonoBehaviour
{
	private float maxTime = 99;		//初期値
    private float currentTime = 99;	//現在値
	private int EmphasizTime = 10;	//強調表示する時間
	private float displayTime;
	private float downNum=5;

	public bool isPlay = false;
    bool isEndCont = false;

    public Image firstDigit;		//一桁目のimage
    public Image secondDigit;		//二桁目のimage
	public Image msecondDigit;		//ミリ秒のimage
	public Image dsecondDigit;		//ディシ秒のimage
	public Image back;				//タイマーの背景
	public GameObject internalPower;		//内部電源
	public GameObject outsidePower;		//外部電源

	public Sprite[] numSprite = new Sprite[10];

	private Color initColor;

	/// <summary>
	/// タイマーを初期値へ戻す
	/// </summary>
	public void ResetTimer()
	{
		isPlay = false;
		isEndCont = false;
		currentTime = maxTime;
		UpdateDisplay(currentTime);

		firstDigit.color = initColor;
		secondDigit.color = initColor;
		msecondDigit.color = initColor;
		dsecondDigit.color = initColor;
		back.color = initColor;
	}

	/// <summary>
	/// カウントダウンを開始・停止する、引数にtrueだと開始、引数にfalseだと停止
	/// </summary>
	public void PlayCountDown(bool flag)
	{
		isPlay = true;
		isEndCont = true;
	}

	/// <summary>
	/// カウントダウンの終了を通知
	/// </summary>
	/// <returns></returns>
	public bool isEndCountDown()
    {
		return isEndCont;
    }

    /// <summary>
    /// タイマー表示の更新
    /// </summary>
    /// <param name="count"></param>
    private void UpdateDisplay(float count)
    {
		var castCount = Mathf.Clamp(count,0,99);
        firstDigit.sprite = numSprite[(int)(castCount / 1 % 10)];
        secondDigit.sprite = numSprite[(int)(castCount / 1 / 10)];
		msecondDigit.sprite = numSprite[(int)(castCount * 10 / 1 % 10)];
		dsecondDigit.sprite = numSprite[(int)(castCount * 100 / 1 % 10)];
	}
	
	/// <summary>
	/// タイマーの強調表示
	/// </summary>
	/// <param name="fromNumber"></param>
	private void EmphasizNumber(int fromNumber)
	{
		if(currentTime <= (fromNumber + 1))
		{
			firstDigit.color = new Color(255, 0, 0, 1);
			secondDigit.color = new Color(255, 0, 0, 1);
			msecondDigit.color = new Color(255, 0, 0, 1);
			dsecondDigit.color = new Color(255, 0, 0, 1);
			back.color = new Color(255, 0, 0, 1);
		}
	}

	/// <summary>
	/// カウントを10秒はやめる 
	/// </summary>

	public void CountTenSeconds()
	{
		currentTime -= 10;
	}

	public void StopTimer()
	{
		isPlay = false;
	}

    private void Start()
    {
        currentTime = maxTime;
		initColor = firstDigit.color;
	}
    private void Update()
    {
        if (currentTime <= 0.0f)
        {
            isEndCont = false;
        }

        if (isPlay == true)
        {
            currentTime -= Time.deltaTime;

            displayTime = currentTime;
            UpdateDisplay(displayTime);
			EmphasizNumber(EmphasizTime);
		}
		else if (isPlay == false)
		{
			internalPower.SetActive(false);
			outsidePower.SetActive(true);
		}
		else
		{
			internalPower.SetActive(true);
			outsidePower.SetActive(false);
		}
	}

	public void Call_HideImage()
	{
		firstDigit.enabled = false;
		secondDigit.enabled = false;
		msecondDigit.enabled = false;
		dsecondDigit.enabled = false;
	}
	public void DownTimer()
	{
		currentTime -= downNum;
	}
}
