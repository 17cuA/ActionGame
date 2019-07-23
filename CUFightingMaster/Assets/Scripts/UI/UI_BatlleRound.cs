//---------------------------------------
// 試合中に表示されるUI
//---------------------------------------
// 作成者:高野
// 作成日:2019.07.16
//--------------------------------------
// 更新履歴
// 2019.07.15 作成
//--------------------------------------
// 仕様 
//※07/15どう動かせば良いのかわからないので、一時的なプログラムです、随時変更して下さい※
// 0716現在表示するだけです
//----------------------------------------
// MEMO 
// 制御するUIの参照は手動です
//----------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_BatlleRound : MonoBehaviour
{
	//参照
	public GameObject inGameUI;

	public void DisplayBatlleUI()
	{
		inGameUI.SetActive(true);
	}

	public void NotDisplayBattleUI()
	{
		inGameUI.SetActive(false);
	}
	
}
