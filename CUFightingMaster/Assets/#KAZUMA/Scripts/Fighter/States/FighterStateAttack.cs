using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CUEngine.Pattern;

public class FighterStateAttack : StateBaseScriptMonoBehaviour
{
    private FighterStateBase stateBase;
     private void Start()
    {
        stateBase = GetComponent<FighterStateBase>();
    }
    #region 地上
    public void AttackStart()
    {
        if((stateBase.input.groundMoveCommand.inputCommandName!="")&&(stateBase.input.groundMoveCommand.inputCommandName!=null))
        {
            //キーがあれば発動してreturn
            if (stateBase.groundSkills.ContainsKey(stateBase.input.groundMoveCommand.inputCommandName))
            {
                if (stateBase.core.SpecialGauge >= stateBase.groundSkills[stateBase.input.groundMoveCommand.inputCommandName].skillCost)
                {
                    var _move = stateBase.groundSkills[stateBase.input.groundMoveCommand.inputCommandName];
                    stateBase.core.SetSkill(_move.skill, 0);
                    stateBase.core.SpecialGauge -= _move.skillCost;
                    return;
                }
            }
        }
        string atk = stateBase.input.GetPlayerAtk();
        var _dir = stateBase.input.GetPlayerMoveDirection(stateBase);
        switch (atk)
        {
            case CommonConstants.Buttons.Atk1:
                if (_dir == Direction.Down || _dir == Direction.DownBack || _dir == Direction.DownFront)
                {
                    stateBase.ChangeSkillConstant(SkillConstants.Crouching_Light_Jab, 0);
                }
                else
                {
                    stateBase.ChangeSkillConstant(SkillConstants.Stand_Light_Jab, 0);

                }
                break;
            case CommonConstants.Buttons.Atk2:
                if (_dir == Direction.Down || _dir == Direction.DownBack || _dir == Direction.DownFront)
                {
                    stateBase.ChangeSkillConstant(SkillConstants.Crouching_Middle_Jab, 0);

                }
                else
                {
                    stateBase.ChangeSkillConstant(SkillConstants.Stand_Middle_Jab, 0);
                }
                break;
            case CommonConstants.Buttons.Atk3:
                if (_dir == Direction.Down || _dir == Direction.DownBack || _dir == Direction.DownFront)
                {
                    stateBase.ChangeSkillConstant(SkillConstants.Crouching_Strong_Jab, 0);
                }
                else
                {
                    stateBase.ChangeSkillConstant(SkillConstants.Stand_Strong_Jab, 0);
                }
                break;
            case CommonConstants.Buttons.Atk4:
                if (_dir != Direction.Down && _dir != Direction.DownBack || _dir != Direction.DownFront)
                {
                    stateBase.ChangeSkillConstant(SkillConstants.Throw_Atk, 0);
                }
                break;
            case CommonConstants.Buttons.Atk6:
                if (stateBase.core.SpecialGauge >= stateBase.core.Status.SpecialGuage)
                {
                    stateBase.core.SpecialGauge = 0;
                    stateBase.ChangeSkillConstant(SkillConstants.SpecialAttack, 0);
                }
                break;
        }
    }
    //攻撃中の処理
    public void AttackUpdate()
    {
        
    }
    //条件
    public bool IsEndAttack()
    {
        return stateBase.core.AnimationPlayerCompornent.EndAnimFrag;
    }
	//キャンセル技の発動条件
	public bool IsCancelAttack()
	{
		//攻撃がヒットした後
		if (stateBase.core.IsHitAttack)
		{
			FighterSkill _nowSkill = stateBase.core.NowPlaySkill;
			if (stateBase.core.GroundCheck())
			{
				#region コマンド技
				if ((stateBase.input.groundMoveCommand.inputCommandName != "") && (stateBase.input.groundMoveCommand.inputCommandName != null))
				{
					if (stateBase.groundSkills.ContainsKey(stateBase.input.groundMoveCommand.inputCommandName))
					{
                        var _move = stateBase.groundSkills[stateBase.input.groundMoveCommand.inputCommandName];
                        //キャンセルできるかどうか（技モード、AND演算）
                        if (stateBase.ChancelConditions(_nowSkill, _move.skill))
						{
							if(_move.skillCost<=stateBase.core.SpecialGauge)
							{
                                return true;
                            }
						}
					}
				}
				#endregion

				#region 通常技
				string atk = stateBase.input.GetPlayerAtk();
                var _dir = stateBase.input.GetPlayerMoveDirection(stateBase);
                switch (atk)
                {
                    case CommonConstants.Buttons.Atk1:
                        if (_dir == Direction.Down || _dir == Direction.DownBack || _dir == Direction.DownFront)
                        {
                            if (stateBase.ChancelConditions(_nowSkill, stateBase.core.Status.constantsSkills[(int)SkillConstants.Crouching_Light_Jab])) return true;
                        }
                        else
                        {
                            if (stateBase.ChancelConditions(_nowSkill, stateBase.core.Status.constantsSkills[(int)SkillConstants.Stand_Light_Jab])) return true;
                        }
                        break;
                    case CommonConstants.Buttons.Atk2:
                        if (_dir == Direction.Down || _dir == Direction.DownBack || _dir == Direction.DownFront)
                        {
                            if (stateBase.ChancelConditions(_nowSkill, stateBase.core.Status.constantsSkills[(int)SkillConstants.Crouching_Middle_Jab])) return true;
                        }
                        else
                        {
                            if (stateBase.ChancelConditions(_nowSkill, stateBase.core.Status.constantsSkills[(int)SkillConstants.Stand_Middle_Jab])) return true;
                        }
                        break;
                    case CommonConstants.Buttons.Atk3:
                        if (_dir == Direction.Down || _dir == Direction.DownBack || _dir == Direction.DownFront)
                        {
                            if (stateBase.ChancelConditions(_nowSkill, stateBase.core.Status.constantsSkills[(int)SkillConstants.Crouching_Strong_Jab])) return true;
                        }
                        else
                        {
                            if (stateBase.ChancelConditions(_nowSkill, stateBase.core.Status.constantsSkills[(int)SkillConstants.Stand_Strong_Jab])) return true;
                        }
                        break;
					case CommonConstants.Buttons.Atk6:
                        if (stateBase.core.SpecialGauge >= stateBase.core.Status.SpecialGuage)
                        {
                            if (stateBase.ChancelConditions(_nowSkill, stateBase.core.Status.constantsSkills[(int)SkillConstants.SpecialAttack])) return true;
                        }

                        break;
                    case CommonConstants.Buttons.Atk4:
                        break;
                }
                #endregion
            }
			else
			{
				if ((stateBase.input.airMoveCommand.inputCommandName != "") && (stateBase.input.airMoveCommand.inputCommandName != null))
				{
					if (stateBase.airSkills.ContainsKey(stateBase.input.airMoveCommand.inputCommandName))
					{
                        var _move = stateBase.airSkills[stateBase.input.airMoveCommand.inputCommandName];
                        //キャンセルできるかどうか（技モード、AND演算）
                        if (stateBase.ChancelConditions(_nowSkill, _move.skill))
						{
                            if (stateBase.core.SpecialGauge >= _move.skillCost)
                            {
								if(stateBase.airCountValids.ContainsKey(stateBase.input.airMoveCommand.inputCommandName))
								{
                                    FighterStateBase.CountValidsSkill sk = stateBase.airCountValids[stateBase.input.airMoveCommand.inputCommandName];
                                    if(sk.count<sk.maxCount||sk.maxCount == 0)
									{
                                        return true;
                                    }
								}
                            }
                        }
					}
				}
				string atk = stateBase.input.GetPlayerAtk();
				if (stateBase.core.PlayerMoveStates == PlayerMoveState.Jump)
				{
					switch (atk)
					{
						case CommonConstants.Buttons.Atk1:
							if (stateBase.ChancelConditions(_nowSkill, stateBase.core.Status.constantsSkills[(int)SkillConstants.Air_Light_Jab])) return true;
							break;
						case CommonConstants.Buttons.Atk2:
							if (stateBase.ChancelConditions(_nowSkill, stateBase.core.Status.constantsSkills[(int)SkillConstants.Air_Middle_Jab])) return true;
							break;
						case CommonConstants.Buttons.Atk3:
							if (stateBase.ChancelConditions(_nowSkill, stateBase.core.Status.constantsSkills[(int)SkillConstants.Air_Strong_Jab])) return true;
							break;
						case CommonConstants.Buttons.Atk4:
							break;
						default:
                            break;
                    }
				}
				else if (stateBase.core.PlayerMoveStates == PlayerMoveState.Back_Jump)
				{
                    switch (atk)
                    {
                        case CommonConstants.Buttons.Atk1:
                            if (stateBase.ChancelConditions(_nowSkill, stateBase.core.Status.constantsSkills[(int)SkillConstants.Air_Back_Light_Jab])) return true;
                            break;
                        case CommonConstants.Buttons.Atk2:
                            if (stateBase.ChancelConditions(_nowSkill, stateBase.core.Status.constantsSkills[(int)SkillConstants.Air_Back_Middle_Jab])) return true;
                            break;
                        case CommonConstants.Buttons.Atk3:
                            if (stateBase.ChancelConditions(_nowSkill, stateBase.core.Status.constantsSkills[(int)SkillConstants.Air_Back_Strong_Jab])) return true;
                            break;
                        case CommonConstants.Buttons.Atk4:
                            break;
                        default:
                            break;

                    }
                }
				else if (stateBase.core.PlayerMoveStates == PlayerMoveState.Front_Jump)
				{
					switch (atk)
					{
						case CommonConstants.Buttons.Atk1:
							if (stateBase.ChancelConditions(_nowSkill, stateBase.core.Status.constantsSkills[(int)SkillConstants.Air_Front_Light_Jab])) return true;
							break;
						case CommonConstants.Buttons.Atk2:
							if (stateBase.ChancelConditions(_nowSkill, stateBase.core.Status.constantsSkills[(int)SkillConstants.Air_Front_Middle_Jab])) return true;
							break;
						case CommonConstants.Buttons.Atk3:
							if (stateBase.ChancelConditions(_nowSkill, stateBase.core.Status.constantsSkills[(int)SkillConstants.Air_Front_Strong_Jab])) return true;
							break;
						case CommonConstants.Buttons.Atk4:
							break;
                        default:
                            break;

					}
				}
				else
				{
                    switch (atk)
                    {
                        case CommonConstants.Buttons.Atk1:
                            if (stateBase.ChancelConditions(_nowSkill, stateBase.core.Status.constantsSkills[(int)SkillConstants.Air_Light_Jab])) return true;
                            break;
                        case CommonConstants.Buttons.Atk2:
                            if (stateBase.ChancelConditions(_nowSkill, stateBase.core.Status.constantsSkills[(int)SkillConstants.Air_Middle_Jab])) return true;
                            break;
                        case CommonConstants.Buttons.Atk3:
                            if (stateBase.ChancelConditions(_nowSkill, stateBase.core.Status.constantsSkills[(int)SkillConstants.Air_Strong_Jab])) return true;
                            break;
                        case CommonConstants.Buttons.Atk4:
                            break;
                        default:
                            break;

                    }
                }
			}
		}
		return false;

	}
	public bool IsAirChancelAttack()
	{
		//攻撃がヒットした後
		if (stateBase.core.IsHitAttack)
		{
			FighterSkill _nowSkill = stateBase.core.NowPlaySkill;
			if (stateBase.core.GroundCheck())
			{
				#region コマンド技
				if ((stateBase.input.groundMoveCommand.inputCommandName != "") && (stateBase.input.groundMoveCommand.inputCommandName != null))
				{
                    var _move = stateBase.groundSkills[stateBase.input.groundMoveCommand.inputCommandName];
                    if (stateBase.groundSkills.ContainsKey(stateBase.input.groundMoveCommand.inputCommandName))
					{
						//キャンセルできるかどうか（技モード、AND演算）
						if (stateBase.ChancelConditions(_nowSkill, _move.skill))
						{
                            if (stateBase.core.SpecialGauge >= _move.skillCost)
                            {
                                return true;
                            }
                        }
					}
				}
				#endregion

				#region 通常技
				string atk = stateBase.input.GetPlayerAtk();
                switch (atk)
                {
                    case CommonConstants.Buttons.Atk1:
                        if (stateBase.core.PlayerMoveStates == PlayerMoveState.Crouching)
                        {
                            if (stateBase.ChancelConditions(_nowSkill, stateBase.core.Status.constantsSkills[(int)SkillConstants.Crouching_Light_Jab])) return true;
                        }
                        else
                        {
                            if (stateBase.ChancelConditions(_nowSkill, stateBase.core.Status.constantsSkills[(int)SkillConstants.Stand_Light_Jab])) return true;
                        }
                        break;
                    case CommonConstants.Buttons.Atk2:
                        if (stateBase.core.PlayerMoveStates == PlayerMoveState.Crouching)
                        {
                            if (stateBase.ChancelConditions(_nowSkill, stateBase.core.Status.constantsSkills[(int)SkillConstants.Crouching_Middle_Jab])) return true;
                        }
                        else
                        {
                            if (stateBase.ChancelConditions(_nowSkill, stateBase.core.Status.constantsSkills[(int)SkillConstants.Stand_Middle_Jab])) return true;
                        }
                        break;
                    case CommonConstants.Buttons.Atk3:
                        if (stateBase.core.PlayerMoveStates == PlayerMoveState.Crouching)
                        {
                            if (stateBase.ChancelConditions(_nowSkill, stateBase.core.Status.constantsSkills[(int)SkillConstants.Crouching_Strong_Jab])) return true;
                        }
                        else
                        {
                            if (stateBase.ChancelConditions(_nowSkill, stateBase.core.Status.constantsSkills[(int)SkillConstants.Stand_Strong_Jab])) return true;
                        }
                        break;
                    case CommonConstants.Buttons.Atk6:
                        if (stateBase.core.SpecialGauge >= stateBase.core.Status.SpecialGuage)
                        {
                            if (stateBase.ChancelConditions(_nowSkill, stateBase.core.Status.constantsSkills[(int)SkillConstants.SpecialAttack])) return true;
                        }
                        break;
                    case CommonConstants.Buttons.Atk4:
                        break;
                }
                #endregion
            }
			else
			{
				if ((stateBase.input.airMoveCommand.inputCommandName != "") && (stateBase.input.airMoveCommand.inputCommandName != null))
				{
					if (stateBase.airSkills.ContainsKey(stateBase.input.airMoveCommand.inputCommandName))
					{
                        var _move = stateBase.airSkills[stateBase.input.airMoveCommand.inputCommandName];
                        //キャンセルできるかどうか（技モード、AND演算）
                        if (stateBase.ChancelConditions(_nowSkill, _move.skill))
						{
                            if (stateBase.core.SpecialGauge >= _move.skillCost)
                            {
                                if (stateBase.airCountValids.ContainsKey(stateBase.input.airMoveCommand.inputCommandName))
                                {
                                    FighterStateBase.CountValidsSkill sk = stateBase.airCountValids[stateBase.input.airMoveCommand.inputCommandName];
                                    if(sk.count<sk.maxCount||sk.maxCount == 0)
                                    {
                                        return true;
                                    }
                                }
                            }
						}
					}
				}
				string atk = stateBase.input.GetPlayerAtk();
				if (stateBase.core.PlayerMoveStates == PlayerMoveState.Jump)
				{
					switch (atk)
					{
						case CommonConstants.Buttons.Atk1:
							if (stateBase.ChancelConditions(_nowSkill, stateBase.core.Status.constantsSkills[(int)SkillConstants.Air_Light_Jab])) return true;
							break;
						case CommonConstants.Buttons.Atk2:
							if (stateBase.ChancelConditions(_nowSkill, stateBase.core.Status.constantsSkills[(int)SkillConstants.Air_Middle_Jab])) return true;
							break;
						case CommonConstants.Buttons.Atk3:
							if (stateBase.ChancelConditions(_nowSkill, stateBase.core.Status.constantsSkills[(int)SkillConstants.Air_Strong_Jab])) return true;
							break;
						case CommonConstants.Buttons.Atk4:
							break;

					}
				}
				else if (stateBase.core.PlayerMoveStates == PlayerMoveState.Back_Jump)
				{
					switch (atk)
					{
						case CommonConstants.Buttons.Atk1:
							if (stateBase.ChancelConditions(_nowSkill, stateBase.core.Status.constantsSkills[(int)SkillConstants.Air_Back_Light_Jab])) return true;
							break;
						case CommonConstants.Buttons.Atk2:
							if (stateBase.ChancelConditions(_nowSkill, stateBase.core.Status.constantsSkills[(int)SkillConstants.Air_Back_Middle_Jab])) return true;
							break;
						case CommonConstants.Buttons.Atk3:
							if (stateBase.ChancelConditions(_nowSkill, stateBase.core.Status.constantsSkills[(int)SkillConstants.Air_Back_Strong_Jab])) return true;
							break;
						case CommonConstants.Buttons.Atk4:
							break;

					}
				}
				else if (stateBase.core.PlayerMoveStates == PlayerMoveState.Front_Jump)
				{
					switch (atk)
					{
						case CommonConstants.Buttons.Atk1:
							if (stateBase.ChancelConditions(_nowSkill, stateBase.core.Status.constantsSkills[(int)SkillConstants.Air_Front_Light_Jab])) return true;
							break;
						case CommonConstants.Buttons.Atk2:
							if (stateBase.ChancelConditions(_nowSkill, stateBase.core.Status.constantsSkills[(int)SkillConstants.Air_Front_Middle_Jab])) return true;
							break;
						case CommonConstants.Buttons.Atk3:
							if (stateBase.ChancelConditions(_nowSkill, stateBase.core.Status.constantsSkills[(int)SkillConstants.Air_Front_Strong_Jab])) return true;
							break;
						case CommonConstants.Buttons.Atk4:
							break;

					}
				}
				else
				{
					switch (atk)
					{
						case CommonConstants.Buttons.Atk1:
							if (stateBase.ChancelConditions(_nowSkill, stateBase.core.Status.constantsSkills[(int)SkillConstants.Air_Light_Jab])) return true;
							break;
						case CommonConstants.Buttons.Atk2:
							if (stateBase.ChancelConditions(_nowSkill, stateBase.core.Status.constantsSkills[(int)SkillConstants.Air_Middle_Jab])) return true;
							break;
						case CommonConstants.Buttons.Atk3:
							if (stateBase.ChancelConditions(_nowSkill, stateBase.core.Status.constantsSkills[(int)SkillConstants.Air_Strong_Jab])) return true;
							break;
						case CommonConstants.Buttons.Atk4:
							break;
					}
				}
			}
		}
		return false;
	}
	#endregion

