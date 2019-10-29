//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using System;

//public class LetterAnimationController : MonoBehaviour
//{
//	//[SerializeField] RoundStart_1 roundStart_1;
//	//[SerializeField] RoundStart_2 roundStart_2;
//	//[SerializeField] RoundStart_Final roundStart_Final;
//	//[SerializeField] RoundStart_Fight roundStart_Fight;
//	//[SerializeField] RoundFinish_KO roundFinish_KO;
//	//[SerializeField] RoundFinish_TimeOver roundFinish_TimeOver;
//	//[SerializeField] GameFinish_1PWin gameFinish_1PWin;
//	//[SerializeField] GameFinish_2PWin gameFinish_2PWin;
//	//[SerializeField] GameFinish_Draw gameFinish_Draw;

//	GameObject curretAnimeObject;

//	Action action_KO;
//	Action action_TimeOver;
//	Action action_Player1Win;
//	Action action_Player2Win;
//	Action action_Draw;

//	private delegate void Delegate(int num = 0);
//	Delegate action2;

//	public bool IsPlay { private set; get; } = false;

//	#region ラウンドスタート時のアニメーション再生メソッド

//	/// <summary>
//	/// ラウンドスタート時のアニメーション再生するときに呼ぶ
//	/// </summary>
//	/// <param name="_currentRound"></param>
//	/// <returns></returns>
//	public bool Call_PlayRoundStart(int _currentRound)
//	{
//		action2(_currentRound);
//		return IsPlay;
//	}

//	/// <summary>
//	/// Roundのアニメーション再生
//	/// </summary>
//	/// <param name="_currentRound"></param>
//	private void PlayRoundStart(int _currentRound)
//	{
//		if (IsPlay == false)
//		{
//			IsPlay = true;
//			switch (_currentRound)
//			{
//				case 1:
//					roundStart_1.PlayAnimation();
//					break;
//				case 2:
//					roundStart_2.PlayAnimation();
//					break;
//				case 3:
//					roundStart_Final.PlayAnimation();
//					break;
//			}
//		}
//		action2 = PlayFight;
//	}

//	/// <summary>
//	/// Fightのアニメーション再生
//	/// </summary>
//	/// <param name="_currentRound"></param>
//	private void PlayFight(int _currentRound)
//	{
//		switch (_currentRound)
//		{
//			case 1:
//				if (roundStart_1.IsPlay == false)
//				{
//					roundStart_Fight.PlayAnimation();
//					action2 = IsPlayStartRound;
//					return;
//				}
//				break;

//			case 2:
//				if (roundStart_2.IsPlay == false)
//				{
//					roundStart_Fight.PlayAnimation();
//					action2 = IsPlayStartRound;
//					return;
//				}
//				break;

//			case 3:
//				if (roundStart_Final.IsPlay == false)
//				{
//					roundStart_Fight.PlayAnimation();
//					action2 = IsPlayStartRound;
//					return;
//				}
//				break;
//		}
//	}

//	/// <summary>
//	/// ラウンドスタート時のアニメーションの終了を検知する
//	/// </summary>
//	/// <param name="_notUsed"></param>
//	private void IsPlayStartRound(int _notUsed = 0)
//	{
//		IsPlay = roundStart_Fight.IsPlay;
//	}
//	#endregion


//	public void aaa()
//	{
//		curretAnimeObject.GetComponent<AnimationUIBase>().PlayAnimation();
//	}

//	public bool Call_PlayRoundFinish_KO()
//	{
//		return IsPlay;
//	}

//	private void PlayRoundFinish_KO()
//	{
//		roundFinish_KO.PlayAnimation();
//	}

//	private void IsPlayRoundFinish_KO()
//	{
//		IsPlay = roundFinish_KO.IsPlay;
//	}

//	public bool Call_PlayRoundFinish_TimeOver()
//	{
//		roundFinish_TimeOver.PlayAnimation();
//		return roundFinish_TimeOver.IsPlay;
//	}

//	public bool Call_PlayGameFinish_1PWin()
//	{
//		gameFinish_1PWin.PlayAnimation();
//		return gameFinish_1PWin.IsPlay;
//	}

//	public bool Call_PlayerGameFinish_2PWin()
//	{
//		gameFinish_2PWin.PlayAnimation();
//		return gameFinish_2PWin.IsPlay;
//	}

//	public bool Call_PlayerGameFinish_Draw()
//	{
//		gameFinish_Draw.PlayAnimation();
//		return gameFinish_Draw.IsPlay;
//	}

//	private void Start()
//	{
//		action_KO = PlayRoundStart;
//	}
//}
