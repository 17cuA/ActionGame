/*InputManager.cs
 * Input管理スクリプト
 * 製作者：宮島幸大
 * 制作日：2019/06/13
 * ----------更新----------
 * 2019/07/03：シーン名をGameからBatleへ変更
 * 
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputManager : MonoBehaviour
{
	//	マネージャー関係
	public GameObject managerObject;	//	そのシーンのマネージャがアタッチされているオブジェクトを収納する	※基本的にEventSystemにアタッチ
	Manager_Title mT;							//	Titleのマネージャー
	Manager_DemoMovie mD;				//	DemoMovieのマネージャー
	Manager_CharacterSelect mC;			//	CharacterSelectのマネージャー
	Manager_Game mG;						//	Gameのマネージャー

	//	--------------------
	//	初期化
	//	--------------------
	void Start()
	{
		switch (SceneManager.GetActiveScene().name)
		{
			case "Title":
				mT = managerObject.GetComponent<Manager_Title>();
				break;
			case "DemoMovie":
				mD = managerObject.GetComponent<Manager_DemoMovie>();
				break;
			case "CharacterSelect":
				mC = managerObject.GetComponent<Manager_CharacterSelect>();
				break;
			case "Battle":
				mG = managerObject.GetComponent<Manager_Game>();
				break;
		}
	}

	//	--------------------
	//	アップデート
	//	--------------------
	void Update()
	{
		//	いずれかのキー
		if (Input.anyKey)
		{
			switch (SceneManager.GetActiveScene().name)
			{
				case "Title":
					mT.mMM.FadeOut(0);
					break;
				case "DemoMovie":
					mD.MoveScene();
					
					break;
			}
			Cursor.visible = false;
		}
		//---------------------------------------------------------------------

		//	デバッグ用1key
		if (Input.GetKeyDown(KeyCode.Alpha1))
			switch (SceneManager.GetActiveScene().name)
			{
				case "CharacterSelect":
					if (mC.activeCharaselect)
						mC.playerDecision[0] = true;
					break;
				case "Battle":
					if (mG.activeGame)
					mG.playerStatus[0].Damage(1000);
					break;
			}

		//	
		if (Input.GetKeyDown(KeyCode.Alpha2))
			switch (SceneManager.GetActiveScene().name)
			{
				case "CharacterSelect":
					if (mC.activeCharaselect)
						mC.playerDecision[1] = true;
					break;
				case "Battle":
					if (mG.activeGame)
						mG.playerStatus[1].Damage(1000);
					break;
			}

		//	デバッグ用１P 右
		if (Input.GetKeyDown(KeyCode.D))
		{
			switch (SceneManager.GetActiveScene().name)
			{
				case "CharacterSelect":
					mC.PlayerSelectMove(0, "Right");
					break;
			}
		}
		//	デバッグ用１P 左
		if (Input.GetKeyDown(KeyCode.A))
		{
			switch (SceneManager.GetActiveScene().name)
			{
				case "CharacterSelect":
					mC.PlayerSelectMove(0, "Left");
					break;
			}
		}
		//	//デバッグ用１P 上
		if (Input.GetKeyDown(KeyCode.W))
		{
			switch (SceneManager.GetActiveScene().name)
			{
				case "CharacterSelect":
					mC.PlayerSelectMove(0, "Up");
					break;
			}
		}
		//	デバッグ用１P 下
		if (Input.GetKeyDown(KeyCode.S))
		{
			switch (SceneManager.GetActiveScene().name)
			{
				case "CharacterSelect":
					mC.PlayerSelectMove(0, "Down");
					break;
			}
		}

		//------------------------------------------
		//	joyconの左アナログスティック横軸
		//	Right
		if (1 <= Input.GetAxis("Horizontal"))
		{
			//	現在のシーンが
			switch (SceneManager.GetActiveScene().name)
			{
				//	CharacterSelectだったら
				case "CharacterSelect":
					mC.PlayerSelectMove(0, "Right");
					break;
				case "Game":
					//PlayerMover.instance.moveInput = Input.GetAxis("Horizontal");
					break;
			}
			Debug.Log("Right");
		}
		//	Left
		else if (-1 >= Input.GetAxis("Horizontal"))
		{
			//	現在のシーンが
			switch (SceneManager.GetActiveScene().name)
			{
				//	CharacterSelectだったら
				case "CharacterSelect":
					mC.PlayerSelectMove(0, "Left");
					break;
				case "Game":
					//PlayerMover.instance.moveInput = Input.GetAxis("Horizontal");
					break;
			}
			Debug.Log("Left");
		}
		//	joyconの左アナログスティック縦軸
		//	UP
		if (1 <= Input.GetAxis("Vertical"))
		{
			//	現在のシーンが
			switch (SceneManager.GetActiveScene().name)
			{
				//	CharacterSelectだったら
				case "CharacterSelect":
					mC.PlayerSelectMove(0, "Up");
					break;
			}
			Debug.Log("Up");
		}
		//	Down
		else if (-1 >= Input.GetAxis("Vertical"))
		{
			//	現在のシーンが
			switch (SceneManager.GetActiveScene().name)
			{
				//	CharacterSelectだったら
				case "CharacterSelect":
					mC.PlayerSelectMove(0, "Down");
					break;
			}
			Debug.Log("Down");
		}
	}
}
//write by Miyajima Kodai