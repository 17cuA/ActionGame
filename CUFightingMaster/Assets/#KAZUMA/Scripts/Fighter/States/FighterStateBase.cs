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
    //格納用
    public struct MoveAndSkills
    {
        public int skillCost;//発動のためのコスト
        public int countValid;//空中の場合何回発動できるか
        public FighterSkill skill;
    }
    public Dictionary<string, MoveAndSkills> groundSkills = new Dictionary<string, MoveAndSkills>();//地上コマンド技
    public Dictionary<string, MoveAndSkills> airSkills = new Dictionary<string, MoveAndSkills>();//空中コマンド技

    private bool isEndHitStop = false;//KO時のヒットストップをしたかどうか

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

        foreach (var s in core.Status.groundMoveSkills)
        {
            var _s = new MoveAndSkills();
            _s.skill = s.skill;
            _s.skillCost = s.skillCost;
            _s.countValid = s.countValid;
            groundSkills.Add(s.name, _s);
        }
        foreach (var s in core.Status.groundAttackSkills)
        {
            var _s = new MoveAndSkills();
            _s.skill = s.skill;
            _s.skillCost = s.skillCost;
            _s.countValid = s.countValid;
            groundSkills.Add(s.name + s.trigger, _s);
        }
        foreach (var s in core.Status.airMoveSkills)
        {
            var _s = new MoveAndSkills();
            _s.skill = s.skill;
            _s.skillCost = s.skillCost;
            _s.countValid = s.countValid;
            airSkills.Add(s.name, _s);
        }
        foreach (var s in core.Status.airAttackSkills)
        {
            var _s = new MoveAndSkills();
            _s.skill = s.skill;
            _s.skillCost = s.skillCost;
            _s.countValid = s.countValid;
            airSkills.Add(s.name + s.trigger, _s);
        }
    }
    #endregion

    public void StartGame(bool isUpdate)
    {
        //向きの初期化
        int numberP = 1;
        if (core.PlayerNumber == PlayerNumber.Player1)
        {
            numberP = 0;
        }
        Transform t = core.AnimationPlayerCompornent.gameObject.transform;
        core.SetDirection(PlayerDirection.Left);
        core.scaleChangeObject.transform.localScale = new Vector3(1, 1, -1);
        core.rotationChangeObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        core.SetMaterial(core.Status.playerMaterials[numberP].inversionMaterial);


        isEndHitStop = false;
        core.SetDamage(new FighterSkill.CustomHitBox(), null);
        core.DirectionChangeMaterial();
        if (isUpdate == false)
        {
            ChangeSkillConstant(SkillConstants.Start_Game_Motion, 0);
        }
    }
    public void Start_NO_HP()
    {
        ChangeSkillConstant(SkillConstants.Not_HP_Down, 10);
    }
    public void NO_HP_HitStop()
	{
        if (!isEndHitStop)
        {
            GameManager.Instance.SetHitStop(core.PlayerNumber, GameManager.Instance.Settings.KOHitStopFrame);
            GameManager.Instance.SetHitStop(core.EnemyNumber, GameManager.Instance.Settings.KOHitStopFrame);
            isEndHitStop = true;
        }
    }
	/*　汎用条件式　*/
	//ゲーム開始する条件
	public bool IsStartGame(bool _game)
    {
        //現在条件なし
        return GameManager.Instance.isStartGame == _game && core.GroundCheck();
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
		if(stateGuard.isGuard == true)
		{
			return false;
		}
        //相手側が投げ技を喰らっていたら投げを無効にする
        bool f = core.GetDamage.frameHitBoxes.Count > 0;
        if((f)&&(GameManager.Instance.GetPlayFighterCore(core.EnemyNumber).GetDamage.isThrow))
        {
            //掴み同士だったらどちらも無効
            if(core.GetDamage.isThrow)
            {
                core.SetDamage(new FighterSkill.CustomHitBox(), null);
                GameManager.Instance.GetPlayFighterCore(core.EnemyNumber).SetDamage(new FighterSkill.CustomHitBox(), null);
                return false;
            }
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
    public bool IsEndRound()
    {
        return GameManager.Instance.isEndRound;
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
	//スキル入れ替え（移動カスタム）
	public void ChangeSkillCustomMoveConstant(SkillConstants _constants, int _weightFrame, List<FighterSkill.Move> _move)
	{
		FighterSkill s = Instantiate(core.Status.constantsSkills[(int)_constants]);
		s.movements = _move;
		core.SetSkill(s, _weightFrame);
	}

	//キャンセル可能かどうか
	public bool ChancelConditions(FighterSkill _now, FighterSkill _s, int _i = 0)
    {
        //キャンセルできるかどうか（技モード、AND演算）
        if(_now==null)
        {
            return false;
        }
        if(_s == null)
        {
            return false;
        }
        if (_now.cancelFrag.HasFlag(_s.status))
        {
            return true;
        }
        //連打キャンセル(同じ技)
        if ((_now.barrageCancelFrag) && (_now == _s))
        {
            return true;
        }
        //特殊キャンセル
        foreach(var _can in _now.cancelLayer)
        {
            if(_can == _s.skillLayer)
            {
                return true;
            }
        }
        return false;
    }
}
