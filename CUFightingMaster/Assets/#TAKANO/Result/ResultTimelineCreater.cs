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

	[SerializeField]private int[] fighterNum = new int[2];
	[SerializeField]private int[] victoryNum = new int[2];

	/// <summary>
	/// タイムラインを生成する
	/// </summary>
	/// <param name="_playerNum">PlayerNum</param>
	/// <returns></returns>
	public GameObject CreateTimeline(int _playerNum)
	{
		return Instantiate(timeLineList[fighterNum[_playerNum]].trackList[victoryNum[_playerNum]].timeLine);
	}
	public GameObject GetTimeLineObject(int _playerNum)
	{
		return timeLineList[fighterNum[_playerNum]].trackList[fighterNum[_playerNum]].timeLine;
	}

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