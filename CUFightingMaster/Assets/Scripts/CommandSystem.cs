using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using System;

public class CommandSystem : StateBaseScriptMonoBehaviour
{
    InputValue inputValue = new InputValue();
	string lastDir = "5";
	int gaba = 3;

	public void CommandStart()
	{
		print("CommandStart");
	}

	public void AttackStart(string str)
	{
		print(str);
	}

	/// <summary>
	/// コマンド
	/// </summary>
	/// <param name="str"></param>
	/// <returns></returns>
	public bool CheckCommand(string str)
	{
		if (str == inputValue.GetDirection()/*GetKeyboardValue.Instance.key*/)
		{
            //Debug.Log(str);
			lastDir = str;
			return true;
		}
		return false;
	}
	public bool CheckUnCommand(string str)
	{
		string dir = inputValue.GetDirection()/*GetKeyboardValue.Instance.key*/;
		if (lastDir == dir || dir == null) return false;
		else if(gaba>0)
		{
			gaba--;
			return false;
		}
		return dir != str;
	}
	public bool OutputCommand(string str)
	{
		GetComponent<PlayerMove>().SetCommand(str);
		Debug.Log(str);
		return true;
	}
	public bool CheckAttack(string str)
	{
			if (Input.GetKeyDown(str))
			{
				return true;
			}
		return false;
	}
	public void ResetGaba()
	{
		gaba = 3;
	}
}
