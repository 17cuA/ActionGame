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
	public string spriteName;
	private int totalSpriteCount;
	private int nowSpriteCount;
	public int FadeOutFrame;
	private float currentRemainFrame;
    private Color initColor;
	private void Start()
    {
		path = "Sprites/UI/AnimationUI/ROUND ANIMATION/" + spriteName;
		totalSpriteCount = Directory.GetFiles("Assets/Resources/" + path, "*", SearchOption.TopDirectoryOnly).Length / 2;
        initColor = gameObject.GetComponent<Image>().color;
        Init();
    }

    private void Update()
    {
        if (nowSpriteCount < totalSpriteCount)
		{
			var sprite = Resources.Load<Sprite>(string.Format("{0}/{1}_{2}", path, spriteName, nowSpriteCount.ToString("D5")));
			gameObject.GetComponent<Image>().sprite = sprite;
			if (nowSpriteCount > (totalSpriteCount - FadeOutFrame - 1))
			{
				//徐々にフェードアウト
				currentRemainFrame--;
				float alpha = currentRemainFrame / FadeOutFrame;
				var color = gameObject.GetComponent<Image>().color;
				color.a = alpha;
				gameObject.GetComponent<Image>().color = color;
			}
			nowSpriteCount++;
		}
		else
		{
            gameObject.SetActive(false);
            gameObject.GetComponent<Image>().color = initColor;
            Init();
		}
    }
    private void Init()
    {
        nowSpriteCount = 0;
        currentRemainFrame = FadeOutFrame;
    }
}
