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
	[SerializeField] PlayableDirector timeLine_1;
	[SerializeField] PlayableDirector timeLine_2;

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
		resultTimelineController.TrackSet();
	}

	// Start is called before the first frame update
	void Start()
    {
		//カーテンを閉じる
		//canvasController_Result.InitDownCurtain();

		//キャラクターの生成
		fighterCreater.FighterCreate();
		fighter1 = fighterCreater.Fighter1;
		fighter2 = fighterCreater.Fighter2;
		

	}

    // Update is called once per frame
    void Update()
    {
		
    }
}
