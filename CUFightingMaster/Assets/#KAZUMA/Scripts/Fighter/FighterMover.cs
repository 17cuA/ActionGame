using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterMover
{
	private float airBraking = 0;

	private FighterCore core;
	private Transform transform;//動かすTransform
	private int nowPlayMoveNumber = -1;		//現在再生中の移動配列の要素数(-1だと動かない)
	private int nowPlayGravityNumber = -1;  //現在再生中の重力配列の要素数(-1だと動かない)
	private List<FighterSkill.Move> moves = new List<FighterSkill.Move>();//移動配列
	private List<FighterSkill.GravityMove> gravity = new List<FighterSkill.GravityMove>();//重力配列
	private int gravityFrame = 0;   //重力用のフレーム数

    //エフェクト
    private List<FighterSkill.FrameEffects> effects = new List<FighterSkill.FrameEffects>();
    private int nowPlayEffectNumber = -1;

	private List<FighterSkill.FrameBullets> bullets = new List<FighterSkill.FrameBullets>();
	private int nowPlayBulletNumber = -1;
	int RightLeft = 1;
    //初期化
    public FighterMover(FighterCore fighterCore)
	{
		core = fighterCore;
		transform = fighterCore.transform;
	}
    public void UpdateGame()
    {
		if (core.Direction == PlayerDirection.Right)
		{
			RightLeft = 1;
		}
		else if (core.Direction == PlayerDirection.Left)
		{
			RightLeft = -1;
		}
		ChangeSkillInit();//技の入れ替え
		MovementSkill();//移動
		GravityMovementSkill();
        gravityFrame++;
    }
	public void UpdateEffects()
	{
		PlayEffects();
		PlayBullets();
	}
	public void SetAirXMove(float _x)
	{
		airBraking = _x;
	}
	#region 技入れ替えチェック
	//入れ替わり処理
	private void ChangeSkillInit()
	{
		//入れ替わったかどうか
		if (core.changeSkill == false) return;
		if (core.NowPlaySkill != null)
		{

			//重力の継続をするか否か
			if(!core.NowPlaySkill.isContinue)
			{
				gravityFrame = 0;
			}
			moves = new List<FighterSkill.Move>(core.NowPlaySkill.movements);
            if (!core.NowPlaySkill.isContinue)
            {
                gravity = new List<FighterSkill.GravityMove>(core.NowPlaySkill.gravityMoves);
            }
            effects = new List<FighterSkill.FrameEffects>(core.NowPlaySkill.frameEffects);
			bullets = new List<FighterSkill.FrameBullets>(core.NowPlaySkill.frameBullets);

			nowPlayMoveNumber = -1;
			nowPlayGravityNumber = -1;
			nowPlayEffectNumber = -1;
			nowPlayBulletNumber = -1;

			//移動配列のソート、フレームが近い順に並べる
			if (moves.Count > 1)
			{
				moves.Sort((a, b) => a.startFrame - b.startFrame);
			}
			if(gravity.Count > 1)
			{
				gravity.Sort((a, b) => a.startFrame - b.startFrame);
			}
            if(effects.Count>1)
            {
                effects.Sort((a, b) => a.frame - b.frame);
			}
			if (effects.Count > 0)
			{
				//現在再生中の移動の次の移動フレームを越えれば
				if (effects[0].frame == 0)
				{
					nowPlayEffectNumber++;
					GameObject obj = null;
					if (effects[nowPlayEffectNumber].worldPositionFlag)
					{
						obj = Object.Instantiate(effects[nowPlayEffectNumber].effect, effects[nowPlayEffectNumber].position, Quaternion.identity);
					}
					else
					{
						if (RightLeft == 1)
						{
							obj = Object.Instantiate(effects[nowPlayEffectNumber].effect, core.transform.position + (new Vector3(effects[nowPlayEffectNumber].position.x * RightLeft, effects[nowPlayEffectNumber].position.y, effects[nowPlayEffectNumber].position.z)), Quaternion.identity);
						}
						else if (RightLeft == -1)
						{
							obj = Object.Instantiate(effects[nowPlayEffectNumber].effect, core.transform.position + (new Vector3(effects[nowPlayEffectNumber].position.x * RightLeft, effects[nowPlayEffectNumber].position.y, effects[nowPlayEffectNumber].position.z)), Quaternion.Euler(0, 180, 0));
						}
					}

					//子にするか否か
					if (effects[nowPlayEffectNumber].childFlag)
					{
						obj.transform.parent = core.transform;
					}
				}
			}

			if (bullets.Count>1)
			{
				bullets.Sort((a, b) => a.frame - b.frame);
			}
        }
		//なければなし
		else
		{
			moves = null;
			gravity = null;
            effects = null;
			bullets = null;
            gravityFrame = 0;
		}
	}
    #endregion
    #region 移動
    //移動
    private void MovementSkill()
    {
		//空中制動
		if(airBraking != 0)
		{
			transform.Translate(new Vector3(airBraking * RightLeft, 0, 0));
		}
		if ((moves == null) || (moves.Count == 0)) return;//nullチェック
                                                          //次があるかどうか
        if (moves.Count > nowPlayMoveNumber + 1)
        {
            //現在再生中の移動の次の移動フレームを越えれば
            if (moves[nowPlayMoveNumber + 1].startFrame <= core.AnimationPlayerCompornent.NowFrame)
            {
                nowPlayMoveNumber++;
            }
        }
        //ループ時
        else
        {
            if(moves[0].startFrame<=core.AnimationPlayerCompornent.NowFrame && moves[nowPlayMoveNumber].startFrame > core.AnimationPlayerCompornent.NowFrame)
            {
                nowPlayMoveNumber = -1;
            }
        }
        if (nowPlayMoveNumber < 0) return;//-1なら動かない
        int xDirection = 1;
        if (core.Direction == PlayerDirection.Left) xDirection = -1;
        Vector3 move = moves[nowPlayMoveNumber].movement;
        move.x *= xDirection;
        //移動
        transform.Translate(move * 0.1f);
    }
    //重力移動
    private void GravityMovementSkill()
    {
        if ((gravity == null) || (gravity.Count == 0)) return;
        if (gravity.Count > nowPlayGravityNumber + 1)
        {
            //現在再生中の移動の次の移動フレームを越えれば
            if (gravity[nowPlayGravityNumber + 1].startFrame <= core.AnimationPlayerCompornent.NowFrame)
            {
                nowPlayGravityNumber++;
            }
        }
        if (nowPlayGravityNumber < 0) return;
        //移動保存用
        Vector3 move = gravity[nowPlayGravityNumber].movement * gravityFrame;
        int xDirection = 1;
        if (core.Direction == PlayerDirection.Left) xDirection = -1;
        if (Mathf.Abs(move.x) > Mathf.Abs(gravity[nowPlayGravityNumber].limitMove.x))
        {
            move.x = gravity[nowPlayGravityNumber].limitMove.x;
        }
        if (Mathf.Abs(move.y) > Mathf.Abs(gravity[nowPlayGravityNumber].limitMove.y))
        {
            move.y = gravity[nowPlayGravityNumber].limitMove.y;
        }
        move.x *= xDirection;
        transform.Translate(move * 0.1f);
    }
    #endregion

    private void PlayEffects()
    {
        if ((effects == null) || (effects.Count == 0)) return;
		if (effects.Count > nowPlayEffectNumber + 1)
		{
			//現在再生中の移動の次の移動フレームを越えれば
			if (effects[nowPlayEffectNumber + 1].frame <= core.AnimationPlayerCompornent.NowFrame)
			{
				nowPlayEffectNumber++;
				if ((effects[nowPlayEffectNumber].effect == null))
				{
					return;
				}
				GameObject obj = null;
				if (effects[nowPlayEffectNumber].worldPositionFlag)
				{
					obj = Object.Instantiate(effects[nowPlayEffectNumber].effect, effects[nowPlayEffectNumber].position, Quaternion.identity);
				}
				else
				{
					if (RightLeft == 1)
					{
						obj = Object.Instantiate(effects[nowPlayEffectNumber].effect, core.transform.position + (new Vector3(effects[nowPlayEffectNumber].position.x * RightLeft, effects[nowPlayEffectNumber].position.y, effects[nowPlayEffectNumber].position.z)), Quaternion.identity);
					}
					else if(RightLeft == -1)
					{
						obj = Object.Instantiate(effects[nowPlayEffectNumber].effect, core.transform.position + (new Vector3(effects[nowPlayEffectNumber].position.x * RightLeft, effects[nowPlayEffectNumber].position.y, effects[nowPlayEffectNumber].position.z)), Quaternion.Euler(0, 180, 0));
					}
				}
				if (obj != null)
				{
					obj.layer = LayerMask.NameToLayer(CommonConstants.Layers.Effect);
				}

				//子にするか否か
				if (effects[nowPlayEffectNumber].childFlag)
				{
					obj.transform.parent = core.transform;
				}
			}
		}
		//ループ
		else
		{
			if (effects[0].frame <= core.AnimationPlayerCompornent.NowFrame && effects[nowPlayEffectNumber].frame > core.AnimationPlayerCompornent.NowFrame)
			{
				nowPlayEffectNumber = -1;
			}
		}
	}
	private void PlayBullets()
	{
		if ((bullets == null) || (bullets.Count == 0)) return;
		Debug.Log(bullets[0].frame);
		if (bullets.Count > nowPlayBulletNumber + 1)
		{
			//現在再生中の移動の次の移動フレームを越えれば
			if (bullets[nowPlayBulletNumber + 1].frame <= core.AnimationPlayerCompornent.NowFrame)
			{
				nowPlayBulletNumber++;
				if ((bullets[nowPlayBulletNumber].bullet == null))
				{
					return;
				}
				GameObject obj;
				if (bullets[nowPlayBulletNumber].worldPositionFlag)
				{
					obj = Object.Instantiate(bullets[nowPlayBulletNumber].bullet.gameObject, bullets[nowPlayBulletNumber].position, Quaternion.identity);
				}
				else
				{
					obj = Object.Instantiate(bullets[nowPlayBulletNumber].bullet.gameObject, core.transform.position + (new Vector3(bullets[nowPlayBulletNumber].position.x * RightLeft, bullets[nowPlayBulletNumber].position.y, bullets[nowPlayBulletNumber].position.z)), Quaternion.identity);
				}
				//初期化
				BulletCore bulletCore = obj.GetComponent<BulletCore>();
				bulletCore.RightLeft = RightLeft;
				bulletCore.playerNumber = core.PlayerNumber;
				//子にするか否か
				if (bullets[nowPlayBulletNumber].childFlag)
				{
					obj.transform.parent = core.transform;
				}
			}
		}
	}

}
