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
	[SerializeField] AnimationSetter animationSetter;
	[SerializeField] CameraMover cameraMover;
	[SerializeField] CanvasController_Result canvasController_Result;

	private Action currentUpdate;

	/// <summary>
	/// 勝者の判定
	/// </summary>
	private void DiscriminantWinner()
	{
		//キャラクターの生成
		fighterCreater.FighterCreate();
		
		if (GameDataStrage.Instance.winFlag_PlayerOne == true)
		{
			currentUpdate = OnePlayerWonCameraSet;
		}
		else if(GameDataStrage.Instance.winFlag_PlayerTwo == true)
		{
			currentUpdate = TwoPlayerWonCameraSet;
		}
		else
		{

		}
	}

	/// <summary>
	/// 1Pが勝った時
	/// </summary>
	private void OnePlayerWonCameraSet()
	{
<<<<<<< HEAD
		//カメラをセット
=======
>>>>>>> f427b8379863ffc4c85f6ba1b7778e80bff047af
		cameraMover.OnePlayerWonCameraSet();
	}

	/// <summary>
	/// 2Pが勝った時
	/// </summary>
	private void TwoPlayerWonCameraSet()
	{
<<<<<<< HEAD
		//カメラをセット
		cameraMover.TwoPlayerWonCameraSet();
	}

	/// <summary>
	/// タイムラインの再生
	/// </summary>
	private void PlayTimeline()
	{
		//勝ったのファイターの判定
		if (GameDataStrage.Instance.fighterStatuses[(int)GameDataStrage.Instance.WiningPlayer].PlayerID == (int)FighterType.CLICO)
		{
			//カメラワークを再生
			cameraMover.ClicoWin();
			//アニメーション再生
			animationSetter.ClicoWonAnimationSet(fighterCreater.FighterPlayer1);
		}
		else if (GameDataStrage.Instance.fighterStatuses[(int)GameDataStrage.Instance.WiningPlayer].PlayerID == (int)FighterType.OBACHAN)
		{
			//カメラワークを再生
			cameraMover.ObachanWin();
			//アニメーション再生
			animationSetter.ObachanWonAnimationSet(fighterCreater.FighterPlayer1);
		}

		//負けたファイターの判定
		if (GameDataStrage.Instance.fighterStatuses[(int)PlayerNumber.Player2].PlayerID == (int)FighterType.CLICO)
		{
			//カメラワークを再生
			cameraMover.ClicoLose();
			//アニメーション再生
			animationSetter.ClicoLosingAnimationSet(fighterCreater.FighterPlayer2);
		}
		else if (GameDataStrage.Instance.fighterStatuses[(int)PlayerNumber.Player2].PlayerID == (int)FighterType.OBACHAN)
		{
			//カメラワークを再生
			cameraMover.ObachanLose();
			//アニメーションセット
			animationSetter.ObachanWonAnimationSet(fighterCreater.FighterPlayer2);
=======
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
>>>>>>> f427b8379863ffc4c85f6ba1b7778e80bff047af
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
