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

	[SerializeField] CinemasceneBrainRefGetter[] CinemaSceneBrainRefGetters = new CinemasceneBrainRefGetter[2];

	public PlayableDirector[] playableDirector = new PlayableDirector[2];

	public bool isEndTimeline_1;
	public bool isEndTimeline_2;

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

	/// <summary>
	/// タイムラインの参照を取得する
	/// </summary>
	public void RefTimeline()
	{
		for (int i = 0; i < 2; i++)
		{
			//TimelineからAnimaiton Trackを取得
			var animatonTrack = playableDirector[i].playableAsset.outputs.First(c => c.streamName == "Animation Track");
			//TimelineからCinemaScene Trackを取得
			var chinemaSceneTrack = playableDirector[i].playableAsset.outputs.First(c => c.streamName == "Cinemascene Track");
			Debug.Log(animatonTrack);
			Debug.Log(chinemaSceneTrack);
			//Animation TrackにAnimatorの参照を追加
			playableDirector[i].SetGenericBinding(animatonTrack.sourceObject, fighterCreater.GetRefAnimator(i));
			//CinemaSceneTrackにCinemaSceneBrainの参照を追加
			playableDirector[i].SetGenericBinding(chinemaSceneTrack.sourceObject, CinemaSceneBrainRefGetters[i].getRefCinemaSceneBrain());
		}
	}

	/// <summary>
	/// 二つのタイムラインが停止したか
	/// </summary>
	/// <returns></returns>
	public bool isEndPlayTimelines()
	{
		if (isEndTimeline_1 && isEndTimeline_2)
			return true;
		return false;
	}

	/// <summary>
	/// タイムライン終了時にイベントを追加する
	/// </summary>
	/// <param name="_director"></param>
	public void OnPlayableDirector1Stopped(PlayableDirector _director)
	{
		isEndTimeline_1 = true;
	}
	public void OnPlayableDirector2Stopped(PlayableDirector _director)
	{
		isEndTimeline_2 = true;
	}

	public void Start()
	{
		//終了通知をタイムラインの再生終了時に追加する
		playableDirector[0].stopped += OnPlayableDirector1Stopped;
		playableDirector[1].stopped += OnPlayableDirector2Stopped;
	}
}

