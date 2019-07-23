using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using Cinemachine;

public class CinemaController : MonoBehaviour, ITimeControl
{
	public void OnControlTimeStop()
	{
		Manager_Game.instance.EndAppearance();
		UIManager_Game.instance.call_Once = true;
	}
	public void OnControlTimeStart()
	{
		Debug.Log("きどう");
	}
	public void SetTime(double time)
	{
	}
}
