using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ClicoCam : MonoBehaviour
{
	public CinemachineVirtualCamera Vcam0;
	public CinemachineVirtualCamera Vcam1;
	public CinemachineVirtualCamera Vcam2;

	public int n = 50;
	public int d = 10;

    // Update is called once per frame
    void Update()
    {
		if (Input.GetKeyDown(KeyCode.J))
		{
			Vcam0.Priority = n;
			Vcam1.Priority = d;
			Vcam2.Priority = d;
		}
        if (Input.GetKeyDown(KeyCode.K))
		{
			Vcam0.Priority = d;
			Vcam1.Priority = n;
			Vcam2.Priority = d;
		}
		if (Input.GetKeyDown(KeyCode.L))
		{
			Vcam0.Priority = d;
			Vcam1.Priority = d;
			Vcam2.Priority = n;
		}
    }
}
