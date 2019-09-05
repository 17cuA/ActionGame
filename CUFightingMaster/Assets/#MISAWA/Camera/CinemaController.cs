using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
//using Cinemachine;

public class CinemaController : MonoBehaviour, ITimeControl
{
	//再生中のフラグ
	public bool isPlay = true;
	public void OnControlTimeStop()
	{
		isPlay = false;
        //0715 by takano コメントアウト
		//Manager_Game.instance.EndAppearance();
		//UIManager_Game.instance.call_Once = true;
	}
	public void OnControlTimeStart()
	{
		isPlay = true;	
	}
	public void SetTime(double time)
	{
	}
}
