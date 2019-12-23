using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultTimelineController : MonoBehaviour
{
	[SerializeField] ResultTrackChanger resultTrackChanger;
	[SerializeField] TrackUnmute[] trackUnmutes = new TrackUnmute[2];
	[SerializeField] AnimaitonBindController[] animaitonBindControllers = new AnimaitonBindController[2];



	/// <summary>
	/// タイムラインのトラックを更新
	/// </summary>
	public void TrackSet()
	{
		//二人分のタイムラインを更新する
		for (int i = 0; i < 2; i++)
		{
			//アニメーションのバインド
			animaitonBindControllers[i].AnimationClip = resultTrackChanger.GetTrack((int)GameDataStrage.Instance.matchResult[i],
				GameDataStrage.Instance.fighterStatuses[i].PlayerID);
			//chinemaSceneのUnMute
			trackUnmutes[i].UnMuteTrack(resultTrackChanger.GetCameraTrackName((int)GameDataStrage.Instance.matchResult[i],
				GameDataStrage.Instance.fighterStatuses[i].PlayerID));
		}
	}

	public void SetAnimator(Animator _animator_1, Animator _animator_2)
	{
		animaitonBindControllers[0].FigterAnimator = _animator_1;
		animaitonBindControllers[1].FigterAnimator = _animator_2;
	}
}
