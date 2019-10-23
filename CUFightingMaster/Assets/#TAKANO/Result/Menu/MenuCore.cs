using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MenuCore : MonoBehaviour
{
	public Menu_Parameter menu_Parameter = new Menu_Parameter();

	[SerializeField] Menu_Rematch menu_Rematch;
	[SerializeField] Menu_ToCharacterSelect menu_ToCharacterSelect;
	[SerializeField] Menu_ToTitle menu_ToTitle;

	public Type currentSelectMenuElement;

}
