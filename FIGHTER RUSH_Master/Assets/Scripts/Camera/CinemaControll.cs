using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using Cinemachine;

public class CinemaControll : MonoBehaviour, ITimeControl
{
	public void OnControlTimeStop()
	{
		Manager_Game.instance.EndAppearance();
		UIManager_Game.instance.call_Once = true;
	}
	public void OnControlTimeStart()
	{
	}
	public void SetTime(double time)
	{
	}
}
