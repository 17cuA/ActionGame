//作成者：高野
//ManagarTitleがつらくなったので新しくつくりました

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
	private Action currentUpdate;

    [SerializeField] private LogoAnimation logoAnimation;
	[SerializeField] private CanvasController_Title canvasController_Title;
	[SerializeField] private DemoMovie_Sound demoMovie_Sound;
	public GameObject[] blackBackLine;

	private bool isRunDemoMovie = false;

    public float waitPlayDemoMovieTime;

    private float time;

    public float demoMoveTimeMax;

    public float demoMovieVolume = 0.7f;

    private float demoMoveTime;

    public GameObject[] pressAnykey;

	private void InitTitle()
	{
		canvasController_Title.StopPressAnyButton();
        currentUpdate = StartTitle;
	}

	private void StartTitle()
	{
        time = waitPlayDemoMovieTime;

        //画面を明るくする
        if (canvasController_Title.StartFadeIn())
		{
			currentUpdate = UpCurtain;
		}

		logoAnimation.InitLogoAnimation();
	}

	//カーテンを開ける
	private void UpCurtain()
	{
		Debug.Log(canvasController_Title.UpCurtain());

		if (canvasController_Title.UpCurtain() == true)
		{
			//Logoアニメーションの初期化
			//BGMの再生開始
			Sound.LoadBgm("BGM_Title", "BGM_Title");
			Sound.PlayBgm("BGM_Title", 0.3f, 1, true);
			
			currentUpdate = TitleUpdate;	
		}
	}

	private void TitleUpdate()
	{
		//Logoアニメーション開始
		logoAnimation.PlayLogoAnimation();

		if (canvasController_Title.IsEndLogoAnime())
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
    /// 画面を暗くする、デモムービーへの入り
    /// </summary>
    private void StartDemoMovie_FadeOut()
    {
		Sound.StopBgm();

		if (canvasController_Title.StartFadeOut())
        {
            if(canvasController_Title.IsEnabledRenderTexture() == true)
			{
				currentUpdate = PlayDemoMovie;
			}
        }
    }

    /// <summary>
    /// 画面を明るくする、デモムービーへの入り
    /// </summary>
    /// <param name="action">コールバック</param>
    private void StartDemoMovie_Fadein()
    {

		if (canvasController_Title.StartFadeIn())
			currentUpdate = DemoMovieUpdate;
    }


    /// <summary>
    /// DemoMovie再生する
    /// </summary>
    private void PlayDemoMovie()
	{
		for (int i = 0;i < blackBackLine.Length;i++)
		{
			blackBackLine[i].GetComponent<AnimationUIManager>().isStart = true;
		}
        demoMoveTime = demoMoveTimeMax;
        canvasController_Title.PlayVideo();
		currentUpdate = StartDemoMovie_Fadein;
	}

	/// <summary>
	/// デモムービー中のUpdate
	/// </summary>
	private void DemoMovieUpdate()
	{

        demoMoveTime -= Time.deltaTime;
		//音量を徐々に上げる
		demoMovie_Sound.Volume_Up( demoMovieVolume );

		if (Input.anyKeyDown && !Input.GetKeyDown(KeyCode.Escape) && !Input.GetKeyDown(KeyCode.F1) && !Input.GetKeyDown(KeyCode.F2) && !Input.GetKeyDown(KeyCode.F3) && !Input.GetKeyDown(KeyCode.F4))
		{
			currentUpdate = EndDemoMovie_FadeOut;

		}
        //デモムービーの再生が終わったら
        if (demoMoveTime <= 0)
		{
			logoAnimation.InitLogoAnimation();
			demoMovie_Sound.Volume_Down();
			currentUpdate = EndDemoMovie_FadeOut;
		}
	}

    private void EndDemoMovie_FadeOut()
    {
        if (canvasController_Title.StartFadeOut())
        {
			for (int i = 0;i < blackBackLine.Length;i++)
			{
				blackBackLine[i].GetComponent<AnimationUIManager>().isInterruption = true;
				pressAnykey[i].GetComponent<AnimationUIManager>().isInterruption = true;
			}
            canvasController_Title.StopVideo();
            canvasController_Title.DisabledRenderTexture();
            currentUpdate = InitTitle;
        }
    }

    private void EndDemoMovie_FadeIn()
    {
        if (canvasController_Title.StartFadeIn())
            currentUpdate = InitTitle;
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

		//画面を暗くする
		canvasController_Title.BrackOut();

		currentUpdate = InitTitle;

		//幕を閉じた状態から始まる
		canvasController_Title.InitDownCurtain();
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
