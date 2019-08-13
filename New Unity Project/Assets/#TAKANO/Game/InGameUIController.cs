//---------------------------------------
// Gameシーン中のUIの操作
//---------------------------------------
// 作成者:高野
// 作成日:2019.07.12
//--------------------------------------
// 更新履歴
// 2019.07.12 作成
// 2019.07.29 ResetRoundCounter()の追加
// 2019.07.30 必殺技ゲージ（SP）の追加
//--------------------------------------
// 仕様
//----------------------------------------
//MEMO
// 書き直すところ(0718)
// 1P2Pでメソッドが分かれているところ
//
// UIの参照について 0718 
// UIの表示非表示を、setActiveでやってるので、参照のタイミングが違うものがある
//
// 参照先について (0726 金沢) 
// 2画面対応のために親のCanvas取得、そのCanvasから他Component取得
//----------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameUIController : MonoBehaviour
{
    //参照先
    [SerializeField] GameObject Player01;
    [SerializeField] GameObject Player02;

    [SerializeField] CountDownTimer uI_countDownTimer;
    [SerializeField] UI_HP uI_hp_P1;
    [SerializeField] UI_HP uI_hp_P2;
	[SerializeField] UI_Gauge uI_sp_P1;
	[SerializeField] UI_Gauge uI_sp_P2;

    [SerializeField] UI_RoundWinCounter uI_RoundWinCounter;
    [SerializeField] UI_StartRound uI_StartRound;
    [SerializeField] UI_BattleRound uI_BatlleRound;
    [SerializeField] UI_FinishRound_KO uI_FinishRound_KO;
    [SerializeField] UI_FinishRound_TimeOver uI_FinishRound_TimeOver;
    [SerializeField] UI_GameVictory uI_GameVictory;

	[SerializeField] GameObject currentCanvas;	// このInGameUIControllerの親のCanvas
	[SerializeField] GameObject uI_InGameUI;

	/// <summary>
	/// ラウンド開始時のUIの再生。再生が終わったら、falseを返す
	/// </summary>
	/// <param name="roundCount">ラウンド数</param>
	/// <returns>再生が終了したか( 再生中:true 終了:false )</returns>
	public bool PlayStartRound(int roundCount)
    {
        return uI_StartRound.PlayStartRound(roundCount);
    }

    /// <summary>
    /// ラウンド開始後のUIの表示
    /// </summary>
	public void PlayBattleRound()
    {
        uI_BatlleRound.DisplayBatlleUI();
        uI_countDownTimer = uI_InGameUI.transform.Find("UI_CountDownTimer").GetComponent<CountDownTimer>();

        //一時的
        uI_hp_P1 = uI_InGameUI.transform.Find("UI_HP_P1").GetComponent<UI_HP>();
        uI_hp_P2 = uI_InGameUI.transform.Find("UI_HP_P2").GetComponent<UI_HP>();
		//uI_sp_P1 = uI_InGameUI.transform.Find("UI_SP_P1").GetComponent<UI_Gauge>();
		//uI_sp_P2 = uI_InGameUI.transform.Find("UI_SP_P2").GetComponent<UI_Gauge>();
	}

    /// <summary>
    /// KOでラウンドが終わった時のUIの再生
    /// </summary>
    /// <returns></returns>
	public bool PlayFinishRound_KO()
    {
        return uI_FinishRound_KO.PlayFinishRound_KO();
    }

    /// <summary>
    /// TimeOverでラウンドが終わった時のUiの再生
    /// </summary>
    /// <returns></returns>
    public bool PlayFinishRound_TimeOver()
    {
        return uI_FinishRound_TimeOver.PlayFinishRound_TimeOver();
    }

    /// <summary>
    /// プレイヤーのhp表示
    /// </summary>
    public void DisplayPlayerHp(int currentHp_P1, int currentHp_P2)
    {
        uI_hp_P1.Call_UpdateHpGuage(currentHp_P1);
        uI_hp_P2.Call_UpdateHpGuage(currentHp_P2);
    }

	/// <summary>
	/// プレイヤーのsp表示
	/// </summary>
	public void DisplayPlayerSp(int currentSp_P1, int currentSp_P2)
	{
		uI_sp_P1.UpdateSliderValue(currentSp_P1);
		uI_sp_P1.UpdateSliderValue(currentSp_P2);
	}

    /// <summary>
    /// プレイヤー1が勝ったときのUI表示
    /// </summary>
    public bool DisplayVictory_winP1()
    {
        return uI_GameVictory.WinP1();
    }

    /// <summary>
    /// プレイヤー2が勝ったときのUI表示
    /// </summary>
    public bool DisplayVictory_winP2()
    {
       return  uI_GameVictory.WinP2();
    }

    /// <summary>
    /// カウントダウン開始
    /// </summary>
    public void StartCountDown()
    {
        uI_countDownTimer.ResetTimer();
		uI_countDownTimer.PlayCountDown(true);
    }

    /// <summary>
    /// カウントダウンストップ
    /// </summary>
    public void StopCountDown()
    {
		uI_countDownTimer.PlayCountDown(false);
    }

    /// <summary>
    /// カウントダウンが終了したか
    /// </summary>
    /// <returns>終了していたらfalse</returns>
    public bool DoEndCountDown()
    {
        return uI_countDownTimer.isEndCountDown();
    }

    /// <summary>
    /// ラウンドカウンターの更新
    /// </summary>
    /// <param name="p1Value">プレイヤー1のラウンド取得数</param>
    /// <param name="p2Value">プレイヤー2のラウンド取得数</param>
    public void UpdateWinCounter(int p1Value, int p2Value)
    {
        uI_RoundWinCounter.UpdateRoundCounter(p1Value, p2Value);
    }

	/// <summary>
	/// ラウンドカウンターをリセット（0729現在、デバッグでしか使っていない）
	/// </summary>
	public void ResetWinCounter()
	{
		uI_RoundWinCounter.ResetWinCounter();
	}

    /// <summary>
    ///UIのパラメータのリセット
    /// </summary>
    public void ResetUIParameter()
    {
        uI_StartRound.Reset_isCalled();
        uI_FinishRound_KO.isCalled = false;
        uI_FinishRound_TimeOver.isCalled = false;
        uI_GameVictory.isCalled = false;
        uI_countDownTimer.ResetTimer();
    }

    private void Awake()
    {
		//参照
		currentCanvas = transform.root.gameObject;
		uI_InGameUI = currentCanvas.transform.Find("InGameUI").gameObject;
        uI_StartRound = currentCanvas.transform.Find("RoundStart").GetComponent<UI_StartRound>();
        uI_BatlleRound = currentCanvas.transform.Find("BatlleRound").GetComponent<UI_BattleRound>();
        uI_FinishRound_KO = currentCanvas.transform.Find("FinishRound_KO").GetComponent<UI_FinishRound_KO>();
        uI_GameVictory = currentCanvas.transform.Find("FinishGame_Victory").GetComponent<UI_GameVictory>();
        uI_FinishRound_TimeOver = currentCanvas.transform.Find("FinishRound_TimeOver").GetComponent<UI_FinishRound_TimeOver>();
        uI_RoundWinCounter = currentCanvas.transform.Find("RoundWinCounter").GetComponent<UI_RoundWinCounter>();

    }
}
