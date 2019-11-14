//---------------------------------------
// リザルトのメニューのコントローラー
//---------------------------------------
// 作成者:高野
// 作成日:2019.10.24
//--------------------------------------
// 更新履歴
// 2019.10.24 作成
//--------------------------------------
// 仕様 
//
//----------------------------------------
// MEMO 
//
//----------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ResultMenuController : MonoBehaviour
{
	[SerializeField] ResultMenuItems resutMenuItems;
	[SerializeField] GameObject  cursor;

	[SerializeField] public Menu_Base currentMenu_Base;

	/// <summary>
	/// メニューの表示
	/// </summary>
	public void Call_MenuDisplay()
	{
		currentMenu_Base.Display();
	}

	/// <summary>
	/// メニューの非表示
	/// </summary>
	public void Call_MenuInvisible()
	{
		currentMenu_Base.Invisivle();
	}

	/// <summary>
	/// 上の入力があったときに呼び出す
	/// </summary>
	public void Call_PressedUpperButton()
	{
		currentMenu_Base = currentMenu_Base.onMenuItem;
		cursor.transform.position = currentMenu_Base.transform.position ;
	}

	/// <summary>
	/// 下の入力があったときに呼び出す
	/// </summary>
	public void Call_PressedDownButton()
	{
		currentMenu_Base = currentMenu_Base.underMenuItem;
		cursor.transform.position = currentMenu_Base.transform.position ;
	}

	/// <summary>
	/// 決定の入力があったときに呼び出す
	/// </summary>
	public  void Call_PressedDecideButton()
	{
		currentMenu_Base.Decide();
		cursor.transform.position = currentMenu_Base.transform.position ;
	}
	
	private void Start()
	{
		//最初に選択されているメニュ項目
		currentMenu_Base = resutMenuItems.menu_Rematch;
	}
	private void Update()
	{
		//Debu	g------------------------
		if(Input.GetKeyDown("w"))
		{
			Call_PressedUpperButton();
		}
		if(Input.GetKeyDown("s"))
		{
			Call_PressedDownButton();
		}
		if(Input.GetKeyDown("e"))
		{
			Call_PressedDecideButton();
		}
	}
}
