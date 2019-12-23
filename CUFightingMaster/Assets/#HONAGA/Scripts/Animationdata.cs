using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationData : MonoBehaviour
{
	// Inspectorからモデルについているアニメーションをセットする
	public NomalAnimationPlayer animationData;
	public float animationSpeed = 1.0f;		// アニメーションのスピード
	public AnimationClip defaltClip;			// 元々の
	public AnimationClip acceptClip;			//  

	public GameObject RotationObject;
	public GameObject ScaleObject;

	public bool animFrag;
	public bool resultFlag = false;
	public int PlayerNum;
	// Start is called before the first frame update
	void Start()
	{
        animationSpeed = 1.0f;
        if (animationData != null && resultFlag ==false)
		{
			animationData.SetPlayAnimation(defaltClip, animationSpeed, 0);
		}
	}

	// Update is called once per frame
	void Update()
	{
		if (animationData.NowClip != animationClip[0] && animFrag == false && resultFlag == false)
		{
			animationData.SetPlayAnimation(animationClip[0], animationSpeed, 0);
		}
	}
    public void ChangeAnimation()
    {
        if (animFrag == true && animationData.NowClip != acceptClip)
        {
            animationData.SetPlayAnimation(animationClip[1], animationSpeed, 0);
        }
    }
}
