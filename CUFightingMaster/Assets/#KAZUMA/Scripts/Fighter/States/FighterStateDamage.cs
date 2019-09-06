using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CUEngine.Pattern;

public class FighterStateDamage : StateBaseScriptMonoBehaviour
{
	private FighterStateBase stateBase;
	private int hitRigor = 0;
	private int hitCount = 0;

    private bool isEndStun = false;
	private void Start()
	{
		stateBase = GetComponent<FighterStateBase>();
	}
    #region 地上やられ
    //やられ
    public void HitStunStart()
    {
        isEndStun = false;
        FighterSkill.CustomHitBox box = stateBase.core.GetDamage;
        //硬直
        hitRigor = box.hitRigor;
        hitCount = 0;

		//ヒットストップ
		GameManager.Instance.SetHitStop(stateBase.core.PlayerNumber, box.hitStop);
        //相打ち時に受けたほうを優先する
        if (GameManager.Instance.GetHitStop(stateBase.core.EnemyNumber) <= 0)
        {
            //遠距離の場合は相手側ヒットストップなし
            if (box.mode != HitBoxMode.Bullet)
            {
                GameManager.Instance.SetHitStop(stateBase.core.EnemyNumber, box.hitStop);
            }
        }
        //立ちやられ
        if (stateBase.core.GetDamage.frameHitBoxes.Count > 0)
        {
            //ダメージ処理
            stateBase.core.HP -= box.damage + GameDataStrage.Instance.GetPlusDamage(stateBase.core.EnemyNumber);
			//stateBase.core.SpecialGauge
            if (stateBase.core.HP < 0)
            {
                stateBase.core.HP = 0;
            }
            switch (box.hitPoint)
            {
                case HitPoint.Bottom:
                    switch (box.hitStrength)
                    {
                        case HitStrength.Light:
                            if (stateBase.core.PlayerMoveStates == PlayerMoveState.Crouching)
                            {
                                stateBase.ChangeSkillConstant(SkillConstants.Crouching_Light_Bottom_HitMotion, 0);
                            }
                            else
                            {
                                stateBase.ChangeSkillConstant(SkillConstants.Stand_Light_Bottom_HitMotion, 0);
                            }
                            break;
                        case HitStrength.Middle:
                            if (stateBase.core.PlayerMoveStates == PlayerMoveState.Crouching)
                            {

                                stateBase.ChangeSkillConstant(SkillConstants.Crouching_Middle_Bottom_HitMotion, 0);
                            }
                            else
                            {
                                stateBase.ChangeSkillConstant(SkillConstants.Stand_Middle_Bottom_HitMotion, 0);
                            }
                            break;
                        case HitStrength.Strong:
                            if (stateBase.core.PlayerMoveStates == PlayerMoveState.Crouching)
                            {
                                stateBase.ChangeSkillConstant(SkillConstants.Crouching_Strong_Bottom_HitMotion, 0);
                            }
                            else
                            {
                                stateBase.ChangeSkillConstant(SkillConstants.Stand_Strong_Bottom_HitMotion, 0);
                            }
                            break;
                    }
                    break;
                case HitPoint.Middle:
                    switch (box.hitStrength)
                    {
                        case HitStrength.Light:
                            if (stateBase.core.PlayerMoveStates == PlayerMoveState.Crouching)
                            {

                                stateBase.ChangeSkillConstant(SkillConstants.Crouching_Light_Middle_HitMotion, 0);
                            }
                            else
                            {
                                stateBase.ChangeSkillConstant(SkillConstants.Stand_Light_Middle_HitMotion, 0);
                            }
                            break;
                        case HitStrength.Middle:
                            if (stateBase.core.PlayerMoveStates == PlayerMoveState.Crouching)
                            {

                                stateBase.ChangeSkillConstant(SkillConstants.Crouching_Middle_Middle_HitMotion, 0);
                            }
                            else
                            {
                                stateBase.ChangeSkillConstant(SkillConstants.Stand_Middle_Middle_HitMotion, 0);
                            }
                            break;
                        case HitStrength.Strong:
                            if (stateBase.core.PlayerMoveStates == PlayerMoveState.Crouching)
                            {

                                stateBase.ChangeSkillConstant(SkillConstants.Crouching_Strong_Middle_HitMotion, 0);
                            }
                            else
                            {
                                stateBase.ChangeSkillConstant(SkillConstants.Stand_Strong_Middle_HitMotion, 0);
                            }
                            break;
                    }
                    break;
                case HitPoint.Top:
                    switch (box.hitStrength)
                    {
                        case HitStrength.Light:
                            if (stateBase.core.PlayerMoveStates == PlayerMoveState.Crouching)
                            {
                                stateBase.ChangeSkillConstant(SkillConstants.Crouching_Light_Top_HitMotion, 0);
                            }
                            else
                            {
                                stateBase.ChangeSkillConstant(SkillConstants.Stand_Light_Top_HitMotion, 0);
                            }
                            break;
                        case HitStrength.Middle:
                            if (stateBase.core.PlayerMoveStates == PlayerMoveState.Crouching)
                            {
                                stateBase.ChangeSkillConstant(SkillConstants.Crouching_Middle_Top_HitMotion, 0);
                            }
                            else
                            {
                                stateBase.ChangeSkillConstant(SkillConstants.Stand_Light_Top_HitMotion, 0);
                            }
                            break;
                        case HitStrength.Strong:
                            if (stateBase.core.PlayerMoveStates == PlayerMoveState.Crouching)
                            {
                                stateBase.ChangeSkillConstant(SkillConstants.Crouching_Strong_Top_HitMotion, 0);
                            }
                            else
                            {
                                stateBase.ChangeSkillConstant(SkillConstants.Stand_Light_Top_HitMotion, 0);
                            }
                            break;
                    }
                    break;
            }
        }
        //ノックバックのセット
        PlayerDirection tmpDir;
        //右向きにノックバックするか左向きにノックバックするか
        if (GameManager.Instance.GetPlayFighterCore(stateBase.core.EnemyNumber).Direction == PlayerDirection.Right)
        {
            tmpDir = PlayerDirection.Left;
        }
        else
        {
            tmpDir = PlayerDirection.Right;
        }
        //エフェクト再生
        BoxCollider _bCol = stateBase.core.GetDamageCollider;
        Transform t = _bCol.gameObject.transform;
        CreateHitEffects(_bCol, t,box);
        //遠距離の場合は相手側ノックバックなし
        if (box.mode != HitBoxMode.Bullet)
        {
            stateBase.core.SetKnockBack(box.knockBack, stateBase.core.EnemyNumber, tmpDir);
        }
        else
        {
            stateBase.core.SetKnockBack(box.knockBack, stateBase.core.EnemyNumber, tmpDir, false);
        }
        //ゲージ増加
        stateBase.core.SpecialGauge += box.enemyPlusGauge;
        //ダメージを受けたのでリセット
        stateBase.core.SetDamage(new FighterSkill.CustomHitBox(), null);
    }
    #endregion

