using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultNomalAnimationController : MonoBehaviour
{
	[SerializeField]ResultTimelineCreater resultTimelineCreater;
	[SerializeField] FighterCreater fighterCreater;
	[SerializeField] AnimationClip loseLoopAnimaton;

	private NomalAnimationPlayer[] nomalAnimationPlayers = new NomalAnimationPlayer[2];
	private Animationdata[] animationdatas = new Animationdata[2];
	
	/// <summary>
	/// モデルについてるNomalAniamitonとAnimatondataを操作する、
	/// </summary>
	/// 勝った方のモデルは無効化、負けた方はループアニメするため、AnimatonDataだけ無効化
	public void DisabledNomalAnimationModels ()
	{
		for (int playerNum = 0; playerNum < 2; playerNum++)
		{
			if (GameDataStrage.Instance.matchResult[playerNum] == MatchResult.WIN)
			{
				//animationdatas[i].enabled = true;
				nomalAnimationPlayers[playerNum].enabled = false;
				//animationdatas[i].enabled = false;
			}
			else
			{
				//animationdatas[i].enabled = false;
				nomalAnimationPlayers[playerNum].SetIdling(loseLoopAnimaton, 0.3f);
			}
		}
	}

	private void Start()
	{
		//nomalAniamtionPlayerを取得取得
		for (int playerNum = 0; playerNum < 2; playerNum++)
		{
			nomalAnimationPlayers[playerNum] = fighterCreater.GeReftNomalAnimationPlayer(playerNum);
		}
	}
}
