using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultNomalAnimationController : MonoBehaviour
{
	[SerializeField]ResultTimelineCreater resultTimelineCreater;
	[SerializeField] FighterCreater fighterCreater;
	public NomalAnimationPlayer[] nomalAnimationPlayers = new NomalAnimationPlayer[2];

	public void DisabledNomalAnimation()
	{
		for (int i = 0; i < 2; i++)
		{
			nomalAnimationPlayers[i] = fighterCreater.GeReftNomalAnimationPlayer(i);
			nomalAnimationPlayers[i].enabled = false;
			
			//nomalAnimationPlayers[i].SetIdling(resultTimelineCreater.GetAnimationClip(i) , 1.0f);
		}
	}
}
