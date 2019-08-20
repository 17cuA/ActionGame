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
                stateBase.core.SetSkill(stateBase.groundSkills[stateBase.input.groundMoveCommand.inputCommandName], 0);
            }
            return;
        }
        string atk = stateBase.input.GetPlayerAtk();
        switch (atk)
        {
            case CommonConstants.Buttons.Atk1:
                if (stateBase.core.PlayerMoveStates == PlayerMoveState.Crouching)
                {
					// 飯塚追加-------------------------------------------
                    Sound.LoadSe("PunchW", "Se_punch_weak");
                    Sound.PlaySe("PunchW", 2, 0.4f);
                    // ---------------------------------------------------
                    stateBase.ChangeSkillConstant(SkillConstants.Crouching_Light_Jab, 0);
                }
                else
                {
					// 飯塚追加-------------------------------------------
                    Sound.LoadSe("PunchW", "Se_punch_weak");
                    Sound.PlaySe("PunchW", 2, 0.4f);
                    // ---------------------------------------------------
                    stateBase.ChangeSkillConstant(SkillConstants.Stand_Light_Jab, 0);

                }
                break;
            case CommonConstants.Buttons.Atk2:
                if (stateBase.core.PlayerMoveStates == PlayerMoveState.Crouching)
                {
					 // 飯塚追加-------------------------------------------
                    Sound.LoadSe("PunchM", "Se_punch_medium");
                    Sound.PlaySe("PunchM", 2, 0.4f);
                    // ---------------------------------------------------
                    stateBase.ChangeSkillConstant(SkillConstants.Crouching_Middle_Jab, 0);

                }
                else
                {
					// 飯塚追加-------------------------------------------
                    Sound.LoadSe("PunchM", "Se_punch_medium");
                    Sound.PlaySe("PunchM", 2, 0.4f);
                    // ---------------------------------------------------
                    stateBase.ChangeSkillConstant(SkillConstants.Stand_Middle_Jab, 0);
                }
                break;
            case CommonConstants.Buttons.Atk3:
                if (stateBase.core.PlayerMoveStates == PlayerMoveState.Crouching)
                {
					// 飯塚追加-------------------------------------------
                    Sound.LoadSe("PunchS", "Se_punch_strong");
                    Sound.PlaySe("PunchS", 2, 0.4f);
                    // ---------------------------------------------------
                    stateBase.ChangeSkillConstant(SkillConstants.Crouching_Strong_Jab, 0);
                }
                else
                {
					 // 飯塚追加-------------------------------------------
                    Sound.LoadSe("PunchS", "Se_punch_strong");
                    Sound.PlaySe("PunchS", 2, 0.4f);
                    // ---------------------------------------------------
                    stateBase.ChangeSkillConstant(SkillConstants.Stand_Strong_Jab, 0);
                }
                break;
            case CommonConstants.Buttons.Atk4:
                if(stateBase.core.PlayerMoveStates != PlayerMoveState.Crouching)
                {
                    stateBase.ChangeSkillConstant(SkillConstants.Throw_Atk, 0);
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
            #region コマンド技
            if ((stateBase.input.groundMoveCommand.inputCommandName != "") && (stateBase.input.groundMoveCommand.inputCommandName != null))
            {
                if (stateBase.groundSkills.ContainsKey(stateBase.input.groundMoveCommand.inputCommandName))
                {
                    //キャンセルできるかどうか（技モード、AND演算）
                    if (ChancelConditions(_nowSkill,stateBase.groundSkills[stateBase.input.groundMoveCommand.inputCommandName]))
                    {
                        return true;
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
                        if(ChancelConditions(_nowSkill,stateBase.core.Status.constantsSkills[(int)SkillConstants.Crouching_Light_Jab])) return true;
                    }
                    else
                    {
                        if(ChancelConditions(_nowSkill,stateBase.core.Status.constantsSkills[(int)SkillConstants.Stand_Light_Jab])) return true;
                    }
                    break;
                case CommonConstants.Buttons.Atk2:
                    if (stateBase.core.PlayerMoveStates == PlayerMoveState.Crouching)
                    {
                        if(ChancelConditions(_nowSkill,stateBase.core.Status.constantsSkills[(int)SkillConstants.Crouching_Middle_Jab])) return true;
                    }
                    else
                    {
                        if(ChancelConditions(_nowSkill,stateBase.core.Status.constantsSkills[(int)SkillConstants.Stand_Middle_Jab])) return true;
                    }
                    break;
                case CommonConstants.Buttons.Atk3:
                    if (stateBase.core.PlayerMoveStates == PlayerMoveState.Crouching)
                    {
                        if(ChancelConditions(_nowSkill,stateBase.core.Status.constantsSkills[(int)SkillConstants.Crouching_Strong_Jab])) return true;
                    }
                    else
                    {
                        if(ChancelConditions(_nowSkill,stateBase.core.Status.constantsSkills[(int)SkillConstants.Stand_Strong_Jab])) return true;
                    }
                    break;
                case CommonConstants.Buttons.Atk4:
                    break;
            }
            #endregion

        }
        return false;
    }
    //キャンセル条件を満たしているかどうか
    private bool ChancelConditions(FighterSkill _now, FighterSkill _s)
    {
        //キャンセルできるかどうか（技モード、AND演算）
        if (_now.cancelFrag.HasFlag(_s.status))
        {
            return true;
        }
        //連打キャンセル(同じ技)
        if ((_now.barrageCancelFrag) && (_now == _s))
        {
            return true;
        }
        return false;
    }
    #endregion

    #region  空中
    public void AirAttackStart()
    {
        if ((stateBase.input.airMoveCommand.inputCommandName != "") && (stateBase.input.airMoveCommand.inputCommandName != null))
        {
            Debug.Log(stateBase.input.airMoveCommand.inputCommandName);
            //キーがあれば発動してreturn
            if (stateBase.airSkills.ContainsKey(stateBase.input.airMoveCommand.inputCommandName))
            {
                stateBase.core.SetSkill(stateBase.airSkills[stateBase.input.airMoveCommand.inputCommandName], 0);
            }
            return;
        }
        string atk = stateBase.input.GetPlayerAtk();
        if (stateBase.core.PlayerMoveStates == PlayerMoveState.Jump)
        {
            switch (atk)
            {
            case CommonConstants.Buttons.Atk1:
					// 飯塚追加-------------------------------------------
                    Sound.LoadSe("PunchW", "Se_punch_weak");
                    Sound.PlaySe("PunchW", 2, 0.4f);
                    // ---------------------------------------------------
                    stateBase.ChangeSkillConstant(SkillConstants.Air_Light_Jab, 0);
                    break;
            case CommonConstants.Buttons.Atk2:
					  // 飯塚追加-------------------------------------------
                    Sound.LoadSe("PunchM", "Se_punch_medium");
                    Sound.PlaySe("PunchM", 2, 0.4f);
                    // ---------------------------------------------------
                    stateBase.ChangeSkillConstant(SkillConstants.Air_Middle_Jab, 0);
                    break;
            case CommonConstants.Buttons.Atk3:
					 // 飯塚追加-------------------------------------------
                    Sound.LoadSe("PunchS", "Se_punch_strong");
                    Sound.PlaySe("PunchS", 2, 0.4f);
                    // ---------------------------------------------------
                    stateBase.ChangeSkillConstant(SkillConstants.Air_Strong_Jab, 0);
                    break;
            }
        }
        else if (stateBase.core.PlayerMoveStates == PlayerMoveState.Back_Jump)
        {
            switch (atk)
            {
            case CommonConstants.Buttons.Atk1:
                    // 飯塚追加-------------------------------------------
                    Sound.LoadSe("PunchW", "Se_punch_weak");
                    Sound.PlaySe("PunchW", 2, 0.4f);
                    // ---------------------------------------------------
                    stateBase.ChangeSkillConstant(SkillConstants.Air_Back_Light_Jab, 0);
                    break;
            case CommonConstants.Buttons.Atk2:
					 // 飯塚追加-------------------------------------------
                    Sound.LoadSe("PunchM", "Se_punch_medium");
                    Sound.PlaySe("PunchM", 2, 0.4f);
                    // ---------------------------------------------------
                    stateBase.ChangeSkillConstant(SkillConstants.Air_Back_Middle_Jab, 0);
                    break;
            case CommonConstants.Buttons.Atk3:
					 // 飯塚追加-------------------------------------------
                    Sound.LoadSe("PunchS", "Se_punch_strong");
                    Sound.PlaySe("PunchS", 2, 0.4f);
                    // ---------------------------------------------------
                    stateBase.ChangeSkillConstant(SkillConstants.Air_Back_Strong_Jab, 0);
                    break;
            }
        }
        else if (stateBase.core.PlayerMoveStates == PlayerMoveState.Front_Jump)
        {
            switch (atk)
            {
            case CommonConstants.Buttons.Atk1:
					// 飯塚追加-------------------------------------------
                    Sound.LoadSe("PunchW", "Se_punch_weak");
                    Sound.PlaySe("PunchW", 2, 0.4f);
                    // ---------------------------------------------------
                    stateBase.ChangeSkillConstant(SkillConstants.Air_Front_Light_Jab, 0);
                    break;
            case CommonConstants.Buttons.Atk2:
					 // 飯塚追加-------------------------------------------
                    Sound.LoadSe("PunchM", "Se_punch_medium");
                    Sound.PlaySe("PunchM", 2, 0.4f);
                    // ---------------------------------------------------
                    stateBase.ChangeSkillConstant(SkillConstants.Air_Front_Middle_Jab, 0);
                    break;
            case CommonConstants.Buttons.Atk3:
					// 飯塚追加-------------------------------------------
                    Sound.LoadSe("PunchS", "Se_punch_strong");
                    Sound.PlaySe("PunchS", 2, 0.4f);
                    // ---------------------------------------------------
                    stateBase.ChangeSkillConstant(SkillConstants.Air_Front_Strong_Jab, 0);
                    break;
            }
        }
    }
    public bool IsEndAirAttack()
    {
        return stateBase.core.AnimationPlayerCompornent.EndAnimFrag;
    }
    #endregion
}
