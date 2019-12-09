using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerManager_All : MonoBehaviour
{
	public Image firstDigit;
	public Image secondDigit;
	public Image msecondDigit;
	public Image dsecondDigit;
	public Sprite[] numSprite=new Sprite[10];

	public void Display(float count)
	{
		firstDigit.sprite = numSprite[(int)(count % 10)];
		secondDigit.sprite = numSprite[(int)(count / 10)];
		msecondDigit.sprite = numSprite[(int)(count * 10 / 1 % 10)];
		dsecondDigit.sprite = numSprite[(int)(count * 100 / 1 % 10)];
	}
}
