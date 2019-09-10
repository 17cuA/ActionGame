using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animationdata : MonoBehaviour
{
    public NomalAnimationPlayer animationData;
    public float animationSpeed;
    public AnimationClip[] animationClip = new AnimationClip[2];

	public GameObject RotationObject;
	public GameObject ScaleObject;

    public bool animFrag;
    public int PlayerNum;
    // Start is called before the first frame update
    void Start()
    {
		if (animationData != null)
		{
			animationData.SetPlayAnimation(animationClip[0], animationSpeed, 0);
		}
    }

    // Update is called once per frame
    void Update()
    {
        if (animationData.NowClip != animationClip[0] && animFrag == false)
        {
            animationData.SetPlayAnimation(animationClip[0], animationSpeed, 0);
        }
        if (animFrag == true && animationData.NowClip != animationClip[1])
        {
            animationData.SetPlayAnimation(animationClip[1], animationSpeed, 0);
        }
		//if (animationData!=null)
		//{
		//	animationData.Update();
		//}
    }
}
