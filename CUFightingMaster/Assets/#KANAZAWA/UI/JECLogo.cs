using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JECLogo : MonoBehaviour
{
	private float timeMax;		// JECLogoを表示する秒数
	private float timeCurrent;	// 表示してからの秒数
	private bool fade_1;		// Canvas1のFade完了フラグ
	private bool fade_2;    // Canvas1のFade完了フラグ
	private ScreenFade screenFade1;
	private ScreenFade screenFade2;

	public Canvas canvas1;
	public Canvas canvas2;

	// Start is called before the first frame update
	void Start()
    {
		timeMax = 2.0f;
		timeCurrent = 0.0f;
		fade_1 = false;
		fade_2 = false;
		screenFade1 = canvas1.transform.Find("ScreenFade").GetComponent<ScreenFade>();
		screenFade2 = canvas2.transform.Find("ScreenFade").GetComponent<ScreenFade>();
	}

	// Update is called once per frame
	void Update()
    {
		if (timeCurrent == 0.0f)
		{
			fade_1 = screenFade1.StartFadeIn();
			fade_2 = screenFade2.StartFadeIn();
		}
		else if (timeCurrent >= timeMax)
		{
			fade_1 = screenFade1.StartFadeOut();
			fade_2 = screenFade2.StartFadeOut();
		}

		if (fade_1 && fade_2)
		{
			if (timeCurrent >= timeMax) SceneManager.LoadScene("Title");
			timeCurrent += Time.deltaTime;
		}

	}
}
