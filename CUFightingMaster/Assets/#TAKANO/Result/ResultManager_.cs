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

public class ResultManager_ : MonoBehaviour
{
	[SerializeField] GameDataStrage gameDataStrage;
	[SerializeField] FighterCreater fighterCreater;
	[SerializeField] CanvasController_Result canvasController_Result;
	[SerializeField] ResultTimelineController resultTimelineController;
	[SerializeField] ResultNomalAnimationController resultNomalAnimationController;

	private Action currentUpdate;

	[SerializeField] private GameObject fighter1;
	[SerializeField] private GameObject fighter2;

	private int[] elementNum = new int[2];

	/// <summary>
	/// UIの移動を開始
	/// </summary>
	private void StartMoveUI_1()
	{
		Invoke("MoveUIGroup1", 0.75f);
	}
	/// <summary>
	/// UIの移動を開始
	/// </summary>
	private void StartMoveUI_2()
	{
		Invoke("MoveUIGroup2", 0.75f);
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

	// Start is called before the first frame update
	void Start()
    {
		//カーテンを閉じる
		//canvasController_Result.InitDownCurtain();

		//キャラクターの生成
		fighterCreater.FighterCreate();

		//タイムラインの生成
		resultTimelineController.CreateTimeline();

		//タイムラインへの参照
		resultTimelineController.RefTimeline();

		//NomalAniamitonPlayerをDisabled
		//resultNomalAnimationController.Disabled();



	}
    // Update is called once per frame
    void Update()
    {
		

		//UI_1の移動を開始
		StartMoveUI_1();

		//タイムラインの再生が終わった
		if (resultTimelineController.isEndPlayTimelines())
		{
			//Ui_2の移動を開始
			StartMoveUI_2();

			resultNomalAnimationController.EnabledLoser();
		}

		else
		{
			resultNomalAnimationController.Disabled();
		}

	}
}
