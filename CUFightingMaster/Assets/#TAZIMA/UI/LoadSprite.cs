using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CUEngine.Pattern;

public class LoadSprite : SingletonMono<LoadSprite>
{
	private string path;
	public string[] spriteNames;
	public Dictionary<string, Sprite[]> sprites = new Dictionary<string, Sprite[]>();
	private void Start()
	{
		for (int i = 0; i < spriteNames.Length; i++)
		{
			//各スプライトを読み込む
			var spriteCount  = Resources.LoadAll(path + spriteNames[i]).Length / 2;
			//for (int j = 0;)
		}
	}
}
