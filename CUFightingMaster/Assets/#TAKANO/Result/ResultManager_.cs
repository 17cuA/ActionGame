//---------------------------------------
// Result管理
//---------------------------------------
// 作成者:高野
// 作成日:2019.11.14
//--------------------------------------
// 更新履歴
// 2019.11.14 作成
//--------------------------------------
// 仕様 
//----------------------------------------
// MEMO 
//----------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ResultManager_ : MonoBehaviour
{
	[SerializeField] FighterCreater fighterCreater;
	[SerializeField] CameraMover cameraMover;
	[SerializeField] CanvasController_Result canvasController_Result;

	private Action currentUpdate;

	/// <summary>
	/// 勝者の判定
	/// </summary>
	private void JudgeWinner()
	{
		if(GameDataStrage.Instance.winFlag_PlayerOne == true)
		{
			currentUpdate = OnePlayerWon;
		}
		else if(GameDataStrage.Instance.winFlag_PlayerTwo == true)
		{
			currentUpdate = TwoPlayerWon;
		}
	}

	/// <summary>
	/// 1Pが勝った時
	/// </summary>
	private void OnePlayerWon()
	{
		cameraMover.OnePlayerWonCamera();
	}

	/// <summary>
	/// 2Pが勝った時
	/// </summary>
	private void TwoPlayerWon()
	{
		cameraMover.TwoPlayerWonCamera();
	}

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		currentUpdate();
    }
}
