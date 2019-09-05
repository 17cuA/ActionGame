//アニメーションUI表示用スクリプト
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class AnimationUIManager : MonoBehaviour
{
	//UIのアニメーションを一時的に止めるためのクラス
    [System.Serializable]
    public class StopUIClass
    {
        public int StartFrame;
        public int StopFrame;
        public int CountFrame;
    }
    private string path;
    public string spriteName;
    private int totalSpriteCount;
    private int nowSpriteCount;
    public int FadeOutFrame;
    private float currentRemainFrame;
	private Sprite[] sprites;
    private Color initColor;
    private Sprite defaultSprite;
    public List<StopUIClass> stopUIs = null;
    public bool isStart;
    public bool isLeave;

    private void Update()
    {
        if (isStart)
        {
            StartAnimation();
        }
    }

	public void Init()
	{
        //各スプライトを格納
        //デフォルトのスプライト
        path = "Sprites/UI/AnimationUI/";
        defaultSprite = Resources.Load<Sprite>(string.Format("{0}{1}", path, "DefaultImage"));
        //表示するスプライト
        path += spriteName;
        totalSpriteCount = Resources.LoadAll(path).Length / 2;
        sprites = new Sprite[totalSpriteCount];
        for (int i = 0; i < totalSpriteCount; i++)
        {
            sprites[i] = Resources.Load<Sprite>(string.Format("{0}/{1}_{2}", path, spriteName,i.ToString("D5")));
        }
        initColor = gameObject.GetComponent<Image>().color;
        ResetUI();
	}
	private void ResetUI()
    {
        isStart = false;
        nowSpriteCount = 0;
        currentRemainFrame = FadeOutFrame;
        if (stopUIs.Count != 0)
        {
            for (int i = 0; i < stopUIs.Count; i++)
            {
                stopUIs[i].CountFrame = 0;
            }
        }
    }
    private void StartAnimation()
    {
        //アニメーション処理
        if (nowSpriteCount < totalSpriteCount)
        {
            //指定フレームで指定フレーム分止められるようにする
            var isStopUI = false;

            //UIを止めていない時の処理
            if (!isStopUI)
            {
                //パスで次のスプライトを指定して差し替える
                var sprite = sprites[nowSpriteCount];
                gameObject.GetComponent<Image>().sprite = sprite;
                //指定フレームを過ぎたらフェードアウト処理
                if (nowSpriteCount > (totalSpriteCount - FadeOutFrame - 1))
                {
                    FadeOutUI();
                }
                nowSpriteCount++;
            }
        }
        else
        {
            if (!isLeave)
            {
                //再利用できるように元に戻しておく
                gameObject.GetComponent<Image>().sprite = defaultSprite;
                gameObject.GetComponent<Image>().color = initColor;
                ResetUI();
            }
        }
    }
	private bool StopUI()
	{
		for (int i = 0; i < stopUIs.Count; i++)
		{
			//現在のスプライトが指定したフレームと同じでカウントが指定したフレーム数より少ないときUIを止める
			if (nowSpriteCount == stopUIs[i].StartFrame && stopUIs[i].CountFrame < stopUIs[i].StopFrame)
			{
				stopUIs[i].CountFrame++;
				return true;
			}
		}
		return false;
	}
    private void FadeOutUI()
    {
        //徐々にフェードアウト
        currentRemainFrame--;
        float alpha = currentRemainFrame / FadeOutFrame;
        var color = gameObject.GetComponent<Image>().color;
        color.a = alpha;
        gameObject.GetComponent<Image>().color = color;
    }
}
