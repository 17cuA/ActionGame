using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Characterselect_Timer
{
	public Sprite[] timerSprites = new Sprite[10];

	public float maxTime;
	public float currentTime;
	public int displayTime;

	public bool startFlag = false;
	private bool isPlayCountDown = false;
	public bool IsPlayCountDown { get { return isPlayCountDown; } }

	public Image[] firstDigit;    //一桁目のimage
	public Image[] secondDigit;   //二桁目のimage

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
	public void PlayCountDown(bool _isPlay)
	{
		if (_isPlay) isPlayCountDown = true;
		else isPlayCountDown = false;
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
	/// タイマーの画像を変更
	/// </summary>
	/// <param name="_count"></param>
	private void UpdateDisplay(int _count)
	{
		// 10の位の数字を変更
		for (int i = 0; i < firstDigit.Length; i++)
		{
			firstDigit[i].sprite = timerSprites[_count / 10];
		}
		// 1の位の数字を変更
		for (int i = 0; i < secondDigit.Length; i++)
		{
			secondDigit[i].sprite = timerSprites[_count % 10];
		}
	}

	public void TimerStart()
	{
		startFlag = true;
		isPlayCountDown = true;
		currentTime = maxTime;
	}
	public void TimerUpdate()
	{
		PlayCountDown(isPlayCountDown);
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
