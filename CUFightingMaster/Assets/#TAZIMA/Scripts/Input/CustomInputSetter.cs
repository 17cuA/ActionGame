using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomInputSetter : MonoBehaviour
{

    private const int playerNum = 2;
    public GameObject[] obj = new GameObject[playerNum];
	private string[] controllerNames = new string[playerNum];
	private int[] setControllerNum = new int[playerNum];

	void Awake()
	{
		var nowControllerNames = Input.GetJoystickNames();
		for (int i = 0; i < playerNum; i++)
		{
			if (i < nowControllerNames.Length) controllerNames[i] = nowControllerNames[i];
			else controllerNames[i] = "";
			
			if (controllerNames[i] == "")
			{
				obj[i].GetComponent<TestInput>().controllerName = controllerNames[i];
				setControllerNum[i] = -1 * i;
				obj[i].GetComponent<TestInput>().controllerIndex = setControllerNum[i];
			}
			else
			{
				obj[i].GetComponent<TestInput>().controllerName = controllerNames[i] + "_";
				setControllerNum[i] = i;
				obj[i].GetComponent<TestInput>().controllerIndex = setControllerNum[i];
			}
		}
	}

    void Update()
    {
		//コントローラーの接続が変更されているかどうか確認する
        var nowControllerNames = Input.GetJoystickNames();
        for (int i = 0; i < playerNum; i++)
        {
			//コントローラーが接続されていない場合変更処理をさせる
			if (setControllerNum[i] > 0)
			{
				//設定されているコントローラーが現在も接続されている場合処理を行わない
				if (controllerNames[i] == nowControllerNames[setControllerNum[i]]) continue;
			}

			//変更処理
			for (int j = 0; j < nowControllerNames.Length; j++)
			{
				if (nowControllerNames[j] != "" && SearchUsedController(nowControllerNames.Length,j))
				{
					obj[i].GetComponent<TestInput>().controllerName = nowControllerNames[j] + "_";
					controllerNames[i] = nowControllerNames[j];
					setControllerNum[i] = j;
					obj[i].GetComponent<TestInput>().controllerIndex = setControllerNum[i];
					continue;
				}
			}
			//コントローラーがなかった場合キーボード操作
			obj[i].GetComponent<TestInput>().controllerName = "";
			controllerNames[i] = "";
			setControllerNum[i] = -1 * i;
			obj[i].GetComponent<TestInput>().controllerIndex = setControllerNum[i];
		}
    }
	//コントローラーが他のInputで使われているかどうか判定しbool値を返す
	private bool SearchUsedController(int _index, int _num)
	{
		for (int i = 0; i < _index; i++)
		{
			if (setControllerNum[i] == _num) return true;
		}
		return false;
	}
}
