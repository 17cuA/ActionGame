//作成者：高野
//ManagarTitleがつらくなったので新しくつくりました

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
	private Action currentUpdate;

    [SerializeField] private LogoAnimation logoAnimation;
	[SerializeField] private CanvasController_Title canvasController_Title;

    public bool isRunDemoMovie = false;

     public float waitPlayDemoMovieTime;

    private float time;

    public GameObject[] pressAnykey;

	private void InitTitle()
	{
		//画面を暗くする
		canvasController_Title.BrackOut();
        //幕を閉じた状態から始まる
        canvasController_Title.InitDownCurtain();
        currentUpdate = StartTitle;
	}

	private void StartTitle()
	{
        //BGMの再生開始
        Sound.LoadBgm("BGM_Title", "BGM_Title");
        Sound.PlayBgm("BGM_Title", 0.3f, 1, true);

        //Logoアニメーションの初期化
        logoAnimation.InitLogoAnimation();

        time = waitPlayDemoMovieTime;

        //画面を明るくする
        if (canvasController_Title.StartFadeIn())
		{
			currentUpdate = UpCurtain;
		}
	}

	//カーテンを開ける
	private void UpCurtain()
	{
		if(canvasController_Title.UpCurtain())
		{
			currentUpdate = TitleUpdate;	
		}
	}

	private void TitleUpdate()
	{
		//Logoアニメーション開始
		logoAnimation.PlayLogoAnimation();

        if(canvasController_Title.IsEndLogoAnime())
              canvasController_Title.PlayPressAnyButton();

        Debug.Log("Update");
		//キーの入力受付
		if (Input.anyKeyDown && !Input.GetKeyDown(KeyCode.Escape) && !Input.GetKeyDown(KeyCode.F1) && !Input.GetKeyDown(KeyCode.F2) && !Input.GetKeyDown(KeyCode.F3) && !Input.GetKeyDown(KeyCode.F4))
		{
            if (pressAnykey[0].activeSelf == true && pressAnykey[1].activeSelf == true)
			{
				Sound.LoadSe("Menu_Decision", "Se_menu_decision");
				Sound.PlaySe("Menu_Decision", 1, 0.3f);
				currentUpdate = DownCurtain;
			}
		}

        time -= Time.deltaTime;

		if (time <= 0)
		{
			currentUpdate = StartDemoMovie_FadeOut;
		}
	}

    /// <summary>
    /// 画面を暗くする
    /// </summary>
    private void StartDemoMovie_FadeOut()
    {
        if (canvasController_Title.StartFadeOut())
        {
            if(canvasController_Title.IsEnabledRenderTexture() == true)
			{
				currentUpdate = StartDemoMovie_Fadein;
			}
        }
    }

    /// <summary>
    /// 画面を明るくする
    /// </summary>
    /// <param name="action">コールバック</param>
    private void StartDemoMovie_Fadein()
    {
		if (canvasController_Title.StartFadeIn())
			currentUpdate = PlayDemoMovie;
    }


    /// <summary>
    /// DemoMovie再生する
    /// </summary>
    private void PlayDemoMovie()
	{
		canvasController_Title.PlayVideo();
		currentUpdate = DemoMovieUpdate;
	}

	/// <summary>
	/// デモムービー中のUpdate
	/// </summary>
	private void DemoMovieUpdate()
	{
		if (Input.anyKeyDown && !Input.GetKeyDown(KeyCode.Escape) && !Input.GetKeyDown(KeyCode.F1) && !Input.GetKeyDown(KeyCode.F2) && !Input.GetKeyDown(KeyCode.F3) && !Input.GetKeyDown(KeyCode.F4))
		{
			currentUpdate = DownCurtain;
		}

		//デモムービーの再生が終わったら
		if (canvasController_Title.IsEndPlayVideo())
		{
			canvasController_Title.StopVideo();
			currentUpdate = EndDemoMovie_FadeOut;
		}
	}

    private void EndDemoMovie_FadeOut()
    {
        if (canvasController_Title.StartFadeOut())
		{
			if(canvasController_Title.IsDisabledRenderTexture() == true)
			{
				currentUpdate = EndDemoMovie_FadeIn;
			}
		}
    }

    private void EndDemoMovie_FadeIn()
    {
        //Logoアニメーションの初期化
        logoAnimation.InitLogoAnimation();

        if (canvasController_Title.StartFadeIn())
            currentUpdate = StartTitle;
    }

    private void DownCurtain()
	{
		if(canvasController_Title.DownCurtain())
		{
			SceneManager.LoadScene("CharacterSelect");
		}
	}
    // Start is called before the first frame update
    void Start()
    {
		Application.targetFrameRate = 60;

		currentUpdate = InitTitle;
        currentUpdate();

       // canvasController_Title.PlayVideo();
    }

    // Update is called once per frame
    void Update()
    {
		//ポーズ処理
		if (Mathf.Approximately(Time.timeScale, 0f)) return;

		currentUpdate();

        
    }
}