    #region 空中やられ
    //やられ
    public void AirHitStunStart()
    {
        stateBase.core.DirectionChangeMaterial();
        isEndStun = false;
        FighterSkill.CustomHitBox box = stateBase.core.GetDamage;
        //硬直
        hitRigor = box.hitRigor;
        hitCount = 0;

        //ヒットストップ
        GameManager.Instance.SetHitStop(stateBase.core.PlayerNumber, box.hitStop);
        //相打ち時に受けたほうを優先する
        if (GameManager.Instance.GetHitStop(stateBase.core.EnemyNumber) <= 0)
        {
			//遠距離の場合は相手側ヒットストップなし
			if (box.mode != HitBoxMode.Bullet)
			{
				GameManager.Instance.SetHitStop(stateBase.core.EnemyNumber, box.hitStop);
			}
        }
        //立ちやられ
        if (stateBase.core.GetDamage.frameHitBoxes.Count > 0)
        {
            //ダメージ処理
            stateBase.core.HP -= box.damage + GameDataStrage.Instance.GetPlusDamage(stateBase.core.EnemyNumber);
            if (stateBase.core.HP < 0)
            {
                stateBase.core.HP = 0;
            }
            switch (box.hitPoint)
            {
                case HitPoint.Bottom:
                    switch (box.hitStrength)
                    {
                        case HitStrength.Light:
							stateBase.ChangeSkillCustomMoveConstant(SkillConstants.Air_Light_Bottom_HitMotion, 0, box.airDamageMovements);
                            break;
                        case HitStrength.Middle:
							stateBase.ChangeSkillCustomMoveConstant(SkillConstants.Air_Middle_Bottom_HitMotion, 0, box.airDamageMovements);
                            break;
                        case HitStrength.Strong:
							stateBase.ChangeSkillCustomMoveConstant(SkillConstants.Air_Strong_Bottom_HitMotion, 0, box.airDamageMovements);
                            break;
                    }
                    break;
                case HitPoint.Middle:
                    switch (box.hitStrength)
                    {
                        case HitStrength.Light:
							stateBase.ChangeSkillCustomMoveConstant(SkillConstants.Air_Light_Middle_HitMotion, 0, box.airDamageMovements);
                            break;
                        case HitStrength.Middle:
							stateBase.ChangeSkillCustomMoveConstant(SkillConstants.Air_Middle_Middle_HitMotion, 0, box.airDamageMovements);
                            break;
                        case HitStrength.Strong:
							stateBase.ChangeSkillCustomMoveConstant(SkillConstants.Air_Strong_Middle_HitMotion, 0, box.airDamageMovements);
                            break;
                    }
                    break;
                case HitPoint.Top:
                    switch (box.hitStrength)
                    {
                        case HitStrength.Light:
							stateBase.ChangeSkillCustomMoveConstant(SkillConstants.Air_Light_Top_HitMotion, 0, box.airDamageMovements);
                            break;
                        case HitStrength.Middle:
							stateBase.ChangeSkillCustomMoveConstant(SkillConstants.Air_Middle_Top_HitMotion, 0, box.airDamageMovements);
                            break;
                        case HitStrength.Strong:
							stateBase.ChangeSkillCustomMoveConstant(SkillConstants.Air_Strong_Top_HitMotion, 0, box.airDamageMovements);
                            break;
                    }
                    break;
            }
        }
        PlayerDirection tmpDir;

        //ノックバックのセット
        if (GameManager.Instance.GetPlayFighterCore(stateBase.core.EnemyNumber).Direction == PlayerDirection.Right)
        {
            tmpDir = PlayerDirection.Left;
        }
        else
        {
            tmpDir = PlayerDirection.Right;
        }

		//エフェクト再生
		BoxCollider _bCol = stateBase.core.GetDamageCollider;
		Transform t = _bCol.gameObject.transform;
        CreateHitEffects(_bCol, t,box);

		//ノックバックのセット
		//遠距離の場合は相手側ノックバックなし
		if (box.mode != HitBoxMode.Bullet)
		{
			stateBase.core.SetKnockBack(box.airKnockBack, stateBase.core.EnemyNumber, tmpDir);
		}
		else
		{
			stateBase.core.SetKnockBack(box.airKnockBack, stateBase.core.EnemyNumber, tmpDir, false);
		}
        //ゲージ増加
        stateBase.core.SpecialGauge += box.enemyPlusGauge;
		stateBase.core.SetDamage(new FighterSkill.CustomHitBox(),null);
    }
	#endregion

