/*	MovingMaskManager.cs
 *	シーン変更時に画面暗転のマスク用Script
 *	製作者：宮島幸大
 *	制作日：2019/06/14
 *	----------更新----------
 *	2019/06/26：フェードイン・フェードアウト開始を遅延させるように変更・および現状での最適化(宮島)
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MovingMaskManager : MonoBehaviour
{
	//	フラグ関係
	public float fadeInSpeed;
	public float fadeOutSpeed;
	private float red, green, blue, alfa;					//	パネルの色、不透明度を管理
	bool isFadeOut = false;						//	フェードアウト処理の開始、完了を管理するフラグ
	public bool isFadeIn = false;				//	フェードイン処理の開始、完了を管理するフラグ
	Image fadeImage;								//	透明度を変更するパネルのイメージ

	//	マネージャー関係
	public GameObject managerObject;        //	そのシーンのマネージャがアタッチされているオブジェクトを収納する	※基本的にEventSystemにアタッチ
	Manager_Title mT;								//	Titleのマネージャー
	Manager_DemoMovie mD;					//	DemoMovieのマネージャー
	Manager_CharacterSelect mC;				//	CharacterSelectのマネージャー
	Manager_Game mG;							//	Gameのマネージャー

	//	--------------------
	//	スタート
	//	--------------------
	void Start()
	{
		//	現在のシーンの名前が
		switch (SceneManager.GetActiveScene().name)
		{
			//	Titleだったら
			case "Title":
				mT = managerObject.GetComponent<Manager_Title>();	//mTにTitleマネージャーをロード
				break;
				//	DemoMovieだったら
			case "DemoMovie":
				mD = managerObject.GetComponent<Manager_DemoMovie>();	//mDにDemoMovieマネージャーをロード
				break;
				//	CharacterSelectだったら
			case "CharacterSelect":
				mC = managerObject.GetComponent<Manager_CharacterSelect>();	//mCにCharacterSelectマネージャーをロード
				break;
				//	GAMEだったら
			case "Game":
			case "Battle":
				mG = managerObject.GetComponent<Manager_Game>();	//mGにGameマネージャーをロード
				break;
		}
		fadeImage = GetComponent<Image>();
		isFadeIn = true;
		alfa = 1;
	}

	//	--------------------
	//	アップデート
	//	--------------------
	void Update()
	{
		//	フェードインフラグが立ったら
		if (isFadeIn)
		{
			StartFadeIn();
		}
		//	フェードアウトフラグが立ったら
		if (isFadeOut)
		{
			StartFadeOut();
		}
	}

	//	フェードインさせる
	void StartFadeIn()
	{
		fadeImage.enabled = true;
		alfa -= 1/fadeInSpeed*Time.deltaTime;					//	不透明度を徐々に下げる
		SetAlpha();							//	変更した不透明度パネルに反映する
		if (alfa <= 0)
		{											//	完全に透明になったら処理を抜ける
			isFadeIn = false;
			fadeImage.enabled = false;		//	パネルの表示をオフにする
			switch (SceneManager.GetActiveScene().name)
			{
				//	
				case "Logo":
					FadeOut(1);
					break;
					//	
				case "Title":
					mT.activeTitle = true;
					break;
					//	
				case "CharacterSelect":
					mC.activeCharaselect = true;
					break;
				//	
				//case "Game":
				//case "Battle":
				//	if (mG.round == 0)
				//	{
				//		Debug.Log("よばれた");
				//		mG.EndAppearance();
				//	}
				//	break;
			}
		}
	}

	//	--------------------
	//	フェードアウトさせる
	//	--------------------
	void StartFadeOut()
	{
		if(isFadeIn)
		{
			isFadeIn = false;
		}
		fadeImage.enabled = true;	//	パネルの表示をオンにする
		alfa += 1/fadeOutSpeed*Time.deltaTime;			//	不透明度を徐々にあげる
		SetAlpha();						//	変更した不透明度パネルに反映する
		if (alfa >= 1)
		{										//	完全に不透明になったら処理を抜ける
			isFadeOut = false;
			//	そのシーンの名前が
			switch (SceneManager.GetActiveScene().name)
			{
				case "Logo":
					SceneController.instance.MoveTitle();
					break;
				//	Titleだったら
				case "Title":
					if (mT.demoMovie)
						SceneController.instance.MoveDemoMovie();
					else
						SceneController.instance.MoveCharacterSelect();
					break;
					//	DemoMovieだったら
				case "DemoMovie":
					SceneController.instance.MoveTitle();
					break;
				//	CharacterSelectだったら
				case "CharacterSelect":
					SceneController.instance.MoveBattle();
					break;
					//	Gameだったら
				case "Game":
				case "Battle":
					//	MGの決着フラグが立ったら
					if (mG.finalDecision)
					{
						//	決着
						SceneController.instance.MoveTitle();
						Debug.Log("決着");
					}
					else
					{
						//	次のラウンドへ
						isFadeOut = false;
						mG.Reset();
						isFadeIn = true;
					}
					break;
			}
		}
	}

	//	--------------------
	//	アルファ値の更新
	//	作成者：宮島 幸大
	//	--------------------
	void SetAlpha()
	{
		fadeImage.color = new Color(red, green, blue, alfa);
	}

	//	--------------------
	//	フェードインさせる関数(ディレイ可)
	//	作成者：宮島 幸大
	//	--------------------
	public void FadeIn(float delay)
	{
		Invoke("FadeInFlagOn", delay);
	}

	//	--------------------
	//	フェードアウトさせる関数(ディレイ可)
	//	作成者：宮島 幸大
	//	--------------------
	public void FadeOut(float delay)
	{
		Invoke("FadeOutFlagOn", delay);
	}

	//	--------------------
	//	フェードインのフラグを立てる
	//	作成者：宮島 幸大
	//	--------------------
	void FadeInFlagOn()
	{
		isFadeIn = true;
	}

	//	--------------------
	//	フェードアウトのフラグを立てる
	//	作成者：宮島 幸大
	//	--------------------
	void FadeOutFlagOn()
	{
		isFadeOut = true;
	}
}
//	write by Miyajima Kodai