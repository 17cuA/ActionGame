using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_ToTitle : Menu_Base
{
	public override void Decide()
	{
		Debug.Log("タイトル");
		//SceneManager.LoadScene("Title");
	}
}
