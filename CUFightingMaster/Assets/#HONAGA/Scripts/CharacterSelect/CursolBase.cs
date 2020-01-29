using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
/// <typeparam name="T">Enumで属性を作る</typeparam>
/// <typeparam name="Gamobject">動かしたいオブジェクトの型を入れる(基本GameObjectで大丈夫)</typeparam>
public class CursolBase<TEnum,TGameObject>where TEnum:System.Enum
{
	// 動けるかどうかのフラグ
	private bool activeFlag;
	public bool ActiveFlag { get { return activeFlag; } set { activeFlag = value; } }

	//// 縦方向への移動があるかのフラグ
	//private bool inputFlagDirectionY;
	//public bool InputFlagDirectionY { get { return inputFlagDirectionY; } set { inputFlagDirectionY = value; } }
	//// 横方向への移動があるかのフラグ
	//private bool inputFlagDirectionX;
	//public bool InputFlagDirectionX { get { return inputFlagDirectionX; } set { inputFlagDirectionX = value; } }
	// 選択したかのフラグ

	private bool acceptFlag;
	public bool AcceptFlag { get { return acceptFlag; } set { acceptFlag = value; } }
	// 入力を受け取る変数
	private Vector2 inputDirection;
	// 1Pか2Pか3Pか、プレイヤーの番号
	public int playerNumber;
	// コントローラーの名前
	public string controllerName;
	// カーソルの待機時間
	private float moveCursorFrame;
	// カーソルの最大待機時間(この時間を超えたら移動できる)
	private float limitCursorFrame;

	private TEnum currentNumber;
	private TGamobject moveobject;

	/// <summary>
	/// CursorのUpdateの処理、使用する場面ごとにoverrideで処理を変える
	/// </summary>
	public void Update()
	{
		// カーソルが動けない場合早期リターンで処理を止める
		if (ActiveFlag == false) return;
		// カーソルが動いた後のクールタイム
		moveCursorFrame += Time.deltaTime;

		InputCursol();

		// 入力があり、決定していない時のみカーソルを動かす
		if (inputDirection != Vector2.zero && AcceptFlag == false)
		{
			if (moveCursorFrame >= limitCursorFrame)
			{
				InputCursolDirection(currentCharacter, inputDeirection);
				CursolMove(characterSelectObjectDatas[(int)currentCharacter]);
			}
		}
		// オバーフロー防止のため追加
		if (moveCursorFrame > 1.0f)
		{
			moveCursorFrame = 1.0f;
		}
	}
	public void InitCursol(bool _inputFlagY,bool _inputFlagX,int _playerNumber,string _controllerName,float _limitCursolFrame)
	{
		activeFlag = true;
		InputFlagDirectionY = _inputFlagY;
		InputFlagDirectionX = _inputFlagX;
		playerNumber = _playerNumber;
		limitCursorFrame = _limitCursolFrame;
		SetController();
	}
	/// <summary>
	///	プレイヤーごとにコントローラーをセット
	/// </summary>
	public void SetController()
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
	public void InputCursol()
	{
		inputDirection.y = Input.GetAxisRaw(string.Format("{0}Player{1}_Vertical", controllerName, playerNumber));
		inputDirection.x = Input.GetAxisRaw(string.Format("{0}Player{1}_Horizontal", controllerName, playerNumber));
	}
	/// <summary>
	/// カーソルの位置(posirion)を移動させる
	/// </summary>
	/// <param name="_characterSelectObjectData">キャラデータ(設定されているキャラごとのパネルの位置へ移動させる)</param>
	public void CursolMove(CharacterSelectObjectData _characterSelectObjectData)
	{
		var asdf = System.Enum.GetValues(typeof(TEnum));
		

		cursol.transform.position = _characterSelectObjectData.PanelPosition[playerNumber].transform.position;
	}
}
