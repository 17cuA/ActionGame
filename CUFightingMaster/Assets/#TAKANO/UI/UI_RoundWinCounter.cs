//---------------------------------------
// ゲーム中のラウンドカウントUIの制御
//---------------------------------------
// 作成者:高野
// 作成日:2019.07.18
//--------------------------------------
// 更新履歴
// 2019.07.18 作成
// 2019.07.29 勝っていないときの表示に更新出来るようにした
//--------------------------------------
// 仕様 
//----------------------------------------
// MEMO 
// 制御するUIの参照は手動です
//----------------------------------------
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

class UI_RoundWinCounter : MonoBehaviour
 {
	public Sprite KO_Image;
	public Sprite DoubleKO_Image;
	public Sprite timeOver_Image;
	public Sprite default_Image;

    //参照
    public GameObject[] RoundCounter_P1 = new GameObject[3];
    public GameObject[] RoundCounter_P2 = new GameObject[3];

	/// <summary>
	/// ラウンドカウンターを更新する
	/// </summary>
	/// <param name="winP1cnt">P1が勝った回数</param>
	/// <param name="winP2cnt">P2が勝った回数</param>
    public void UpdateRoundCounter(string winP1cnt, string winP2cnt)
    {
		if (winP1cnt.Length > 0)
		{
			//Debug.Log(Mathf.Clamp(winP1cnt.Length - 1, 0, 2));
			switch (winP1cnt[Mathf.Clamp(winP1cnt.Length - 1,0,2)])
			{
				case '1':
					RoundCounter_P1[Mathf.Clamp(winP1cnt.Length - 1, 0, 2)].GetComponents<AnimationUIManager>()[0].isStart = true;
					break;
				case'2':
					RoundCounter_P1[Mathf.Clamp(winP1cnt.Length - 1, 0, 2)].GetComponents<AnimationUIManager>()[1].isStart = true;
					break;
				case '3':
					RoundCounter_P1[Mathf.Clamp(winP1cnt.Length - 1, 0, 2)].GetComponents<AnimationUIManager>()[2].isStart = true;
					break;
				default:
					break;
			}
		}
		if (winP2cnt.Length > 0)
		{
			switch (winP2cnt[Mathf.Clamp(winP2cnt.Length - 1, 0, 2)])
			{
				case '1':
					RoundCounter_P2[Mathf.Clamp(winP2cnt.Length - 1, 0, 2)].GetComponents<AnimationUIManager>()[0].isStart = true;
					break;
				case '2':
					RoundCounter_P2[Mathf.Clamp(winP2cnt.Length - 1, 0, 2)].GetComponents<AnimationUIManager>()[1].isStart = true;
					break;
				case '3':
					RoundCounter_P2[Mathf.Clamp(winP2cnt.Length - 1, 0, 2)].GetComponents<AnimationUIManager>()[2].isStart = true;
					break;
				default:
					break;
			}
		}
	}

	/// <summary>
	/// ラウンドカウンターをリセットする(0729現在、デバッグでしか使っていない)
	/// </summary>
	public void ResetWinCounter()
	{
		//foreach( GameObject uI_RoundWin in RoundCounter_P1 )
		//{
		//	uI_RoundWin.UpdateImage(default_Image);
		//}
		//foreach( GameObject uI_RoundWin in RoundCounter_P2 )
		//{
		//	uI_RoundWin.UpdateImage(default_Image);
		//}
	}

	public void Call_HideImage()
	{
		RoundCounter_P1[0].GetComponent<UI_RoundWin>().HideImage();
		RoundCounter_P1[1].GetComponent<UI_RoundWin>().HideImage();
		RoundCounter_P2[0].GetComponent<UI_RoundWin>().HideImage();
		RoundCounter_P2[1].GetComponent<UI_RoundWin>().HideImage();
	}
}

