using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//enumを使うために必要
using System;

public class TestInput : MonoBehaviour {
	  //正規表現でコマンドを判別するスクリプト
	public CommandManager groundMoveCommand; //地上
    public CommandManager airMoveCommand;	//空中

    public int playerIndex; //プレイヤー番号
	public string player;   //Inputでプレイヤー毎の入力を識別するための文字列
    public string controllerName = ""; //使用するコントローラーの名前
	public Vector2 inputDirection; //ジョイスティックの入力方向
	public string direction; //現在のジョイスティックの方向
	public int lastDir = 5; //前回のジョイスティックの方向
	public string playerDirection; //プレイヤーの入力方向
	public string atkButton; //攻撃ボタンの名前を格納
    public string commandName;  //発動するコマンドの名前

    private const int validInputFrame = 60;
    private const int validShotFrame = 15;

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

    private void Awake()
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
    }



    //追加、初期化処理
    public void InitCommandManagers(FighterCore _core)
    {
        //正規表現でコマンドを判別するスクリプトの変数初期化
        groundMoveCommand = gameObject.AddComponent<CommandManager>();
        groundMoveCommand.attackParameters = new List<AttackParameter>();
        airMoveCommand = gameObject.AddComponent<CommandManager>();
        airMoveCommand.attackParameters = new List<AttackParameter>();

        //地上移動
        foreach (var co in _core.Status.groundMoveSkills)
        {
            var param = new AttackParameter();
            param.commandName = co.name;
            param.command = co.command;
            param.shotTrigger = "";
            param.validInputFrame = validInputFrame;
            param.validShotFrame = validShotFrame;
            param.intervalFrame = 0;
            groundMoveCommand.attackParameters.Add(param);
        }
        //地上コマンド攻撃
        foreach(var co in _core.Status.groundAttackSkills)
        {
            var param = new AttackParameter();
            param.commandName = co.name;
            param.command = co.command;
            param.shotTrigger = co.trigger;
            param.validInputFrame = validInputFrame;
            param.validShotFrame = co.validShotFrame;
            param.intervalFrame = 0;
            groundMoveCommand.attackParameters.Add(param);

        }
        groundMoveCommand.Init();

        //空中の初期化
        foreach (var co in _core.Status.airMoveSkills)
        {
            var param = new AttackParameter();
            param.commandName = co.name;
            param.command = co.command;
            param.shotTrigger = "";
            param.validInputFrame = validInputFrame;
            param.validShotFrame = validShotFrame;
            param.intervalFrame = 0;
            airMoveCommand.attackParameters.Add(param);
        }
        //空中コマンド攻撃
        foreach (var co in _core.Status.airAttackSkills)
        {
            var param = new AttackParameter();
            param.commandName = co.name;
            param.command = co.command;
            param.shotTrigger = co.trigger;
            param.validInputFrame = validInputFrame;
            param.validShotFrame = co.validShotFrame;
            param.intervalFrame = 0;
            airMoveCommand.attackParameters.Add(param);
        }

        airMoveCommand.Init();
    }
	//コマンド消去
	public void DeleteCommand()
	{
        groundMoveCommand.inputCommandName = "";
        airMoveCommand.inputCommandName = "";
    }

    public void UpdateGame (FighterCore _core) {
        //入力管理
		DownKeyCheck (_core);
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
				lastDir = 5;
                playerDirection = "5";
				break;
		}
	}

    //プレイヤーの入力をまとめている関数
	public void DownKeyCheck (FighterCore _dir) {
		//ジョイスティックまたはキーボードでの方向入力
		SetDirection ();
        //攻撃ボタン入力
        SetAtkBotton();
        //コマンドの判別

        groundMoveCommand.GetCommandData(((int)GetPlayerMoveDirection(_dir)).ToString());
        airMoveCommand.GetCommandData(((int)GetPlayerMoveDirection(_dir)).ToString());
    }

    //攻撃ボタンの入力を管理
    public void SetAtkBotton()
    {
        atkButton = "";
        //つかみ
        if (Input.GetButtonDown(controllerName + player + "Attack4"))
        {
            atkButton = "D";
        }
        //攻撃(強中弱)
        else
        {
            if (Input.GetButtonDown(controllerName + player + "Attack3"))
            {
                atkButton = "C";
            }
            else if (Input.GetButtonDown(controllerName + player + "Attack2"))
            {
                atkButton = "B";
            }
            else if (Input.GetButtonDown(controllerName + player + "Attack1"))
            {
                atkButton = "A";
            }
        }
        //何かしら入力されたとき(デバッグ用)
        if (atkButton != "")
        {
            Debug.Log(atkButton);
        }
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
    //移動の取得
    public Direction GetPlayerMoveDirection(FighterCore _core)
    {
        switch (playerDirection)
        {
            case "1":
                if (_core.Direction == PlayerDirection.Right)
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
                if (_core.Direction == PlayerDirection.Right)
                {
                    return Direction.DownFront;
                }
                else
                {
                    return Direction.DownBack;
                }
            case "4":
                if (_core.Direction == PlayerDirection.Right)
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
                if (_core.Direction == PlayerDirection.Right)
                {
                    return Direction.Front;
                }
                else
                {
                    return Direction.Back;
                }
            case "7":
                if (_core.Direction == PlayerDirection.Right)
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
                if (_core.Direction == PlayerDirection.Right)
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