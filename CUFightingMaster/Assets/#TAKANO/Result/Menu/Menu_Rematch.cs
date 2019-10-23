using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_Rematch : Menu_Base
{
	public override void CreateInstance(Menu_Base menu_Base)
	{
		base.CreateInstance(menu_Base);
	}

	public override void Decide()
	{
		SceneManager.LoadScene("CharacterSelect");
	}

}
