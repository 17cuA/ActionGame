//---------------------------------------
// 生成したファイターにアニメーションをセットする
//---------------------------------------
// 作成者:高野
// 作成日:2019.11.14
//--------------------------------------
// 更新履歴
// 2019.11.14 作成
//--------------------------------------
// 仕様 
// FigthterCreaterクラスから生成したファイターの情報を参照してアニメーションをセットしている
//----------------------------------------
// MEMO 
//----------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSetter : MonoBehaviour
{
	[SerializeField] private FighterCreater fighterCreater;

	[SerializeField] private AnimationClip clico_Won;
	[SerializeField] private AnimationClip clico_Lost;
	[SerializeField] private AnimationClip clico_LostLoop;
	[SerializeField] private AnimationClip obachan_Won;
	[SerializeField] private AnimationClip obachan_Lost;
	[SerializeField] private AnimationClip obachan_LostLoop;

	public void OnePlayerWinAnimationSet()
	{
		//fighterCreater.Fighter1.GetComponent<Animationdata>().ResultAnimation(FighterClips[2], 0.5f, FighterClips[6]);
	}
	public void TwoPlayerWinAnimationSet()
	{

	}
}
