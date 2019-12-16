using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScriptableObject_UI : ScriptableObject
{
	[SerializeField]
	private List<UIImageClass> ui = new List<UIImageClass>();
	public List<UIImageClass> Ui
	{
		get { return ui; }
		set { ui = value; }
	}
}

// imageの設定用
[System.Serializable]
public class UIImageClass
{
	public string name = "no name";
	public Sprite image;
    [SerializeField]
	public GameObject gameObj;
	//public Image obj;

	//	private float _imageSizeWidth;
	//	public float ImageSizeWidth
	//	{
	//		get { return _imageSizeWidth; }
	//#if UNITY_EDITOR
	//		set { _imageSizeWidth = Mathf.Clamp(value, 0, float.MaxValue); }
	//#endif
	//	//}
	//	private float _imageSizeHeight;
	//	public float ImageSizeHeight
	//	{
	//		get { return _imageSizeHeight; }
	//#if UNITY_EDITOR
	//		set { _imageSizeHeight = Mathf.Clamp(value, 0, float.MaxValue); }
	//#endif
	//	}
	public void Copy(UIImageClass US)
    {
        if (US != null)
        {
            name = US.name;
            image = US.image;
            gameObj = US.gameObj;
            //ImageSizeHeight = US.ImageSizeHeight;
            //ImageSizeWidth = US.ImageSizeWidth;
        }
    }
}
