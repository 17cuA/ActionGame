using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ResultMenuController : MonoBehaviour
{
	[SerializeField]MenuCore menuCore;

	[SerializeField] Menu_Rematch menu_Rematch;
	[SerializeField] Menu_ToCharacterSelect menu_ToCharacterSelect;
	[SerializeField] Menu_ToTitle menu_ToTitle;

	public void Call_PressedUpperButton()
	{
		menuCore.currentSelectMenuElement.
	}

	public void Call_PressedDownButton()
	{

	}

	private void SetCurrentSelectMenuElement( Type type)
	{
		menuCore.currentSelectMenuElement = type;
	}
	
	private void Start()
	{
		//初期化
		SetCurrentSelectMenuElement(menu_Rematch.GetType());

		menuCore.menu_Parameter.menuMaxNumber = 3;
		menuCore.menu_Parameter.currentMenuNumber = 1;
	}

	private void Update()
	{
	}
}
