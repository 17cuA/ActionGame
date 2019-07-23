using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerManager_All : MonoBehaviour
{
	public Image firstDigit;
	public Image secondDigit;

	public Sprite[] numSprite=new Sprite[10];

	public void Display(int count)
	{
		firstDigit.sprite = numSprite[count % 10];
		secondDigit.sprite = numSprite[count / 10];
	}
}
