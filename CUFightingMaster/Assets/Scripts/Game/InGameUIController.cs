//---------------------------------------
// Gameシーン中のUIの操作
//---------------------------------------
// 作成者:高野
// 作成日:2019.07.12
//--------------------------------------
// 更新履歴
// 2019.07.12 作成
//--------------------------------------
// 仕様
//----------------------------------------
//MEMO
// 書き直すところ(0718)
// 1P2Pでメソッドが分かれているところ
//
// UIの参照について 0718 
// UIの表示非表示を、setActiveでやってるので、参照のタイミングが違うものがある
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
    [SerializeField] UI_Gauge uI_hp_P1;
    [SerializeField] UI_Gauge uI_hp_P2;

    [SerializeField] UI_RoundWinCounter uI_RoundWinCounter;
    [SerializeField] UI_StartRound uI_StartRound;
    [SerializeField] UI_BatlleRound uI_BatlleRound;
    [SerializeField] UI_FinishRound_KO uI_FinishRound_KO;
    [SerializeField] UI_FinishRound_TimeOver uI_FinishRound_TimeOver;
    [SerializeField] UI_GameVictory uI_GameVictory;

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
	public void PlayBatlleRound()
    {
        uI_BatlleRound.DisplayBatlleUI();
        uI_countDownTimer = GameObject.Find("UI_CountDownTimer").GetComponent<CountDownTimer>();

        //一時的
        uI_hp_P1 = GameObject.Find("UI_HP_P1").GetComponent<UI_Gauge>();
        uI_hp_P2 = GameObject.Find("UI_HP_P2").GetComponent<UI_Gauge>();
    }

    /// <summary>
    /// KOでラウンドが終わった時のUIの再生
    /// </summary>
    /// <returns></returns>
	public bool PlayFinishRound__KO()
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
        uI_hp_P1.UpdateSliderValue(currentHp_P1);
        uI_hp_P2.UpdateSliderValue(currentHp_P2);
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
    public void StartCoundDouwn()
    {
        uI_countDownTimer.ResetTimer();
        uI_countDownTimer.isPlayCountDown = true;
    }

    /// <summary>
    /// カウントダウンストップ
    /// </summary>
    public void StopCountDown()
    {
        uI_countDownTimer.isPlayCountDown = false;
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
    ///UIのパラメータのリセット
    /// </summary>
    public void ResetUIParameter()
    {
        uI_StartRound.isCalled = false;
        uI_FinishRound_KO.isCalled = false;
        uI_FinishRound_TimeOver.isCalled = false;
        uI_GameVictory.isCalled = false;
        uI_countDownTimer.ResetTimer();
    }

    private void Awake()
    {
        //参照
        uI_StartRound = GameObject.Find("RoundStart").GetComponent<UI_StartRound>();
        uI_BatlleRound = GameObject.Find("BatlleRound").GetComponent<UI_BatlleRound>();
        uI_FinishRound_KO = GameObject.Find("FinishRound_KO").GetComponent<UI_FinishRound_KO>();
        uI_GameVictory = GameObject.Find("FinishGame_Victory").GetComponent<UI_GameVictory>();
        uI_FinishRound_TimeOver = GameObject.Find("FinishRound_TimeOver").GetComponent<UI_FinishRound_TimeOver>();
        uI_RoundWinCounter = GameObject.Find("RoundWinCounter").GetComponent<UI_RoundWinCounter>();

    }
}
