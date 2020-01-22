using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NomalAnimationPlayer : AnimationPlayerBase
{
    // Update is called once per frame
    private AnimationClip idlingAnimation = null;
    private float idlingSpeed = 1;
	public void Update()
	{
		base.UpdateGame();
        if(idlingAnimation!=null)
        {
            if(EndAnimFrag)
            {
                SetPlayAnimation(idlingAnimation, 1, 0);
            }
        }
	}
    public void SetIdling(AnimationClip _idl,float _s)
    {
        idlingAnimation = _idl;
        idlingSpeed = _s;
    }
}
