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

public class ResultManager_ : MonoBehaviour
{
	enum FighterType
	{
		CLICO,		//0
		OBACHAN,	//1
	}

	[SerializeField] GameDataStrage gameDataStrage;
	[SerializeField] FighterCreater fighterCreater;
	[SerializeField] ResultTrackChanger resultTrackChanger;
	[SerializeField] CanvasController_Result canvasController_Result;
	[SerializeField] AnimaitonBindController animaitonBindController;
	[SerializeField] CameraBindController cameraBindController;

	private Action currentUpdate;

	private GameObject fighter1;
	private GameObject fighter2;

	private int[] elementNum = new int[2];

	private const int CLICO = 0;
	private const int OBACHAN = 1;
	private const int WIN = 0;
	private const int LOSE = 1;

	void BindTimeline()
	{
		for(int i = 0; i < 2; i++ )
		{
			animaitonBindController.AnimationClip = resultTrackChanger.GetTrack((int)GameDataStrage.Instance.matchResult[0],
				GameDataStrage.Instance.fighterStatuses[0].PlayerID); 
			cameraBindController.cinemachineBrain = resultTrackChanger.GetCinemachineBrain((int)GameDataStrage.Instance.matchResult[0],
				GameDataStrage.Instance.fighterStatuses[0].PlayerID);
		}
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
		currentUpdate();
    }
}
