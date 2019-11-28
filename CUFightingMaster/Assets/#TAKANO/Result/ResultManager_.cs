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
	enum FighterType
	{
		CLICO,		//0
		OBACHAN,	//1
	}

	[SerializeField] FighterCreater fighterCreater;
	[SerializeField] ResultAnimationChanger resultAnimationChanger;
	[SerializeField] ResultAnimationPlayer resultAnimationPlayer_1;
	[SerializeField] ResultAnimationPlayer resultAniamtionPlayer_2;
	[SerializeField] CameraMover cameraMover;
	[SerializeField] CanvasController_Result canvasController_Result;

	private Action currentUpdate;

	private GameObject fighter1;
	private GameObject fighter2;

	private int[] elementNum = new int[2];

	private const int CLICO = 0;
	private const int OBACHAN = 1;
	private const int WIN = 0;
	private const int LOSE = 1;

	/// <summary>
	/// 勝者の判定
	/// </summary>
	private void DiscriminantWinner()
	{
		if (GameDataStrage.Instance.matchResult == MatchResult.PLAYER1WON)
		{
			currentUpdate = OnePlayerWonCameraSet;
		}
		else if (GameDataStrage.Instance.matchResult == MatchResult.PLAYER2WON)
		{
			currentUpdate = TwoPlayerWonCameraSet;
		}
		else if (GameDataStrage.Instance.matchResult == MatchResult.DRAW)
		{
			currentUpdate = DrawCameraSet;
		}
	}

	/// <summary>
	/// 1Pが勝った時
	/// </summary>
	private void OnePlayerWonCameraSet()
	{
		//カメラをセット
		cameraMover.OnePlayerWonCameraSet();

		//アニメーションをセット
		resultAnimationChanger.SetAnimation_1(GameDataStrage.Instance.fighterStatuses[(int)PlayerNumber.Player1].PlayerID , WIN);
		resultAnimationChanger.SetAnimation_2(GameDataStrage.Instance.fighterStatuses[(int)PlayerNumber.Player2].PlayerID , LOSE);
	}

	/// <summary>
	/// 2Pが勝った時
	/// </summary>
	private void TwoPlayerWonCameraSet()
	{
		//カメラをセット
		cameraMover.TwoPlayerWonCameraSet();

		//アニメーションをセット
		resultAnimationChanger.SetAnimation_1(GameDataStrage.Instance.fighterStatuses[(int)PlayerNumber.Player1].PlayerID, LOSE);
		resultAnimationChanger.SetAnimation_2(GameDataStrage.Instance.fighterStatuses[(int)PlayerNumber.Player2].PlayerID, WIN);
	}


	/// <summary>
	/// 引き分けの時
	/// </summary>
	private void DrawCameraSet()
	{
		//カメラをセット
		cameraMover.DrawCameraSet();

		//アニメーションをセット
		resultAnimationChanger.SetAnimation_1(GameDataStrage.Instance.fighterStatuses[(int)PlayerNumber.Player1].PlayerID, LOSE);
		resultAnimationChanger.SetAnimation_2(GameDataStrage.Instance.fighterStatuses[(int)PlayerNumber.Player2].PlayerID, LOSE);
	}

	/// <summary>
	/// タイムラインの再生
	/// </summary>
	private void PlayTimeline()
	{
		//GameDataStrage
	}

	private void JudgeFighterType(FighterStatus _fighterStatus)
	{
		if(_fighterStatus.PlayerID == (int)FighterType.CLICO)
		{

		}
	}
	
	/// <summary>
	/// 引き分けだった時
	/// </summary>
	private void Draw()
	{
		
	}

	private void PlayAnimation()
	{
		resultAnimationPlayer_1.PlayAnimatmion();
	}

    // Start is called before the first frame update
    void Start()
    {
		//キャラクターの生成
		fighterCreater.FighterCreate();
		fighter1 = fighterCreater.Fighter1;
		fighter2 = fighterCreater.Fighter2;
	}

    // Update is called once per frame
    void Update()
    {
		currentUpdate();
    }
}
