using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAsset
{
	// UIのプロパティ
	[SerializeField]
	#region // オブジェクト関係
	private Image image;
	public Image Image
	{
		get { return image; }
		set { image = value; }
	}
	#endregion

	#region        // RectTransform関係
	private RectTransform rectTransform;
	public RectTransform RectTransform
	{
		get { return rectTransform; }
		set { rectTransform = value; }
	}
	#endregion

	#region // 画像比率を保つかどうか
	private int isScale;
	public int IsScale
	{
		get { return isScale; }
		set { isScale = value; }
	}
	#endregion

	#region // 画像比率を保ったままスケールを変更する
	private Vector3 scaleDef;
	public Vector3 ScaleDef
	{
		get { return scaleDef; }
		set { scaleDef = value; }
	}
	private float scale = 1.0f;
	public float Scale
	{
		get { return scale; }
		set { scale = value; }
	}
	#endregion
}