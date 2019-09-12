//更新履歴
//2019/09/06 : カウントダウンを早めるキー(F8)の追加 by高野

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using CUEngine.Pattern;

public class FuncKeyManager : SingletonMono<FuncKeyManager>
{
	private bool isPause = false;   //ポーズしているかどうか
	//シーンの名前
	private string[] sceneNames = { "JECLogo", "Title", "CharacterSelect", "Battle", "Result" };
	private int sceneIndex;

	[SerializeField] private CountDownTimer countDownTimer_1;
	[SerializeField] private CountDownTimer countDownTimer_2;

	void Start()
    {
		Application.targetFrameRate = 60;
		for (int i = 0; i < SceneManager.sceneCountInBuildSettings; ++i)
		{
			Scene scene = SceneManager.GetSceneByBuildIndex(i);
			//現在のシーンが何番目かを格納
			if (scene.IsValid())
			{
				sceneIndex = i;
			}
		}

		if(sceneNames[sceneIndex] == "Battle")
		{
			//参照チェック
			if (countDownTimer_2 == null || countDownTimer_2 == null)
				Debug.LogError("参照ミス : FunckeyManagerにcountDownTimerの参照を追加してください");
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
			#region すべてのシーンで使用するキー（Ecs,F1 - F4）
			if (Input.GetKeyDown(KeyCode.Escape))
			{
				if (isPause == false)
				{
					Time.timeScale = 0f;
					isPause = true;
				}
				else
				{
					Time.timeScale = 1f;
					isPause = false;
				}
			}
			//1シーン分戻る
			if (Input.GetKeyDown(KeyCode.F1))
			{
				sceneIndex = Mathf.Clamp(sceneIndex - 1, 0, sceneNames.Length);
				SceneManager.LoadScene(sceneNames[sceneIndex]);
				Sound.StopBgm();
			}
			//1シーン分進む
			if (Input.GetKeyDown(KeyCode.F2)/* && SceneManager.GetActiveScene().name != "CharacterSelect"*/)
			{
				sceneIndex = Mathf.Clamp(sceneIndex + 1, 0, sceneNames.Length - 1);
				SceneManager.LoadScene(sceneNames[sceneIndex]);
				Sound.StopBgm();
			}
			//現在のシーンを再ロード
			if (Input.GetKeyDown(KeyCode.F3))
			{
				SceneManager.LoadScene(sceneNames[sceneIndex]);
				Sound.StopBgm();
			}
			//タイトルに戻る
			if (Input.GetKeyDown(KeyCode.F4))
			{
				sceneIndex = 0;
				SceneManager.LoadScene(sceneNames[sceneIndex]);
				Sound.StopBgm();
			}
			#endregion
			#region バトルシーンで使用するキー
			if (sceneNames[sceneIndex] == "Battle")
			{
				//1P側の体力を5減らす
				if (Input.GetKeyDown(KeyCode.F5))
				{
					GameManager.Instance.Player_one.HP -= 25;
				}
				//2P側の体力を5減らす
				if (Input.GetKeyDown(KeyCode.F6))
				{
					GameManager.Instance.Player_two.HP -= 25;
				}
				//タイマーカウントダウンを止める
				if (Input.GetKeyDown(KeyCode.F7))
				{
					countDownTimer_1.PlayCountDown(false);
					countDownTimer_2.PlayCountDown(false);
				}
				//タイマーカウントダウンを進める
				if (Input.GetKeyDown(KeyCode.F8))
				{
					countDownTimer_1.PlayCountDown(true);
					countDownTimer_2.PlayCountDown(true);
				}
				//タイマーカウントを10ずつ進める
				if (Input.GetKeyDown(KeyCode.F9))
				{
					countDownTimer_1.CountTenSeconds();
					countDownTimer_2.CountTenSeconds();
				}
			}
			#endregion
		}
	}
}
