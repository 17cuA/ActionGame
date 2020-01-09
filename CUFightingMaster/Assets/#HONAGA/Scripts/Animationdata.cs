using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationData : MonoBehaviour
{
	// Inspectorからモデルについているアニメーションをセットする
	public NomalAnimationPlayer animationData;
	public float animationSpeed;				// アニメーションのスピード
	public AnimationClip defaltClip;			// 元々のアニメーション
	public AnimationClip acceptClip;			//  決定した時のアニメーション

	public GameObject RotationObject;		// キャラを回転させる
	public GameObject ScaleObject;			// キャラのサイズを変更するためのオブジェクト

	public bool animFrag;
	// Start is called before the first frame update
	void Start()
	{
        animationSpeed = 1.0f;
        if (animationData != null)
		{
			animationData.SetPlayAnimation(defaltClip, animationSpeed);
		}
	}
	void ChangeAnimation(bool flag)
	{
		if(flag)	animationData.SetPlayAnimation(acceptClip, animationSpeed);
		else		animationData.SetPlayAnimation(defaltClip, animationSpeed);
	}
}
