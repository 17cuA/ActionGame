using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

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
	public string chinemaTrackName;
}
