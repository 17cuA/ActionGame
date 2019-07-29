//---------------------------------------------------------------
// シーンのコントロール
//---------------------------------------------------------------
// 作成者:三沢
// 作成日:2019.07.04
//----------------------------------------------------
// 更新履歴
// 2019.07.04 クソ適当に作ってみたよ、まともなものを作ったら消してね
//-----------------------------------------------------
// 仕様
// LoadSceneをすべてここに作る(なんとなく)
//-----------------------------------------------------
// MEMO
//----------------------------------------------------
// 現在判明しているバグ
//----------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
	public static SceneController instance;     // MovingMaskManagerにて使用していますよー

	void Awake()
	{
		instance = FindObjectOfType<SceneController>();
	}
	// LogoSceneに飛ぶ
	public void MoveLogo()
	{
		SceneManager.LoadScene("Logo");
	}
	// TitleSceneに飛ぶ
	public void MoveTitle()
	{
		SceneManager.LoadScene("Title");
	}
	// DemoMovieに飛ぶ
	public void MoveDemoMovie()
	{
		SceneManager.LoadScene("DemoMovie");
	}
	// CharacterSelectに飛ぶ
	public void MoveCharacterSelect()
	{
		SceneManager.LoadScene("CharacterSelect");
	}
	// Battleに飛ぶ
	public void MoveBattle()
	{
		SceneManager.LoadScene("Battle");
	}
}
