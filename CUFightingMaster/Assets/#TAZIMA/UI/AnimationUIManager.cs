//アニメーションUI表示用スクリプト
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class AnimationUIManager : MonoBehaviour
{
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
    private Color initColor;
	private Sprite[] sprites;
    public List<StopUIClass> stopUIs = null;
    

    //初期化処理
	private void Start()
    {
		Init();
    }

    private void Update()
    {
        //アニメーション処理
        if (nowSpriteCount < totalSpriteCount)
		{
   //         //パスで次のスプライトを指定して差し替える
			//var sprite = Resources.Load<Sprite>(string.Format("{0}/{1}_{2}", path, spriteName, nowSpriteCount.ToString("D5")));
			//gameObject.GetComponent<Image>().sprite = sprite;
            //指定フレームで指定フレーム分止められるようにする
            var isStopUI = false;

            //UIを止めていない時の処理
            if (!isStopUI)
            {
                //パスで次のスプライトを指定して差し替える
                var sprite = Resources.Load<Sprite>(string.Format("{0}/{1}_{2}", path, spriteName, nowSpriteCount.ToString("D5")));
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
            //再利用できるように元に戻しておく
            gameObject.SetActive(false);
            gameObject.GetComponent<Image>().color = initColor;
            ResetUI();
		}
    }

	private void Init()
	{
		//スプライトが保存されている場所のパス
		path = "Sprites/UI/AnimationUI/ROUND ANIMATION/" + spriteName;
		totalSpriteCount = Directory.GetFiles("Assets/Resources/" + path, "*", SearchOption.TopDirectoryOnly).Length / 2;
		initColor = gameObject.GetComponent<Image>().color;
		////スプライト格納
		//for (int i = 0;i < totalSpriteCount;i++)
		//{
		//	Debug.Log(string.Format("{0}/{1}_{2}", path, spriteName, i.ToString("D5")));
		//	sprites[i] = Resources.Load<Sprite>(string.Format("{0}/{1}_{2}", path, spriteName, i.ToString("D5")));
		//}
		ResetUI();
	}
	private void ResetUI()
    {
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
