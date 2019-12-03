using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

[System.SerializableAttribute]
public class ResultAnimationList
{
	public List<AnimationClip> animeList = new List<AnimationClip>();

	public ResultAnimationList(List<AnimationClip> animationClips)
	{
		animeList = animationClips;
	}
}

[System.SerializableAttribute]
public class ResultTrackList
{
	public List<ResultTrack> trackList = new List<ResultTrack>();

	public ResultTrackList(List<ResultTrack> resultTracks)
	{
		trackList = resultTracks;
	}
}

[System.SerializableAttribute]
public class ResultTrack
{
	public AnimationClip AnimationClip;
	public CinemachineBrain CinemachineBrain;
}
