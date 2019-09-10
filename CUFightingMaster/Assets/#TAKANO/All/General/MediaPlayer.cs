using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class MediaPlayer : MonoBehaviour
{
	public VideoPlayer videoPlayer;

	private Action callBack_EndPlay;

	/// <summary>
	/// 再生
	/// </summary>
	public void PlayVideo()
	{
        if(callBack_EndPlay != null)
        {
            //ループ終わりの時に実行するコールバックを設定
            videoPlayer.loopPointReached += EndPlayVideo;
            //ビデオを再生
            videoPlayer.Play();
        }
	}

	/// <summary>
	/// 再生終了時に呼ぶ関数
	/// </summary>
	/// <returns></returns>
	public void EndPlayVideo(VideoPlayer videoPlayer)
	{
		videoPlayer.isLooping = false;
        if( callBack_EndPlay != null )
        {
            callBack_EndPlay();
        }
	}
	
	/// <summary>
	/// 再生終了時にコールバックする関数の登録
	/// </summary>
	/// <param name="action"></param>
	public void SetEndPlayCallBack(Action action )
	{
		callBack_EndPlay = action;
	}

    // Start is called before the first frame update
    void Start()
    {
		videoPlayer.isLooping = true;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
