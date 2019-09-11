//---------------------------------------
// Result管理
//---------------------------------------
// 作成者:三沢
// 作成日:2019.08.14
//--------------------------------------
// 更新履歴
// 2019.08.14 作成
//--------------------------------------
// 仕様 
// 情報を受け取り、2つのCanvasに対応できるよう
// ResultControllerに投げる
//----------------------------------------
// MEMO 
// 勝敗判定(完成？)
// 勝敗時にランダムでキャラクターテキスト表示(未作成)
//----------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultManager : MonoBehaviour
{
	public Canvas canvas_1;
	[SerializeField] private ResultController resultController_1;

	public Canvas canvas_2;
	[SerializeField] private ResultController resultController_2;

    [SerializeField] private CanvasController_Result canvasController_Result;

    [SerializeField] private Result_Manager result_Manager;

    private Action currentUpdate;

	void Awake()
	{
        canvasController_Result.InitDownCurtain();

        resultController_1 = canvas_1.transform.Find("ResultController").GetComponent<ResultController>();
		resultController_2 = canvas_2.transform.Find("ResultController").GetComponent<ResultController>();
	
        currentUpdate = UpCurtain;

    }

	void Update()
    {
		//ポーズ処理
		if (Mathf.Approximately(Time.timeScale, 0f)) return;

        currentUpdate();

	}

	//キャラの生成
	void CreateFighter()
	{
		result_Manager.CreateFighter();
		currentUpdate = UpCurtain;
	}

	//カーテンが上がる
    void UpCurtain()
    {
        if(canvasController_Result.UpCurtain())
        {
            currentUpdate = PlayUIAnime;
        }

    }

	//ResultAnime
    void PlayUIAnime()
    {
        result_Manager.FirstPlayUIAnime();
        canvasController_Result.PlayUIAnime();
        currentUpdate = ResultUpdate;
    }

    void ResultUpdate()
    {
        if (Input.anyKeyDown && !Input.GetKeyDown(KeyCode.Escape) && !Input.GetKeyDown(KeyCode.F1) && !Input.GetKeyDown(KeyCode.F2) && !Input.GetKeyDown(KeyCode.F3) && !Input.GetKeyDown(KeyCode.F4))
        {
            currentUpdate = DownCurtain;
        }
    }

    void DownCurtain()
    {
        if(canvasController_Result.DownCurtain())
        {
            currentUpdate = FadeOut;
        }
    }

    void FadeOut()
    {
        if(canvasController_Result.StartFadeOut())
        {
            SceneManager.LoadScene("JECLogo");
        }

    }
}