	#region  空中
	public void AirAttackStart()
    {
        if ((stateBase.input.airMoveCommand.inputCommandName != "") && (stateBase.input.airMoveCommand.inputCommandName != null))
        {
            //キーがあれば発動してreturn
            if (stateBase.airSkills.ContainsKey(stateBase.input.airMoveCommand.inputCommandName))
            {
                if (stateBase.core.SpecialGauge >= stateBase.airSkills[stateBase.input.airMoveCommand.inputCommandName].skillCost)
                {
					//空中で発動できる回数
					if(stateBase.airCountValids.ContainsKey(stateBase.input.airMoveCommand.inputCommandName))
					{
                        FighterStateBase.CountValidsSkill sk = stateBase.airCountValids[stateBase.input.airMoveCommand.inputCommandName];
                        if (sk.count < sk.maxCount || sk.maxCount == 0)
                        {
                            sk.count++;
                            stateBase.countAirName.Add(stateBase.input.airMoveCommand.inputCommandName);
                            var _move = stateBase.airSkills[stateBase.input.airMoveCommand.inputCommandName];
                            stateBase.core.SetSkill(_move.skill, 0);
                            stateBase.core.SpecialGauge -= _move.skillCost;
                            return;
                        }
                    }
                }
            }
        }
        string atk = stateBase.input.GetPlayerAtk();
        if (stateBase.core.PlayerMoveStates == PlayerMoveState.Jump)
        {
            switch (atk)
            {
            case CommonConstants.Buttons.Atk1:
                    stateBase.ChangeSkillConstant(SkillConstants.Air_Light_Jab, 0);
                    break;
            case CommonConstants.Buttons.Atk2:
                    stateBase.ChangeSkillConstant(SkillConstants.Air_Middle_Jab, 0);
                    break;
            case CommonConstants.Buttons.Atk3:
                    stateBase.ChangeSkillConstant(SkillConstants.Air_Strong_Jab, 0);
                    break;
            }
        }
        else if (stateBase.core.PlayerMoveStates == PlayerMoveState.Back_Jump)
        {
            switch (atk)
            {
            case CommonConstants.Buttons.Atk1:
                    stateBase.ChangeSkillConstant(SkillConstants.Air_Back_Light_Jab, 0);
                    break;
            case CommonConstants.Buttons.Atk2:
                    stateBase.ChangeSkillConstant(SkillConstants.Air_Back_Middle_Jab, 0);
                    break;
            case CommonConstants.Buttons.Atk3:
                    stateBase.ChangeSkillConstant(SkillConstants.Air_Back_Strong_Jab, 0);
                    break;
            }
        }
        else if (stateBase.core.PlayerMoveStates == PlayerMoveState.Front_Jump)
        {
            switch (atk)
            {
            case CommonConstants.Buttons.Atk1:
                    stateBase.ChangeSkillConstant(SkillConstants.Air_Front_Light_Jab, 0);
                    break;
            case CommonConstants.Buttons.Atk2:
                    stateBase.ChangeSkillConstant(SkillConstants.Air_Front_Middle_Jab, 0);
                    break;
            case CommonConstants.Buttons.Atk3:
                    stateBase.ChangeSkillConstant(SkillConstants.Air_Front_Strong_Jab, 0);
                    break;
            }
        }
        else
        {
            switch (atk)
            {
            case CommonConstants.Buttons.Atk1:
                    stateBase.ChangeSkillConstant(SkillConstants.Air_Light_Jab, 0);
                    break;
            case CommonConstants.Buttons.Atk2:
                    stateBase.ChangeSkillConstant(SkillConstants.Air_Middle_Jab, 0);
                    break;
            case CommonConstants.Buttons.Atk3:
                    stateBase.ChangeSkillConstant(SkillConstants.Air_Strong_Jab, 0);
                    break;
            }
        }
    }
    public bool IsEndAirAttack()
    {
        if (stateBase.core.NowPlaySkill.isGroundEnd)
        {
            return false;
        }
        return stateBase.core.AnimationPlayerCompornent.EndAnimFrag;
    }
    #endregion
	public void NextSkillStart()
	{
        stateBase.core.SetSkill(stateBase.core.NowPlaySkill.endNextAnimation, stateBase.core.NowPlaySkill.endNextWeight);
    }
	//終了時スキル
	public bool IsEndNextSkill()
	{
        return (stateBase.core.NowPlaySkill.endNextAnimation != null);
    }
	public void GroundLandingSkillStart()
	{
        stateBase.core.SetSkill(stateBase.core.NowPlaySkill.groundLandingSkill, stateBase.core.NowPlaySkill.groundLandingWeight);
    }
	//着地時スキル
	public bool IsGroundLandingSkill()
	{
        return (stateBase.core.NowPlaySkill.groundLandingSkill != null);
    }
}
