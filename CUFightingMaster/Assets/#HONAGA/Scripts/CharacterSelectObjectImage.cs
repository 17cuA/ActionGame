using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectObjectImage
{
	private RectTransform characterSelectObjectsTransform;

	// コンストラクタ
	public CharacterSelectObjectImage(ref Image _obj ,Texture2D _image)
	{
        if (_obj != null)
        {
            if (_obj.GetComponent<Image>() == null)
            {
                AddImage(ref _obj, _image);
            }
            else
            {
                RectConversion(ref _obj, _image);
            }
        }
        else
        {
            Debug.LogError("オブジェクトがないよ");
        }
	}

	public RectTransform GetRectTransform()
	{
		return characterSelectObjectsTransform;
	}
    // 対象オブジェクトにImageがついていない時、つける
	void AddImage(ref Image _obj ,Texture2D _image)
	{
		RectConversion(ref _obj, _image);
	}
    // 画像サイズを一定にしたいため、画像の比率のみ変更し、画像を貼り替える
	void RectConversion(ref Image _obj ,Texture2D _image)
	{
		// 画像のサイズを取得
		Vector2 temp = _obj.sprite.bounds.size;
		// xを１として、yの比率を求める
		temp.y /= temp.x;
		temp.x /= temp.x;
		// 正規化
		temp = temp.normalized;
        // 画像の変更
        _obj.sprite = ConvertToSpriteExtensiton.ConvertToSprite(_image);
		// _objのRectTransform.rectの横幅、長さに上の計算で出したtempの値をかける
		((_obj.transform) as RectTransform).sizeDelta = new Vector2((_obj.transform as RectTransform).rect.width*temp.x,(_obj.transform as RectTransform).rect.height*temp.y);
	}
}

// Texture2DからSpriteへの変化、便利なので拡張メソッドの定義
public static class ConvertToSpriteExtensiton
{
    public static Sprite ConvertToSprite(this Texture2D texture)
    {
        return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
    }
}