using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineController : CameraBase
{
	public PlayableDirector director;

	public override void PlayCamera()
	{ 
		director.Play();
    }
}
