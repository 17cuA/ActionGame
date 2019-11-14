//---------------------------------------
// メニューの要素のベースクラス
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
using UnityEngine.UI;
[RequireComponent(typeof(Image))]

public class Menu_Base : MonoBehaviour , IMenuItem
{
	[SerializeField] private Image image;	//見た目

	[SerializeField] public Menu_Base onMenuItem;			//この項目の一つ上の項目
	[SerializeField] public Menu_Base underMenuItem;	//この項目の一つ下の項目
	
	/// <summary>
	/// 決定された時の動作
	/// </summary>
	public virtual void  Decide(){}

	/// <summary>
	/// 表示
	/// </summary>
	public void Display()
	{
		image.enabled = true;
	}

	/// <summary>
	/// 非表示
	/// </summary>
	public void Invisivle()
	{
		image.enabled = false;
	}
}
