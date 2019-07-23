/*	Manager_Game.cs
 *	Gameシーンでの中央管理スクリプト
 *	製作者：宮島幸大
 *	制作日：2019/06/13
 *	 ----------更新----------
 *	 2019/06/18：UIとの処理を分離化
 *	 2019/07/02：Soundが追加
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager_Game : MonoBehaviour
{
	public GameObject UIManagerObject;
	UIManager_Game UIMG;

	//	画面のマスク関係
	public GameObject maskOb;           //	マスク用のイメージが入ってるオブジェクト
	public MovingMaskManager mMM;   //	マスク用スクリプトをロードするため

	//	キャラクターの表示関係
	public GameObject[] characterOb = new GameObject[6];										// キャラクターのオブジェクト
	public GameObject[] playerOb = new GameObject[2];											//	Playerのオブジェクト
	public CharacterStatus_Game[] playerStatus = new CharacterStatus_Game[2];		//	Playerのステータスを管理する
	public GameObject[] targetPoint = new GameObject[2];										//	生成するターゲットポイント

	//	フラグ関係
	public bool activeGame;					// ゲームがアクティブか
	[SerializeField]
	private float timerMax;					// 制限時間の最大値
	public float timer;							// 残りの制限時間
	public int roundMax;						// 何ラウンドで勝利か
	public int round;								// 現在何ラウンド目か
	public int[] getRound;						// 何ラウンドとったか
	public int win;								// 勝利条件 0:ノーマル,1:1Pの勝ち,2:2Pの勝ち,3:引き分け
	private bool operationReception;		// 操作を受け付ける
	public bool finalDecision;					// 決着フラグ
	public bool winTextFlag;

	// 三沢が追加(応急処置)
	public GameObject player1;
	public GameObject player2;
	public static Manager_Game instance;

	//	--------------------
	//	アウェイク
	//	--------------------
	void Awake()
	{
		//	プレイヤーが2人いるので
		for (int i = 0; i < 2; i++)
		{
			playerOb[i] = Instantiate(characterOb[0], targetPoint[i].transform.position, Quaternion.identity);
			playerStatus[i] = playerOb[i].GetComponent<CharacterStatus_Game>();
			finalDecision = false;
		}
		instance = FindObjectOfType<Manager_Game>();
	}

	//	--------------------
	//	スタート
	//	--------------------
	void Start()
	{
		UIMG = UIManagerObject.GetComponent<UIManager_Game>();

		//	マスク用のデータロード
		mMM = maskOb.GetComponent<MovingMaskManager>();

		// バトル時間
		timerMax = 30;
		timer = timerMax;

		activeGame = false;
		winTextFlag = false;

		//	BGM周り
		// BGMロード
		Sound.LoadBgm("Bgm01", "Bgm01");
		// BGM再生　(アクセスキー, ボリューム, ピッチ)
		Sound.PlayBgm("Bgm01", 0.4f, 1);
	}

	//	--------------------
	//	アップデート
	//	--------------------
	void Update()
	{
		//
		if (activeGame)
		{
			//	タイマーが0以上か
			if (timer >= 0)
			{
				timer -= Time.deltaTime;
			}
			else
			{
				activeGame = false;
				TimeUp();
			}

			//	勝利判定
			if ((playerStatus[0].hp <= 0) && (playerStatus[1].hp <= 0)) //	同時にHPが0
			{
				activeGame = false;
				DOUBLEKO();
			}
			else if (playerStatus[0].hp <= 0)   //	2Pの勝ち
			{
				activeGame = false;
				Win_2P();
			}
			else if (playerStatus[1].hp <= 0)   //	1Pの勝ち
			{
				activeGame = false;
				Win_1P();
			}
		}
	}


	void Win_1P()
	{
		win = 1;
		getRound[0]++;
		winTextFlag = true;
		mMM.FadeOut(1.5f);
		if (getRound[0] >= roundMax)
		{
			finalDecision = true;
		}
	}

	void Win_2P()
	{
		win = 2;
		getRound[1]++;
		winTextFlag = true;
		mMM.FadeOut(1.5f);
		if (getRound[1] >= roundMax)
		{
			finalDecision = true;
		}
	}

	//	--------------------
	//	ダブルＫＯ
	//	作成者：宮島 幸大
	//	--------------------
	void DOUBLEKO()
	{
		win = 3;
		winTextFlag = true;
		mMM.FadeOut(1.5f);
	}

	void Draw()
	{
		win = 4;
		winTextFlag = true;
		mMM.FadeOut(1.5f);
	}

	//	--------------------
	//	タイムアップ処理
	//	作成者：宮島 幸大
	//	--------------------
	void TimeUp()
	{
		winTextFlag = true;
		//	プレイヤー1のHPがプレイヤー2のHPを上回ったら
		if (playerStatus[0].hp > playerStatus[1].hp)
		{
			//	1Pプレイヤーの勝ち
			Invoke("Win_1P", 1.5f);
		}
		//	プレイヤー1のHPがプレイヤー2のHPを下回ったら
		else if (playerStatus[0].hp < playerStatus[1].hp)
		{
			//	2Pプレイヤーの勝ち
			Invoke("Win_2P", 1.5f);
		}
		else
		{
			//	引き分け
			Invoke("Draw", 1.5f);
		}
	}

	//	--------------------
	//	決着
	//	作成者：宮島 幸大
	//	--------------------
	void FinalDecision(bool p1)
	{

	}

	//	--------------------
	//	登場シーン終了
	//	--------------------
	public void EndAppearance()
	{
		UIMG.active_UI = true;
		Invoke("RoundStartAdd", 0.5f);
		player1.transform.position = new Vector3(-2.5f, 0, 0);
		player2.transform.position = new Vector3(2.5f, 0, 0);
        //player1.transform.Rotate(new Vector3(0f, 270f, 0f));
        //player2.transform.Rotate(new Vector3(0f, 90f, 0));
        Debug.Log("EndAppearance起動");
	}

	void RoundStartAdd()
	{
		UIMG.RoundStart();
	}

	//	--------------------
	//	ラウンド開始の初期に戻す(ラウンド取得数は変わらない)
	//	作成者：宮島 幸大
	//	--------------------
	public void Reset()
	{
		winTextFlag = false;
		round++;
		win = 0;
		//	プレイヤーが2人いるので
		for (int i = 0; i < 2; i++)
		{
			timer = timerMax;
			playerOb[i].transform.position = targetPoint[i].transform.position;
			playerStatus[i].Reset();
			player1.transform.position = new Vector3(-2.5f, 0, 0);
			player2.transform.position = new Vector3(2.5f, 0, 0);
		}
	}
}
//	write by Miyajima Kodai