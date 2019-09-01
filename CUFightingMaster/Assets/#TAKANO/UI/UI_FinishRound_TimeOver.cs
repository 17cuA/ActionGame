//---------------------------------------
// TimeOverでラウンドが終わった時のUI表示
//---------------------------------------
// 作成者:高野
// 作成日:2019.07.15
//--------------------------------------
// 更新履歴
// 2019.07.15 作成
//--------------------------------------
// 仕様
//※07/15どう動かせば良いのかわからないので、一時的なプログラムです※
// 07/15現在、表示をするだけです。何らかの処理がある場合、随時追加してください
// 
//コルーチンの再生は一度だけです(isCalled)
//----------------------------------------
// MEMO
// 07/13現在、制御するUIの参照は手動です（一時的
//----------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UI_FinishRound_TimeOver : MonoBehaviour
{
	public int interval01;

	public bool isPlay = false;         //再生中か（InGameManagerへの戻り値）
	public bool isCalled = false;    //既に呼ばれているか(コルーチンの再生を一度きりに使う)

	private Image image;

	public GameObject timeOver;

	public bool PlayFinishRound_TimeOver()
	{
		if (isCalled == false)
		{
			isCalled = true;
			StartCoroutine(RoundFinish_KOCoroutine());
		}
		return isPlay;
	}
	IEnumerator RoundFinish_KOCoroutine()
	{
		 // 飯塚追加-------------------------------------------
        Sound.LoadSe("Draw", "Voice_Draw");
        Sound.PlaySe("Draw", 3, 0.8f);
        // ---------------------------------------------------
		isPlay = true;

		image.enabled = true;
		yield return new WaitForSeconds(interval01);
		image.enabled = false;

		isPlay = false;
	}
	private void Awake()
	{
		image = timeOver.GetComponent<Image>();
	}
}
