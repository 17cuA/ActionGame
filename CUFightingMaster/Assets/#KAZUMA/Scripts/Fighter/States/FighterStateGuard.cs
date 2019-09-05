using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CUEngine.Pattern;

public class FighterStateGuard : StateBaseScriptMonoBehaviour
{
    private FighterStateBase stateBase;
	public bool isGuard = false;

	private int hitRigor = 0;
	private int hitCount = 0;

	private bool isGuardEnd = false;

	private void Start()
    {
        stateBase = GetComponent<FighterStateBase>();
    }
	//ガードになる条件
	public bool IsApplyGuard()
	{
		if (stateBase.core.GetDamage.frameHitBoxes.Count > 0)
		{
			Direction inp = stateBase.input.GetPlayerMoveDirection(stateBase);

			if (stateBase.core.GetDamage.hitPoint == HitPoint.Top || stateBase.core.GetDamage.hitPoint == HitPoint.Middle)
			{
				if ((inp == Direction.Back)&&(stateBase.core.GetDamage.isThrow==false))
				{
					isGuard = true;
				}
			}
			if(stateBase.core.GetDamage.hitPoint == HitPoint.Bottom|| stateBase.core.GetDamage.hitPoint == HitPoint.Top)
			{
				if ((inp == Direction.DownBack)&&(stateBase.core.GetDamage.isThrow==false))
				{
					isGuard = true;
				}
			}
		}
		return isGuard;
	}
	//ガード開始
	public void GuardStart()
	{
		isGuard = false;
		isGuardEnd = false;
		FighterSkill.CustomHitBox box = stateBase.core.GetDamage;
		//硬直
		hitRigor = box.guardHitRigor;
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

		Direction inp = stateBase.input.GetPlayerMoveDirection(stateBase);
		if (inp == Direction.Back)
		{
			stateBase.ChangeSkillConstant(SkillConstants.Stand_Guard, 0);
		}
		else if (inp == Direction.DownBack)
		{
			stateBase.ChangeSkillConstant(SkillConstants.Crouching_Guard, 0);
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
        CreateGuardEffects(_bCol, t, box);

        //遠距離の場合は相手側ノックバックなし
        if (box.mode != HitBoxMode.Bullet)
		{
			stateBase.core.SetKnockBack(box.guardKnockBack, stateBase.core.EnemyNumber, tmpDir);
		}
		else
		{
			stateBase.core.SetKnockBack(box.guardKnockBack, stateBase.core.EnemyNumber, tmpDir, false);
		}

		//ダメージを受けたのでリセット
		stateBase.core.SetDamage(new FighterSkill.CustomHitBox(),null);
	}
	//ヒット硬直時間をプラス
	public void GuardUpdate()
	{
		if (GameManager.Instance.GetHitStop(stateBase.core.PlayerNumber) <= 0)
		{
			hitCount++;
		}
	}

	//ヒット硬直時間が終わったら
	public bool IsEndGuardCount()
	{
		return hitCount >= hitRigor;
	}

	//ガードから抜ける
	public bool IsEndGuard()
	{
		return isGuardEnd;
	}
	//ステートエンド
	public void EndGuardFlag()
	{
		isGuardEnd = true;
	}
    //エフェクトの生成
    private void CreateGuardEffects(BoxCollider _boxCollider, Transform _enemyTrans, FighterSkill.CustomHitBox _box)
    {
        Transform _en = _enemyTrans;
        for (int i = 0; i < _box.hitEffects.Count; i++)
        {
            if (_box.hitEffects[i].guardEffect != null)
            {
                _enemyTrans = _en;
                GameObject obj = null;
                if (!_box.hitEffects[i].isEnemyPos)
                {
                    if (GameManager.Instance.GetPlayFighterCore(stateBase.core.EnemyNumber).Direction == PlayerDirection.Right)
                    {
                        obj = Object.Instantiate(_box.hitEffects[i].guardEffect, new Vector3(_enemyTrans.position.x + _boxCollider.center.x + _box.hitEffects[i].position.x, _enemyTrans.position.y + _boxCollider.center.y + _box.hitEffects[i].position.y, _enemyTrans.position.z + _boxCollider.center.z + _box.hitEffects[i].position.z), Quaternion.identity);
                    }
                    else if (GameManager.Instance.GetPlayFighterCore(stateBase.core.EnemyNumber).Direction == PlayerDirection.Left)
                    {
                        obj = Object.Instantiate(_box.hitEffects[i].guardEffect, new Vector3(_enemyTrans.position.x + _boxCollider.center.x + _box.hitEffects[i].position.x, _enemyTrans.position.y + _boxCollider.center.y + _box.hitEffects[i].position.y, _enemyTrans.position.z + _boxCollider.center.z + _box.hitEffects[i].position.z), Quaternion.Euler(0, 180, 0));
                    }
                }
                else
                {
                    _enemyTrans = stateBase.core.transform;
                    if (GameManager.Instance.GetPlayFighterCore(stateBase.core.EnemyNumber).Direction == PlayerDirection.Right)
                    {
                        obj = Object.Instantiate(_box.hitEffects[i].guardEffect, new Vector3(_enemyTrans.position.x + _boxCollider.center.x + _box.hitEffects[i].position.x, _enemyTrans.position.y + _boxCollider.center.y + _box.hitEffects[i].position.y, _enemyTrans.position.z + _boxCollider.center.z + _box.hitEffects[i].position.z), Quaternion.identity);
                    }
                    else if (GameManager.Instance.GetPlayFighterCore(stateBase.core.EnemyNumber).Direction == PlayerDirection.Left)
                    {
                        obj = Object.Instantiate(_box.hitEffects[i].guardEffect, new Vector3(_enemyTrans.position.x + _boxCollider.center.x + _box.hitEffects[i].position.x, _enemyTrans.position.y + _boxCollider.center.y + _box.hitEffects[i].position.y, _enemyTrans.position.z + _boxCollider.center.z + _box.hitEffects[i].position.z), Quaternion.Euler(0, 180, 0));
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
