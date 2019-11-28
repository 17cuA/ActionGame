using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultAnimationPlayer : MonoBehaviour
{
	[SerializeField] private float animeSpeed;

	[SerializeField] private NomalAnimationPlayer animationPlayer;
	[SerializeField] private FighterCreater fighterCreater;
	[SerializeField] private AnimationClip animationClip;

	/// <summary>
	/// アニメーションを再生
	/// </summary>
	/// <param name="_animationClip"></param>
	public void PlayAnimatmion()
	{
		animationPlayer.SetPlayAnimation( animationClip, animeSpeed);
	}

	/// <summary>
	/// アニメーションセット
	/// </summary>
	/// <param name="_animationClip"></param>
	public void SetAnimetion(AnimationClip _animationClip)
	{
		animationClip = _animationClip;
	}

}
