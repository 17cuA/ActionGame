using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Characterselect_Timer : MonoBehaviour
{
	public float maxTime;   //初期値
	public float currentTime; //現在値

	public int displayTime;

	bool isPlayCountDown = false;
	public bool IsPlayCountDown
	{
		get { return isPlayCountDown; }
	}

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
		isPlayCountDown = true;
		currentTime = maxTime;
	}
	private void Update()
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
