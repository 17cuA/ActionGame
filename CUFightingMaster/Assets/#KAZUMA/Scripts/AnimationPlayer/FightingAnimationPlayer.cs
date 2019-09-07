using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightingAnimationPlayer : AnimationPlayerBase
{
    [SerializeField]
    private FighterSkill nowPlaySkill = null;
    private FighterSkill beforeNowPlaySkill = null;
    public FighterSkill NowPlaySkill
    {
        get { return nowPlaySkill; }
    }
    private new void Start()
    {
        //インスペクタ上でアタッチされてればスキルセット
        if (nowPlaySkill != null)
        {
            if (nowPlaySkill.animationClip != null)
            {
                SetSkillAnimation(nowPlaySkill);
            }
        }
        base.Start();
    }
    public new void UpdateGame()
    {
        //技のセット
        if (nowPlaySkill != beforeNowPlaySkill)
        {
            SetSkillAnimation(nowPlaySkill);
        }
        base.UpdateGame();
    }
    /// <summary>
    /// 再生する技の変更
    /// </summary>
    /// <param name="skill"></param>
    /// <param name="speed"></param>
    /// <param name="weightFrame"></param>
    public void SetSkillAnimation(FighterSkill skill, int weightFrame = 0)
    {
        nowPlaySkill = skill;
        beforeNowPlaySkill = nowPlaySkill;
        if(nowPlaySkill == null)
        {
            return;
        }
        if (nowPlaySkill.animationClip != null)
        {
            SetPlayAnimation(skill.animationClip, skill.animationSpeed, weightFrame);
        }
    }
    public void CheckTimeStop(PlayerNumber _num)
    {
        if(nowPlaySkill==null)return;
        if ((nowPlaySkill.timeStops.startFrame <= (NowFrame + 1)) && nowPlaySkill.timeStops.endFrame >= (NowFrame + 1))
        {
            if(_num == PlayerNumber.Player1)
            {
                GameManager.Instance.isTimeStop_Two = true;
            }
            else if(_num == PlayerNumber.Player2)
            {
                GameManager.Instance.isTimeStop_One = true;
            }
        }
        else if(nowPlaySkill.timeStops.endFrame<(NowFrame+1))
        {
            if(_num == PlayerNumber.Player1)
            {
                GameManager.Instance.isTimeStop_Two = false;
            }
            else if(_num == PlayerNumber.Player2)
            {
                GameManager.Instance.isTimeStop_One = false;
            }
        }
    }
}
