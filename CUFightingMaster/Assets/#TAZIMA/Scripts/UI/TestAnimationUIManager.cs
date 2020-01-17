using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestAnimationUIManager : MonoBehaviour
{
	/// <summary>
	/// AnimationUIManagerの変数
	/// </summary>
	private string path;					//スプライトの場所を参照するパス
	private Sprite[] sprites;			//読み込んだスプライトを格納
	private int totalSpriteCount;	//合計スプライト数
	private int nowSpriteCount;	//現在のスプライトの番号をカウント

	public string spriteName;		//スプライトの名前
	public Sprite defaultSprite;	//アニメーションUIを出していないときに出しておくスプライト

	private void Awake()
	{

	}
	private void Update()
    {
		StartAnimation();
	}

	/// <summary>
	/// 初期化用メソッド
	/// </summary>
	public void Init()
	{
		//各スプライトを格納
		//デフォルトのスプライトをインスペクタで設定していなければデフォルトのスプライトを設定
		path = "Sprites/UI/AnimationUI/";
		if (defaultSprite == null) defaultSprite = Resources.Load<Sprite>(string.Format("{0}{1}", path, "DefaultImage"));
		//表示するスプライト
		path += spriteName;
		totalSpriteCount = Resources.LoadAll<Sprite>(path).Length;
		sprites = new Sprite[totalSpriteCount];
		for (int i = 0; i < totalSpriteCount; i++)
		{
			sprites[i] = Resources.Load<Sprite>(string.Format("{0}/{1}_{2}", path, spriteName, i.ToString("D5")));
		}
		ResetUI();
	}

	/// <summary>
	/// リセット用メソッド
	/// </summary>
	private void ResetUI()
	{
		nowSpriteCount = 0;
		gameObject.GetComponent<Image>().sprite = defaultSprite;
	}

	/// <summary>
	/// アニメーション開始用メソッド
	/// </summary>
	private void StartAnimation()
	{
		//アニメーション処理
		if (nowSpriteCount < totalSpriteCount)
		{
			//パスで次のスプライトを指定して差し替える
			var sprite = sprites[nowSpriteCount];
			gameObject.GetComponent<Image>().sprite = sprite;
			nowSpriteCount++;
		}
		else
		{
			//再利用できるように元に戻しておく
			ResetUI();
			StartAnimation();
		}
	}

	public static void ChangeAnimationEventHandler(PlayerContext context)
	{

	}

	public interface AnimUIState
	{
		AnimUIState DoPlayEvent();
		AnimUIState DoStopEvent();
		AnimUIState DoResetEvent();
	}

	public class UIContext
	{

	}

	public class PlayerContext
	{
		//アニメーションの状態
		private AnimUIState state = null;

		public UIContext UIContext { get; set; } = null;

		public PlayerContext(UIContext someUIContext)
		{
			this.UIContext = someUIContext;
			if (state == null)
			{
				//state = new state;
			}
		}

		public AnimUIState GetState()
		{
			return this.state;
		}

		public void DoPlay()
		{
			this.state = this.state.DoPlayEvent();
		}

		public void DoStop()
		{
			this.state = this.state.DoStopEvent();
		}

		public void DoReset()
		{
			this.state = this.state.DoResetEvent();
		}
	}
}
