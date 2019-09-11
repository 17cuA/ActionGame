﻿	using System;
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
        videoPlayer.Play();
	}

    public bool EnabledRenderTexture()
    {
        rawImage.enabled = true;
		if (rawImage.enabled == true)
			return true;

		return false;
	}

	public bool DisabledRenderTexture()
	{
		rawImage.enabled =false;
		if (rawImage.enabled == false)
			return true;

		return false;
	}

	/// <summary>
	/// 再生終了時にTrue
	/// </summary>
	/// <returns></returns>
	public bool IsEndPlayVideo()
	{
		if ((ulong)videoPlayer.frame == (videoPlayer.frameCount - 1))
		{

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
