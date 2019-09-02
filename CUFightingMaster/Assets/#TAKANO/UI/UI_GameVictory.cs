//---------------------------------------
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

    public GameObject roundResult;
    public string win1P;
    public string win2P;
    public string draw;

	// 飯塚追加-------------------------------------------
    bool flag = true;
    // ---------------------------------------------------

	public bool WinP1()
	{
		 // 飯塚追加-------------------------------------------
        if (flag)
        {
            Sound.LoadSe("PlayerOneWin", "Voice_Player1_Win");
            Sound.PlaySe("PlayerOneWin", 3, 1);
            flag = false;
        }
        // ---------------------------------------------------
        roundResult.GetComponent<AnimationUIManager>().spriteName = win1P;
        GameVictoryStartCoroutine();
        return isPlay;
	}

    public bool WinP2()
    {
		 // 飯塚追加-------------------------------------------
        if (flag)
        {
            Sound.LoadSe("Voice_PlayerTwoWin", "Voice_Player2_Win");
            Sound.PlaySe("Voice_PlayerTwoWin", 3, 1);
            flag = false;
        }
        // ---------------------------------------------------
        roundResult.GetComponent<AnimationUIManager>().spriteName = win2P;
        GameVictoryStartCoroutine();
        return isPlay;
    }
	public bool Drow()
	{
		//下記の処理をドロー用に変更してください
		// 飯塚追加------------------------------------------
		Sound.LoadSe("Draw", "Voice_Draw");
		Sound.PlaySe("Draw", 3, 0.8f);
		// -------------------------------------------------

		roundResult.GetComponent<AnimationUIManager>().spriteName = draw;
		GameVictoryStartCoroutine();
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

        roundResult.SetActive(true);
		yield return new WaitForSeconds(interval01);

		isPlay = false;
	}
}
