using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CustomInputClass;

public class InputConfirmation : MonoBehaviour
{
    CustomInput customInput = new CustomInput();
    void Start()
    {
        customInput.SetConfig(0,1);
    }
    void Update()
    {
		if (Input.anyKeyDown)
        {
            //if (customInput.GetButtonDown("Player0_Attack1")/*Input.GetKeyDown(code)*/)
            //{
            //    //処理を書く
            //    Debug.Log(true);
            //}
            foreach (KeyCode code in Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(code))
                {
                    //処理を書く
                    Debug.Log(code);
                }
            }
        }
    }
}
