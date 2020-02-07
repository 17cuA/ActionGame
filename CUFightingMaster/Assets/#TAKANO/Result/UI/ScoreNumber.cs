using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreNumber : MonoBehaviour
{
    public void SetImage(Sprite _sprite)
	{
		var image = gameObject.GetComponent<Image>();
		image.sprite = _sprite;
	}
}

public static class CharExt
{
	public static int ToInt(this char self)
	{
		return self - '0';
	}
}