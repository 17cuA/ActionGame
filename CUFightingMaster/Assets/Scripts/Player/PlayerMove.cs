using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerMove : MonoBehaviour
{
	public Animator anim;
	public string comName;		//コマンドの名前を格納
	public string atkBotton;		//攻撃ボタンの名前を格納
	//コマンドを保持する時間
	public int comRetentionFrame = 7;
	public int comRetentionNowFrame;
	//-------------------------------------------------------------------
	//Start
	//-------------------------------------------------------------------
	private void Start()
    {
		anim = GetComponent<Animator>();
		comName = null;
		comRetentionNowFrame = 0;
	}
	//-------------------------------------------------------------------
	//Update
	//-------------------------------------------------------------------
	private void Update()
    {
		//技発動
		SetAtkBotton();			//攻撃ボタン入力
		if(atkBotton != null)
		{
			if (comName != null) CommandTrigger();		//コマンド技判定
			else AtkTrigger();							//通常技判定
		}
        CommandReset();
	}
	//-------------------------------------------------------------------
	//関数
	//-------------------------------------------------------------------
	private void CommandReset()
	{
		if (comRetentionNowFrame <= comRetentionFrame) comRetentionNowFrame++;
		else if (comName != null) comName = null;
	}
	public void SetCommand(string str)
	{
		comName = str;
		comRetentionNowFrame = 0;
	}
    public void CommandTrigger()
    {
		//コマンド技処理
		Debug.Log(atkBotton + comName);
    }
	public void SetAtkBotton()
	{
		if (Input.anyKeyDown)
		{
			atkBotton = "";
			if (Input.GetButtonDown("P1"))	atkBotton += "P1_";
			if (Input.GetButtonDown("P2"))	atkBotton += "P2_";
			if (Input.GetButtonDown("P3"))	atkBotton += "P3_";
			if (Input.GetButtonDown("K1"))	atkBotton += "K1_";
			if (Input.GetButtonDown("K2"))	atkBotton += "K2_";
			if (Input.GetButtonDown("K3"))	atkBotton += "K3_";
		}
		else if (atkBotton != null) atkBotton = null;
	}
	public void AtkTrigger()
	{
		//通常技処理
		Debug.Log(atkBotton);
	}
}
