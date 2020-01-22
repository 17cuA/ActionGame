﻿//更新履歴
//2019/09/06 : カウントダウンを早めるキー(F8)の追加 by高野

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using CUEngine.Pattern;

public class FuncKeyManager : SingletonMono<FuncKeyManager>
{
	//シーンの名前
	private string[] sceneNames = { "JECLogo", "Title", "CharacterSelect", "Battle", "Result" };
	private int sceneIndex;

	[SerializeField] CanvasController canvasController;
	CountDownTimer countDownTimer_1;
	CountDownTimer countDownTimer_2;

    public bool isOnCommandUI = true;

	public void Init()
	{
		if (Application.targetFrameRate != 60) Application.targetFrameRate = 60;
		for (int i = 0; i < SceneManager.sceneCountInBuildSettings; ++i)
		{
			Scene scene = SceneManager.GetSceneByBuildIndex(i);
			//現在のシーンが何番目かを格納
			if (scene.IsValid())
			{
				sceneIndex = i;
			}
		}

		if (sceneNames[sceneIndex] == "Battle")
		{
			//参照チェック
			if (canvasController == null)
				Debug.LogError("参照ミス : FunckeyManagerにcanvascontrollerの参照を追加してください");
		}
	}
	void Update()
    {
		InputFuncKey();
	}
	public void InputFuncKey()
	{
		//ファンクションキーの処理
		if (Input.anyKeyDown)
		{
			#region すべてのシーンで使用するキー（Ecs,F1 - F4,F10,F12）
			if (Input.GetKeyDown(KeyCode.Escape))
			{
				//終了
				Application.Quit();
			}
			//1シーン分戻る
			if (Input.GetKeyDown(KeyCode.F1))
			{
				sceneIndex = Mathf.Clamp(sceneIndex - 1, 0, sceneNames.Length);
				SceneManager.LoadScene(sceneNames[sceneIndex]);
				Sound.StopBGM();
			}
			//1シーン分進む
			if (Input.GetKeyDown(KeyCode.F2))
			{
				sceneIndex = Mathf.Clamp(sceneIndex + 1, 0, sceneNames.Length - 1);
				SceneManager.LoadScene(sceneNames[sceneIndex]);
				Sound.StopBGM();
			}
			//現在のシーンを再ロード
			if (Input.GetKeyDown(KeyCode.F3))
			{
				SceneManager.LoadScene(sceneNames[sceneIndex]);
				Sound.StopBGM();
			}
			//タイトルに戻る
			if (Input.GetKeyDown(KeyCode.F4))
			{
				sceneIndex = 0;
				SceneManager.LoadScene(sceneNames[sceneIndex]);
				Sound.StopBGM();
			}
            if(Input.GetKeyDown(KeyCode.F12))
            {
                isOnCommandUI = !isOnCommandUI;
            }
			if (Input.GetKeyDown(KeyCode.F10))
			{

			}
			#endregion
			#region バトルシーンで使用するキー
			if (sceneNames[sceneIndex] == "Battle")
			{
				//1P側の体力を5減らす
				if (Input.GetKeyDown(KeyCode.F5))
				{
					GameManager.Instance.Player_one.HP -= 400;
				}
				//2P側の体力を5減らす
				if (Input.GetKeyDown(KeyCode.F6))
				{
					GameManager.Instance.Player_two.HP -= 400;
				}
				//タイマーカウントダウンを止める
				if (Input.GetKeyDown(KeyCode.F7))
				{
					canvasController.Call_StopCountDown();
				}
				//タイマーカウントダウンを進める
				if (Input.GetKeyDown(KeyCode.F8))
				{
					canvasController.Call_ResumeCountdown();
				}
				//タイマーカウントを10ずつ進める
				if (Input.GetKeyDown(KeyCode.F9))
				{
					canvasController.Call_ResumeCountdown();
				}
			}
			#endregion
		}
	}
}
