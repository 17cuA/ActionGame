//---------------------------------------------------------------
// TimeLineのアニメーショントラックの操作
//---------------------------------------------------------------
// 作成者:高野
// 作成日:2019.12.02
//----------------------------------------------------
// 更新履歴
//-----------------------------------------------------
// 仕様
// インスペクタでplayableDirectorを参照してください
// PlayOnAwake = true
//-----------------------------------------------------
// MEMO
//
// 
//-------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using System.Linq;

public class AnimaitonBindController : MonoBehaviour
{
	[SerializeField] PlayableDirector playableDirector;
	[SerializeField] Animator animator;
	[SerializeField] string trackName = "Animation Track";

	public AnimationClip AnimationClip { set; private get; }
	public PlayableBinding playableBinding;

	private void Start()
	{
		playableBinding = playableDirector.playableAsset.outputs.First(c => c.streamName == trackName);
		if (playableDirector.playOnAwake == true)
			BindAnimation();
	}

	public void BindAnimation()
	{
		playableDirector.SetGenericBinding(playableBinding.sourceObject, AnimationClip);
	}

	public void PlayAnimation()
	{
		playableDirector.Play();
	}
}
