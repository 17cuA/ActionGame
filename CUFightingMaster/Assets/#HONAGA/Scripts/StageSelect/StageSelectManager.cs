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
		Sound.LoadBGM("BGM_Menu", "BGM_Menu");
		Sound.PlayBGM("BGM_Menu", 1, 1.0f, true);
		// カーテンを一気に下す処理
		CanvasController_CharacterSelect.CanvasControllerInstance.InitDownCurtain();
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
	public GameObject[] mskObject;

	public int playerNumber = 0;        // プレイヤーの番号(Inspecterでインスタンスごとに設定)
	public string controllerName;       // コントローラーの名前(自動設定)
	public float moveCursorFrame;       // カーソルが移動していない時間
	public float limitCursorFrame;      // カーソルが移動できるようになるためのリミット

	public static bool acceptFlag = false;
	public static StageNumber currentStage;

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
		Mask();
		CustomButtonMethod(controllerName, playerNumber);
	}

	public void InputCursol()
	{
		inputDeirection.y = Input.GetAxisRaw(string.Format("{0}Player{1}_Vertical", controllerName, playerNumber));
		inputDeirection.x = Input.GetAxisRaw(string.Format("{0}Player{1}_Horizontal", controllerName, playerNumber));
	}
	public void Mask()
	{
		if (currentStage == StageNumber.SINSEKAI)
		{
			mskObject[1].active = true;
			mskObject[0].active = false;
		}
		else if (currentStage == StageNumber.DOUTONBORI)
		{
			mskObject[0].active = true;
			mskObject[1].active = false;
		}
	}
	public virtual void InputCursolDirection(StageNumber _selectStage, Vector2 _inputDir)
	{
		Sound.LoadSE("Menu_MoveCursor", "Se_menu_moveCursor");
		Sound.PlaySE("Menu_MoveCursor", 1, 0.8f);

		_selectStage += (int)_inputDir.y;

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
			acceptFlag = !acceptFlag;
		}
		//-------------------------------------------------------------------------------------------------------------------------------------
	}
}