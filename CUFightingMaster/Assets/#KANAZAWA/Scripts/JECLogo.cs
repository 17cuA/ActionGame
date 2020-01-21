//---------------------------------------
// 日電ロゴの表示
//---------------------------------------
// 作成者:金沢
// 作成日:2019.08.13
//--------------------------------------
// 更新履歴
// 2019.08.13 作成
//--------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JECLogo : MonoBehaviour
{
	private delegate void Move();
	private float timeMax;		// JECLogoを表示する秒数
	private float timeCurrent;	// 表示してからの秒数
	private bool fade_1;		// Canvas1のFade完了フラグ
	private bool fade_2;		// Canvas2のFade完了フラグ
	private ScreenFade screenFade1;	// canvas1のScreenFade
	private ScreenFade screenFade2;	// canvas1のScreenFade
	private SpriteRenderer displayImage;    // 自分のSpriteRenderer
	public Sprite changeImage;	// 切り替える画像
	private Move currentUpdate;

	public Canvas canvas1;
	public Canvas canvas2;
	void Start()
    {
		timeMax = 2.0f;
		timeCurrent = 0.0f;
		fade_1 = false;
		fade_2 = false;
		screenFade1 = canvas1.transform.Find("ScreenFade").GetComponent<ScreenFade>();
		screenFade2 = canvas2.transform.Find("ScreenFade").GetComponent<ScreenFade>();
		displayImage = GetComponent<SpriteRenderer>();
		currentUpdate = Caution;
	}

	// Update is called once per frame
	void Update()
    {
		//ポーズ処理
		if (Mathf.Approximately(Time.timeScale, 0f)) return;
		currentUpdate();
	}

	private void Caution()
	{
		// ゲーム開始直後に処理
		if (timeCurrent == 0.0f)
		{
			// 各モニターでフェードインの処理を行う
			fade_1 = screenFade1.StartFadeIn();
			fade_2 = screenFade2.StartFadeIn();
		}
		// CAUTIONの表示が終了した時
		else if (timeCurrent >= timeMax)
		{
			fade_1 = screenFade1.StartFadeOut();
			fade_2 = screenFade2.StartFadeOut();
		}

		// 各モニターでフェードインが終了した時
		if (fade_1 && fade_2)
		{
			if (timeCurrent >= timeMax) currentUpdate = Reset;
			timeCurrent += Time.deltaTime;
		}
	}

	private void Reset()
	{
		timeCurrent = 0.0f;
		fade_1 = false;
		fade_2 = false;
		displayImage.sprite = changeImage;	// 画像切り替え
		currentUpdate = JecLogo;
	}

	private void JecLogo()
	{
		// ゲーム開始直後に処理
		if (timeCurrent == 0.0f)
		{
			// 各モニターでフェードインの処理を行う
			fade_1 = screenFade1.StartFadeIn();
			fade_2 = screenFade2.StartFadeIn();
		}
		// 日電ロゴの表示が終了した時
		else if (timeCurrent >= timeMax)
		{
			fade_1 = screenFade1.StartFadeOut();
			fade_2 = screenFade2.StartFadeOut();
		}

		// 各モニターでフェードインが終了した時
		if (fade_1 && fade_2)
		{
			if (timeCurrent >= timeMax) SceneManager.LoadScene("Title");
			timeCurrent += Time.deltaTime;
		}
	}
}
