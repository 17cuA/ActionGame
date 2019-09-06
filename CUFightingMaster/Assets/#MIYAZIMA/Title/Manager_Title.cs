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
	private float movieMax; // ムービーが流れるまでの待ち時間

	[SerializeField]private CanvasController_Title canvasController_Title;

	//	画面のマスク関係
	public GameObject maskOb;   //	マスク用のイメージが入ってるオブジェクト

	private bool isBright = false;
    private bool isPushKey = false;

	//	--------------------
	//	スタート
	//	--------------------
	void Start()
	{
		//画面を暗くする
		canvasController_Title.BrackOut();
		 // 飯塚追加-------------------------------------------
        Sound.LoadBgm("BGM_Title", "BGM_Title");
        Sound.PlayBgm("BGM_Title", 0.4f, 1, true);
        // ---------------------------------------------------
		activeTitle = false;
		demoMovie = false;
        isBright = false;
		movieMax = 10;
		movie = movieMax;
	}

	//	--------------------
	//	アップデート
	//	--------------------
	void Update()
	{
		//ポーズ処理
		if (Mathf.Approximately(Time.timeScale, 0f)) return;
		
        //画面が明るいとき
		if (isBright == true)
		{

            if (Input.anyKeyDown && !Input.GetKeyDown(KeyCode.Escape) && !Input.GetKeyDown(KeyCode.F1) && !Input.GetKeyDown(KeyCode.F2) && !Input.GetKeyDown(KeyCode.F3) && !Input.GetKeyDown(KeyCode.F4))
			{
				Sound.LoadSe("Menu_Decision", "Se_menu_decision");
				Sound.PlaySe("Menu_Decision", 1, 0.3f);
                isPushKey = true;
			}

            if (isPushKey)
            {
                //画面が暗くなったらシーン遷移
                if (canvasController_Title.StartFadeOut())
                    SceneManager.LoadScene("CharacterSelect");
            }
        }

        //画面が暗いとき
        //画面を徐々に明るくする
        else if (canvasController_Title.StartFadeIn())
            isBright = true;

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