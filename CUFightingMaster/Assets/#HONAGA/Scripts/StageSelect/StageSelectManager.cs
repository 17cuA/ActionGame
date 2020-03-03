using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class StageSelectManager : MonoBehaviour
{
	public StageSelectCursol stageSelectCursol_1P;
	public StageSelectCursol stageSelectCursol_2P;
	// Start is called before the first frame update
	void Start()
	{
		StageSelectCursol.acceptFlag = false;
		stageSelectCursol_1P.InitCursol();
		stageSelectCursol_2P.InitCursol();
		Sound.LoadBGM("BGM_Menu", "BGM_Menu");
		Sound.PlayBGM("BGM_Menu", 0.6f, 1.0f, true);
		// カーテンを一気に下す処理
		CanvasController_CharacterSelect.CanvasControllerInstance.InitDownCurtain();
		//ステージ選択カーソルの初期化
		StageSelectCursol.currentStage = StageSelectCursol.StageNumber.SINSEKAI;
	}

	// Update is called once per frame
	void Update()
	{
		if (CanvasController_CharacterSelect.CanvasControllerInstance.curtainFlag == false)
		{
			// カーテンが上がるまでループ
			CanvasController_CharacterSelect.CanvasControllerInstance.curtainFlag = CanvasController_CharacterSelect.CanvasControllerInstance.UpCurtain();
			return;
		}

		stageSelectCursol_1P.CursolUpdate();
		stageSelectCursol_2P.CursolUpdate();

		if (StageSelectCursol.acceptFlag)
		{
			switch (StageSelectCursol.currentStage)
			{
				case StageSelectCursol.StageNumber.SINSEKAI:
					if (CanvasController_CharacterSelect.CanvasControllerInstance.DownCurtain())
					{
						SceneManager.LoadScene("Battle");
					}
					break;
				case StageSelectCursol.StageNumber.DOUTONBORI:
					if (CanvasController_CharacterSelect.CanvasControllerInstance.DownCurtain())
					{
						SceneManager.LoadScene("Battle2");
					}
					break;
			}
		}
	}
}
[Serializable]
public class StageSelectCursol
{
	public enum StageNumber
	{
		SINSEKAI,
		DOUTONBORI,
	}
	public Vector2 inputDeirection;     // 移動の移動の方向
	public GameObject[] stagePanels;
	public GameObject[] stageObjects;

	public int playerNumber = 0;        // プレイヤーの番号(Inspecterでインスタンスごとに設定)
	public string controllerName;       // コントローラーの名前(自動設定)
	public float moveCursorFrame;       // カーソルが移動していない時間
	public float limitCursorFrame;      // カーソルが移動できるようになるためのリミット

	public static bool acceptFlag = false;
	public static StageNumber currentStage;

	public void InitCursol()
	{
		var controllerNames = Input.GetJoystickNames();
		if (playerNumber < controllerNames.Length)
		{
			if (controllerNames[playerNumber] != "")
			{
				controllerName = string.Format("{0}_", controllerNames[playerNumber]);
			}
		}
	}
	public void CursolUpdate()
	{
		InputCursol();
		moveCursorFrame += Time.deltaTime;

		// 入力があり、決定していない時のみカーソルを動かす
		if (inputDeirection != Vector2.zero && acceptFlag == false)
		{
			if (moveCursorFrame >= limitCursorFrame)
			{
				InputCursolDirection(currentStage, inputDeirection);
			}
		}
		// オバーフロー防止のため追加
		if (moveCursorFrame > 1.0f)
		{
			moveCursorFrame = 1.0f;
		}
		StageChange();
		CustomButtonMethod(controllerName, playerNumber);
	}

	public void InputCursol()
	{
		inputDeirection.y = Input.GetAxisRaw(string.Format("{0}Player{1}_Vertical", controllerName, playerNumber));
		inputDeirection.x = Input.GetAxisRaw(string.Format("{0}Player{1}_Horizontal", controllerName, playerNumber));
	}
	public void StageChange()
	{
		if (currentStage == StageNumber.SINSEKAI)
		{
			stagePanels[0].transform.position = new Vector3(stagePanels[0].transform.position.x, stagePanels[0].transform.position.y, -3.0f);
			stagePanels[1].transform.position = new Vector3(stagePanels[1].transform.position.x, stagePanels[1].transform.position.y, -0.8f);
			stageObjects[0].active = true;
			stageObjects[1].active = false;
		}
		else if (currentStage == StageNumber.DOUTONBORI)
		{
			stagePanels[1].transform.position = new Vector3(stagePanels[1].transform.position.x, stagePanels[0].transform.position.y, -3.0f);
			stagePanels[0].transform.position = new Vector3(stagePanels[0].transform.position.x, stagePanels[1].transform.position.y, -0.8f);
			stageObjects[0].active = false;
			stageObjects[1].active = true;
		}
	}
	public virtual void InputCursolDirection(StageNumber _selectStage, Vector2 _inputDir)
	{
		Sound.LoadSE("Menu_MoveCursor", "Se_menu_moveCursor");
		Sound.PlaySE("Menu_MoveCursor", 1, 0.1f);

		_selectStage += (int)_inputDir.x * -1;

		if (_selectStage < (StageNumber)0)
		{
			_selectStage = StageNumber.SINSEKAI;
		}
		else if (_selectStage > (StageNumber)1)
		{
			_selectStage = StageNumber.DOUTONBORI;
		}
		moveCursorFrame = 0;
		currentStage = _selectStage;
	}
	public void CustomButtonMethod(string _controllerName, int _playerNumber)
	{
		// 入力ごとの処理-------------------------------------------------------------------------------------------------------------------
		if (Input.GetButtonDown(string.Format("{0}Player{1}_Attack1", _controllerName, _playerNumber)))
		{
			Sound.LoadSE("Menu_Decision", "Se_menu_decision");
			Sound.PlaySE("Menu_Decision", 1, 0.1f);
			//ステージ移行のフラグがfalseだったらステージ移行のフラグをtrueにする
			if (!acceptFlag) acceptFlag = !acceptFlag;
		}
		//-------------------------------------------------------------------------------------------------------------------------------------
	}
}