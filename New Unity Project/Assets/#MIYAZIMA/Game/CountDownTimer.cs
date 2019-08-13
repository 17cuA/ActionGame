//---------------------------------------
// カウントダウンをするタイマー
//---------------------------------------
// 作成者:宮島
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
    public float maxTime;   //初期値
    public float currentTime; //現在値

    public int displayTime;

    bool isPlayCountDown = false;

    public Image firstDigit;    //一桁目のimage
    public Image secondDigit;   //二桁目のimage

    public Sprite[] numSprite = new Sprite[10];

    /// <summary>
    /// タイマーを初期値へ戻す
    /// </summary>
    public void ResetTimer()
    {
        isPlayCountDown = false;
        currentTime = maxTime;
        UpdateDisplay((int)currentTime);
    }

	/// <summary>
	/// カウントダウンを開始・終了する
	/// </summary>
	public void PlayCountDown(bool isPlay)
	{
		if (isPlay) isPlayCountDown = true;
		else	isPlayCountDown = false;
	}

	/// <summary>
	/// カウントダウンの終了を通知
	/// </summary>
	/// <returns></returns>
	public bool isEndCountDown()
    {
        return isPlayCountDown;
    }
    /// <summary>
    /// タイマー表示の更新
    /// </summary>
    /// <param name="count"></param>
    private void UpdateDisplay(int count)
    {
        firstDigit.sprite = numSprite[count % 10];
        secondDigit.sprite = numSprite[count / 10];
    }

    private void Start()
    {
        currentTime = maxTime;
    }
    private void Update()
    {
        if (currentTime <= 0.0f)
        {
            isPlayCountDown = false;
        }

        if (isPlayCountDown == true)
        {
            currentTime -= Time.deltaTime;

            displayTime = (int)currentTime;
            UpdateDisplay(displayTime);
        }
    }
}
