using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using Cinemachine;
using System.Linq;
public class ResultTimelineController : MonoBehaviour
{
	[SerializeField] ResultTrackChanger resultTrackChanger;
	[SerializeField] TrackUnmute[] trackUnmutes = new TrackUnmute[2];
	[SerializeField] AnimaitonBindController[] animaitonBindControllers = new AnimaitonBindController[2];

	/// <summary>
	/// タイムラインのトラックを更新する
	/// </summary>
	public void TrackSet()
	{
		//二人分のタイムラインを更新する
		for (int i = 0; i < 2; i++)
		{
			//選択されていたファイターの種類から、バインドするAnimationClipを取得する
			animaitonBindControllers[i].AnimationClip = resultTrackChanger.GetTrack((int)GameDataStrage.Instance.matchResult[i],
				GameDataStrage.Instance.fighterStatuses[i].PlayerID);

			animaitonBindControllers[i].BindAnimation();

			//このfighterの勝敗の結果から、UnMuteするChinemaChineTrackを取得する
		trackUnmutes[i].UnMuteTrack(resultTrackChanger.GetCameraTrackName((int)GameDataStrage.Instance.matchResult[i],
				GameDataStrage.Instance.fighterStatuses[i].PlayerID));
		}
	}
	
	public void SetAnimator(Animator _animator_1, Animator _animator_2)
	{
		animaitonBindControllers[0].FigterAnimator = _animator_1;
		animaitonBindControllers[1].FigterAnimator = _animator_2;
	}

	public void MuteTrack()
	{
		for (int i = 0; i < 2; i++)
		{
			//このfighterの勝敗の結果から、MuteするChinemaChineTrackを取得する
			trackUnmutes[i].MuteTrack(resultTrackChanger.GetCameraTrackName((int)GameDataStrage.Instance.matchResult[i],
				GameDataStrage.Instance.fighterStatuses[i].PlayerID));
		}
	}
}
