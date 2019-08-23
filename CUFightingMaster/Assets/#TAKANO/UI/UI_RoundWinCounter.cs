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
	public Sprite getRound_Image;
	public Sprite default_Image;

    //参照
    public UI_RoundWin[] RoundCounter_P1 = new UI_RoundWin[3];
    public UI_RoundWin[] RoundCounter_P2 = new UI_RoundWin[3];

	/// <summary>
	/// ラウンドカウンターを更新する
	/// </summary>
	/// <param name="winP1cnt">P1が勝った回数</param>
	/// <param name="winP2cnt">P2が勝った回数</param>
    public void UpdateRoundCounter(int winP1cnt, int winP2cnt)
    {
        if (winP1cnt > 0)
            RoundCounter_P1[winP1cnt - 1].UpdateImage(getRound_Image);
        if(winP2cnt  > 0)
            RoundCounter_P2[winP2cnt - 1].UpdateImage(getRound_Image);
    }

	/// <summary>
	/// ラウンドカウンターをリセットする(0729現在、デバッグでしか使っていない)
	/// </summary>
	public void ResetWinCounter()
	{
		foreach( UI_RoundWin uI_RoundWin in RoundCounter_P1 )
		{
			uI_RoundWin.UpdateImage(default_Image);
		}
		foreach( UI_RoundWin uI_RoundWin in RoundCounter_P2 )
		{
			uI_RoundWin.UpdateImage(default_Image);
		}
	}
}

