using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultNomalAnimationController : MonoBehaviour
{
	[SerializeField]ResultTimelineCreater resultTimelineCreater;
	[SerializeField] FighterCreater fighterCreater;
	public NomalAnimationPlayer[] nomalAnimationPlayers = new NomalAnimationPlayer[2];
	private Animationdata[] animationdatas = new Animationdata[2];
	[SerializeField] AnimationClip loseLoopAnimaton;

	/// <summary>
	/// モデルについてるNomalAniamitonとAnimatondataを操作する、
	/// </summary>
	/// 勝った方のモデルは無効化、負けた方はループアニメするため、AnimatonDataだけ無効化
	public void DisabledNomalAnimationModels ()
	{
		for (int i = 0; i < 2; i++)
		{
			if (GameDataStrage.Instance.matchResult[i] == MatchResult.WIN)
			{
				//animationdatas[i].enabled = true;
				nomalAnimationPlayers[i].enabled = false;
				//animationdatas[i].enabled = false;
			}
			else
			{
				//animationdatas[i].enabled = false;
				nomalAnimationPlayers[i].SetIdling(loseLoopAnimaton, 0.3f);
			}
		}
	}

	private void Start()
	{
		for (int i = 0; i < 2; i++)
		{
			nomalAnimationPlayers[i] = fighterCreater.GeReftNomalAnimationPlayer(i);
			//animationdatas[i] = fighterCreater.GetRefAnimatondata(i);
			nomalAnimationPlayers[i].SetIdling(resultTimelineCreater.GetAnimationClip(i), 0.3f);
		}
	}
}
