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
    public string[] controllerNames = new string[playerNum];        //コントローラーの名前を管理
	private int[] controllerIndex = new int[playerNum];				//接続されているコントローラーの要素数を保存しておく

    /// <summary>
    /// 初期化処理
    /// </summary>
    void Start()
    {
        for (int i = 0;i < playerNum;i++)
        {
            controllerNames[i] = inputManager[i].GetComponent<TestInput>().controllerName;
        }
    }

    void Update()
    {
        var nowControllerNames = Input.GetJoystickNames();
		var isSet = new bool[playerNum];//変更した場合trueにする
        for (int i = 0;i < playerNum; i++)
        {
			isSet[i] = false;
			if (inputManager[i].GetComponent<TestInput>().controllerName != nowControllerNames[controllerIndex[i]])
			{
				for (int j = 0; j < nowControllerNames.Length; j++)
				{
					//現在のコントローラーの要素数をプレイヤー数で割った数が現在設定しているプレイヤーの要素数でなければループ
					if (j % playerNum != i) continue;
					//コントローラーが接続されていればそのコントローラーの名前に変更する
					if (nowControllerNames[j] != "")
					{
						inputManager[i].GetComponent<TestInput>().controllerName = nowControllerNames[j] + "_";
						isSet[i] = true;
					}
					//変更された場合このループのその後の処理を無視する
					if (isSet[i]) break;
				}
				//変更が必要なのに変更されていなかった場合コントローラー名を空にする
				if (!isSet[i])	inputManager[i].GetComponent<TestInput>().controllerName = "";
			}
        }
    }
}
