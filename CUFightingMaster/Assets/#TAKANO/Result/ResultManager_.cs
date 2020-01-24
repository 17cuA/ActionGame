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

	private Action currentUpdate;

	[SerializeField] private GameObject fighter1;
	[SerializeField] private GameObject fighter2;

	private int[] elementNum = new int[2];

	void TimelineSet()
	{
		
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

	}


    // Update is called once per frame
    void Update()
    {

	}
}
