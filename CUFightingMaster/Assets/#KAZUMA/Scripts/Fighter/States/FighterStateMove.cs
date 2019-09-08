//地上移動に関する挙動
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CUEngine.Pattern;

public class FighterStateMove : StateBaseScriptMonoBehaviour
{
    private FighterStateBase stateBase;
    private Direction beforeInput = Direction.Neutral;

    private int jumpTimes = 0;
	private bool isJump = false;

    private int jumpTimesMax = 1;
    #region 初期化
    private void Start()
    {
        stateBase = GetComponent<FighterStateBase>();
    }
    #endregion

    /* ステートに入った時 */
    public void MoveStart()
    {
        //空中発動のカウントを0に戻す
        foreach(var c in stateBase.countAirName)
        {
            stateBase.airCountValids[c].count = 0;
        }
		isJump = false;
        stateBase.core.DirectionChangeMaterial();
        beforeInput = stateBase.input.GetPlayerMoveDirection(stateBase);
        ChangeMove(beforeInput);
    }
    /* ステート中 */
    public void MoveUpdate()
    {
        stateBase.core.DirectionChangeMaterial();
        Direction inp = stateBase.input.GetPlayerMoveDirection(stateBase);
        if (inp == beforeInput) return;
        ChangeMove(inp);
        beforeInput = inp;
    }
    //空中初期化
    public void GroundInit()
    {
        jumpTimes = 0;
    }
    public void AirMoveStart()
    {
        stateBase.core.DirectionChangeMaterial();
        stateBase.ChangeSkillConstant(SkillConstants.Air_Idle, 5);
        stateBase.core.SetPlayerMoveState(PlayerMoveState.Jump);
    }
    public void AirUpdateMove()
    {
        Direction dir = stateBase.input.GetPlayerMoveDirection(stateBase);
        if (dir == Direction.Back || dir == Direction.DownBack || dir == Direction.UpBack)
        {
            stateBase.core.Mover.SetAirXMove(-stateBase.core.Status.airBraking * 0.1f);
        }
        else if (dir == Direction.Front || dir == Direction.UpFront || dir == Direction.DownFront)
        {
            stateBase.core.Mover.SetAirXMove(stateBase.core.Status.airBraking * 0.1f);
        }
        //空中ジャンプ
        if (Direction.Up != beforeInput && Direction.UpFront != beforeInput && Direction.UpBack != beforeInput && jumpTimes < jumpTimesMax)
        {
            JumpChange();
        }
        Direction inp = stateBase.input.GetPlayerMoveDirection(stateBase);
        beforeInput = inp;
    }
    /* 条件式 */
    public bool Input_Atk_True()
    {
        if (stateBase.core.GroundCheck())
        {
            if ((stateBase.input.groundMoveCommand.inputCommandName != "") && (stateBase.input.groundMoveCommand.inputCommandName != null))
            {
                //キーがあれば発動してreturn
                if (stateBase.groundSkills.ContainsKey(stateBase.input.groundMoveCommand.inputCommandName))
                {
                    if (stateBase.core.SpecialGauge >= stateBase.groundSkills[stateBase.input.groundMoveCommand.inputCommandName].skillCost)
                    {
                        return true;
                    }
                }
            }
        }
        else
        {
            if ((stateBase.input.airMoveCommand.inputCommandName != "") && (stateBase.input.airMoveCommand.inputCommandName != null))
            {
                //キーがあれば発動してreturn
                if (stateBase.airSkills.ContainsKey(stateBase.input.airMoveCommand.inputCommandName))
                {
                    if (stateBase.core.SpecialGauge >= stateBase.airSkills[stateBase.input.airMoveCommand.inputCommandName].skillCost)
                    {
                        if (stateBase.airCountValids.ContainsKey(stateBase.input.airMoveCommand.inputCommandName))
                        {
                            FighterStateBase.CountValidsSkill sk = stateBase.airCountValids[stateBase.input.airMoveCommand.inputCommandName];
                            if (sk.count < sk.maxCount || sk.maxCount == 0)
                            {
                                return true;
                            }
                        }
                    }
                }
            }
        }
        //攻撃を行わない特殊な場合
        //しゃがみ時の投げ
        if ((stateBase.input.GetPlayerAtk() == CommonConstants.Buttons.Atk4))
        {
            if (stateBase.core.PlayerMoveStates == PlayerMoveState.Crouching)
            {
                return false;
            }
            if (stateBase.core.GroundCheck() == false)
            {
                return false;
            }
        }
        return stateBase.input.GetPlayerAtk() != null;
    }
    //着地
    public void LandingStart()
    {
        stateBase.core.DirectionChangeMaterial();
        stateBase.ChangeSkillConstant(SkillConstants.Landing, 0);
    }
    public bool IsEndLanding()
    {
        if (stateBase.core.AnimationPlayerCompornent.NowFrame > 3)
        {
            return true;
        }
        return false;
    }
	public bool IsJump()
	{
		return isJump;
	}
    //ジャンプキャンセル
    public bool IsJumpCancel(bool _isCount)
    {
		if (stateBase.core.HitAattackBox == null) return false;
        if (stateBase.core.HitAattackBox.isJumpCancel)
        {
            if (jumpTimes < jumpTimesMax&&stateBase.core.IsHitAttack)
            {
                return JumpChange(_isCount);
            }
        }
        return false;
    }
    #region 取得系
    private void ChangeMove(Direction _dir)
    {

        switch (_dir)
        {
            case Direction.Neutral:
                if (stateBase.core.NowPlaySkill == stateBase.core.Status.constantsSkills[(int)SkillConstants.Jump] || stateBase.core.NowPlaySkill == stateBase.core.Status.constantsSkills[(int)SkillConstants.Front_Jump] || stateBase.core.NowPlaySkill == stateBase.core.Status.constantsSkills[(int)SkillConstants.Back_Jump])
                {
                    return;
                }
                stateBase.core.SetIsCrouching(false);
                stateBase.ChangeSkillConstant(SkillConstants.Idle, 5);
                stateBase.core.SetPlayerMoveState(PlayerMoveState.Idle);
                break;
            case Direction.Front:
                if (stateBase.core.NowPlaySkill == stateBase.core.Status.constantsSkills[(int)SkillConstants.Jump] || stateBase.core.NowPlaySkill == stateBase.core.Status.constantsSkills[(int)SkillConstants.Front_Jump] || stateBase.core.NowPlaySkill == stateBase.core.Status.constantsSkills[(int)SkillConstants.Back_Jump])
                {
                    return;
                }
                stateBase.core.SetIsCrouching(false);
                stateBase.ChangeSkillConstant(SkillConstants.Front_Walk, 5);
                stateBase.core.SetPlayerMoveState(PlayerMoveState.Front_Walk);
                break;
            case Direction.Back:
                if (stateBase.core.NowPlaySkill == stateBase.core.Status.constantsSkills[(int)SkillConstants.Jump] || stateBase.core.NowPlaySkill == stateBase.core.Status.constantsSkills[(int)SkillConstants.Front_Jump] || stateBase.core.NowPlaySkill == stateBase.core.Status.constantsSkills[(int)SkillConstants.Back_Jump])
                {
                    return;
                }
                stateBase.core.SetIsCrouching(false);
                stateBase.ChangeSkillConstant(SkillConstants.Back_Walk, 5);
                stateBase.core.SetPlayerMoveState(PlayerMoveState.Back_Walk);
                break;
            case Direction.Down:
                if (stateBase.core.NowPlaySkill == stateBase.core.Status.constantsSkills[(int)SkillConstants.Jump] || stateBase.core.NowPlaySkill == stateBase.core.Status.constantsSkills[(int)SkillConstants.Front_Jump] || stateBase.core.NowPlaySkill == stateBase.core.Status.constantsSkills[(int)SkillConstants.Back_Jump])
                {
                    return;
                }
                stateBase.core.SetIsCrouching(true);
                if (stateBase.core.NowPlaySkill != stateBase.core.Status.constantsSkills[(int)SkillConstants.Crouching])
                {
                    stateBase.ChangeSkillConstant(SkillConstants.Crouching, 5);
                }
                stateBase.core.SetPlayerMoveState(PlayerMoveState.Crouching);
                break;
            case Direction.DownBack:
                if (stateBase.core.NowPlaySkill == stateBase.core.Status.constantsSkills[(int)SkillConstants.Jump] || stateBase.core.NowPlaySkill == stateBase.core.Status.constantsSkills[(int)SkillConstants.Front_Jump] || stateBase.core.NowPlaySkill == stateBase.core.Status.constantsSkills[(int)SkillConstants.Back_Jump])
                {
                    return;
                }
                stateBase.core.SetIsCrouching(true);
                if (stateBase.core.NowPlaySkill != stateBase.core.Status.constantsSkills[(int)SkillConstants.Crouching])
                {
                    stateBase.ChangeSkillConstant(SkillConstants.Crouching, 5);
                }
                stateBase.core.SetPlayerMoveState(PlayerMoveState.Crouching);

                break;
            case Direction.DownFront:
                if (stateBase.core.NowPlaySkill == stateBase.core.Status.constantsSkills[(int)SkillConstants.Jump] || stateBase.core.NowPlaySkill == stateBase.core.Status.constantsSkills[(int)SkillConstants.Front_Jump] || stateBase.core.NowPlaySkill == stateBase.core.Status.constantsSkills[(int)SkillConstants.Back_Jump])
                {
                    return;
                }
                stateBase.core.SetIsCrouching(true);
                if (stateBase.core.NowPlaySkill != stateBase.core.Status.constantsSkills[(int)SkillConstants.Crouching])
                {
                    stateBase.ChangeSkillConstant(SkillConstants.Crouching, 5);
                }
                stateBase.core.SetPlayerMoveState(PlayerMoveState.Crouching);

                break;
            case Direction.Up:
                if (stateBase.core.NowPlaySkill == stateBase.core.Status.constantsSkills[(int)SkillConstants.Jump] || stateBase.core.NowPlaySkill == stateBase.core.Status.constantsSkills[(int)SkillConstants.Front_Jump] || stateBase.core.NowPlaySkill == stateBase.core.Status.constantsSkills[(int)SkillConstants.Back_Jump])
                {
                    return;
                }
				isJump = true;
				stateBase.core.SetIsCrouching(false);
                stateBase.ChangeSkillConstant(SkillConstants.Jump, 0);
                stateBase.core.SetPlayerMoveState(PlayerMoveState.Jump);

                break;
            case Direction.UpFront:
                if (stateBase.core.NowPlaySkill == stateBase.core.Status.constantsSkills[(int)SkillConstants.Jump] || stateBase.core.NowPlaySkill == stateBase.core.Status.constantsSkills[(int)SkillConstants.Front_Jump] || stateBase.core.NowPlaySkill == stateBase.core.Status.constantsSkills[(int)SkillConstants.Back_Jump])
                {
                    return;
                }
				isJump = true;
				stateBase.core.SetIsCrouching(false);
                stateBase.ChangeSkillConstant(SkillConstants.Front_Jump, 0);
                stateBase.core.SetPlayerMoveState(PlayerMoveState.Front_Jump);

                break;
            case Direction.UpBack:
                if (stateBase.core.NowPlaySkill == stateBase.core.Status.constantsSkills[(int)SkillConstants.Jump] || stateBase.core.NowPlaySkill == stateBase.core.Status.constantsSkills[(int)SkillConstants.Front_Jump] || stateBase.core.NowPlaySkill == stateBase.core.Status.constantsSkills[(int)SkillConstants.Back_Jump])
                {
                    return;
                }
				isJump = true;
				stateBase.core.SetIsCrouching(false);
                stateBase.ChangeSkillConstant(SkillConstants.Back_Jump, 0);
                stateBase.core.SetPlayerMoveState(PlayerMoveState.Back_Jump);

                break;
        }

    }
    #endregion
    //ジャンプ入力時のみ
    private bool JumpChange(bool _isCount = true)
    {
        Direction _dir = stateBase.input.GetPlayerMoveDirection(stateBase);
        if(_dir == Direction.Up||_dir == Direction.UpFront||_dir == Direction.UpBack)
        {
            stateBase.core.DirectionChangeMaterial();
        }
        if (!stateBase.core.GroundCheck())
        {
            _dir = stateBase.input.GetPlayerMoveDirection(stateBase);
            switch (_dir)
            {
                case Direction.Up:
                    stateBase.core.SetIsCrouching(false);
                    stateBase.ChangeSkillConstant(SkillConstants.Double_Jump, 0);
                    stateBase.core.SetPlayerMoveState(PlayerMoveState.Jump);
                    if (_isCount)
                    {
                        jumpTimes++;
                    }
                    return true;
                case Direction.UpFront:
                    stateBase.core.SetIsCrouching(false);
                    stateBase.ChangeSkillConstant(SkillConstants.Double_Jump_Front, 0);
                    stateBase.core.SetPlayerMoveState(PlayerMoveState.Front_Jump);
                    if (_isCount)
                    {
                        jumpTimes++;
                    }
                    return true;
                case Direction.UpBack:
                    stateBase.core.SetIsCrouching(false);
                    stateBase.ChangeSkillConstant(SkillConstants.Double_Jump_Back, 0);
                    stateBase.core.SetPlayerMoveState(PlayerMoveState.Back_Jump);
                    if (_isCount)
                    {
                        jumpTimes++;
                    }
                    return true;
                default:
                    return false;
            }
        }
        else
        {
            _dir = stateBase.input.GetPlayerMoveDirection(stateBase);
            switch (_dir)
            {
                case Direction.Up:
                    stateBase.core.SetIsCrouching(false);
                    stateBase.ChangeSkillConstant(SkillConstants.Jump, 0);
                    stateBase.core.SetPlayerMoveState(PlayerMoveState.Jump);
                    if (_isCount)
                    {
                        jumpTimes++;
                    }
                    return true;
                case Direction.UpFront:
                    stateBase.core.SetIsCrouching(false);
                    stateBase.ChangeSkillConstant(SkillConstants.Front_Jump, 0);
                    stateBase.core.SetPlayerMoveState(PlayerMoveState.Front_Jump);
                    if (_isCount)
                    {
                        jumpTimes++;
                    }
                    return true;
                case Direction.UpBack:
                    stateBase.core.SetIsCrouching(false);
                    stateBase.ChangeSkillConstant(SkillConstants.Back_Jump, 0);
                    stateBase.core.SetPlayerMoveState(PlayerMoveState.Back_Jump);
                    if (_isCount)
                    {
                        jumpTimes++;
                    }
                    return true;
                default:
                    return false;
            }
        }
        return false;
    }
}
