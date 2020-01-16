using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationData : MonoBehaviour
{
	// Inspectorからモデルについているアニメーションをセットする
	public NomalAnimationPlayer animationData;
	public float animationSpeed = 1.0f;                // アニメーションのスピード
	public AnimationClip defaltClip;            // 元々のアニメーション
	public AnimationClip acceptClip;            //  決定した時のアニメーション

	public GameObject RotationObject;       // キャラを回転させる
	public GameObject ScaleObject;          // キャラのサイズを変更するためのオブジェクト

	[SerializeField]
	private bool animFlag;
	public bool AnimFlag { get; set; }
	public void Start()
	{
		animFlag = false;
		animationSpeed = 1.0f;
		if (animationData != null)
		{
			animationData.SetPlayAnimation(defaltClip, animationSpeed);
		}
	}
	public void Update()
	{
		if(Input.GetKeyDown(KeyCode.Space))
		{
			animFlag = true;
		}
		if (animFlag == true)
		{
			// 今再生しているアニメーションが勝利ポーズじゃなかったら
			if (animationData.NowClip != acceptClip)
			{
				animationData.SetPlayAnimation(acceptClip, animationSpeed);
			}
			// 勝利ポーズ再生中に決定を取り消されたとき、defaultポーズに戻る
			else if (animationData.NowClip != defaltClip)
			{
				animationData.SetPlayAnimation(defaltClip, animationSpeed);
			}
			animFlag = false;
		}
	}
}