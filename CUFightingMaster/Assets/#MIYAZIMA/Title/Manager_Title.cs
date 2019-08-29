/*	Manager_Title.cs
 *	Titleシーンの中央管理スクリプト
 *	製作者：宮島幸大
 *	制作日：2019/06/07 
 *	----------更新----------
 *	コメントがない、変数名がわかりづらいところすべて変更 by三沢
 *	フェードインフェードアウトさせる処理の追加  2019.08.24 by 高野
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager_Title : MonoBehaviour
{
	public bool activeTitle;    // TitleがActiveかどうか
	public bool demoMovie;  // デモムービーを再生するかどうか
	private float movie;        // ムービー再生までの時間を引く
	private float movieMax; // ムービーが流れるまでの待ち時間

	[SerializeField] private CanvasController_Title canvasController_Title;
	private bool isPushedAnykey = false;

	//	画面のマスク関係
	public GameObject maskOb;   //	マスク用のイメージが入ってるオブジェクト

	//	--------------------
	//	スタート
	//	--------------------
	void Start()
	{
		// 飯塚追加-------------------------------------------
		Sound.LoadBgm("BGM03", "BGM03");
		Sound.PlayBgm("BGM03", 0.4f, 1, true);
		// ---------------------------------------------------
		activeTitle = false;
		demoMovie = false;
		movieMax = 10;
		movie = movieMax;

		canvasController_Title.BrackOut();
	}

	//	--------------------
	//	アップデート
	//	--------------------
	void Update()
	{
		if (isPushedAnykey == true)
		{
			if (canvasController_Title.StartFadeOut())
				SceneManager.LoadScene("CharacterSelect");
		}
		else
		{
			if (canvasController_Title.StartFadeIn())
			{
				//ポーズ処理
				if (Mathf.Approximately(Time.timeScale, 0f)) return;

				//入力があったら
				if (Input.anyKeyDown && !Input.GetKeyDown(KeyCode.Escape) && !Input.GetKeyDown(KeyCode.F1) && !Input.GetKeyDown(KeyCode.F2) && !Input.GetKeyDown(KeyCode.F3) && !Input.GetKeyDown(KeyCode.F4))
				{
					// 飯塚追加-------------------------------------------
					Sound.LoadSe("Menu_Decision", "Se_menu_decision");
					Sound.PlaySe("Menu_Decision", 1, 0.8f);
					// ---------------------------------------------------
					isPushedAnykey = true;
				}
			}
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