//---------------------------------------
// Result画面
//---------------------------------------
// 作成者:三沢
// 作成日:2019.08.14
//--------------------------------------
// 更新履歴
// 2019.08.14 作成
//--------------------------------------
// 仕様 
// InGameManagerとCharacterSelectManagerから
// 勝敗判定と選ばれていたキャラクターを取得
//----------------------------------------
// MEMO 
// ゴリ押し
//----------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultManager : MonoBehaviour
{
	public Canvas canvas_1;
	public Canvas canvas_2;
	#region Update
	void Update()
    {
		//ポーズ処理
		if (Mathf.Approximately(Time.timeScale, 0f)) return;
		if (Input.anyKeyDown && !Input.GetKeyDown(KeyCode.Escape) && !Input.GetKeyDown(KeyCode.F1) && !Input.GetKeyDown(KeyCode.F2) && !Input.GetKeyDown(KeyCode.F3) && !Input.GetKeyDown(KeyCode.F4))
		{
			SceneManager.LoadScene("JECLogo");
		}
		Debug.Log(ShareSceneVariable.P1_info.isWin);
		Debug.Log(ShareSceneVariable.P2_info.isWin);
		Judge();
	}
	#endregion
	void Judge()
	{
		if (ShareSceneVariable.P1_info.isWin)
		{
			canvas_1.transform.Find("VictoryORdefeat/WIN").gameObject.SetActive(true);
			canvas_1.transform.Find("VictoryORdefeat/LOSE").gameObject.SetActive(false);
			canvas_2.transform.Find("VictoryORdefeat/WIN").gameObject.SetActive(false);
			canvas_2.transform.Find("VictoryORdefeat/LOSE").gameObject.SetActive(true);
		}
		if (ShareSceneVariable.P2_info.isWin)
		{
			canvas_1.transform.Find("VictoryORdefeat/WIN").gameObject.SetActive(false);
			canvas_1.transform.Find("VictoryORdefeat/LOSE").gameObject.SetActive(true);
			canvas_2.transform.Find("VictoryORdefeat/WIN").gameObject.SetActive(true);
			canvas_2.transform.Find("VictoryORdefeat/LOSE").gameObject.SetActive(false);
		}
	}
}