	#region 飛ばし（ダウン技）
	public void DownHitStart()
	{
		isEndStun = false;
        FighterSkill.CustomHitBox box = stateBase.core.GetDamage;
        //ヒット硬直（空中受け身まで）
        hitRigor = box.hitRigor;
        hitCount = 0;

        //ヒットストップ
        GameManager.Instance.SetHitStop(stateBase.core.PlayerNumber, box.hitStop);
        //相打ち時に受けたほうを優先する
        if (GameManager.Instance.GetHitStop(stateBase.core.EnemyNumber) <= 0)
        {
			//遠距離の場合は相手側ヒットストップなし
			if (box.mode != HitBoxMode.Bullet)
			{
				GameManager.Instance.SetHitStop(stateBase.core.EnemyNumber, box.hitStop);
			}
        }
        //ダウンやられ
        if (stateBase.core.GetDamage.frameHitBoxes.Count > 0)
        {
            //ダメージ処理
            stateBase.core.HP -= box.damage + GameDataStrage.Instance.GetPlusDamage(stateBase.core.EnemyNumber);
            if (stateBase.core.HP < 0)
            {
                stateBase.core.HP = 0;
            }
            stateBase.ChangeSkillCustomMoveConstant(SkillConstants.Damage_Fly_HitMotion,0,box.movements,box.gravityMoves,box.isContinue);
        }
        PlayerDirection tmpDir;

        //ノックバックのセット
        if (GameManager.Instance.GetPlayFighterCore(stateBase.core.EnemyNumber).Direction == PlayerDirection.Right)
        {
            tmpDir = PlayerDirection.Left;
        }
        else
        {
            tmpDir = PlayerDirection.Right;
        }

		//エフェクト再生
		BoxCollider _bCol = stateBase.core.GetDamageCollider;
		if (_bCol != null)
		{
			Transform t = _bCol.gameObject.transform;
			CreateHitEffects(_bCol, t, box);
		}

		//遠距離の場合は相手側ノックバックなし
		if (box.mode != HitBoxMode.Bullet)
		{
			stateBase.core.SetKnockBack(box.airKnockBack, stateBase.core.EnemyNumber, tmpDir);
		}
		else
		{
			stateBase.core.SetKnockBack(box.airKnockBack, stateBase.core.EnemyNumber, tmpDir, false);
		}
        //ゲージ増加
        stateBase.core.SpecialGauge += box.enemyPlusGauge;
		stateBase.core.SetDamage(new FighterSkill.CustomHitBox(),null);
	}
    #endregion

