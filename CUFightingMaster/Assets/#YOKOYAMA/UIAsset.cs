using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAsset
{
	// UIのプロパティ
	[SerializeField]
	#region // オブジェクト関係
	private Sprite sprite;                // 画像
	public Sprite Sprite
	{
		get { return sprite; }
		set { sprite = value; }
	}

	private string nameSprite;			   // 画像の名前
	public string NameSprite
	{
		get { return nameSprite; }
		set { nameSprite = value; }
	}

	private Color color;                   // 画像の色
	public Color Color
	{
		get { return color; }
		set { color = value; }
	}
	#endregion

	#region		// RectTransform関係
	private Vector3 rectPos;     // 座標(RectTransform)
	public Vector3 RectPos
	{
		get { return rectPos; }
		set { rectPos = value; }
	}

	private Vector2 pivot;           // pivot座標
	public Vector2 Pivot
	{
		get { return pivot; }
		set { pivot = value; }
	}

	private Vector2 anchorMax;   // Canvasからみた座標(最低値)
	public Vector2 AnchorMax
	{
		get { return anchorMax; }
		set { anchorMax = value; }
	}

	private Vector2 anchorMin;   // Canvasからみた座標(最大値)
	public Vector2 AnchorMin
	{
		get { return anchorMin; }
		set { anchorMin = value; }
	}

	private Vector2 scale;           // Canvasからみた座標(最大値)
	public Vector2 Scale
	{
		get { return scale; }
		set { scale = value; }
	}
	#endregion
}