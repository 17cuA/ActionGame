﻿//---------------------------------------
// Victry中のUI
//---------------------------------------
// 作成者:高野
// 作成日:2019.07.17
//--------------------------------------
// 更新履歴
// 2019.07.17 作成
//--------------------------------------
// 仕様 
//
//----------------------------------------
// MEMO 
// 一時的なスクリプト、仕様が決定次第
// 参照は手動
//----------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_GameVictory : MonoBehaviour
{
	public int interval01;

	public bool isPlay = false;         //再生中か（InGameManagerへの戻り値）
    public bool isCalled = false;    //既に呼ばれているか(コルーチンの再生を一度きりに使う)

	private Image win_P1_image;
    private Image win_P2_image;
	private Image win_image;
    public Image displayImage;
	public Image displayWin;
	public GameObject win_P1;
    public GameObject win_P2;
	public GameObject Win_sprite;

	    // 飯塚追加-------------------------------------------
    bool flag = true;
    // ---------------------------------------------------

	public bool WinP1()
	{
		 // 飯塚追加-------------------------------------------
        if (flag)
        {
            Sound.LoadSe("PlayerOneWin", "voice_playerOneWin");
            Sound.PlaySe("PlayerOneWin", 3, 1);
            flag = false;
        }
        // ---------------------------------------------------
        displayImage = win_P1_image;
		//displayWin = win_image;
        GameVictoryStartCoroutine();
        return isPlay;
	}

    public bool WinP2()
    {
		 // 飯塚追加-------------------------------------------
        if (flag)
        {
            Sound.LoadSe("Voice_PlayerTwoWin", "Voice_PlayerTwoWin");
            Sound.PlaySe("Voice_PlayerTwoWin", 3, 1);
            flag = false;
        }
        // ---------------------------------------------------
        displayImage = win_P2_image;
		//displayWin = win_image;
		GameVictoryStartCoroutine();
        return isPlay;
    }
	public bool Drow()
	{
		//下記の処理をドロー用に変更してください
		//// 飯塚追加-------------------------------------------
		//if (flag)
		//{
		//	Sound.LoadSe("Voice_PlayerTwoWin", "Voice_PlayerTwoWin");
		//	Sound.PlaySe("Voice_PlayerTwoWin", 3, 1);
		//	flag = false;
		//}
		//// ---------------------------------------------------
		//displayImage = win_P2_image;
		//displayWin = win_image;
		//GameVictoryStartCoroutine();
		return isPlay;
	}

	void GameVictoryStartCoroutine()
    {
        if (isCalled == false)
		{
			isCalled = true;
			StartCoroutine(RoundFinish_KOCoroutine());
		}
    }
	IEnumerator RoundFinish_KOCoroutine()
	{
		isPlay = true;

		displayImage.enabled = true;
		//displayWin.enabled = true;
		yield return new WaitForSeconds(interval01);
		displayImage.enabled = false;
		//displayWin.enabled = false;

		isPlay = false;
	}
	private void Awake()
	{
		win_P1_image = win_P1.GetComponent<Image>();
        win_P2_image = win_P2.GetComponent<Image>();
		win_image	 = Win_sprite.GetComponent<Image>();
	}
}
