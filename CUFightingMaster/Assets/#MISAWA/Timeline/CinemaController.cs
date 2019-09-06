using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
//using Cinemachine;

public class CinemaController : MonoBehaviour, ITimeControl
{
	//再生中のフラグ
	public bool isPlay;
	//public GameObject P1;
	//public GameObject P2;

	void Start()
	{
		isPlay = true;
	}

	public void OnControlTimeStop()
	{
		isPlay = false;
	}
	public void OnControlTimeStart()
	{
		isPlay = true;	
	}
	public void SetTime(double time)
	{
	}
}
