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
	public GameObject[] FighterModels = new GameObject[2];
	public ResultNomalAnimationController resultNomalAnimatonController;

	[SerializeField]private GameObject player1CreatePos;
	[SerializeField]private GameObject player2CreatePos;

	/// <summary>
	/// キャラクターの生成
	/// </summary>
	public void FighterCreate()
	{
		FighterModels[0] = Instantiate(GameDataStrage.Instance.fighterStatuses[(int)PlayerNumber.Player1].PlayerModel, player1CreatePos.transform.position, transform.rotation);
		FighterModels[1] = Instantiate(GameDataStrage.Instance.fighterStatuses[(int)PlayerNumber.Player2].PlayerModel, player2CreatePos.transform.position, transform.rotation);
	}

	/// <summary>
	/// Animatorの参照
	/// </summary>
	public Animator GetRefAnimator(int _index)
	{
		Animator animator = FighterModels[_index].GetComponentInChildren<Animator>();

		return animator;
	}

	/// <summary>
	/// NomalAniamtionPlayerの参照
	/// </summary>
	/// <param name="_index"></param>
	/// <returns></returns>
	public NomalAnimationPlayer GeReftNomalAnimationPlayer(int _index)
	{
		NomalAnimationPlayer nomalAnimationPlayer = FighterModels[_index].GetComponentInChildren<NomalAnimationPlayer>();

		return nomalAnimationPlayer;
	}
}
