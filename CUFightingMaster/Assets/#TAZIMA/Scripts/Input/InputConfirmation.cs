using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputConfirmation : MonoBehaviour
{
    void Update()
    {
		if (Input.anyKeyDown)
        {
			foreach (KeyCode code in Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(code))
                {
                    //処理を書く
                    Debug.Log(code.ToString());
                    break;
                }
            }
        }
    }
}
