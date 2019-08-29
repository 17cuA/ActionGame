//---------------------------------------
// 勝敗表示
//---------------------------------------
// 作成者:三沢
// 作成日:2019.08.29
//--------------------------------------
// 更新履歴
// 2019.08.29 作成
//--------------------------------------
// 仕様 
// 場所を指定し、そこに勝敗を表示する
//----------------------------------------
// MEMO 
//----------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Judge : MonoBehaviour
{
	[SerializeField] GameObject Win;
	[SerializeField] GameObject Lose;

	void Awake()
	{
		Win = transform.Find("WIN").gameObject;
		Lose = transform.Find("LOSE").gameObject;
	}

	/// <summary>
	/// 勝敗判定を受け取り、表示(生成に変更するよ)
	/// </summary>
	/// <param name="p1Judge">1Pの勝敗</param>
	/// <param name="p2Judge">2Pの勝敗</param>
	public void Judge(int p1Judge, int p2Judge)
	{
		if (p1Judge > p2Judge)
		{
			Win.SetActive(true);
			Lose.SetActive(false);
		}
		if (p2Judge > p1Judge)
		{
			Win.SetActive(false);
			Lose.SetActive(true);
		}
	}
}
