/*	Manager_Title.cs
 *	Titleシーンの中央管理スクリプト
 *	製作者：宮島幸大
 *	制作日：2019/06/07 
 *	----------更新----------
 *	コメントがない、変数名がわかりづらいところすべて変更 by三沢
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager_Title : MonoBehaviour
{
	public bool activeTitle;	// TitleがActiveかどうか
	public bool demoMovie;  // デモムービーを再生するかどうか
	private float movie;		// ムービー再生までの時間を引く
	private float movieMax;	// ムービーが流れるまでの待ち時間


	//	画面のマスク関係
	public GameObject maskOb;	//	マスク用のイメージが入ってるオブジェクト

	//	--------------------
	//	スタート
	//	--------------------
	void Start()
	{
		activeTitle = false;
		demoMovie = false;
		movieMax = 10;
		movie = movieMax;
	}

	//	--------------------
	//	アップデート
	//	--------------------
	void Update()
	{
        if (Input.anyKeyDown)
         {
            SceneManager.LoadScene("CharacterSelect");
        }
		//// TitleシーンがActiveなら
		//if (activeTitle)
		//{
		//	movie -= Time.deltaTime;        // ムービー再生まで待機
											// ムービーの再生
			//if (movie <= 0)
			//{
			//	demoMovie = true;
			//	mMM.FadeOut(0);
			//}
		}
	}