/*------------------------------------------------------------------------------------------------------------------------------------------------------
 * 作成日：	2019/06/06
 * 作成者：	横山凌
 * 
 * 現在設定しているGameWindowのサイズを取得する
 * 
 *					＿人人人人人人人人人人人人人人人人人人人＿
 *					＞　ScreenWindowではないので注意！！　＜
 *					￣Y^Y^Y^Y^Y^Y^Y^Y^Y^Y^Y^Y^Y^Y￣
 ------------------------------------------------------------------------------------------------------------------------------------------------------*/
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GetGameWindow
{
	/// <summary>
	/// GameWindowの幅をint型で返す
	/// </summary>
	/// <returns></returns>
	public static float GetWidth()
	{
		// strng型で幅を取得
		string[] screenres = UnityStats.screenRes.Split('x');

		// string型をint型に変換(キャストではエラーが出る)
		var width = float.Parse(screenres[0]);

		return width;
	}

	/// <summary>
	/// GameWindowの幅をint型で返す
	/// </summary>
	/// <returns></returns>
	public static float GetHeight()
	{
		// strng型で幅を取得
		string[] screenres = UnityStats.screenRes.Split('x');

		// string型をint型に変換(キャストではエラーが出る)
		var height = float.Parse(screenres[1]);
		return height;
	}
}
