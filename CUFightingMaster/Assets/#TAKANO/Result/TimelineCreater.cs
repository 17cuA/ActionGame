using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

//		    | 勝った(0) |    負けた(1)   | 勝ったループ(2) | 負けたループ(3) |
// clico(0) | tracks    |     tracks      |    tracks    |    tracks       |
// oba  (1) | tracks    |     tracks      |    tracks    |    tracks       |

public class ResultTimelineChanger : MonoBehaviour
{
	[SerializeField] private List<TimeLineElements> timeLineList = new List<TimeLineElements>();

	public GameObject GetTimeLineObject(int _fighterNum, int _victoryNum)
	{
		return timeLineList[_fighterNum].trackList[_victoryNum].timeLine;
	}
}