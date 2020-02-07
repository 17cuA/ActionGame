using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemasceneBrainRefGetter: MonoBehaviour
{
	[SerializeField] Camera camera;

	public CinemachineBrain getRefCinemaSceneBrain()
	{
		return camera.GetComponent<CinemachineBrain>();
	}
}
