using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//enumを使うために必要
using System;

public class TestInput : MonoBehaviour {
	CommandManager commandManager;	//正規表現でコマンドを判別するスクリプト

	public int playerIndex; //プレイヤー番号
	public string player;   //Inputでプレイヤー毎の入力を識別するための文字列
    public string controllerName = ""; //使用するコントローラーの名前
	public Vector2 inputDirection; //ジョイスティックの入力方向
	public string direction; //現在のジョイスティックの方向
	public int lastDir = 5; //前回のジョイスティックの方向
	public string playerDirection; //プレイヤーの入力方向
	public string atkButton; //攻撃ボタンの名前を格納

	public string debugCommandStr;      //コマンドの文字列をインスペクター上で確認するための変数

	//ジョイスティックの入力方向（方向はNumパッドに依存）
	enum DirJS {
		NONE,
		d1 = 1, //LEFT_DOWN
        d2 = 2, //DOWN
		d3 = 3, //RIGHT_DOWN
        d4 = 4, //LEFT
        d5 = 5, //CENTER
		d6 = 6, //RIGHT
        d7 = 7, //LEFT_UP
        d8 = 8, //UP
		d9 = 9  //RIGHT_UP
    }

    void Start()
    {
        Application.targetFrameRate = 60;
        player = string.Format("Player{0}_", playerIndex);
        //プレイヤー番号に対応した現在接続されているコントローラーを設定
        var controllerNames = Input.GetJoystickNames();
        if (playerIndex < controllerNames.Length)
        {
            if (controllerNames[playerIndex] != "")
            {
                controllerName = string.Format("{0}_", controllerNames[playerIndex]);
            }
        }

		//正規表現でコマンドを判別するスクリプトの変数初期化
        commandManager = gameObject.GetComponent<CommandManager>();
		commandManager.Init();
    }

	public void UpdateGame () {
		DownKeyCheck ();
	}

	public void SetAxis () {
        //X,Yそれぞれの入力を保存
        inputDirection.x = Input.GetAxisRaw (controllerName + player + "Horizontal");
		inputDirection.y = Input.GetAxisRaw (controllerName + player + "Vertical");
	}
	public void SetDirection () {
		SetAxis ();
		float nowDir = 5 + inputDirection.x + (inputDirection.y * -3);
		//方向を調べる
		switch (nowDir) {
			case (int) DirJS.d1:
				lastDir = (int) DirJS.d1;
				playerDirection = "1";
				break;
			case (int) DirJS.d2:
				lastDir = (int) DirJS.d2;
				playerDirection = "2";
				break;
			case (int) DirJS.d3:
				lastDir = (int) DirJS.d3;
				playerDirection = "3";
				break;
			case (int) DirJS.d4:
				lastDir = (int) DirJS.d4;
				playerDirection = "4";
				break;
			case (int) DirJS.d5:
				lastDir = (int) DirJS.d5;
				playerDirection = "5";
				break;
			case (int) DirJS.d6:
				lastDir = (int) DirJS.d6;
				playerDirection = "6";
				break;
			case (int) DirJS.d7:
				lastDir = (int) DirJS.d7;
				playerDirection = "7";
				break;
			case (int) DirJS.d8:
				lastDir = (int) DirJS.d8;
				playerDirection = "8";
				break;
			case (int) DirJS.d9:
				lastDir = (int) DirJS.d9;
				playerDirection = "9";
				break;
			default:
				lastDir = 0;
				direction = null;
				break;
		}
	}

	public void DownKeyCheck () {
		//ジョイスティックまたはキーボードでの方向入力
		SetDirection ();
		//攻撃ボタン入力
		SetAtkBotton ();
		//コマンドの判別
		commandManager.GetCommandData(playerDirection.ToString());
        debugCommandStr = commandManager.inputCommandData;

	}

	//攻撃ボタンの入力を管理
	public void SetAtkBotton () 
	{
		atkButton = "";

		if (Input.GetButtonDown(controllerName + player + "Attack1")) atkButton += "_Atk1";
		if (Input.GetButtonDown (controllerName + player + "Attack2")) atkButton += "_Atk2";
		if (Input.GetButtonDown (controllerName + player + "Attack3")) atkButton += "_Atk3";
		if (Input.GetButtonDown (controllerName + player + "Attack4")) atkButton += "_Atk4";
		if( atkButton != "")	Debug.Log(atkButton);
	}

	public string GetPlayerAtk()
    {
        string s = null;
        if (atkButton != "")
        {
            s = atkButton;
        }
        return s;
    }
	//移動の取得
	public Direction GetPlayerMoveDirection(FighterStateBase _stateBase)
	{
		switch (playerDirection)
		{
			case "1":
				if (_stateBase.core.Direction == PlayerDirection.Right)
				{
					return Direction.DownBack;
				}
				else
				{
					return Direction.DownFront;
				}
			case "2":
				return Direction.Down;
			case "3":
				if (_stateBase.core.Direction == PlayerDirection.Right)
				{
					return Direction.DownFront;
				}
				else
				{
					return Direction.DownBack;
				}
			case "4":
				if (_stateBase.core.Direction == PlayerDirection.Right)
				{
					return Direction.Back;
				}
				else
				{
					return Direction.Front;
				}
			case "5":
				return Direction.Neutral;
			case "6":
				if (_stateBase.core.Direction == PlayerDirection.Right)
				{
					return Direction.Front;
				}
				else
				{
					return Direction.Back;
				}
			case "7":
				if (_stateBase.core.Direction == PlayerDirection.Right)
				{
					return Direction.UpBack;
				}
				else
				{
					return Direction.UpFront;
				}
			case "8":
				return Direction.Up;
			case "9":
				if (_stateBase.core.Direction == PlayerDirection.Right)
				{
					return Direction.UpFront;
				}
				else
				{
					return Direction.UpBack;
				}
		}
		return Direction.Neutral;
	}
}