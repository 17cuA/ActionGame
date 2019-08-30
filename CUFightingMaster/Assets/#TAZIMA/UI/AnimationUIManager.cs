//アニメーションUI表示用スクリプト
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using UnityEngine.UI;

public class AnimationUIManager : MonoBehaviour
{
	private string path;
	public string filePath;
	public string spriteName;
	private int totalSpriteCount;
	private int nowSpriteCount = 0;
    void Start()
    {
		path = "Sprites/UI/AnimationUI/ROUND ANIMATION/" + filePath;
		totalSpriteCount = Directory.GetFiles("Assets/Resources/" + path, "*", SearchOption.TopDirectoryOnly).Length / 2;
	}

    void Update()
    {
        if (nowSpriteCount < totalSpriteCount)
		{
			var sprite = Resources.Load<Sprite>(string.Format("{0}/{1}_{2}", path, spriteName, nowSpriteCount.ToString("D5")));
			//Debug.Log(string.Format("{0}/{1}_{2}", path, spriteName, nowSpriteCount.ToString("D5")));
			gameObject.GetComponent<SpriteRenderer>().sprite = sprite;
			nowSpriteCount++;
		}
		else
		{
			Destroy(gameObject);
		}
    }
}
