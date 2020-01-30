using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultNomalAnimationController : MonoBehaviour
{
	[SerializeField]ResultTimelineCreater resultTimelineCreater;
	[SerializeField] FighterCreater fighterCreater;
	public NomalAnimationPlayer[] nomalAnimationPlayers = new NomalAnimationPlayer[2];
	private Animationdata[] animationdatas = new Animationdata[2];

	public void Disabled()
	{
		for (int i = 0; i < 2; i++)
		{
			animationdatas[i].enabled = false;
		}
	}

	public void EnabledLoser()
	{
		for (int i = 0; i < 2; i++)
		{
			if(GameDataStrage.Instance.matchResult[i] == MatchResult.WIN)
			{
				nomalAnimationPlayers[i].enabled = false;
			}
		}
	}

	private void Start()
	{
		for (int i = 0; i < 2; i++)
		{
			nomalAnimationPlayers[i] = fighterCreater.GeReftNomalAnimationPlayer(i);
			animationdatas[i] = fighterCreater.GetRefAnimatondata(i);
			nomalAnimationPlayers[i].SetIdling(resultTimelineCreater.GetAnimationClip(i), 0.5f);
		}
	}
}
