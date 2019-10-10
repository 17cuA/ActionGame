//--------------------------------------------------------
//ファイル名：AnimationUIManager.cs
//作成者　　：田嶋颯
//作成日　　：20190830
//
//連番で書き出されたスプライトをアニメーションUIとして表示させる
//--------------------------------------------------------
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimationUIManager : MonoBehaviour
{
	/// <summary>
	/// アニメーションUIを指定したフレームで指定したフレーム数停止させるクラス
	/// </summary>
    [System.Serializable]
    public class StopUIClass
    {
        public int StartFrame;
        public int StopFrame;
        public int CountFrame;
    }
	/// <summary>
	/// AnimationUIManagerの変数
	/// </summary>
    private string path;			//スプライトの場所を参照するパス
    public string spriteName;		//スプライトの名前
    private int totalSpriteCount;		//合計スプライト数
    private int nowSpriteCount;		//現在のスプライトの番号をカウント
	public int delayChangeFrame;	//スプライトを次に移すフレームまでに挟むフレーム
	private int delayFrameCount;	//スプライトを次に移すフレームまでに挟むフレームのカウントをする
	public int fadeInFrame;			//フェードインが完了するまでのフレーム
    public int fadeOutFrame;			//フェードアウトが完了するまでのフレーム
    private float currentRemainFadeInFrame;		//フェードインが完了するまでの残りのフレーム
	private float currentRemainFadeOutFrame;	//フェードアウトが完了するまでの残りのフレーム
	private Sprite[] sprites;		//読み込んだスプライトを格納
    private Color initColor;		//初期化用のカラー
    public Sprite defaultSprite;	//アニメーションUIを出していないときに出しておくスプライト
    public List<StopUIClass> stopUIs = null;    //アニメーションUIを指定したフレームで指定したフレーム数停止させるクラスの変数
	private bool isUnusedStopUI = false;		//stopUIsをを使っている場合true、使っていない場合falseになる変数
	public bool isStart;            //アニメーションを開始するか判定するフラグ
    public bool isLoop;				//アニメーションをループさせるか判定するフラグ
    public bool isLeave;            //アニメーションUIを最後のスプライトで止めるか判定をするフラグ
	public bool isInvisible;        //アニメーションUI再生中に非表示にするか判定をするフラグ
	public bool isInterruption;		//アニメーションUIの再生を中断させるか判定をするフラグ

	/// <summary>
	/// 常時処理
	/// </summary>
    private void Update()
    {
		if (delayChangeFrame <= delayFrameCount)
		{
			delayFrameCount = 0;
			if (isStart) StartAnimation();
		}
		else delayFrameCount++;
    }

	/// <summary>
	/// 初期化用メソッド
	/// </summary>
	public void Init()
	{
        //各スプライトを格納
        //デフォルトのスプライトをインスペクタで設定していなければデフォルトのスプライトを設定
        path = "Sprites/UI/AnimationUI/";
		if (defaultSprite == null)	defaultSprite = Resources.Load<Sprite>(string.Format("{0}{1}", path, "DefaultImage"));
        //表示するスプライト
        path += spriteName;
        totalSpriteCount = Resources.LoadAll<Sprite>(path).Length;
        sprites = new Sprite[totalSpriteCount];
		initColor = gameObject.GetComponent<Image>().color;
		delayFrameCount = 0;
		for (int i = 0; i < totalSpriteCount; i++)
        {
            sprites[i] = Resources.Load<Sprite>(string.Format("{0}/{1}_{2}", path, spriteName,i.ToString("D5")));
        }
        ResetUI();
	}

	/// <summary>
	/// リセット用メソッド
	/// </summary>
	private void ResetUI()
    {
		nowSpriteCount = 0;
		currentRemainFadeInFrame = fadeInFrame;
		currentRemainFadeOutFrame = fadeOutFrame;
		gameObject.GetComponent<Image>().sprite = defaultSprite;
		gameObject.GetComponent<Image>().color = initColor;
		for (int i = 0; i < stopUIs.Count; i++)
		{
			if (!isUnusedStopUI) isUnusedStopUI = true;
			stopUIs[i].CountFrame = 0;
		}
    }

	/// <summary>
	/// アニメーション開始用メソッド
	/// </summary>
	private void StartAnimation()
	{
		//アニメーション処理
		if (nowSpriteCount < totalSpriteCount && !isInterruption || isUnusedStopUI)
		{
			//指定フレームで指定フレーム分止められるようにする
			var isStopUI = StopUI();

			//UIを止めていない時の処理
			if (!isStopUI)
			{
				if (!isInvisible)
				{
					//パスで次のスプライトを指定して差し替える
					var sprite = sprites[nowSpriteCount];
					gameObject.GetComponent<Image>().sprite = sprite;
				}
				else if (gameObject.GetComponent<Image>().sprite != defaultSprite )
				{
					//デフォルトスプライトをセット
					gameObject.GetComponent<Image>().sprite = defaultSprite;
				}
				//指定フレームまでフェードイン処理
				if ((nowSpriteCount) < fadeInFrame)
				{
					FadeInUI();
				}
				//指定フレームを過ぎたらフェードアウト処理
				if ((nowSpriteCount) > (totalSpriteCount - fadeOutFrame - 1))
				{
					FadeOutUI();
				}
				nowSpriteCount++;
			}
		}
		else
		{
			if (!isLoop)
			{
				//再利用できるように元に戻しておく
				if (!isLeave || isInterruption)
				{
					isStart = false;
					isInterruption = false;
					ResetUI();
				} 
			}
			else
			{
				//リセットしてからもう一度呼び出す
				ResetUI();
				StartAnimation();
			}
		}
	}

	/// <summary>
	/// アニメーションUI停止用メソッド
	/// </summary>
	/// <returns></returns>
	private bool StopUI()
	{
		for (int i = 0; i < stopUIs.Count; i++)
		{
			//現在のスプライトが指定したフレームと同じでカウントが指定したフレーム数より少ないときUIを止める
			if (nowSpriteCount == stopUIs[i].StartFrame && stopUIs[i].CountFrame < stopUIs[i].StopFrame)
			{
				stopUIs[i].CountFrame++;
				if (stopUIs[stopUIs.Count - 1].CountFrame == stopUIs[stopUIs.Count - 1].StopFrame && isUnusedStopUI) isUnusedStopUI = false;
				return true;
			}
		}
		return false;
	}

	/// <summary>
	/// アニメーションUIをフェードインさせる
	/// </summary>
	private void FadeInUI()
	{
		//徐々にフェードイン
		currentRemainFadeInFrame--;
		float alpha = (fadeInFrame - currentRemainFadeInFrame) / fadeInFrame;
		var color = gameObject.GetComponent<Image>().color;
		color.a = alpha;
		gameObject.GetComponent<Image>().color = color;
	}

	/// <summary>
	/// アニメーションUIをフェードアウトさせる
	/// </summary>
	private void FadeOutUI()
    {
        //徐々にフェードアウト
        currentRemainFadeOutFrame--;
        float alpha = currentRemainFadeOutFrame / fadeOutFrame;
        var color = gameObject.GetComponent<Image>().color;
        color.a = alpha;
        gameObject.GetComponent<Image>().color = color;
    }
}
