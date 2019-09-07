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

	void Awake()
	{
		resultController_1 = canvas_1.transform.Find("ResultController").GetComponent<ResultController>();
		resultController_2 = canvas_2.transform.Find("ResultController").GetComponent<ResultController>();
		SetResultJudge();
	}

	void Update()
    {
		//ポーズ処理
		if (Mathf.Approximately(Time.timeScale, 0f)) return;
		if (Input.anyKeyDown && !Input.GetKeyDown(KeyCode.Escape) && !Input.GetKeyDown(KeyCode.F1) && !Input.GetKeyDown(KeyCode.F2) && !Input.GetKeyDown(KeyCode.F3) && !Input.GetKeyDown(KeyCode.F4))
		{
			SceneManager.LoadScene("JECLogo");
		}
	}


	/// <summary>
	/// 勝敗を受け取り、プレイヤーに合わせて飛ばす
	/// </summary>
	void SetResultJudge()
	{
		resultController_1.SetJudge(ShareSceneVariable.P1_info.isWin, ShareSceneVariable.P2_info.isWin);
		resultController_2.SetJudge(ShareSceneVariable.P2_info.isWin, ShareSceneVariable.P1_info.isWin);
	}
}
