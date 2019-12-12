using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectObjectImage
{
	private RectTransform characterSelectObjectsTransform;
	
	public CharacterSelectObjectImage(ref GameObject _obj,Image _image)
	{
		if(_obj.GetComponent<Image>() == null)
		{
			AddImage(ref _obj, _image);
		}
		else
		{
			RectConversion(ref _obj, _image);
		}
	}

	public RectTransform GetRectTransform()
	{
		return characterSelectObjectsTransform;
	}

	void AddImage(ref GameObject _obj ,Image _image)
	{
		_obj.AddComponent<Image>();
		RectConversion(ref _obj, _image);
	}

	void RectConversion(ref GameObject _obj ,Image _image)
	{
		// 画像のサイズを取得
		Vector2 temp = _image.sprite.bounds.size;
		// xを１として、yの比率を求める
		temp.y /= temp.x;
		temp.x /= temp.x;
		// 正規化
		temp = temp.normalized;
		// 画像の変更
		_obj.GetComponent<Image>().sprite = _image.sprite;
		// _objのRectTransform.rectの横幅、長さに上の計算で出したtempの値をかける
		((_obj.transform) as RectTransform).sizeDelta = new Vector2((_obj.transform as RectTransform).rect.width*temp.x,(_obj.transform as RectTransform).rect.height*temp.y);
	}
}
