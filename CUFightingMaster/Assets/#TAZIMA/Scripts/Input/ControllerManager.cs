using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerManager : MonoBehaviour
{
    /// <summary>
    /// ControllerManagerの変数
    /// </summary>
    [SerializeField] private const int playerNum = 2;               //プレイヤー数
    public GameObject[] inputManager = new GameObject[playerNum];   //InputManagerオブジェクトを管理
	private int[] controllerIndex = new int[playerNum];				//接続されているコントローラーの要素数を保存しておく

    /// <summary>
    /// 初期化処理
    /// </summary>
    void Start()
    {
		var nowControllerNames = Input.GetJoystickNames();
		//コントローラー名をセット
        for (int i = 0;i < playerNum;i++)
        {
			if (i < nowControllerNames.Length)
			{
				//コントローラーが接続されていればコントローラー名をセット、それ以外なら空
				if (nowControllerNames[i] != "")
				{
					inputManager[i].GetComponent<InputControl>().controllerName = "";
					controllerIndex[i] = i;
					continue;
				}
			}
			inputManager[i].GetComponent<InputControl>().controllerName = "";
			controllerIndex[i] = -1;
        }
    }

    void Update()
    {
        var nowControllerNames = Input.GetJoystickNames();
		var isChange = new bool[playerNum];	//変更が必要な場合trueにする
		var isSet = new bool[playerNum];			//変更した場合trueにする
        for (int i = 0;i < playerNum; i++)
        {
			isChange[i]  =false;
			isSet[i] = false;
			//変更が必要かどうか判定する
			if (controllerIndex[i] == -1)	isChange[i] = true;
			else if (inputManager[i].GetComponent<InputControl>().controllerName != nowControllerNames[controllerIndex[i]])
			{
				isChange[i] = true;
			}
			//変更が必要なら
			if (isChange[i])
			{
				for (int j = 0; j < nowControllerNames.Length; j++)
				{
					//現在のコントローラーの要素数をプレイヤー数で割った数が現在設定しているプレイヤーの要素数でなければループ
					if (j % playerNum != i) continue;
					//コントローラーが接続されていればそのコントローラーの名前に変更する
					if (nowControllerNames[j] != "")
					{
						inputManager[i].GetComponent<InputControl>().controllerName = nowControllerNames[j] + "_";
						controllerIndex[i] = j;
						isSet[i] = true;
					}
					//変更された場合このループのその後の処理を無視する
					if (isSet[i]) break;
				}
				//変更が必要なのに変更されていなかった場合コントローラー名を空にする
				if (!isSet[i])
				{
					inputManager[i].GetComponent<InputControl>().controllerName = "";
					controllerIndex[i] = -1;
				}
			}
        }
    }
}
