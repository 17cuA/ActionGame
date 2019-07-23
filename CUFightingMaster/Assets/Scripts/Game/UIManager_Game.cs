/*	UIManager_Game.cs
 *	GameシーンでのUI管理スクリプト
 *	製作者：宮島幸大
 *	制作日：2019/06/13
 *	----------更新----------
 *	2019/06/18：マネージャとUIの処理を分離化
 *	2019/07/03：キャラクターの名前をTextからImageに変更
 *	2019/07/04：タイマーをTextからImageに変更
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class UIManager_Game : MonoBehaviour
{
	//	マネージャー関係
	public GameObject managerObject;
	Manager_Game mG;

	public Canvas timerCV;
	TimerManager_All tMA;
	//	フラグ関係
	public bool active_UI;

	//	UI関係
	public Canvas[] round;				//	最大ラウンドを表示する(Canvas)
	public Image[] getRound1P;		//	1Pがゲットしたラウンド数を表示する(Image)
	public Image[] getRound2P;		//	2Pがゲットしたラウンド数を表示する(Image)
	public Sprite getRound;				//	ラウンドを取得したときに変える画像(Sprite)
	public Image[] playerIcon;			//	プレイヤーが選んだキャラクターのアイコンを表示(Image)
	public Image[] playerName;			//	プレイヤーが選んだキャラクターの名前を表示(Text)
	public Slider[] playerHP;				//	プレイヤーのHPバー(Slider)
	public Slider[] playerStamina;		//	プレイヤーのスタミナバー(Slider)
	public Slider[] playerDeathblow;	//	プレイヤーの必殺ゲージバー(Slider)

	//	ゲーム(ラウンド)開始時のみ使用
	public Image roundStert;			//	ラウンド開始時画像を表示するためのイメージ(Image)
	public Sprite[] roundstertSprite;	//	現在のラウンド数を表示するための画像(Sprite)
	public Sprite fight;						//	Fight画像(Sprite)

	//	ラウンド終了時のみ使用
	public Image winText;					//	〇PWin等を表示するためのイメージ(Image)
	public Sprite[] winSprites;			//	〇PWin等のsprite

	//	デバッグでのみ使用
	public Image[] debugHit;            //

	// 三沢が追加
	public CinemachineVirtualCamera battleCamera;	// BattleCameraのPriorityを変更する(7月4日に間に合わせるための応急処置)
	private int pMax;												// Priorityの最大値
	public bool call_Once;											// 一度だけ呼び出す用
	public static UIManager_Game instance;				// CinemaControllにて使用(RoundStartに条件追加)
	public GameObject test;										// UIを格納
	public GameObject test02;									// VirtualCameraを格納

	void Awake()
	{
		instance = FindObjectOfType<UIManager_Game>();
	}
	//	--------------------
	//	スタート
	//	--------------------
	void Start()
	{
		pMax = 200;
		mG = managerObject.GetComponent<Manager_Game>();    //mGにGameマネージャーをロード
		tMA = timerCV.GetComponent<TimerManager_All>();
		winText.enabled = false;        //勝利テキスト(Image)を非表示にする
		//
		active_UI = false;
		call_Once = false;

		//	最大ラウンド数によるラウンドカウントの表示数変更
		switch (mG.roundMax)
		{
			case 1:
				round[0].enabled = true;
				round[1].enabled = false;
				round[2].enabled = false;
				break;
			case 2:
				round[0].enabled = true;
				round[1].enabled = true;
				round[2].enabled = false;
				break;
			case 3:
				round[0].enabled = true;
				round[1].enabled = true;
				round[2].enabled = true;
				break;
		}

		//	参照したplayerStatusをもとにUIを初期設定
		for (int i = 0; i < 2; i++)
		{
			playerIcon[i].sprite = mG.playerStatus[i].characterSprite;			//	アイコン(Image)にMGのPSにロードしたキャラクターのイメージをロードする
			playerName[i].sprite = mG.playerStatus[i].charaxterName;			//	プレイヤーネーム(Text)にMGのPSにロードしたキャラクターのネームをロードする
			playerHP[i].maxValue = mG.playerStatus[i].hpMax;					//	HPバーの最大値をMGにロードしたPSのHP最大値をロードする
			playerStamina[i].maxValue = mG.playerStatus[i].staminaMax;	//	STバーの最大値をMGにロードしたPSのST最大値をロードする
			playerDeathblow[i].maxValue = 100;										//	必殺ゲージの最大値を100にする
		}
	}

	//	--------------------
	//	アップデート
	//	--------------------
	void Update()
	{
		if (call_Once)
		{
			test.gameObject.SetActive(true);
			test02.gameObject.SetActive(false);
			//	ラウンド開始前
			//if ((mG.activeGame == false) && (mG.win == 0) && (roundStert.enabled == false) && (mG.mMM.isFadeIn == false)&&(mG.winTextFlag==false))
			//{
			//	RoundStart();
			//}

			tMA.Display((int)mG.timer);
			//	プレイヤーが2人なので
			for (int i = 0; i < 2; i++)
			{
				playerHP[i].value = mG.playerStatus[i].hp;
				playerStamina[i].value = mG.playerStatus[i].stamina;
				playerDeathblow[i].value = mG.playerStatus[i].Deathblow;

				//	デバッグのHit表示
				if (mG.playerStatus[i].Hit == true)
				{
					debugHit[i].enabled = true;
				}
				else
				{
					debugHit[i].enabled = false;
				}
			}

			//	ラウンド取得の表示
			if (mG.winTextFlag)
			{
				winText.sprite = winSprites[mG.win];
				winText.enabled = true;
			}
			else
			{
				winText.enabled = false;
			}
			if (mG.getRound[0] > 0)
				getRound1P[mG.getRound[0] - 1].sprite = getRound;
			if (mG.getRound[1] > 0)
				getRound2P[mG.getRound[1] - 1].sprite = getRound;
		}
	}

	//	--------------------
	//	ラウンドスタート
	//	--------------------
	public void RoundStart()
	{
		Debug.Log("RoundStart起動");
		roundStert.sprite = roundstertSprite[mG.round];
		roundStert.enabled = true;
		Invoke("Fight", 1.5f);
		battleCamera.Priority = pMax;
	}

	//	--------------------
	//	FIGHTを表示
	//	作成者：宮島 幸大
	//	--------------------
	void Fight()
	{
		roundStert.sprite = fight;
		Invoke("GameActive", 1.5f);

	}

	//	--------------------
	//	ゲームをアクティブにする
	//	作成者：宮島 幸大
	//	--------------------
	void GameActive()
	{
		Debug.Log("GameActive起動");
		roundStert.enabled = false;
		mG.activeGame = true;
	}
}
//	write by Miyajima Kodai