//---------------------------------------
// ゲーム中のラウンドカウントUIの制御
//---------------------------------------
// 作成者:高野
// 作成日:2019.07.18
//--------------------------------------
// 更新履歴
// 2019.07.18 作成
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

    //参照
    public UI_RoundWin[] RoundCounter_P1 = new UI_RoundWin[3];
    public UI_RoundWin[] RoundCounter_P2 = new UI_RoundWin[3];

    public void UpdateRoundCounter(int winP1cnt, int winP2cnt)
    {
        if (winP1cnt > 0)
            RoundCounter_P1[winP1cnt - 1].UpdateImage(getRound_Image);
        if(winP2cnt  > 0)
            RoundCounter_P2[winP2cnt - 1].UpdateImage(getRound_Image);
    }
        
}

