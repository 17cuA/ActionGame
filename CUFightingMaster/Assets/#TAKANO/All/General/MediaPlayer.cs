	using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class MediaPlayer : MonoBehaviour
{
	public VideoPlayer videoPlayer;
    public RawImage rawImage;

	/// <summary>
	/// 再生	
	/// </summary>
	public void PlayVideo()
	{
		Application.targetFrameRate = 60;
        videoPlayer.Play();
	}

    public bool EnabledRenderTexture()
    {
        rawImage.enabled = true;
		if (rawImage.enabled == true)
			return true;

		return false;
	}

	public void DisabledRenderTexture()
	{
        rawImage.enabled = false;
	}

	/// <summary>
	/// 再生終了時にTrue
	/// </summary>
	/// <returns></returns>
	public bool IsEndPlayVideo()
	{
        if ((ulong)videoPlayer.frame == (videoPlayer.frameCount) )
		{
            Debug.Log("b");
            return true;
		}
		return false;
	}

	/// <summary>
	/// 停止
	/// </summary>
	public void StopVideo()
	{
		videoPlayer.Stop();
	}

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
		videoPlayer.isLooping = false;
    }

    // Update is called once per frame
    void Update()
    {
		//if(Input.GetKeyDown("x"))
		//{
		//	videoPlayer.Stop();
		//}
		//if (Input.GetKeyDown("z"))
		//{
		//	videoPlayer.Play();
		//}
	}
}
