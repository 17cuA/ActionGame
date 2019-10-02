//---------------------------------------
// Result管理
//---------------------------------------
// 作成者:三沢
// 作成日:2019.08.29
//--------------------------------------
// 更新履歴
// 2019.08.29 作成
//--------------------------------------
// 仕様 
// 情報を受け取り、各プレイヤーに対応できるようにする
//----------------------------------------
// MEMO 
//----------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultController : MonoBehaviour
{
	public UI_Judge ui_Judge;
	void Awake()
	{
		ui_Judge = gameObject.transform.Find("VictoryORdefeat").GetComponent<UI_Judge>();
	}

	/// <summary>
	/// 2つのCanvasに勝敗判定を飛ばす
	/// </summary>
	/// <param name="p1">1Pの勝敗</param>
	/// <param name="p2">2Pの勝敗param>
	public void SetJudge(int p1,int p2)
	{
		ui_Judge.Judge(p1, p2);
	}
}
