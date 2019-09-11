//デモムービーするときの音量操作

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoMovie_Sound : MonoBehaviour
{
	float vol = 0;
	public AudioSource audioSource;
	
	public void Volume_Up()
	{
		if(vol < 1)
		{
			vol += 0.01f;
			audioSource.volume = vol;
		}
	}

	public void Volume_Down()
	{
		if(vol > 0)
		{
			vol -= 0.05f;
			audioSource.volume = vol;
		}
	}
}
