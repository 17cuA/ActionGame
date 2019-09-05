using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchInputManagerInfo : MonoBehaviour
{
	void Start()
	{
		// InputManagerで設定された名前を一覧表示
		foreach (var config in InputManagerInfo.Config)
		{
			Debug.Log(config.name);
		}
	}
}
