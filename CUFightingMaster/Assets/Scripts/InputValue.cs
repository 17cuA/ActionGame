using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class InputValue
{
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
	//変数
	public Vector2 inputDirection;
	public string direction;    //現在のジョイスティックの方向
	public int lastDir = 5;
	public void SetDirection()
    {
        inputDirection.x = Input.GetAxisRaw("Horizontal");
        inputDirection.y = Input.GetAxisRaw("Vertical");
    }

    public string GetDirection()
    {
        SetDirection();
        float nowDir = 5 + inputDirection.x + (inputDirection.y * 3);
		//方向を調べる
		switch (nowDir)
		{
			case ((int)DirJS.d1):
				lastDir = (int)DirJS.d1;
				direction = "1";
				break;
			case (int)DirJS.d2:
				lastDir = (int)DirJS.d2;
				direction = "2";
				break;
			case (int)DirJS.d3:
				lastDir = (int)DirJS.d3;
				direction = "3";
				break;
			case (int)DirJS.d4:
				lastDir = (int)DirJS.d4;
				direction = "4";
				break;
			case (int)DirJS.d5:
				lastDir = (int)DirJS.d5;
				direction = "5";
				break;
			case (int)DirJS.d6:
				lastDir = (int)DirJS.d6;
				direction = "6";
				break;
			case (int)DirJS.d7:
				lastDir = (int)DirJS.d7;
				direction = "7";
				break;
			case (int)DirJS.d8:
				lastDir = (int)DirJS.d8;
				direction = "8";
				break;
			case (int)DirJS.d9:
				lastDir = (int)DirJS.d9;
				direction = "9";
				break;
			default:
				lastDir = 0;
				direction = null;
				break;
		}
		return direction;
	}
}
