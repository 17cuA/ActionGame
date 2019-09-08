using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CUEngine.Pattern;

public class FighterStateDown : StateBaseScriptMonoBehaviour
{
    private FighterStateBase stateBase;
    private float downCount = 0;
    private bool isWakeUp = false;//起き上がり
	private bool isWakeUpPassive = false;//地上受け身
    private void Start()
    {
        stateBase = GetComponent<FighterStateBase>();
    }
    public void DownStart()
    {
        stateBase.core.SetIsGround(false);
        downCount = 0;
        isWakeUp = false;
        stateBase.ChangeSkillConstant(SkillConstants.Down, 0);
    }
    public void UpdateDown()
    {
        downCount += 1.0f / 60.0f;
        if(downCount >= GameManager.Instance.Settings.DownFrame/60.0f)
        {
            isWakeUp = true;
        }
		if(downCount >= GameManager.Instance.Settings.WakeUpFrame/60.0f)
		{
			isWakeUpPassive = true;
		}
    }
    public bool IsWakeUp()
    {
        if(stateBase.core.HP <= 0)
        {
            return false;
        }
        return isWakeUp;
    }
	public bool IsWakeUpPassive()
	{
		if(isWakeUpPassive)
		{
			if (stateBase.input.atkButton != "")
			{
				if (stateBase.input.atkButton != CommonConstants.Buttons.Atk4)
				{
					stateBase.input.atkButton = "";
					return true;
				}
			}
		} 
		return false;
	}
    //起き上がり
    public void WakeUpStart()
    {
        downCount = 0;
        isWakeUp = false;
        isWakeUpPassive = false;
        stateBase.ChangeSkillConstant(SkillConstants.Wake_Up, 0);
    }
	//地上受け身
	public void WakeUpPassiveStart()
	{
		Direction dir = stateBase.input.GetPlayerMoveDirection(stateBase);
		if (dir == Direction.Back)
		{
			stateBase.ChangeSkillConstant(SkillConstants.Ground_Back_Passive, 0);
		}
		else if (dir == Direction.Front)
		{
			stateBase.ChangeSkillConstant(SkillConstants.Ground_Front_Passive, 0);
		}
		else
		{
			stateBase.ChangeSkillConstant(SkillConstants.Ground_Passive, 0);
		}

	}
	public void WakeUpUpdate()
    {
        //振り向き処理
        stateBase.core.DirectionChangeMaterial();
    }
    public void BoundStart()
    {
        FighterCore c = GameManager.Instance.GetPlayFighterCore(stateBase.core.EnemyNumber);
        c.BoundCount++;
        if (c.BoundCount >= GameManager.Instance.Settings.BoundCount)
        {
            c.isBound = true;
        }
        if(stateBase.nowDamage!=null)
        {
            if (stateBase.core.Mover.GetNowMove() != null)
            {
                stateBase.ChangeSkillCustomMoveConstant(SkillConstants.Damage_Fly_HitMotion, 0, stateBase.core.Mover.GetNowMove().boundMovements);
            }
        }
    }
    public bool IsEndWakeUp()
    {
        return stateBase.core.AnimationPlayerCompornent.EndAnimFrag;
    }
    //打ち付け
    public void GroundKnockStart()
    {
        stateBase.ChangeSkillConstant(SkillConstants.Ground_Knock, 0);
    }
	//再生終了に使用
    public bool IsEndGroundKnock()
    {
        return stateBase.core.AnimationPlayerCompornent.EndAnimFrag;
    }
    public bool IsBound()
    {
        //コンボ中にバウンドしてなければ
        if (!GameManager.Instance.GetPlayFighterCore(stateBase.core.EnemyNumber).isBound)
        {
            if (stateBase.core.Mover.GetNowMove() != null)
            {
                return stateBase.core.Mover.GetNowMove().isBound;
            }
        }
        return false;
    }
}
