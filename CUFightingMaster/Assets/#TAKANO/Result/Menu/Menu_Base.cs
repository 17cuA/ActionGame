using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Menu_Base : MonoBehaviour
{
	[SerializeField] ResultMenuController resultMenuController;
	[SerializeField] private Image image;

	[SerializeField] public Type beforeMenu;
	[SerializeField] public Type afterMenu;

	public virtual void Decide() { }
	public virtual Type CreateInstance(Menu_Base menu_Base) { return menu_Base.GetType(); }
}
