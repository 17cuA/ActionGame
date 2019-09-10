//作成者：高野
//ManagarTitleがつらくなったので新しくつくりました

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    public NomalAnimationPlayer animationPlayer;

	private Action currentUpdate;

	[SerializeField] private CanvasController_Title canvasController_Title;

    public bool isRunDemoMovie = false;

    public GameObject[] pressAnykey;

	private void InitTitle()
	{
		//画面を暗くする
		canvasController_Title.BrackOut();
		currentUpdate = StartTitle;
	}

	private void StartTitle()
	{
		//幕を閉じた状態から始まる
		canvasController_Title.InitDownCurtain();
		//BGMの再生開始
		Sound.LoadBgm("BGM_Title", "BGM_Title");
		Sound.PlayBgm("BGM_Title", 0.4f, 1, true);

		//画面を明るくする
		if(canvasController_Title.StartFadeIn())
		{
			currentUpdate = UpCurtain;
		}
	}

	private void UpCurtain()
	{
		if(canvasController_Title.UpCurtain())
		{
			currentUpdate = TitleUpdate;	
		}
	}

	private void TitleUpdate()
	{
        Debug.Log("Update");
        //キーの入力受付
        if (Input.anyKeyDown && !Input.GetKeyDown(KeyCode.Escape) && !Input.GetKeyDown(KeyCode.F1) && !Input.GetKeyDown(KeyCode.F2) && !Input.GetKeyDown(KeyCode.F3) && !Input.GetKeyDown(KeyCode.F4))
		{
            if(pressAnykey[0].activeSelf == true && pressAnykey[1].activeSelf == true)
            {
                Sound.LoadSe("Menu_Decision", "Se_menu_decision");
                Sound.PlaySe("Menu_Decision", 1, 0.3f);
                currentUpdate = DownCurtain;
            }
		}
        //if (isRunDemoMovie == true)
        //{
        //    currentUpdate = FadeOut;
        //}
    }

    /// <summary>
    /// 画面を暗くする
    /// </summary>
    private void FadeOut()
    {
        if (canvasController_Title.StartFadeOut())
            currentUpdate = FadeIn;

    }

    /// <summary>
    /// 画面を明るくする
    /// </summary>
    /// <param name="action">コールバック</param>
    private void FadeIn()
    {
        //if (canvasController_Title.StartFadeIn())
            //currentUpdate =
    }

    ///// <summary>
    ///// DemoMovie再生する
    ///// </summary>
    //private void PlayDemoMovie()
    //{
    //    canvasController_Title.PlayVideo();
    //}

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
		currentUpdate = InitTitle;
        currentUpdate();

        canvasController_Title.PlayVideo();
    }

    // Update is called once per frame
    void Update()
    {
		//ポーズ処理
		if (Mathf.Approximately(Time.timeScale, 0f)) return;

		currentUpdate();

        
    }
}
