using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RectTransformEx
{
	public static RectTransform ToRectTransform(this Transform t)
	{
		return t as RectTransform;
	}
}
