//---------------------------------------
// 2画面時のUI表示を同時に行う
//---------------------------------------
// 作成者:金沢
// 作成日:2019.07.26
//--------------------------------------
// 更新履歴
// 2019.07.26 作成
// 2019.07.30 Call_DisplayPlayerSp()を追加
//--------------------------------------
// 仕様 
// InGameManagerにて呼び出しているバトル画面のUIの表示・切り替えなどの関数を
// 2つのCanvas分同時に呼び出しています。
//----------------------------------------
// MEMO 
// ごり押し。各CanvasはInspecterから直接アタッチする必要あり。
//----------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoBehaviour
{
	[SerializeField] private Canvas canvas_1;
	[SerializeField] private InGameUIController inGameUIController_1;   // canvas_1の子分子のInGameUIController
	[SerializeField] private ScreenFade screenFade_1;   // canvas_1の子分子のScreenFade

	[SerializeField] private Canvas canvas_2;
	[SerializeField] private InGameUIController inGameUIController_2;   // canvas_2の子分子のInGameUIController
	[SerializeField] private ScreenFade screenFade_2;   // canvas_2の子分子のScreenFade

    [SerializeField] private CurtainMover curtainMover_1;
    [SerializeField] private CurtainMover curtainMover_2;

	public GameObject inGameUI_1;
	public GameObject inGameUI_2;

    // 取得
    void Awake()
	{
		if (canvas_1 == null || canvas_2 == null)
			Debug.LogError("参照ミス : CanvacControllerにCanvasを追加してください");

		inGameUIController_1 = canvas_1.transform.Find("InGameUIController").GetComponent<InGameUIController>();
		inGameUIController_2 = canvas_2.transform.Find("InGameUIController").GetComponent<InGameUIController>();

		screenFade_1 = canvas_1.transform.Find("ScreenFade").GetComponent<ScreenFade>();
		screenFade_2 = canvas_2.transform.Find("ScreenFade").GetComponent<ScreenFade>();

        curtainMover_1 = canvas_1.transform.Find("Curtain").GetComponent<CurtainMover>();
        curtainMover_2 = canvas_2.transform.Find("Curtain").GetComponent<CurtainMover>();

    }

	#region ScreenFade
	/// <summary>
	/// 画面を徐々に明るくする
	/// </summary>
	/// <returns>終了していたらtrue</returns>
	public bool Call_StartFadeIn()
	{
		bool endFadeIn_1 = screenFade_1.StartFadeIn();
		bool endFadeIn_2 = screenFade_2.StartFadeIn();
		if (endFadeIn_1 && endFadeIn_2) return true;
		return false;
	}

	/// <summary>
	/// 画面を徐々に暗くする
	/// </summary>
	/// <returns>終了していたらtrue</returns>
	public bool Call_StartFadeOut()
	{
		bool endFadeOut_1 = screenFade_1.StartFadeOut();
		bool endFadeOut_2 = screenFade_2.StartFadeOut();
		if (endFadeOut_1 && endFadeOut_2) return true;
		return false;
	}

	public void Call_BrackOut()
	{
		screenFade_1.BrackOut();
		screenFade_2.BrackOut();
	}
	#endregion

	#region ラウンド開始・終了時のUI表示
	/// <summary>
	/// ラウンド開始時のUIの再生。再生が終わったら、falseを返す
	/// </summary>
	/// <param name="roundCount">ラウンド数</param>
	/// <returns>再生が終了したか( 再生中:true 終了:false )</returns>
	public bool Call_PlayStartRound(int roundCount)
	{
		if (inGameUIController_1.PlayStartRound(roundCount) && inGameUIController_2.PlayStartRound(roundCount)) return true;
		return false;
	}

	/// <summary>
	/// ラウンド開始後のUIの表示
	/// </summary>
	public void Call_PlayBattleRound()
	{
		inGameUIController_1.PlayBattleRound();
		inGameUIController_2.PlayBattleRound();
	}

	/// <summary>
	/// KOでラウンドが終わった時のUIの再生
	/// </summary>
	/// <returns>終了していたらtrue</returns>
	public bool Call_PlayFinishRound_KO()
	{
		if (inGameUIController_1.PlayFinishRound_KO() && inGameUIController_2.PlayFinishRound_KO()) return true;
		return false;
	}

	/// <summary>
	/// TimeOverでラウンドが終わった時のUIの再生
	/// </summary>
	/// <returns>終了していたらtrue</returns>
	public bool Call_PlayFinishRound_TimeOver()
	{
		if (inGameUIController_1.PlayFinishRound_TimeOver() && inGameUIController_2.PlayFinishRound_TimeOver()) return true;
		return false;
	}

	/// <summary>
	/// Player1が勝ったときのUI表示
	/// </summary>
	public bool Call_DisplayVictory_winP1()
	{
		if (inGameUIController_1.DisplayVictory_winP1() && inGameUIController_2.DisplayVictory_winP1()) return true;
		return false;
	}

	/// <summary>
	/// Player2が勝ったときのUI表示
	/// </summary>
	public bool Call_DisplayVictory_winP2()
	{
		if (inGameUIController_1.DisplayVictory_winP2() && inGameUIController_2.DisplayVictory_winP2()) return true;
		return false;
	}

	/// <summary>
	/// 引き分けたときのUI表示
	/// </summary>
	public bool Call_DisplayVictory_draw()
	{
		if (inGameUIController_1.DisplayVictory_draw() && inGameUIController_2.DisplayVictory_draw()) return true;
		return false;
	}

	#endregion

	#region CountDown
	/// <summary>
	/// カウントダウン開始
	/// </summary>
	public void Call_StartCountDown()
	{
		inGameUIController_1.StartCountDown();
		inGameUIController_2.StartCountDown();
	}

	/// <summary>
	/// カウントダウンが終了したか
	/// </summary>
	/// <returns>終了していたらfalse</returns>
	public bool Call_DoEndCountDown()
	{
		if (inGameUIController_1.DoEndCountDown() == false && inGameUIController_2.DoEndCountDown() == false) return false;
		return true;
	}

    /// <summary>
    /// カウントダウン一時停止
    /// </summary>
    public void Call_StopCountDown()
    {
        inGameUIController_1.StopCountDown();
        inGameUIController_2.StopCountDown();
    }

    /// <summary>
    /// カウントダウン再開
    /// </summary>
    public void Call_ResumeCountdown()
    {
        inGameUIController_1.ResumeCountdown();
        inGameUIController_2.ResumeCountdown();
    }
    #endregion

    #region UIの更新
    /// <summary>
    /// PlayerのHP表示
    /// </summary>
    /// <param name="currentHp_P1">Player1の現在のHP</param>
    /// <param name="currentHp_P2">Player2の現在のHP</param>
    public void Call_DisplayPlayerHp(int currentHp_P1, int currentHp_P2)
	{
		inGameUIController_1.DisplayPlayerHp(currentHp_P1, currentHp_P2);
		inGameUIController_2.DisplayPlayerHp(currentHp_P1, currentHp_P2);
	}

	/// <summary>
	/// PlayerのSp表示
	/// </summary>
	/// <param name="currentSp_P1">Player1の現在のSp</param>
	/// <param name="currentSp_P2">Player2の現在のSp</param>
	public void Call_DisplayPlayerSp(int currentSp_P1, int currentSp_P2)
	{
		inGameUIController_1.DisplayPlayerSp(currentSp_P1, currentSp_P2);
		inGameUIController_2.DisplayPlayerSp(currentSp_P1, currentSp_P2);
	}

	/// <summary>
	/// PlayerのSt表示
	/// </summary>
	/// <param name="currentSt_P1">Player1の現在のSp</param>
	/// <param name="currentSt_P2">Player2の現在のSp</param>
	public void Call_DisplayPlayerSt(int currentSt_P1, int currentSt_P2)
	{
		inGameUIController_1.DisplayPlayerSt(currentSt_P1, currentSt_P2);
		inGameUIController_2.DisplayPlayerSt(currentSt_P1, currentSt_P2);
	}

	/// <summary>
	/// ラウンドカウンターの更新
	/// </summary>
	/// <param name="p1Value">Player1のラウンド取得数</param>
	/// <param name="p2Value">Player2のラウンド取得数</param>
	public void Call_UpdateWinCounter(string p1Value, string p2Value)
	{
		inGameUIController_1.UpdateWinCounter(p2Value, p1Value);
		inGameUIController_2.UpdateWinCounter(p2Value, p1Value);
	}

	/// <summary>
	/// ラウンドカウンターをリセット（0729現在、デバッグでしか使っていない）
	/// </summary>
	public void ResetWinCounter()
	{
		inGameUIController_1.ResetWinCounter();
		inGameUIController_2.ResetWinCounter();
	}
	#endregion

	/// <summary>
	///UIのパラメータのリセット
	/// </summary>
	public void Call_ResetUIParameter()
	{
		inGameUIController_1.ResetUIParameter();
		inGameUIController_2.ResetUIParameter();
	}

	/// <summary>
	/// HPバーにキャラのHPの最大値をセット
	/// </summary>
	/// <param name="charaHp_P1"></param>
	/// <param name="charaHp_P2"></param>
	public void Call_SetUIParameterMax(int charaHp_P1, int charaHp_P2 , int charaSp_P1 , int charaSp_P2 )
	{
		inGameUIController_1.SetHpMax(charaHp_P1, charaHp_P2);
		inGameUIController_2.SetHpMax(charaHp_P1, charaHp_P2);
		inGameUIController_1.SetSpMax(charaSp_P1, charaSp_P2);
		inGameUIController_2.SetSpMax(charaSp_P1, charaSp_P2);
	}

    /// <summary>
    ///徐々に 幕を開ける
    /// </summary>
    public bool Call_UpCurtain()
    {
        bool isEnd1 = curtainMover_1.UpCurtain();
        bool isEnd2 = curtainMover_2.UpCurtain();

        if (isEnd1 && isEnd2)
            return true;

        return false;
    }

    /// <summary>
    ///徐々に幕を下ろす
    /// </summary>
    public bool Call_DownCurtain()
    {
        bool isEnd1 = curtainMover_1.DownCurtain();
        bool isEnd2 = curtainMover_2.DownCurtain();

        if (isEnd1 && isEnd2)
            return true;
        return false;
    }

    /// <summary>
    ///一気に幕を下ろす
    /// </summary>
    public void Call_InitDownCurtain()
    {
        curtainMover_1.InitDownCurtain();
        curtainMover_2.InitDownCurtain();
    }

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.F7))
		{
			inGameUI_1.SetActive(!inGameUI_1.active);
			inGameUI_2.SetActive(!inGameUI_2.active);
		}
	}

}
