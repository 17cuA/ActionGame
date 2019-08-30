using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBullet : BulletCore
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
        transform.Translate(move.x * RightLeft, move.y, move.z);
        if(hitAttackNum>0)
        {
            isDestroyFlag = true;
        }
        if(nowFrame>180)
        {
            isDestroyFlag = true;
        }
    }
}
