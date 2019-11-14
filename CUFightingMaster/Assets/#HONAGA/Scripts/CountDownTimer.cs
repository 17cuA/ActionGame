//---------------------------------------
// カウントダウンをするタイマー
//---------------------------------------
// 作成者:宮島だったもの
// 作成日:2019
//--------------------------------------
// 更新履歴
// 2019.07.18 コメント追記 by takano
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
	private int displayTime;

	public bool isPlay = false;
    bool isEndCont = false;

    public Image firstDigit;	//一桁目のimage
    public Image secondDigit;	//二桁目のimage

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
        UpdateDisplay((int)currentTime);

		firstDigit.color = initColor;
		secondDigit.color = initColor;
    }

	/// <summary>
	/// カウントダウンを開始・終了する、引数にtrueだと開始、引数にfalseだと停止
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
    private void UpdateDisplay(int count)
    {
		var castCount = Mathf.Clamp(count,0,99);
        firstDigit.sprite = numSprite[castCount % 10];
        secondDigit.sprite = numSprite[castCount / 10];
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

            displayTime = (int)currentTime;
            UpdateDisplay(displayTime);
			EmphasizNumber(EmphasizTime);
		}
    }
}
