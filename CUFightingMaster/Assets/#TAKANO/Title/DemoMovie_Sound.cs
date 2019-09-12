//デモムービーするときの音量操作

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoMovie_Sound : MonoBehaviour
{
	float vol = 0;
	public AudioSource audioSource;
	
    /// <summary>
    /// ボリュームを徐々に上げる(0.0 ~ 1.0)
    /// </summary>
    /// <param name="max"></param>
	public void Volume_Up( float max)
	{
		if(vol < max)
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
