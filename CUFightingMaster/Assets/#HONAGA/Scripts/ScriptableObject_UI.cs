using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScriptableObject_UI : ScriptableObject
{
	//[SerializeField]
	//private float _imageSizeWidth;
	//[SerializeField]
	//private float _imageSizeHeight;
	[SerializeField]
	private List<UIImageClass> us = new List<UIImageClass>();
	public List<UIImageClass> Us
	{
		get { return us; }
#if UNITY_EDITOR
		set { us = value; }
#endif
	}
}

public class UIImageClass
{
	private float _imageSizeWidth;
	public float ImageSizeWidth
	{
		get { return _imageSizeWidth; }
#if UNITY_EDITOR
		set { _imageSizeWidth = Mathf.Clamp(value, 0, float.MaxValue); }
#endif
	}
	private float _imageSizeHeight;
	public float ImageSizeHeight
	{
		get { return _imageSizeHeight; }
#if UNITY_EDITOR
		set { _imageSizeHeight = Mathf.Clamp(value, 0, float.MaxValue); }
#endif
	}
	string name;
	Texture2D image;
	//Image image;
}