﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class ResultTrackChanger : MonoBehaviour
{
	//		    | 勝った(0) |    負けた(1)   | 勝ったループ(2) | 負けたループ(3) |
	// glico(0) | tracks    |     tracks      |    tracks    |    tracks       |
	// oba  (1) | tracks    |     tracks      |    tracks    |    tracks       |

	[SerializeField] private List<ResultTrackList> resultTrackList = new List<ResultTrackList>();

	/// <summary>
	/// リザルト用アニメーションをゲット
	/// </summary>
	/// <param name="_fighterNun">PlayerID</param>
	/// <param name="_victoryNum">1=win,2=lose</param>
	/// <returns></returns>
	public AnimationClip GetTrack(int _fighterNun, int _victoryNum)
	{
		return resultTrackList[_fighterNun].trackList[_victoryNum].AnimationClip;
	}

	/// <summary>
	/// リザルト用カメラをゲット
	/// </summary>
	/// <param name="_fighterNun"></param>
	/// <param name="_victoryNum"></param>
	/// <returns></returns>
	public CinemachineBrain GetCinemachineBrain(int _fighterNun, int _victoryNum)
	{
		return resultTrackList[_fighterNun].trackList[_victoryNum].CinemachineBrain;
	}
}
