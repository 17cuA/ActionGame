//---------------------------------------
// Result進行
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
using UnityEngine.Playables;
using UnityEngine.Timeline;
using Cinemachine;
using UnityEngine.SceneManagement;

public class ResultManager_ : MonoBehaviour
{
	[SerializeField] GameDataStrage gameDataStrage;
	[SerializeField] FighterCreater fighterCreater;
	[SerializeField] CanvasController_Result canvasController_Result;
	[SerializeField] ResultTimelineController resultTimelineController;
	[SerializeField] ResultNomalAnimationController resultNomalAnimationController;
	[SerializeField] CinemachineBrain cinemachineBrain1;
	[SerializeField] CinemachineBrain cinemachineBrain2;
	private Action alreadyOneUpdate;

	private GameObject fighter1;
	private GameObject fighter2;

	private int[] elementNum = new int[2];

	//最初のシーンへ戻る時間(sec)
	[SerializeField] private float waitTime;

	#region UIの移動
	/// <summary>
	/// UIの移動を開始
	/// </summary>
	private void StartMoveUI_1()
	{
		Invoke("MoveUIGroup1", 1.5f);
	}
	/// <summary>
	/// UIの移動を開始
	/// </summary>
	private void StartMoveUI_2()
	{
		Invoke("MoveUIGroup2", 0.75f);
		Invoke("MoveUIGroup3", 0.85f);
		Invoke("MoveUIGroup4", 0.95f);
	}
	/// <summary>
	/// UIの移動
	/// </summary>
	private void MoveUIGroup1()
	{
		canvasController_Result.MoveUIGroup1();
	}
	/// <summary>
	/// UIの移動
	/// </summary>
	private void MoveUIGroup2()
	{
		canvasController_Result.MoveUIGroup2();
	}
	/// <summary>
	/// UIの移動
	/// </summary>
	private void MoveUIGroup3()
	{
		canvasController_Result.MoveUIGroup3();
	}
	/// <summary>
	/// UIの移動
	/// </summary>
	private void MoveUIGroup4()
	{
		canvasController_Result.MoveUIGroup4();
	}
	#endregion
	#region カーテンの移動
	/// <summary>
	/// Curtainが上に
	/// </summary>
	private void MoveUpCurtain()
	{
		if (canvasController_Result.UpCurtain())
			alreadyOneUpdate = DisabledNomalAnimetion;
	}
	/// <summary>
	/// Curtainが下にCurtainが下に
	/// </summary>
	void DownCurtain()
	{
		if (canvasController_Result.DownCurtain())
		{
			alreadyOneUpdate = FadeOut;
		}
	}
	#endregion
	/// <summary>
	/// 入力待ち
	/// </summary>
	void WaitInput()
	{
		if (Input.anyKeyDown && !Input.GetKeyDown(KeyCode.Escape) && !Input.GetKeyDown(KeyCode.F1) && !Input.GetKeyDown(KeyCode.F2) && !Input.GetKeyDown(KeyCode.F3) && !Input.GetKeyDown(KeyCode.F4))
		{
			alreadyOneUpdate = DownCurtain;
		}
		waitTime -= Time.deltaTime;

		if (waitTime < 0)
		{
			alreadyOneUpdate = DownCurtain;
		}
	}
	/// <summary>
	/// FadeOut
	/// </summary>
	void FadeOut()
	{
		if (canvasController_Result.StartFadeOut())
		{
			SceneManager.LoadScene("JECLogo");
		}
	}
	/// <summary>
	/// AnimationDataを無効化する
	/// </summary>
	private void DisabledNomalAnimetion()
	{
		resultNomalAnimationController.DisabledNomalAnimationModels();
		alreadyOneUpdate = WaitInput;
	}

	void Start()
    {
		//Curtainを上に
		alreadyOneUpdate = MoveUpCurtain;

		//BGM再生
		Sound.LoadBGM("BGM_Result", "BGM_Result");
		Sound.PlayBGM("BGM_Result", 1, 1.0f, true);

		//カーテンを閉じた状態にする
		canvasController_Result.InitDownCurtain();

		//キャラクターの生成
		fighterCreater.FighterCreate();
		
		//タイムラインの生成
		resultTimelineController.CreateTimeline();

		//タイムラインへの参照
		resultTimelineController.RefTimeline();

		//試合結果を表示するUIの更新
		canvasController_Result.RoundGetDisplay();
		//与えたダメージと残ったHPを表示するUIの更新
		canvasController_Result.PassHPtoScore();
		//どちらのPlayerが勝ったかを取得
		if(GameDataStrage.Instance.matchResult[0] == MatchResult.WIN)
		{
			canvasController_Result.P1WinDisplay();	//表示するUIを切り変える
			cinemachineBrain2.enabled = false;		//負けたほうのsinemaSchineBrainを無効化する
		}
		else
		{
			canvasController_Result.P2WinDisplay();
			cinemachineBrain1.enabled = false;
		}
	}
    // Update is called once per frame
    void Update()
    {
		alreadyOneUpdate();

		//UI_1の移動を開始
		StartMoveUI_1();

		//タイムラインの再生が終わった
		if (resultTimelineController.isEndPlayTimelines())
		{
			//Ui_2の移動を開始
			StartMoveUI_2();
		}
	}
}
