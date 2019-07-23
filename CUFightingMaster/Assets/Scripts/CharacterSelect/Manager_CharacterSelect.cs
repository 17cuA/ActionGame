/*	Manager_CharacterSelect.cs
 *	CharacterSelectシーンの中央管理スクリプト
 *	製作者：宮島幸大
 *	制作日：2019/06/07
 *	----------更新----------
 *	2019/06/14：デザイン変更によるInput処理の追加(宮島)
 *	2019/06/26：デザインの変更及びシーン移行処理の追加(宮島)
 *	2019/06/27：UI関係の処理を分離しUIManager_CharacterSelectに変更(宮島)
 *	2019/07/04：コメントがない、変数名がわかりづらいところほぼ変更(三沢)
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager_CharacterSelect : MonoBehaviour
{
	//	画面のマスク関係
	public GameObject maskOb;   //	マスク用のイメージが入ってるオブジェクト
	//MovingMaskManager mMM;      //	マスク用スクリプトをロードするため

	//	キャラクターオブジェクト表示関係
	public GameObject[] characterOb;    //	キャラクターオブジェクトを保持
	public CharacterStatus_CharacterSelect[] characterStatuses; //	ロードしたキャラクターオブジェクトのキャラクターステータス保持
	public GameObject[] targetPoint;        //	キャラクターのオブジェクトを生成するためのポイント(GameObject)

	//	フラグ関係
	public bool activeCharaselect;      //	操作できる状態か
	public float timer;                     //	キャラクターを選べる残り時間
	[SerializeField]
	private float timerMax;             //	キャラクターを選べる時間
	public bool[] playerDecision;       //	プレイヤーがキャラクターを選んだかのフラグ
	public int[] playerObn;             //	プレイヤーが選んだキャラクターのオブジェクト保持するための変数
	public GameObject[] playerOb;   //	プレイヤーが選んだキャラクターのオブジェクト保持
	public int[] playerSelect;

	//	--------------------
	//	アウェイク
	//	--------------------
	private void Awake()
	{
		//	オブジェクトに付属しているplayerStatusを参照
		for (int i = 0; i < 6; i++)
		{
			characterStatuses[i] = characterOb[i].GetComponent<CharacterStatus_CharacterSelect>();
		}
	}

	//	--------------------
	//	スタート
	//	--------------------
	void Start()
	{
		//	マスク用のデータロード
		//mMM = maskOb.GetComponent<MovingMaskManager>();

		//	フラグなどの初期化
		activeCharaselect = false;
		timerMax = 10;
		timer = timerMax;
		for (int i = 0; i < 2; i++)
		{
			playerDecision[i] = false;
			playerObn[i] = 7;
		}
	}

	//	--------------------
	//	アップデート
	//	--------------------
	void Update()
	{
		if (activeCharaselect)
		{
			timer -= Time.deltaTime;
		}

		if((playerDecision[0]&&playerDecision[1])||(timer<=0))
		{
			activeCharaselect = false;
			//mMM.FadeOut(0);
		}

		//	プレイヤーが選んだキャラのオブジェクトを表示する
		for (int i = 0; i < 2; i++)
		{
			if(playerObn[i]!=playerSelect[i])
			{
				Destroy(playerOb[i]);
			}
			if (playerObn[i] != playerSelect[i])
			{
				playerOb[i] = Instantiate(characterOb[playerSelect[i]], targetPoint[i].transform.position, Quaternion.identity);
				playerObn[i] = playerSelect[i];
			}
		}
	}

	//	--------------------
	//	ボタンの入力を受け取る関数
	//	作成者：宮島 幸大
	//	--------------------
	public void PlayerSelectMove(int playerNum, string Move)
	{
		switch (Move)
		{
			//
			case "Left":
				switch (playerSelect[playerNum])
				{
					case 0:
						playerSelect[playerNum] = 3;
						break;
					case 1:
						playerSelect[playerNum] = 6;
						break;
					case 2:
					case 3:
					case 4:
						playerSelect[playerNum] = 0;
						break;
					case 5:
					case 6:
						playerSelect[playerNum]--;
						break;
				}
				break;
				//
			case "Right":
				switch (playerSelect[playerNum])
				{
					case 0:
						playerSelect[playerNum] = 4;
						break;
					case 1:
					case 2:
					case 3:
						playerSelect[playerNum] = 0;
						break;
					case 4:
					case 5:
						playerSelect[playerNum]++;
						break;
					case 6:
						playerSelect[playerNum] = 1;
						break;
				}
				break;
				#region コメント
				//case "Up":
				//	switch (playerSelect[playerNum])
				//	{
				//		case 1:
				//			playerSelect[playerNum] = 5;
				//			break;
				//		case 2:
				//			playerSelect[playerNum] = 6;
				//			break;
				//		case 3:
				//			playerSelect[playerNum] = 1;
				//			break;
				//		case 4:
				//			playerSelect[playerNum] = 2;
				//			break;
				//		case 5:
				//			playerSelect[playerNum] = 3;
				//			break;
				//		case 6:
				//			playerSelect[playerNum] = 4;
				//			break;
				//		case 0:
				//			playerSelect[playerNum] = 2;
				//			break;
				//	}
				//	break;
				//case "Down":
				//	switch (playerSelect[playerNum])
				//	{
				//		case 1:
				//			playerSelect[playerNum] = 3;
				//			break;
				//		case 2:
				//			playerSelect[playerNum] = 4;
				//			break;
				//		case 3:
				//			playerSelect[playerNum] = 5;
				//			break;
				//		case 4:
				//			playerSelect[playerNum] = 6;
				//			break;
				//		case 5:
				//			playerSelect[playerNum] = 1;
				//			break;
				//		case 6:
				//			playerSelect[playerNum] = 2;
				//			break;
				//		case 0:
				//			playerSelect[playerNum] = 5;
				//			break;
				//	}
				//break;
				#endregion
		}
	}
}
//	write by Miyajima Kodai