using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JECLogo : MonoBehaviour
{
	float timeMax;
	float timeCurrent;
	bool fade_1;
	bool fade_2;

	public Canvas canvas1;
	public Canvas canvas2;
	public ScreenFade screenFade1;
	public ScreenFade screenFade2;

	// Start is called before the first frame update
	void Start()
    {
		timeMax = 2.0f;
		timeCurrent = 0.0f;
		fade_1 = false;
		fade_2 = false;
		screenFade1 = screenFade1.GetComponent<ScreenFade>();
		screenFade2 = screenFade2.GetComponent<ScreenFade>();
	}

	// Update is called once per frame
	void Update()
    {
		fade_1 = screenFade1.StartFadeIn();
		fade_2 = screenFade2.StartFadeIn();

		if (fade_1 && fade_2)
		{
			timeCurrent += Time.deltaTime;
			fade_1 = false;
			fade_2 = false;
		}

		if(timeCurrent >= timeMax)
		{
			fade_1 = screenFade1.StartFadeOut();
			fade_2 = screenFade2.StartFadeOut();
		}

		if ((fade_1 && fade_2) && timeCurrent >= timeMax)
		{
			SceneManager.LoadScene("CharacterSelect");
		}
	}
}
