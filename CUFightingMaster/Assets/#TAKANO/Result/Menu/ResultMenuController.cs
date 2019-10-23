using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ResultMenuController : MonoBehaviour
{
	[SerializeField]MenuCore menuCore;

	public void SetCurrentSelectMenuElement( Type type)
	{
		menuCore.currentSelectMenuElement = type;
	}
	
	private void Start()
	{
		menuCore.menu_Parameter.menuMaxNumber = 3;
		menuCore.menu_Parameter.currentMenuNumber = 1;
	}

	
}
