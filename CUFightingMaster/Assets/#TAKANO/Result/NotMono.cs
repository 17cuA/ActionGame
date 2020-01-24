using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[System.SerializableAttribute]
public class TimeLineElements
{
	public List<TimeLineElement> trackList = new List<TimeLineElement>();
}

[System.SerializableAttribute]
public class TimeLineElement
{
	public GameObject timeLine;
	public AnimationClip loopAnimatoinClip;
}