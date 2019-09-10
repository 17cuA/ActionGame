using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogoAnimation : MonoBehaviour
{
    public NomalAnimationPlayer[] logoAnimation;
    public AnimationClip[] animationClip;
    public GameObject[] pressAnyKey;

    public void InitLogoAnimation()
    {
        //pressAnyKey[0].SetActive(false);
        //pressAnyKey[1].SetActive(false);
        logoAnimation[0].SetPlayAnimation(animationClip[0], 0.8f, 0);
        logoAnimation[1].SetPlayAnimation(animationClip[2], 0.8f, 0);
    }

    public void PlayLogoAnimation()
    {
        if (Input.anyKeyDown && logoAnimation[0].NowClip == animationClip[0])
        {
            logoAnimation[0].AnimationSpeed = 100.0f;
        }
        if (Input.anyKeyDown && logoAnimation[1].NowClip == animationClip[2])
        {
            logoAnimation[1].AnimationSpeed = 100.0f;
        }
        if (logoAnimation[0].EndAnimFrag == true)
        {
            logoAnimation[0].SetPlayAnimation(animationClip[1], 1.0f, 0);
            //pressAnyKey[0].SetActive(true);
        }
        if (logoAnimation[1].EndAnimFrag == true)
        {
            logoAnimation[1].SetPlayAnimation(animationClip[3], 1.0f, 0);
            //pressAnyKey[1].SetActive(true);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //pressAnyKey[0].SetActive(false);
        //pressAnyKey[1].SetActive(false);
        logoAnimation[0].SetPlayAnimation(animationClip[0], 1.0f, 0);
        logoAnimation[1].SetPlayAnimation(animationClip[2], 1.0f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown && logoAnimation[0].NowClip == animationClip[0])
        {
            logoAnimation[0].AnimationSpeed = 100.0f;
        }
        if (Input.anyKeyDown && logoAnimation[1].NowClip == animationClip[2])
        {
            logoAnimation[1].AnimationSpeed = 100.0f;
        }
        if (logoAnimation[0].EndAnimFrag == true)
        {
            logoAnimation[0].SetPlayAnimation(animationClip[1], 1.0f, 0);
            pressAnyKey[0].SetActive(true);
        }
        if (logoAnimation[1].EndAnimFrag == true)
        {
            logoAnimation[1].SetPlayAnimation(animationClip[3], 1.0f, 0);
            pressAnyKey[1].SetActive(true);
        }
    }
}
