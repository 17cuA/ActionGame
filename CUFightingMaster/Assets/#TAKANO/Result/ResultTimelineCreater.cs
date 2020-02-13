using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using Cinemachine;

//		    | 勝った(0) |    負けた(1)     |
// clico(0) | tracks    |     tracks      |
// oba  (1) | tracks    |     tracks      |

public class ResultTimelineCreater : MonoBehaviour
{
	[SerializeField] private List<TimeLineElements> timeLineList = new List<TimeLineElements>();
	public int[] fighterNum = new int[2];
	public int[] victoryNum = new int[2];

	public GameObject CreateTimeline(int i)
	{
		return Instantiate(timeLineList[fighterNum[i]].trackList[victoryNum[i]].timeLine);
	}
	
	public GameObject GetTimeLineObject(int i)
	{
		return timeLineList[fighterNum[i]].trackList[fighterNum[i]].timeLine;
	}

	//public AnimationClip GetAnimationClip(int i)
	//{
	//	return timeLineList[fighterNum[i]].trackList[fighterNum[i]].loopAnimatoinClip;
	//}

	private void Awake()
	{
		//勝敗情報を取得
		for (int i = 0; i < 2; i++)
		{
			victoryNum[i] = (int)GameDataStrage.Instance.matchResult[i];
			fighterNum[i] = GameDataStrage.Instance.fighterStatuses[i].PlayerID;
		}
	}
}