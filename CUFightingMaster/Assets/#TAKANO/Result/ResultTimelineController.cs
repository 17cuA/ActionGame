using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using Cinemachine;
using System.Linq;
public class ResultTimelineController : MonoBehaviour
{
	[SerializeField] ResultTimelineCreater resultTimelineCreater;

	[SerializeField] FighterCreater fighterCreater;

	[SerializeField] ResultNomalAnimationController resultNomalAnimationController;

	[SerializeField] CinemasceneBrainRefGetter[] CinemaSceneBrainRefGetters = new CinemasceneBrainRefGetter[2];

	public PlayableDirector[] playableDirector = new PlayableDirector[2];

	/// <summary>
	/// タイムラインを作成する
	/// </summary>
	public void CreateTimeline()
	{
		//二人分のタイムライン生成する
		for (int i = 0; i < 2; i++)
		{
			var obj = resultTimelineCreater.CreateTimeline(i);

			//参照するのに使う
			playableDirector[i] = obj.GetComponent<PlayableDirector>();
		}
	}

	public void RefTimeline()
	{
		for (int i = 0; i < 2; i++)
		{
			//TimelineからAnimaiton Trackを取得
			var animatonTrack = playableDirector[i].playableAsset.outputs.First(c => c.streamName == "Animation Track");
			//TimelineからCinemaScene Trackを取得
			var chinemaSceneTrack = playableDirector[i].playableAsset.outputs.First(c => c.streamName == "Cinemascene Track");

			//Animation TrackにAnimatorの参照を追加
			playableDirector[i].SetGenericBinding(animatonTrack.sourceObject, fighterCreater.GetRefAnimator(i));
			//CinemaSceneTrackにCinemaSceneBrainの参照を追加
			playableDirector[i].SetGenericBinding(chinemaSceneTrack.sourceObject, CinemaSceneBrainRefGetters[i].getRefCinemaSceneBrain());

			resultNomalAnimationController.DisabledNomalAnimation();
		}
	}
}

