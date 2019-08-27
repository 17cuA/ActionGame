using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CUEngine.Pattern;
using System;

public class FighterStateBase : StateBaseScriptMonoBehaviour
{
    public FighterCore core;
    public TestInput input = null;
	public FighterStateGuard stateGuard;

    public Dictionary<string, FighterSkill> groundSkills = new Dictionary<string, FighterSkill>();//地上コマンド技
    public Dictionary<string, FighterSkill> airSkills = new Dictionary<string, FighterSkill>();//空中コマンド技

    #region 初期化
    private void Start()
    {
        switch (core.PlayerNumber)
        {
            case PlayerNumber.Player1:
                input = InputManager.Instance.testInput[0];
                break;
            case PlayerNumber.Player2:
                input = InputManager.Instance.testInput[1];
                break;
        }
		stateGuard = GetComponent<FighterStateGuard>();

        foreach(var s in core.Status.groundMoveSkills)
        {
            groundSkills.Add(s.name, s.skill);
        }
        foreach(var s in core.Status.groundAttackSkills)
        {
            groundSkills.Add(s.name + s.trigger, s.skill);
        }
        foreach(var s in core.Status.airMoveSkills)
        {
            airSkills.Add(s.name, s.skill);
        }
        foreach(var s in core.Status.airAttackSkills)
        {
            airSkills.Add(s.name + s.trigger, s.skill);
        }
    }
    #endregion

    public void StartGame()
    {
        ChangeSkillConstant(SkillConstants.Start_Game_Motion, 0);
    }
    public void Start_NO_HP()
    {
        ChangeSkillConstant(SkillConstants.Not_HP_Down, 0);
    }
    /*　汎用条件式　*/
    //ゲーム開始する条件
    public bool IsStartGame(bool _game)
    {
        //現在条件なし
        return GameManager.Instance.isStartGame == _game;
    }
    public bool IsNotHitStop()
    {
        return GameManager.Instance.GetHitStop(core.PlayerNumber) <= 0;
    }
    public bool IsMoveFighter()
    {
        return true;
    }
    public bool IsGroundCheck(bool ground)
    {
        return core.GroundCheck() == ground;
    }
	//ダメージ受けたとき
	public bool IsApplyDamage()
	{
        //一応0以下ならダメージを受けないように
        if(core.HP<=0)
        {
            return false;
        }
		if(stateGuard.isGuard == true)
		{
			return false;
		}
        //相手側が投げ技を喰らっていたら投げを無効にする
        bool f = core.GetDamage.frameHitBoxes.Count > 0;
        if((f)&&(GameManager.Instance.GetPlayFighterCore(core.EnemyNumber).GetDamage.isThrow))
        {
            GameManager.Instance.GetPlayFighterCore(core.EnemyNumber).SetDamage(new FighterSkill.CustomHitBox(), null);
        }
        if(f)
        {
			if (!core.GetDamage.isThrow)
			{
				//コンボカウント
				GameManager.Instance.GetPlayFighterCore(core.EnemyNumber).PlusComboCount(1);
			}
        }
		return f;
	}
    public bool IsHP(bool _f)
    {
        if ((core.HP > 0)==_f)
        {
            return true;
        }
        return false;
    }
	//スキル入れ替え
	public void ChangeSkillConstant(SkillConstants _constants, int _weightFrame)
    {
        core.SetSkill(core.Status.constantsSkills[(int)_constants], _weightFrame);
    }
    //スキル入れ替え（移動カスタム）
    public void ChangeSkillCustomMoveConstant(SkillConstants _constants, int _weightFrame,List<FighterSkill.Move> _move,List<FighterSkill.GravityMove> _grav,bool _con)
    {
        FighterSkill s = Instantiate(core.Status.constantsSkills[(int)_constants]);
        s.movements = _move;
        s.gravityMoves = _grav;
        s.isContinue = _con;
        core.SetSkill(s, _weightFrame);
    }
}
