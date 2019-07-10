/*	UIManager_CharacteSelect.cs
 *	CharacterSelectシーンのUI管理スクリプト
 *	製作者：宮島幸大
 *	制作日：2019/06/27
 *	----------更新----------
 *	2019/06/27：UI関係の処理を分離しUIManager_CharacterSelectに変更(宮島)
 *	2319/07/04：タイマーをTextからImageへ変更
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager_CharacteSelect : MonoBehaviour
{
	//	マネージャー関係
	public GameObject managerObject;
	Manager_CharacterSelect mC;

	public Canvas timerCV;
	TimerManager_All tMA;

	//	UI関係
	public Image[] characterImage = new Image[7];			//
	public Image[] playerSelectCursor=new Image[2];		//
	public Sprite[] selectCursor1P = new Sprite[2];			//	セレクトカーソル(Sprite)
	public Sprite[] selectCursor2P = new Sprite[2];			//	セレクトカーソル(Sprite)
	public Image[] playerName = new Image[2];				//	プレイヤーの名前画像

	//	--------------------
	//	スタート
	//	--------------------
	void Start()
	{
		mC = managerObject.GetComponent<Manager_CharacterSelect>();     //mCにCharacterSelectマネージャーをロード
		tMA = timerCV.GetComponent<TimerManager_All>();
		for (int i = 0; i < 7; i++)
		{
			characterImage[i].sprite = mC.characterStatuses[i].characterIcon;
		}
	}

	//	--------------------
	//	アップデート
	//	--------------------
	void Update()
	{
		tMA.Display((int)mC.timer);

		for (int i = 0; i < 2; i++)
		{
			playerSelectCursor[i].transform.position = characterImage[mC.playerSelect[i]].transform.position;
			playerName[i].sprite = mC.characterStatuses[mC.playerSelect[i]].characterName;
		}

		//	カーソルが重なった時画像を変える
		if (mC.playerSelect[0] == mC.playerSelect[1])
		{
			playerSelectCursor[0].sprite = selectCursor1P[1];
			playerSelectCursor[1].sprite = selectCursor2P[1];
		}
		else
		{
			playerSelectCursor[0].sprite = selectCursor1P[0];
			playerSelectCursor[1].sprite = selectCursor2P[0];
		}
	}
}
//	write by Miyajima Kodai