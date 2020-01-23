using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationData : MonoBehaviour
{
	// Inspectorからモデルについているアニメーションをセットする
	public NomalAnimationPlayer animationData;
	public float animationSpeed = 1.0f;                // アニメーションのスピード
	public AnimationClip defaltClip;             // 元々のアニメーション
	public AnimationClip acceptClip;            // 決定した時のアニメーション

	public GameObject RotationObject;       // キャラを回転させる
	public GameObject ScaleObject;          // キャラのサイズを変更するためのオブジェクト

	[SerializeField]
	private bool animFlag;
	public bool AnimFlag { get; set; }
	public void Start()
	{
		AnimFlag = false;
		animationSpeed = 1.0f;
		if (animationData != null)
		{
			animationData.SetPlayAnimation(defaltClip, 1.0f);
		}
	}
	public void CustomUpdate()
	{
		if (AnimFlag)
		{
			// 今再生しているアニメーションが勝利ポーズじゃなかったら
			if (animationData.NowClip != acceptClip)
			{
				animationData.SetPlayAnimation(acceptClip, animationSpeed);
				AnimFlag = false;
			}
			// 勝利ポーズ再生中に決定を取り消されたとき、defaultポーズに戻る
			if (animationData.NowClip != defaltClip)
			{
				animationData.SetPlayAnimation(defaltClip, animationSpeed);
				AnimFlag = false;
			}
		}
		animationData.Update();
	}
}