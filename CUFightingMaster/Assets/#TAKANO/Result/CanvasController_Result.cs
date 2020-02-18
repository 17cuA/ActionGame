using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController_Result : MonoBehaviour
{
    public Canvas canvas_Display1;
    public Canvas canvas_Display2;

    [SerializeField] private ScreenFade screenFade_Display1;
    [SerializeField] private ScreenFade screenFade_Display2;
    [SerializeField] private CurtainMover curtainMover_1;
    [SerializeField] private CurtainMover curtainMover_2;
    [SerializeField] private AnimationUIManager UIAnimator_1;
    [SerializeField] private AnimationUIManager UIAnimator_2;
	[SerializeField] private UIMover UIMover_1;
	[SerializeField] private UIMover UIMover_2;
	[SerializeField] private WinOrLoseDisplay winOrLoseDisplay_1;
	[SerializeField] private WinOrLoseDisplay winOrLoseDisplay_2;
	[SerializeField] private RoundGetDisplay roundGetDisplay_1;
	[SerializeField] private RoundGetDisplay roundGetDisplay_2;
	[SerializeField] private ScoreDisplay scoreDisplay_damage_1;
	[SerializeField] private ScoreDisplay scoreDisplay_damage_2;
	[SerializeField] private ScoreDisplay scoreDisplay_HP_1;
	[SerializeField] private ScoreDisplay scoreDisplay_HP_2;

	private void Awake()
    {
        if (canvas_Display1 == null || canvas_Display2 == null)
            Debug.LogError("参照ミス : CanvacControllerにCanvasを追加してください");

        //screenFade_Display1 = canvas_Display1.transform.Find("ScreenFade").GetComponent<ScreenFade>();
        //screenFade_Display2 = canvas_Display2.transform.Find("ScreenFade").GetComponent<ScreenFade>();
        //curtainMover_1 = canvas_Display1.transform.Find("Curtain").GetComponent<CurtainMover>();
        //curtainMover_2 = canvas_Display2.transform.Find("Curtain").GetComponent<CurtainMover>();
    }

    /// <summary>
    /// 二画面を徐々に明るくする
    /// </summary>
    /// <returns>明るくなったらtrue</returns>
    public bool StartFadeIn()
    {
        bool isEndFadeIn_1 = screenFade_Display1.StartFadeIn();
        bool isEndFadeIn_2 = screenFade_Display2.StartFadeIn();

        if (isEndFadeIn_1 && isEndFadeIn_2)
            return true;
        return false;
    }

    /// <summary>
    /// 二画面を徐々に暗くする
    /// </summary>
    /// <returns>暗くなったらtrue</returns>
    public bool StartFadeOut()
    {
        bool isEndFadeOut_1 = screenFade_Display1.StartFadeOut();
        bool isEndFadeOut_2 = screenFade_Display2.StartFadeOut();

        if (isEndFadeOut_1 && isEndFadeOut_2)
            return true;
        return false;
    }

    /// <summary>
    ///一気に幕を下ろす
    /// </summary>
    public void InitDownCurtain()
    {
        curtainMover_1.InitDownCurtain();
        curtainMover_2.InitDownCurtain();
    }

    /// <summary>
    ///徐々に 幕を開ける
    /// </summary>
    public bool UpCurtain()
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
    public bool DownCurtain()
    {
        bool isEnd1 = curtainMover_1.DownCurtain();
        bool isEnd2 = curtainMover_2.DownCurtain();

        if (isEnd1 && isEnd2)
            return true;
        return false;
    }

	public void MoveUIGroup1()
	{
		UIMover_1.Move(0);
		UIMover_2.Move(0);
	}

	public void MoveUIGroup2()
	{
		UIMover_1.Move(1);
		UIMover_2.Move(1);
	}

	public void MoveUIGroup3()
	{
		UIMover_1.Move(2);
		UIMover_2.Move(2);
	}
	public void MoveUIGroup4()
	{
		UIMover_1.Move(3);
		UIMover_2.Move(3);
	}

	public void P1WinDisplay()
	{
		winOrLoseDisplay_1.P1WinDisplay();
		winOrLoseDisplay_2.P1WinDisplay();
	}

	public void P2WinDisplay()
	{
		winOrLoseDisplay_1.P2WinDisplay();
		winOrLoseDisplay_2.P2WinDisplay();
	}
	public void RoundGetDisplay()
	{
		roundGetDisplay_1.Display();
		roundGetDisplay_2.Display();
	}

	public void PassHPtoScore()
	{
		scoreDisplay_HP_1.num = GameDataStrage.Instance.remainingHp[(int)PlayerNumber.Player1] * 10000;
		scoreDisplay_HP_2.num = GameDataStrage.Instance.remainingHp[(int)PlayerNumber.Player2]  *10000;
		scoreDisplay_damage_1.num = GameDataStrage.Instance.givenDamage[(int)PlayerNumber.Player1] * 10000;
		scoreDisplay_damage_2.num = GameDataStrage.Instance.givenDamage[(int)PlayerNumber.Player2] * 10000;
	}
}
