using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerManager : MonoBehaviour
{
    private const int playerNum = 2;
    public GameObject[] inputManager = new GameObject[playerNum];   //InputManagerオブジェクトを管理
    public string[] controllerNames = new string[playerNum];        //コントローラーの名前を管理
    void Start()
    {
        for (int i = 0;i < playerNum;i++)
        {
            controllerNames[i] = inputManager[i].GetComponent<TestInput>().controllerName;
            //Debug.Log(controllerNames[i]);
        }
    }

    void Update()
    {
        var nowControllerNames = Input.GetJoystickNames();
        for (int i = 0;i < playerNum; i++)
        {
            if ( i < nowControllerNames.Length)
            {
                //変更が必要かどうか
				if (inputManager[i].GetComponent<TestInput>().controllerName != nowControllerNames[i] + "_")
				{
					if (nowControllerNames[i] != "")
						inputManager[i].GetComponent<TestInput>().controllerName = nowControllerNames[i] + "_";
					else
						inputManager[i].GetComponent<TestInput>().controllerName = nowControllerNames[i];
				}
            }
        }
    }
}
