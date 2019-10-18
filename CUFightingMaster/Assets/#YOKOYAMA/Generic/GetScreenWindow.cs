/*------------------------------------------------------------------------------------------------------------------------------------------------------
 * 作成日：	2019/06/06
 * 作成者：	横山凌
 * 
 * 現在設定しているScreenWindowのサイズを取得する
 * 
 *					＿人人人人人人人人人人人人人人人人人人人＿
 *					＞　GameWindowではないので注意！！　＜
 *					￣Y^Y^Y^Y^Y^Y^Y^Y^Y^Y^Y^Y^Y^Y￣
 ------------------------------------------------------------------------------------------------------------------------------------------------------*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetScreenWindow
{ 
	/// <summary>
	/// ScreenWindowの幅を返す
	/// </summary>
	public static float GetWidth()
	{
		return Screen.width;
	}

	/// <summary>
	/// ScreenWindowの高さを返す
	/// </summary>
	/// <returns></returns>
	public static float GetHeight()
	{
		return Screen.height;
	}
}
