using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.SerializableAttribute]
public class ResultAnimationList
{
	public List<AnimationClip> animeList = new List<AnimationClip>();

	public ResultAnimationList(List<AnimationClip> animationClips)
	{
		animeList = animationClips;
	}
}
