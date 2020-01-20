////---------------------------------------
//// 生成したファイターにアニメーションをセットする
////---------------------------------------
//// 作成者:高野
//// 作成日:2019.11.14
////--------------------------------------
//// 更新履歴
//// 2019.11.14 作成
////--------------------------------------
//// 仕様 
//// FigthterCreaterクラスから生成したファイターの情報を参照してアニメーションをセットしている
////----------------------------------------
//// MEMO 
////----------------------------------------
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class AnimationSetter : MonoBehaviour
//{
//	[SerializeField] private FighterCreater fighterCreater;

//	[SerializeField] private AnimationClip clico_Won;
//	[SerializeField] private AnimationClip clico_WonLoop;
//	[SerializeField] private AnimationClip clico_Losing;
//	[SerializeField] private AnimationClip clico_LosingLoop;

//	[SerializeField] private AnimationClip obachan_Won;
//	[SerializeField] private AnimationClip obachan_WonLoop;
//	[SerializeField] private AnimationClip obachan_Losing;
//	[SerializeField] private AnimationClip obachan_LosingLoop;

//	/// <summary>
//	/// クリコが勝った時のアニメーションをセット
//	/// </summary>
//	/// <param name="_fighter">ファイターオブジェクト</param>
//	public void ClicoWonAnimationSet(GameObject _fighter)
//	{
//		_fighter.GetComponent<Animationdata>().ResultAnimation(clico_Won, 0.5f, clico_WonLoop);
//		_fighter.GetComponent<Animationdata>().resultFlag = true;
//	}

//	/// <summary>
//	/// クリコが負けたときのアニメーションをセット
//	/// </summary>
//	/// <param name="_fighter">ファイターオブジェクト</param>
//	//public void ClicoLosingAnimationSet(GameObject _fighter)
//	//[SerializeField] private AnimationClip animationClip;

//	//[SerializeField] private AnimationClip clico_Won;
//	//[SerializeField] private AnimationClip clico_Lost;
//	//[SerializeField] private AnimationClip clico_LostLoop;
//	//[SerializeField] private AnimationClip obachan_Won;
//	//[SerializeField] private AnimationClip obachan_Lost;
//	//[SerializeField] private AnimationClip obachan_LostLoop;

//	//public void ClicoWonAnimationSet( GameObject _fighter)
//	//{
//	//	//_fighter.GetComponent<Animationdata>().ResultAnimation()
//	//}

//	//private void SetAnimation()
//	//{
//	//	_fighter.GetComponent<Animationdata>().ResultAnimation(clico_Losing, 0.5f, clico_LosingLoop);
//	//	_fighter.GetComponent<Animationdata>().resultFlag = true;
//	//}

//	/// <summary>
//	/// おばちゃんが勝った時のアニメーションをセット
//	/// </summary>
//	/// <param name="_fighter">ファイターオブジェクト</param>

//	public void ObachanWonAnimationSet(GameObject _fighter)
//	{
//		_fighter.GetComponent<Animationdata>().ResultAnimation(obachan_Won, 0.5f, obachan_WonLoop);
//		_fighter.GetComponent<Animationdata>().resultFlag = true;
//	}

//	/// <summary>
//	/// おばちゃんが負けた時のアニメーションをセット	
//	/// </summary>
//	/// <param name="_fighter">ファイターオブジェクト</param>
//	public void ObachanLosingAnimationSet(GameObject _fighter)
//	{
//		_fighter.GetComponent<Animationdata>().ResultAnimation(obachan_Losing, 0.5f, obachan_LosingLoop);
//		_fighter.GetComponent<Animationdata>().resultFlag = true;
//	}
//}
