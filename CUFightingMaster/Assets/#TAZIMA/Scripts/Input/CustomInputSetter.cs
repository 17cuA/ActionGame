using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomInputSetter : MonoBehaviour
{

    private const int playerNum = 2;
    public GameObject[] obj = new GameObject[playerNum];
    public string[] controllerNames = new string[playerNum];
    public bool[] isSet = new bool[playerNum];

    void Start()
    {
        for (int i = 0; i < playerNum; i++)
        {
            controllerNames[i] = obj[i].GetComponent<TestInput>().controllerName;
            isSet[i] = false;
        }
    }

    void Update()
    {
        var nowControllerNames = Input.GetJoystickNames();
        for (int i = 0; i < playerNum; i++)
        {
            if (i < nowControllerNames.Length)
            {
                var controllerIndex = (i + 1) % 2;
                //変更が必要かどうか
                if (obj[controllerIndex].GetComponent<TestInput>().controllerName != nowControllerNames[i] + "_")
                {
                    if (nowControllerNames[i] != "" && isSet[controllerIndex] == false)
                        obj[controllerIndex].GetComponent<TestInput>().controllerName = nowControllerNames[i] + "_";
                    else
                        obj[controllerIndex].GetComponent<TestInput>().controllerName = nowControllerNames[i];
                }
            }
        }
    }
}
