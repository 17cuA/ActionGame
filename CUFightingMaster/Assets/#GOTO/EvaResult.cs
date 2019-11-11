using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class EvaResult : MonoBehaviour
{
	public VideoPlayer videoPlayer;  //アタッチした VideoPlayer をインスペクタでセットする
	public VideoPlayer videoPlayer1;  //アタッチした VideoPlayer をインスペクタでセットする

	private void Update()
	{
		Debug.Log(videoPlayer.frame);
		Debug.Log(videoPlayer.frameCount);
		if ((ulong)videoPlayer.frame == videoPlayer.frameCount - 2)
		{
			//※ここに終了したときの処理など
			SceneManager.LoadScene("Title");
		}
	}
}
