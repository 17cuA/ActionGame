using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class title : MonoBehaviour
{

	public VideoPlayer videoPlayer;  //アタッチした VideoPlayer をインスペクタでセットする
	public VideoPlayer videoPlayer1;  //アタッチした VideoPlayer をインスペクタでセットする

	private void Start()
	{
		
	}
	// Update is called once per frame
	void Update()
	{
		Debug.Log(videoPlayer.frame);
		Debug.Log(videoPlayer.frameCount);
		if ((ulong)videoPlayer.frame == videoPlayer.frameCount - 2)
		{ 
			//※ここに終了したときの処理など
			SceneManager.LoadScene("Battle");
		}
	}
}