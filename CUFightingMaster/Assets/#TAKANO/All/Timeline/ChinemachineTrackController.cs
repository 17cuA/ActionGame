using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using Cinemachine;
using System.Linq;

public class ChinemachineTrackController : MonoBehaviour
{
	[SerializeField] PlayableDirector playableDirector;
	private string trackName;

	[SerializeField] public CinemachineBrain cinemachineBrain { set; private get; }

	private IEnumerable<TrackAsset> trackAssets;

	public void SetTrackName(string _trackName)
	{
		trackName = _trackName;
	}

	/// <summary>
	/// tracnNameからミュートを解除する
	/// </summary>
	public void UnMuteChinemaSceneTrack()
	{
		TrackAsset trackAsset = trackAssets.FirstOrDefault(x => x.name == trackName);
		trackAsset.muted = false;
	}
	 
	private void Start()
	{
		trackAssets = (playableDirector.playableAsset as TimelineAsset).GetOutputTracks();
	}
}
