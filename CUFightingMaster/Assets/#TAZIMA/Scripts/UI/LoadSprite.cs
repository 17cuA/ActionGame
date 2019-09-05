using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CUEngine.Pattern;

public class LoadSprite : SingletonMono<LoadSprite>
{
	public string path;
	public string[] spriteNames;
	public Dictionary<string, Sprite[]> sprites = new Dictionary<string, Sprite[]>();
	public void Init()
	{
				for (int i = 0; i < spriteNames.Length; i++)
		{
            //各スプライトを読み込む
            var spriteCount  = Resources.LoadAll(string.Format("{0}/{1}", path, spriteNames[i])).Length / 2;
			var loadSprites = new Sprite[spriteCount];
			for (int j = 0; j < spriteCount; j++)
			{
				Debug.Log(string.Format("{0}/{1}/{1}_{2}", path, spriteNames[i], j.ToString("D5")));
				loadSprites[j] = Resources.Load<Sprite>(string.Format("{0}/{1}/{1}_{2}", path, spriteNames[i], j.ToString("D5")));
			}
			Debug.Log(string.Format("{0}/{1}/{1}  読み込み完了", path, spriteNames[i]));
			sprites.Add(spriteNames[i],loadSprites);
		}
	}
	public Sprite[] GetSprites(string name)
	{
		return sprites[name];
	}
}
