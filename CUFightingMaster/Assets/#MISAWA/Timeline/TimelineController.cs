using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineController : CameraBase
{
	public PlayableDirector Admissiondirector;
	public PlayableDirector ClicoSpdirector;

	public override void PlayCamera()
	{
		Admissiondirector.Play();
	}

	public override void ClicoSp()
	{
		ClicoSpdirector.Play();
	}
}
