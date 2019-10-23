using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_ToTitle : Menu_Base
{
	public override void Decide()
	{
		SceneManager.LoadScene("Title");
	}
}
