using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSceneManager : MonoBehaviour
{
	public GameObject obj;

	void Update()
    {
        if (Input.anyKeyDown)
		{
			obj.GetComponent<AnimationUIManager>().isStart = true;
		}
    }
}
