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
			var names = Input.GetJoystickNames();
			Debug.Log(names.Length);
			var sampleNames = new string[names.Length];
			for (int i = 0;i < names.Length;i++)
			{
				Debug.Log(names[i]);
			}
			foreach (KeyCode code in Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(code))
                {
                    //処理を書く
                    Debug.Log(code);
                    break;
                }
            }
        }
    }
}
