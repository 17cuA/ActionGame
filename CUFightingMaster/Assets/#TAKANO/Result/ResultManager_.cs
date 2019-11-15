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
	private void DiscriminantWinner()
	{
		if(GameDataStrage.Instance.winFlag_PlayerOne == true)
		{
			currentUpdate = OnePlayerWon;
		}
		else if(GameDataStrage.Instance.winFlag_PlayerTwo == true)
		{
			currentUpdate = TwoPlayerWon;
		}
		else
		{

		}
	}

	/// <summary>
	/// 1Pが勝った時
	/// </summary>
	private void OnePlayerWon()
	{
		cameraMover.OnePlayerWonCameraSet();
	}

	/// <summary>
	/// 2Pが勝った時
	/// </summary>
	private void TwoPlayerWon()
	{
		cameraMover.TwoPlayerWonCameraSet();
	}
	
	/// <summary>
	/// 引き分けだった時
	/// </summary>
	private void Draw()
	{
		
	}

	/// <summary>
	/// ファイターを判別する
	/// </summary>
	private void DiscriminantCharacter()
	{
		int cnt = 0;
		foreach (FighterStatus fighterStatus in GameDataStrage.Instance.fighterStatuses)
		{
			switch(GameDataStrage.Instance.fighterStatuses[cnt].PlayerID)
			{
				case (int)FighterType.CLICO:
					break;
				case (int)FighterType.OBACHAN:
					break;
			}
		}
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