    #region 投げ技
    private FighterSkill.CustomHitBox throwDamages;
    public void Throw_Damage_Start()
    {
        if (GameManager.Instance.GetPlayFighterCore(stateBase.core.EnemyNumber).Direction == PlayerDirection.Right)
        {
            Transform t = stateBase.core.AnimationPlayerCompornent.gameObject.transform;

            stateBase.core.SetDirection(PlayerDirection.Left);
            stateBase.core.scaleChangeObject.transform.localScale = new Vector3(1, 1, -1);
            stateBase.core.rotationChangeObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (GameManager.Instance.GetPlayFighterCore(stateBase.core.EnemyNumber).Direction == PlayerDirection.Left)
        {
            Transform t = stateBase.core.AnimationPlayerCompornent.gameObject.transform;
            stateBase.core.SetDirection(PlayerDirection.Right);
            stateBase.core.AnimationPlayerCompornent.gameObject.transform.localScale = new Vector3(1, 1, 1);
            t.rotation = Quaternion.Euler(0, 180, 0);
        }
        isEndStun = false;
        stateBase.core.SetSkill(stateBase.core.GetDamage.enemyThrowSkill, 0);
        
        throwDamages = stateBase.core.GetDamage;
        //ヒットストップ
        GameManager.Instance.SetHitStop(stateBase.core.PlayerNumber, throwDamages.hitStop);
        GameManager.Instance.SetHitStop(stateBase.core.EnemyNumber, throwDamages.hitStop);

        //エフェクト再生
		BoxCollider _bCol = stateBase.core.GetDamageCollider;
		Transform trans = _bCol.gameObject.transform;
        CreateHitEffects(_bCol, trans, throwDamages);
        //ダメージを受けたのでリセット
        GameManager.Instance.GetPlayFighterCore(stateBase.core.GetDamageCollider.gameObject.layer).SetSkill(stateBase.core.GetDamage.throwSkill, 0);
        stateBase.core.SetDamage(new FighterSkill.CustomHitBox(), null);
    }
	public void Throw_Damage_Update()
	{
        //接地時ダメージ
        if(stateBase.core.GroundCheck())
        {
            if (throwDamages.isThrowGroundDamage)
            {
                stateBase.core.HP -= throwDamages.throwGroundDamage+GameDataStrage.Instance.GetPlusDamage(stateBase.core.EnemyNumber);
                //ゲージ増加
                stateBase.core.SpecialGauge += throwDamages.enemyPlusGauge;
                GameManager.Instance.GetPlayFighterCore(stateBase.core.EnemyNumber).PlusComboCount(1);
                if (stateBase.core.HP < 0)
                {
                    stateBase.core.HP = 0;
                }
            }
        }
		//特定のフレームでダメージを与える
		foreach(var dm in throwDamages.throwDamages)
		{
			if(dm.frame==stateBase.core.AnimationPlayerCompornent.NowFrame)
			{
                //ゲージ増加
                stateBase.core.SpecialGauge += throwDamages.enemyPlusGauge;
                stateBase.core.HP -= dm.damage + GameDataStrage.Instance.GetPlusDamage(stateBase.core.EnemyNumber); ;
                GameManager.Instance.GetPlayFighterCore(stateBase.core.EnemyNumber).PlusComboCount(1);
                if (stateBase.core.HP < 0)
                {
                    stateBase.core.HP = 0;
                }
            }
		}
	}
    #endregion

    //ヒット硬直時間をプラス
    public void HitStunUpdate()
	{
		if (GameManager.Instance.GetHitStop(stateBase.core.PlayerNumber) <= 0)
		{
			hitCount++;
		}
	}
	//受け身再生
	public void PlayPassive()
	{
        stateBase.core.SetKnockBack(0,PlayerNumber.None, PlayerDirection.Right);
        Direction dir = stateBase.input.GetPlayerMoveDirection(stateBase);
		if (dir == Direction.Back)
		{
			stateBase.ChangeSkillConstant(SkillConstants.Air_Back_Passive, 0);
		}
		else if(dir == Direction.Front)
		{
			stateBase.ChangeSkillConstant(SkillConstants.Air_Front_Passive, 0);
		}
		else
		{
			stateBase.ChangeSkillConstant(SkillConstants.Air_Passive, 0);
		}
	}
    //ステートエンド
    public void EndHitStunFlag()
    {

        isEndStun = true;

    }
    //ダウンかどうか
    public bool isDownHit()
    {
        return stateBase.core.GetDamage.isDown;
    }
    public bool isThrowHit()
    {
		//相手側がダメージを受けていたら基本受けない
        if (GameManager.Instance.GetPlayFighterCore(stateBase.core.EnemyNumber).GetDamage.frameHitBoxes.Count > 0)
        {
            //投げ同士は無効
            if(GameManager.Instance.GetPlayFighterCore(stateBase.core.EnemyNumber).GetDamage.isThrow)
            {
                GameManager.Instance.GetPlayFighterCore(stateBase.core.EnemyNumber).SetDamage(new FighterSkill.CustomHitBox(), null);
            }
            else
            {
                //相手が攻撃で自分が投げを喰らっていた場合
                if(stateBase.core.GetDamage.isThrow)
                {
                    stateBase.core.SetDamage(new FighterSkill.CustomHitBox(), null);
                }
            }
			return false;
            //return stateBase.core.GetDamage.isThrow;
        }
		else if(stateBase.core.GetDamage.isThrow)
		{
            return true;
        }
        return false;
    }
	public bool isNoneDamage()
	{
		if(stateBase.core.GetDamage.frameHitBoxes.Count > 0)
		{
            return false;
        }
        return true;
    }
    //ヒット硬直時間が終わったら
    public bool IsEndHitStunCount()
	{
		return hitCount >= hitRigor;
	}
	public bool IsEndHitStun()
	{
        if (isEndStun)
        {
            GameManager.Instance.GetPlayFighterCore(stateBase.core.EnemyNumber).SetComboCount(0);
        }
        return isEndStun;
	}
	//受け身したかどうか
	public bool IsPassiveInput()
	{
        if (stateBase.core.HP <= 0)
        {
            return false;
        }
        if(stateBase.input.atkButton != "")
		{
            if (stateBase.input.atkButton != CommonConstants.Buttons.Atk4)
            {
                stateBase.input.atkButton = "";
                return true;
            }
        }
		return false;
	}
    public bool IsEndThrowDamage()
    {
        if(throwDamages.isThrowGroundAnimEnd)
        {
            return stateBase.core.GroundCheck() == true;
        }
        else
        {
            return stateBase.core.AnimationPlayerCompornent.EndAnimFrag;

        }
    }
    //エフェクトの生成(ガードは別)
    private void CreateHitEffects(BoxCollider _boxCollider, Transform _enemyTrans, FighterSkill.CustomHitBox _box)
    {
        Transform _en = _enemyTrans;
        for (int i = 0; i < _box.hitEffects.Count; i++)
        {
            if (_box.hitEffects[i].effect != null)
            {
                _enemyTrans = _en;
                GameObject obj = null;
                if (!_box.hitEffects[i].isEnemyPos)
                {
                    if (GameManager.Instance.GetPlayFighterCore(stateBase.core.EnemyNumber).Direction == PlayerDirection.Right)
                    {
                        obj = Object.Instantiate(_box.hitEffects[i].effect, new Vector3(_enemyTrans.position.x + _boxCollider.center.x + _box.hitEffects[i].position.x, _enemyTrans.position.y + _boxCollider.center.y + _box.hitEffects[i].position.y, _enemyTrans.position.z + _boxCollider.center.z + _box.hitEffects[i].position.z), Quaternion.identity);
                    }
                    else if (GameManager.Instance.GetPlayFighterCore(stateBase.core.EnemyNumber).Direction == PlayerDirection.Left)
                    {
                        obj = Object.Instantiate(_box.hitEffects[i].effect, new Vector3(_enemyTrans.position.x + _boxCollider.center.x + (_box.hitEffects[i].position.x*-1), _enemyTrans.position.y + _boxCollider.center.y + _box.hitEffects[i].position.y, _enemyTrans.position.z + _boxCollider.center.z + _box.hitEffects[i].position.z), Quaternion.Euler(0, 180, 0));
                    }
                }
                else
                {
                    _enemyTrans = stateBase.core.transform;
                    if (GameManager.Instance.GetPlayFighterCore(stateBase.core.EnemyNumber).Direction == PlayerDirection.Right)
                    {
                        obj = Object.Instantiate(_box.hitEffects[i].effect, new Vector3(_enemyTrans.position.x + _box.hitEffects[i].position.x, _enemyTrans.position.y + _box.hitEffects[i].position.y, _enemyTrans.position.z + _box.hitEffects[i].position.z), Quaternion.identity);
                    }
                    else if (GameManager.Instance.GetPlayFighterCore(stateBase.core.EnemyNumber).Direction == PlayerDirection.Left)
                    {
                        obj = Object.Instantiate(_box.hitEffects[i].effect, new Vector3(_enemyTrans.position.x + (_box.hitEffects[i].position.x * -1), _enemyTrans.position.y + _box.hitEffects[i].position.y, _enemyTrans.position.z + _box.hitEffects[i].position.z), Quaternion.Euler(0, 180, 0));
                    }
                }
                //親子関係
                if(_box.hitEffects[i].isEnemyParant)
                {
                    if (obj != null)
                    {
                        obj.transform.parent = stateBase.core.transform;
                    }
                }
                else if(_box.hitEffects[i].isParant)
                {
                    if(obj!=null)
                    {
                        obj.transform.parent = GameManager.Instance.GetPlayFighterCore(stateBase.core.EnemyNumber).transform;
                    }
                }
            }
        }
    }
}
