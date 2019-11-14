//---------------------------------------
// GameDataStorageからアニメーションをとる
//---------------------------------------
// 作成者:高野
// 作成日:2019.11.14
//--------------------------------------
// 更新履歴
// 2019.11.14 作成
//--------------------------------------
// 仕様 
//----------------------------------------
// MEMO 
//----------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSetter : MonoBehaviour
{
	[SerializeField] private FighterCreater fighterCreater;

	public void OnePlayerWinAnimationSet()
	{
		//fighterCreater.FighterPlayer1.GetComponent<Animationdata>().ResultAnimation(FighterClips[2], 0.5f, FighterClips[6]);
	}
	public void TwoPlayerWinAnimationSet()
	{

	}
}
