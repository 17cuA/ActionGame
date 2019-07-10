using UnityEngine;

public class PlayerInputManager
{
	//キャラクターの情報
	public int playerIndex;             //プレイヤー番号
	public string player;					//Inputでプレイヤー毎の入力を識別するための文字列

	//ジョイスティックの入力に使用する変数
	public Vector2 inputDirection;      //ジョイスティックの入力方向
	public string direction;            //現在のジョイスティックの方向
	public int lastDir = 5;             //前回のジョイスティックの方向

	//プレイヤーが入力した情報を格納する変数
	public int playerDirection;      //プレイヤーの入力方向
	public string atkBotton;        //攻撃ボタンの名前を格納

	//ジョイスティックの入力方向（方向はNumパッドに依存）

	enum DirJS
	{
		NONE,
		d1 = 1, //RIGHT_DOWN
		d2 = 2, //DOWN
		d3 = 3, //LEFT_DOWN
		d4 = 4, //RIGHT
		d5 = 5, //CENTER
		d6 = 6, //LEFT
		d7 = 7, //RIGHT_UP
		d8 = 8, //UP
		d9 = 9  //LEFT_UP
	}
	public void DownKeyCheck()
	{
		//ジョイスティックまたはキーボードでの方向
		SetDirection();
		//攻撃ボタン
		SetAtkBotton();
	}

	public void SetAxis()
	{
		//X,Yそれぞれの入力を保存
		inputDirection.x = Input.GetAxisRaw(player + "_Horizontal");
		inputDirection.y = Input.GetAxisRaw(player + "_Vertical");
	}
	public void SetDirection()
	{
		SetAxis();
		float nowDir = 5 + inputDirection.x + (inputDirection.y * 3);
		//方向を調べる(Numキーパッド基準)
		switch (nowDir)
		{
			case ((int)DirJS.d1):
				lastDir = (int)DirJS.d1;
				playerDirection = 1;
				break;
			case (int)DirJS.d2:
				lastDir = (int)DirJS.d2;
				playerDirection = 2;
				break;
			case (int)DirJS.d3:
				lastDir = (int)DirJS.d3;
				playerDirection = 3;
				break;
			case (int)DirJS.d4:
				lastDir = (int)DirJS.d4;
				playerDirection = 4;
				break;
			case (int)DirJS.d5:
				lastDir = (int)DirJS.d5;
				playerDirection = 5;
				break;
			case (int)DirJS.d6:
				lastDir = (int)DirJS.d6;
				playerDirection = 6;
				break;
			case (int)DirJS.d7:
				lastDir = (int)DirJS.d7;
				playerDirection = 7;
				break;
			case (int)DirJS.d8:
				lastDir = (int)DirJS.d8;
				playerDirection = 8;
				break;
			case (int)DirJS.d9:
				lastDir = (int)DirJS.d9;
				playerDirection = 9;
				break;
			default:
				lastDir = 0;
				direction = null;
				break;
		}
	}

	public void SetAtkBotton()
	{
		atkBotton = "";
		if (Input.GetButtonDown(player + "_Attack1")) atkBotton += "_Atk1";
		if (Input.GetButtonDown(player + "_Attack2")) atkBotton += "_Atk2";
		if (Input.GetButtonDown(player + "_Attack3")) atkBotton += "_Atk3";
	}
}
