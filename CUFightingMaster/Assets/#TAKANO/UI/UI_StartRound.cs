﻿//---------------------------------------
// ラウンドがはじまったときのUI表示
//---------------------------------------
// 作成者:高野
// 作成日:2019.07.12
//--------------------------------------
// 更新履歴
// 2019.07.12 作成、順番に表示
//--------------------------------------
// 仕様
//※07/12どう動かせば良いのかわからないので、一時的なプログラムです※
// 07/12現在、表示をするだけです。何らかの処理がある場合、随時追加してください
// 表示非表示の切り替えは、GameObject.Image.Enabledで制御しています（これもよくない）
//コルーチンの再生は一度だけです(isCalled)
//----------------------------------------
// MEMO
// 07/13現在、制御するUIの参照は手動です（一時的
//----------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_StartRound : MonoBehaviour
{
	public float interval01;
	public float interval02;
	public float interval03;

	public bool isPlay = false;         //再生中か（InGameManagerへの戻り値）
	public bool isCalled= false;    //既に呼ばれているか(コルーチンの再生を一度きりに使う)

	public GameObject round;
	public GameObject[] rundNumber = new GameObject[5];
	public GameObject fight;

	void DisplayRoundImage()
	{
        round.GetComponent<Image>().enabled = true;
	}

	void DisplayRoundNumberImage(int roundCnt)
	{
        rundNumber[roundCnt].GetComponent<Image>().enabled = true;
    }

	void DisplayFightImage(int roundCnt)
	{
        round.GetComponent<Image>().enabled = false;
        rundNumber[roundCnt].GetComponent<Image>().enabled = false;
        fight.GetComponent<Image>().enabled = true;
	}

	public void Reset_isCalled()
	{
		isCalled = false;
	}

	public bool PlayStartRound(int roundCount)
	{
        if (isCalled == false)
        {
            isCalled = true;
            StartCoroutine(StartRoundCoroutine(roundCount));
        }
        return isPlay;
	}

	IEnumerator StartRoundCoroutine(int roundCount)
	{
        isPlay = true;
		// 飯塚追加-------------------------------------------
        Sound.LoadBgm("BGM_Battle", "BGM_Battle");
        Sound.PlayBgm("BGM_Battle", 0.4f, 1, true);
		switch (roundCount)
        {
            case 0:
                Sound.LoadSe("RoundOne", "Voice_Round1");
                Sound.PlaySe("RoundOne", 3, 0.8f);
                break;
            case 1:
                Sound.LoadSe("RoundTwo", "Voice_Round2");
                Sound.PlaySe("RoundTwo", 3, 0.8f);
                break;
            case 2:
                Sound.LoadSe("RoundThree", "Voice_Final_Round");
                Sound.PlaySe("RoundThree", 3, 0.8f);
                break;
        }
        // ---------------------------------------------------
		DisplayRoundImage();
		yield return new WaitForSeconds(interval01);

		DisplayRoundNumberImage( roundCount );
		yield return new WaitForSeconds(interval02);
		  // 飯塚追加-------------------------------------------
        Sound.LoadSe("Fight", "Voice_Fight");
        Sound.PlaySe("Fight", 3, 0.8f);
        // ---------------------------------------------------
		DisplayFightImage (roundCount );
		yield return new WaitForSeconds(interval03);
        fight.GetComponent<Image>().enabled = false;
		isPlay = false;	
    }
}
