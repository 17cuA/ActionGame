using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineController : MonoBehaviour /*: CameraBase*/
{
	public PlayableDirector director;

	//public override void PlayCamera()
	//{ 
	//	director.Play();
	//   }
	void Start()
	{
		Debug.Log("Oキーでタイムラインスタート");
	}
	void Update()
	{
		if(Input.GetKeyDown(KeyCode.O))
		{
			director.Play();
		}
	}
}
