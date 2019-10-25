using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_Rematch : Menu_Base , IMenuItem
{
	public override void Decide()
	{
		Debug.Log("再戦");
		//SceneManager.LoadScene("CharacterSelect");
	}
}
