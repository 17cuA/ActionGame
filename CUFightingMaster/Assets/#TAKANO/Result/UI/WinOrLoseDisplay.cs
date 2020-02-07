using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinOrLoseDisplay : MonoBehaviour
{
	[SerializeField]Sprite winSprite;
	[SerializeField]Sprite loseSprite;

	[SerializeField] Image Image_1;
	[SerializeField] Image Image_2;

	public void P1WinDisplay()
	{
		Image_1.sprite = winSprite;
		Image_2.sprite = loseSprite;
	}
	public void P2WinDisplay()
	{
		Image_1.sprite = loseSprite;
		Image_2.sprite = winSprite;
	}
}
