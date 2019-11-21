﻿//---------------------------------------
// リザルトシーンのキャラクター生成
//---------------------------------------
// 作成者:高野
// 作成日:2019.11.14
//--------------------------------------
// 更新履歴
//
//--------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterCreater : MonoBehaviour
{
	public GameObject FighterPlayer1 { private set; get; }
	public GameObject FighterPlayer2 { private set; get; }

	[SerializeField]private GameObject player1CreatePos;
	[SerializeField]private GameObject player2CreatePos;

	/// <summary>
	/// キャラクターの生成
	/// </summary>
	public void FighterCreate()
	{
		FighterPlayer1 = Instantiate(GameDataStrage.Instance.fighterStatuses[0].PlayerModel, player1CreatePos.transform.position, transform.rotation);
		FighterPlayer2 = Instantiate(GameDataStrage.Instance.fighterStatuses[1].PlayerModel, player2CreatePos.transform.position, transform.rotation);
	}
}
