/*	Manager_DemoMovie.cs
 *	DemoMovieシーンの中央管理スクリプト
 *	製作者：宮島幸大
 *	制作日：2019/06/07
 *	----------更新----------
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class Manager_DemoMovie : MonoBehaviour
{
	public VideoPlayer videoPlayer;     //アタッチした VideoPlayer をインスペクタでセットする

	//	画面のマスク関係
	public GameObject maskOb;		//	マスク用のイメージが入ってるオブジェクト
	MovingMaskManager mMM;			//	マスク用スクリプトをロードするため

	//	--------------------
	//	スタート
	//	--------------------
	void Start()
    {
		mMM = maskOb.GetComponent<MovingMaskManager>();
	}

	//	--------------------
	//	アップデート
	//	--------------------
	void Update()
    {
		if ((ulong)videoPlayer.frame == videoPlayer.frameCount)
		{
			MoveScene();
			return;
		}
	}

	//	--------------------
	//	シーン変更
	//	作成者：宮島 幸大
	//	--------------------
	public void MoveScene()
	{
		mMM.FadeOut(0);
	}
}
//write by Miyajima Kodai