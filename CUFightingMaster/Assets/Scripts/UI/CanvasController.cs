//---------------------------------------
// 2画面時のUI表示を同時に行う
//---------------------------------------
// 作成者:金沢
// 作成日:2019.07.26
//--------------------------------------
// 更新履歴
// 2019.07.26 作成
//--------------------------------------
// 仕様 
// InGameManagerにて呼び出しているバトル画面のUIの表示・切り替えなどの関数を
// 2つのCanvas分同時に呼び出しています。
//----------------------------------------
// MEMO 
// ごり押し。各CanvasはInspecterから直接アタッチする必要あり。
// 現状の問題点としてFadeIn、FadeOutで遅れが発生する。他の関数も合っているか分からない。
//----------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoBehaviour
{
	public Canvas canvas_1;
	[SerializeField] private InGameUIController inGameUIController_1;	// canvas_1の子分子のInGameUIController
	[SerializeField] private ScreenFade screenFade_1;	// canvas_1の子分子のScreenFade

	public Canvas canvas_2;
	[SerializeField] private InGameUIController inGameUIController_2;   // canvas_2の子分子のInGameUIController
	[SerializeField] private ScreenFade screenFade_2;   // canvas_2の子分子のScreenFade

	// 取得
	void Awake()
	{
		inGameUIController_1 = canvas_1.transform.Find("InGameUIController").GetComponent<InGameUIController>();
		inGameUIController_2 = canvas_2.transform.Find("InGameUIController").GetComponent<InGameUIController>();

		screenFade_1 = canvas_1.transform.Find("ScreenFade").GetComponent<ScreenFade>();
		screenFade_2 = canvas_2.transform.Find("ScreenFade").GetComponent<ScreenFade>();
	}

	/// <summary>
	/// 画面を徐々に暗くする
	/// </summary>
	/// <returns>終了していたらtrue</returns>
	public bool Check_StartFadeOut()
	{
		if (screenFade_1.StartFadeOut() && screenFade_2.StartFadeOut()) return true;
		return false;
	}

	/// <summary>
	/// 画面を徐々に明るくする
	/// </summary>
	/// <returns>終了していたらtrue</returns>
	public bool Check_StartFadeIn()
	{
		if (screenFade_1.StartFadeIn() && screenFade_2.StartFadeIn()) return true;
		return false;
	}

	/// <summary>
	/// ラウンド開始後のUIの表示
	/// </summary>
	public void Start_PlayBattleRound()
	{
		inGameUIController_1.PlayBattleRound();
		inGameUIController_2.PlayBattleRound();
	}

	/// <summary>
	/// ラウンド開始時のUIの再生。再生が終わったら、falseを返す
	/// </summary>
	/// <param name="roundCount">ラウンド数</param>
	/// <returns>再生が終了したか( 再生中:true 終了:false )</returns>
	public bool Start_PlayStartRound(int roundCount)
	{
		if (inGameUIController_1.PlayStartRound(roundCount) && inGameUIController_2.PlayStartRound(roundCount)) return true;
		return false;
	}

	/// <summary>
	/// カウントダウン開始
	/// </summary>
	public void Start_CountDown()
	{
		inGameUIController_1.StartCountDown();
		inGameUIController_2.StartCountDown();
	}

	/// <summary>
	/// PlayerのHP表示
	/// </summary>
	/// <param name="currentHp_P1">Player1の現在のHP</param>
	/// <param name="currentHp_P2">Player2の現在のHP</param>
	public void Display_PlayerHP(int currentHp_P1, int currentHp_P2)
	{
		inGameUIController_1.DisplayPlayerHp(currentHp_P1, currentHp_P2);
		inGameUIController_2.DisplayPlayerHp(currentHp_P1, currentHp_P2);
	}

	/// <summary>
	/// カウントダウンが終了したか
	/// </summary>
	/// <returns>終了していたらfalse</returns>
	public bool Check_EndCountDown()
	{
		if (inGameUIController_1.DoEndCountDown() == false && inGameUIController_2.DoEndCountDown() == false) return false;
		return true;
	}

	/// <summary>
	/// KOでラウンドが終わった時のUiの再生
	/// </summary>
	/// <returns>終了していたらtrue</returns>
	public bool Play_FinishRound_KO()
	{
		if (inGameUIController_1.PlayFinishRound_KO() && inGameUIController_2.PlayFinishRound_KO()) return true;
		return false;
	}

	/// <summary>
	/// TimeOverでラウンドが終わった時のUiの再生
	/// </summary>
	/// <returns>終了していたらtrue</returns>
	public bool Play_FinishRound_TimeOver()
	{
		if (inGameUIController_1.PlayFinishRound_TimeOver() && inGameUIController_2.PlayFinishRound_TimeOver()) return true;
		return false;
	}

	/// <summary>
	///UIのパラメータのリセット
	/// </summary>
	public void Reset_UIParameter()
	{
		inGameUIController_1.ResetUIParameter();
		inGameUIController_2.ResetUIParameter();
	}

	/// <summary>
	/// ラウンドカウンターの更新
	/// </summary>
	/// <param name="p1Value">Player1のラウンド取得数</param>
	/// <param name="p2Value">Player2のラウンド取得数</param>
	public void Update_WinCounter(int p1Value, int p2Value)
	{
		inGameUIController_1.UpdateWinCounter(p1Value, p2Value);
		inGameUIController_2.UpdateWinCounter(p1Value, p2Value);
	}

	/// <summary>
	/// Player1が勝ったときのUI表示
	/// </summary>
	public bool Display_Victory_winP1()
	{
		if (inGameUIController_1.DisplayVictory_winP1() && inGameUIController_2.DisplayVictory_winP1()) return true;
		return false;
	}

	/// <summary>
	/// Player2が勝ったときのUI表示
	/// </summary>
	public bool Display_Victory_winP2()
	{
		if (inGameUIController_1.DisplayVictory_winP2() && inGameUIController_2.DisplayVictory_winP2()) return true;
		return false;
	}
}
