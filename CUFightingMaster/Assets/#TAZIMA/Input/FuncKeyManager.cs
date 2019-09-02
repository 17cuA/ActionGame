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
    void Start()
    {
		for (int i = 0; i < SceneManager.sceneCountInBuildSettings; ++i)
		{
			Scene scene = SceneManager.GetSceneByBuildIndex(i);
			//現在のシーンが何番目かを格納
			if (scene.IsValid())
			{
				sceneIndex = i;
			}
		}
	}
    void Update()
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
			if (Input.GetKeyDown(KeyCode.F2))
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
				//ラウンドリセット
				if (Input.GetKeyDown(KeyCode.F7))
				{
					//gameRoundCount = 0;
					//getRoundCount_p1 = 0;
					//getRoundCount_p2 = 0;
					//canvasController.ResetWinCounter();

					//currentUpdate = ResetParameter;
				}
			}
			#endregion
		}
	}
}
