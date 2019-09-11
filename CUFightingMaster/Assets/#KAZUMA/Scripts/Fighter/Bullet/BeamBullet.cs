using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamBullet : BulletCore
{
	private FighterCore core;
	public Vector3 move;
	public override void Start()
	{
		base.Start();
		core = GameManager.Instance.GetPlayFighterCore(playerNumber);
	}
	public override void UpdateGame()
	{
		base.UpdateGame();
		transform.Translate(move.x, move.y, move.z);
		// if (hitAttackNum > 0)
		// {
		// 	isDestroyFlag = true;
		// }
		if (allFrame > 120)
		{
			isDestroyFlag = true;
		}
		//if (isWallHit)
		//{
		//	isDestroyFlag = true;
		//}
		//if (isGroundHit)
		//{
		//	isDestroyFlag = true;
		//}
	}
}
